<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header class="demo-page-header" style="border: 1px solid rgb(235, 237, 240)" title="Sucursales"
            :sub-title=empresa.nombre><!--  -->
            <template #extra>
            <a-button type="primary" @click="showModal">Agregar</a-button>
              <a-modal v-model:open="open" title="Agregar Sucursal" :confirm-loading="confirmLoading" @ok="handleOk">
                <a-divider type="horizontal" />
                    <a-space direction="vertical" style="width: 100%;">
                        <a-input v-model:value="nombre" placeholder="Nombre" />
                        <label>Tiempo de entrega (dias)</label>
                        <a-input v-model:value="tiempoEntrega" type="numeric" />
                        <mapaIngreso :latitud="latitud" :longitud="longitud" :nombre="nombre"
                            @actualizar-coordenadas="actualizarCoordenadas" >
                        </mapaIngreso>
                        <a-space direction="horizontal" style="width: 100%; gap: 30px;">
                            <a-input v-model:value="latitud" placeholder="Latitud" />
                            <a-input v-model:value="longitud" placeholder="Longitud" />
                        </a-space>
                      
                    </a-space>
              </a-modal>
              <a-modal v-model:open="openEditar" title="Editar Sucursal" :confirm-loading="confirmLoading"
                  @ok="handleEditOk">
                  <a-divider type="horizontal" />
                    <a-space direction="vertical" style="width: 100%;">
                        <a-input v-model:value="nombre" placeholder="Nombre" />
                        <label>Tiempo de entrega (dias)</label>
                        <a-input v-model:value="tiempoEntrega" type="numeric" />
                        <mapaIngreso :latitud="latitud" :longitud="longitud" :nombre="nombre"
                            @actualizar-coordenadas="actualizarCoordenadas" >
                        </mapaIngreso>
                        <a-space direction="horizontal" style="width: 100%; gap: 30px;">
                            <a-input v-model:value="latitud" placeholder="Latitud" />
                            <a-input v-model:value="longitud" placeholder="Longitud" />
                        </a-space>
                  </a-space>
              </a-modal>
          </template>
      </a-page-header>
      <div>
          <div class="w-full flex justify-center" v-if="sucursales.length == 0 && loading">
              <a-spin :indicator="indicator" @spinning="true" />
          </div>
          <div v-else>
              <a-table :columns="columns" :data-source="sucursales">
                  

                  <template #bodyCell="{ column, record }">
                      <template v-if="column.key === 'action'">
                          <span>
                              <a-button type="link" @click="edit(record)">Editar</a-button>
                              <a-divider type="vertical" />
                              <a-space>
                                  <a-button type="link" @click="showPromiseConfirm(record)" danger>Eliminar</a-button>
                              </a-space>
                          </span>
                      </template>
                      <template v-if="column.key === 'tiempo'">
                        <span>{{ record.tiempo }} Días</span>
                      </template>
                  </template>
              </a-table>
          </div>
      </div>
      <!--mapaSelect></mapaSelect-->
  </div>
</template>
  
<script>
    import EmpresasController from '../../services/EmpresasController';
    import SucursalesController from '../../services/SucursalesController';
    import { h } from 'vue';
    import { LoadingOutlined, ExclamationCircleOutlined } from '@ant-design/icons-vue';
    import { createVNode } from 'vue';
    import { Modal, message } from 'ant-design-vue';
    import { useRouter, useRoute } from 'vue-router';
    import mapaIngreso from './mapaIngreso.vue';
    //import mapaSelect from './mapaIngreso.vue';

    export default {
    data() {
      return {
        empresa: { nombre: ''},
        idEmpresa: 0,
        sucursales: [],
        open: false,
        openEditar: false,
        confirmLoading: false,
        nombre: '',
        latitud: '',
        longitud: '',
        ubicacion: '',
        tiempoEntrega: 0,
        id: 0,
        loading: true,
        router: useRouter(),        
        route: useRoute(),

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
                width: '25%',
            },
            {
                title: 'Ubicacion',
                dataIndex: 'ubicacion',
                width: '25%',
            },
            {
                title: 'Tiempo de Entrega',
                dataIndex: 'tiempo',
                key: 'tiempo',
                width: '25%'
            },
            {
                title: 'Acciones',
                key: 'action',
            },
        ]
      };
    },
    mounted() {
        const { id } = this.route.params;
        this.idEmpresa = id;
        this.buscarEmpresa();
    },
    components: {
        mapaIngreso, //mapaSelect
    },
    methods: {
        buscarEmpresa() {
            this.loading = true;
            EmpresasController.getById(this.idEmpresa).then((response) => {
                this.empresa = response.data;
                this.sucursales = this.empresa.sucursales
                  .map((e) => ({
                      key: e.id,
                      name: e.nombre,
                      ubicacion: e.ubicacion,
                      tiempo: e.tiempoEntrega
                  }))
                  .sort((a, b) => b.key - a.key);
                  this.loading = false;
            })
            .catch ((error) => {
              console.error('Error al obtener empresa:', error);
            });
        },

        showModal(){
            this.nombre = '';
            this.ubicacion = '';
            this.latitud = '';
            this.longitud = '';
            this.tiempoEntrega = 0;
            this.open = true;
        },

        handleOk(){
            this.confirmLoading = true;
            const body = {
                nombre: this.nombre,
                ubicacion: 'lat: ' + this.latitud + ', lng: ' + this.longitud ,
                tiempoEntrega: this.tiempoEntrega,
                empresaId: this.empresa.id
            };
            console.log(body);
            SucursalesController.create(body)
                .then(() => {
                    this.open = false;
                    this.confirmLoading = false;
                    this.buscarEmpresa(this.empresa.id);
                })
                .catch((error) => {
                    console.error('Error al crear sucursal:', error);
                });
        },

        handleEditOk(){
            this.confirmLoading = true;
            const body = {
                id: this.id,
                nombre: this.nombre,
                ubicacion: 'lat: ' + this.latitud + ', lng: ' + this.longitud ,
                tiempoEntrega: this.tiempoEntrega,
                empresaId: this.empresa.id
            };
            console.log(body);
            SucursalesController.update(body)
                .then(() => {
                    this.openEditar = false;
                    this.confirmLoading = false;
                    this.buscarEmpresa();
                })
                .catch((error) => {
                    console.error('Error al editar sucursal:', error);
                });
        },

        edit(sucursal){
            if(sucursal.ubicacion){
                this.ubicacion = sucursal.ubicacion;
                const inicioLatitud = this.ubicacion.indexOf("lat: ") + 5;
                const finLatitud = this.ubicacion.indexOf(", lng:");
                this.latitud = parseFloat(this.ubicacion.substring(inicioLatitud, finLatitud));

                const inicioLongitud = this.ubicacion.indexOf("lng: ") + 5;
                this.longitud = parseFloat(this.ubicacion.substring(inicioLongitud));

                console.log("Latitud:", this.latitud);
                console.log("Longitud:", this.longitud);
            }

            this.id = sucursal.key;
            this.nombre = sucursal.name;
            this.tiempoEntrega =  sucursal.tiempo;
            this.openEditar = true;
        },

        showPromiseConfirm(sucursal) {
            try {
                Modal.confirm({
                    title: '¿Deseas eliminar esta sucursal?',
                    icon: createVNode(ExclamationCircleOutlined),
                    // content: 'No se eliminará si tiene datos asociados',
                    onOk: async () => {
                        try {
                            await SucursalesController.delete(sucursal.key);
                            await this.buscarEmpresa(); 
                            message.success('La sucursal ha sido eliminada correctamente.');
                        } catch (error) {
                            console.error('Error al eliminar la sucursal:', error);
                            message.error('Hubo un error al intentar eliminar la sucursal.');
                        }
                    },
                    onCancel() {},
                  });
            } catch (error) {
                console.error('Error al mostrar el modal de confirmación:', error);
            }
        },

        actualizarCoordenadas(coordenadas) {
            this.latitud = coordenadas.latitud;
            this.longitud = coordenadas.longitud;
        },
    },

  };
  </script>
 
  