<template>
    <div class=" w-full h-full space-y-10 ">
        <a-space direction="vertical" style="width: 100%; ">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Reclamos" />
            <a-table :columns="columns" :data-source="data" customRender="customRender">
                <template #bodyCell="{ column, record }">
                    <template v-if="column.key === 'action'">
                        <span>
                            <a-button type="link" @click="editProducto(record)">Editar</a-button>
                            <a-divider type="vertical" />
                            <a-space>
                                <a-button type="link" @click="showPromiseConfirm(record)" danger>Eliminar</a-button>
                            </a-space>
                        </span>
                    </template>
                    <template v-if="column.key === 'verOrden'">
                        <span>
                            <a-button type="link" @click="viewOrder(record)">Ver</a-button>
                        </span>
                    </template>
                </template>
            </a-table>
            <a-modal v-model:open="open" title="Datos de la orden " :footer="null" :closable="true">
                <div class="w-full flex justify-center" v-if="loadingModal">
                    <a-spin :indicator="indicator" @spinning="true" />
                </div>
                <a-space direction="vertical" style="width: 100%;" v-else>
                    <a-card>
                        Fecha: {{ ordenAsociada.fecha }} <br />
                        Medio de pago: {{ ordenAsociada.medioDePago }} <br />
                        Direccion de envio: {{ ordenAsociada.direccionDeEnvio }} <br />
                        Fecha estimada de entrega: {{ ordenAsociada.fechaEstimadaEntrega }} <br />
                        Total: {{ ordenAsociada.total }} <br />
                        Estado: {{ ordenAsociada.estadoOrden }} <br />
                    </a-card>
                </a-space>
            </a-modal>
        </a-space>

    </div>
</template>

<script setup>
import EmpresasController from '../../services/EmpresasController';
import OrdenDeCompraController from '../../services/OrdenDeCompraController'
import { ref, h, onMounted } from 'vue';
import { LoadingOutlined } from '@ant-design/icons-vue';
import { Modal, message } from 'ant-design-vue';

const loading = ref(false)
const loadingModal = ref(false)
const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});
const data = ref();
const ordenAsociada = ref();

const columns = [
    {
        title: 'Descripcion',
        dataIndex: 'descripcion',

    },
    {
        title: 'Fecha',
        dataIndex: 'fecha',

    },
    {
        title: 'Orden',
        key: 'verOrden',

    }
];
const open = ref(false)

const empresaId = ref(sessionStorage.getItem('empresaId'))

const cargarReclamos = () => {
    loading.value = true
    EmpresasController.getById(empresaId)
        .then((response) => {
            data.value = response.data.reclamos.reverse();
            loading.value = false;
        })
        .catch((error) => {
            loading.value = false;
            message.error('Error al obtener la lista de productos');
        });
}

const viewOrder = (record) => {
    open.value = true
    loadingModal.value = true
    OrdenDeCompraController.getOrdenById(record.ocId)
        .then((response) => {
            ordenAsociada.value = response.data
            loadingModal.value = false
        })
        .catch((error) => {
            loading.value = false;
            loadingModal.value = false
            message.error('Error al obtener la lista de productos');
        });
}
onMounted(() => {
    cargarReclamos()
});
</script>