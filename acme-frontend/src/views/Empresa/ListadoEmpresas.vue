<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header class="demo-page-header" style="border: 1px solid rgb(235, 237, 240)" title="Empresas">
            <template #extra>
                <a-button type="primary" @click="showModal">Agregar</a-button>
                <a-modal v-model:open="open" title="Agregar Empresa" :confirm-loading="confirmLoading" @ok="handleOk">
                    <a-divider type="horizontal" />
                    <a-space direction="vertical" style="width: 100%; margin-bottom: 8px;">
                        <a-input v-model:value="nombre" placeholder="Nombre" />
                        <a-input v-model:value="rut" placeholder="RUT" />
                    </a-space>
                </a-modal>
                <a-modal v-model:open="openEditar" title="Editar Empresa" :confirm-loading="confirmLoading"
                    @ok="handleEditOk">
                    <a-divider type="horizontal" />
                    <a-space direction="vertical" style="width: 100%; margin-bottom: 8px;">
                        <a-input v-model:value="nombre" placeholder="Nombre" />
                        <a-input v-model:value="rut" placeholder="RUT" />
                    </a-space>
                </a-modal>
            </template>
        </a-page-header>
        <div>
            <div class="w-full flex justify-center" v-if="empresas.length == 0 && loading">
                <a-spin :indicator="indicator" @spinning="true" />
            </div>
            <div v-else>
                <a-table :columns="columns" :data-source="empresas">
                    <template #headerCell="{ column }">
                        <template v-if="column.key === 'name'">
                            <span>
                                Name
                            </span>
                        </template>
                    </template>

                    <template #bodyCell="{ column, record }">
                        <template v-if="column.key === 'action'">
                            <span>
                                <a-button type="link" @click="verSucursales(record)">Ver Sucursales</a-button>
                                <a-divider type="vertical" />
                                <a-button type="link" @click="generarFactura(record)">Generar factura</a-button>
                                <a-divider type="vertical" />
                                <a-button type="link" @click="edit(record)">Editar</a-button>
                                <a-divider type="vertical" />
                                <a-space>
                                    <a-button type="link" @click="showPromiseConfirm(record)" danger>Eliminar</a-button>
                                </a-space>

                            </span>
                        </template>
                    </template>
                </a-table>
            </div>
        </div>
    </div>
    <a-modal v-model:open="verFactura" title="Factura" :footer="false">
            <iframe :src="'data:application/pdf;base64,' + imageFactura" width="470" height="400" frameborder="0"></iframe>
    </a-modal>
</template>
  
<script>
import EmpresasController from '../../services/EmpresasController';
import { h } from 'vue';
import { LoadingOutlined, ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { createVNode } from 'vue';
import { Modal, message } from 'ant-design-vue';
import { useRouter } from 'vue-router';
import ApiFacturas from '../../services/ApiFacturas';


export default {
    data() {
        return {
            empresas: [],
            open: false,
            openEditar: false,
            confirmLoading: false,
            nombre: '',
            rut: '',
            id: 0,
            loading: true,
            router: useRouter(),
            imageFactura: '',
            verFactura: false,

            indicator: h(LoadingOutlined, {
                style: {
                    fontSize: '24px',
                },
                spin: true,
            }),

            columns: [
                {
                    title: 'Nombre',
                    dataIndex: 'name',
                    sorter: true,
                    width: '33%',
                },
                {
                    title: 'RUT',
                    dataIndex: 'rut',
                    width: '33%',
                },
                {
                    title: 'Acciones',
                    key: 'action',
                },
            ]
        };
    },
    mounted() {
        this.obtenerEmpresas();
    },
    methods: {
        obtenerEmpresas() {
            this.loading = true;
            EmpresasController.get().then((response) => {
                this.empresas = response.data
                    .map((e) => ({
                        key: e.id,
                        name: e.nombre,
                        rut: e.rut
                    }))
                    .sort((a, b) => b.key - a.key);
                this.loading = false;
            })
                .catch((error) => {
                    console.error('Error al obtener empresas:', error);
                });
        },

        showModal() {
            this.nombre = '';
            this.rut = '';
            this.open = true;
        },

        handleOk() {
            this.confirmLoading = true;
            EmpresasController.create(this.nombre, this.rut)
                .then(() => {
                    this.open = false;
                    this.confirmLoading = false;
                    this.obtenerEmpresas();
                })
                .catch((error) => {
                    console.error('Error al crear empresa:', error);
                });
        },

        handleEditOk() {
            this.confirmLoading = true;
            EmpresasController.update(this.id, this.nombre, this.rut)
                .then(() => {
                    this.openEditar = false;
                    this.confirmLoading = false;
                    this.obtenerEmpresas();
                })
                .catch((error) => {
                    console.error('Error al editar empresa:', error);
                });
        },

        edit(empresa) {
            this.id = empresa.key;
            this.nombre = empresa.name;
            this.rut = empresa.rut;
            this.openEditar = true;
        },

        generarFactura(empresa) {
            console.log(empresa)
            EmpresasController.generarFactura(empresa.key).then((response) => {
                if (response.status == 200) {
                    message.success('Factura generada');
                    ApiFacturas.getFactura(response.data).then((response) => {
                        if (response.status == 200) {
                            this.imageFactura = response.data;
                            this.verFactura = true;
                        }
                    })
                } else {
                    message.error('Error al generar factura');
                }
            }).catch(() => {
                message.info('No hay oredenes de compra para esa empresa');
            })
        },
        showPromiseConfirm(empresa) {
            try {
                Modal.confirm({
                    title: '¿Deseas eliminar esta empresa?',
                    icon: createVNode(ExclamationCircleOutlined),
                    //content: 'No se eliminará si tiene datos asociados',
                    onOk: async () => {
                        try {
                            await EmpresasController.delete(empresa.key);
                            this.obtenerEmpresas();
                            message.success('La empresa ha sido eliminada correctamente.');
                        } catch (error) {
                            console.error('Error al eliminar la empresa:', error);
                            message.error('Hubo un error al intentar eliminar la empresa.');
                        }
                    },
                    onCancel() { },
                });
            } catch (error) {
                console.error('Error al mostrar el modal de confirmación:', error);
            }
        },

        verSucursales(empresa) {
            this.router.push('/Sucursales/' + empresa.key);
        }
    },

};
</script>
 
  