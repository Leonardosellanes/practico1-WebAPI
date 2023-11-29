<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Ordenes de compra"></a-page-header>
        <a-table :columns="columns" :data-source="data.value" class="w-full px-6" v-if="loading == false">
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'action'">
                    <span>
                        <a @click="verReclamo(record.rcs)" v-if="record.rcs != null">Ver reclamo</a>
                        <a-divider type="vertical" />
                        <a @click="verProductos(record.carritos)">Ver productos</a>
                        <a-divider type="vertical" />
                        <a @click="CambiarEstado(record)">Cambiar estado</a>
                    </span>
                </template>
            </template>
        </a-table>
        <a-modal v-model:open="openViewReclamo" title="Informacion de su reclamo" :footer="null">
            <a-space direction="vertical" style="width: 100%;">
                <a-card>
                    Fecha: {{ reclamoAsociado.value.fecha }} <br />
                </a-card>
                <a-card>
                    Descripcion: {{ reclamoAsociado.value.descripcion }} <br />
                </a-card>
            </a-space>
        </a-modal>
        <a-modal v-model:open="openViewProducto" title="Productos" :footer="null">
            <a-space direction="vertical" style="width: 100%;">
                <template v-for="product in productosAsociados.value" >
                    <a-card style="width: 100%; margin-right: 16px;">
                        <a-card-meta :title="product.pOs.titulo" :description="'$' + product.pOs.precio.toFixed(2)">
                            <template #avatar>
                                <a-avatar :size="64" :src="'data:image/png;base64,' + product.pOs.base64" />
                            </template>
                            
                        </a-card-meta>
                        <a-card style="margin-top: 8%;">
                            Cantidad: {{ product.cantidad }}
                        </a-card>
                    </a-card>
                </template>
            </a-space>
        </a-modal>
        <a-modal v-model:open="openEstado" title="Cambiar Estado" @ok="handleEstado" @cancel="handleCancelEstado">
            <a-space direction="vertical" style="width: 100%;">
                <a-checkbox v-model:checked="checked" :disabled="checked">Pedido preparado</a-checkbox>
                <a-checkbox v-model:checked="checked2" :disabled="checked2">Pedido enviado</a-checkbox>
                <a-checkbox v-model:checked="checked3" :disabled="checked3">Listo para retirar</a-checkbox>
                <a-checkbox v-model:checked="checked4" :disabled="checked4">Entregado</a-checkbox>
            </a-space>
        </a-modal>
    </div>
</template>

<script setup>
import { ref, onMounted, h, computed } from 'vue';
import OrdenDeCompraController from '../../services/OrdenDeCompraController';
import { useStore } from 'vuex';
import ServerError from 'ant-design-vue/es/result/serverError';

const empresaId = ref(sessionStorage.getItem('empresaId'));

const loading = ref(false);
const openViewReclamo = ref(false);
const openViewProducto = ref(false);
const openEstado = ref(false);
const checked = ref(false);
const checked2 =ref(false)
const checked3 =ref(false)
const checked4 =ref(false)
const retirar =ref(false)

const columns = [
    {
        title: 'Fecha',
        dataIndex: 'fecha',
        width: '10%'
    },
    {
        title: 'Medio de pago',
        dataIndex: 'medioDePago',
        width: '10%'
    },
    {
        title: 'Direccion',
        dataIndex: 'direccionDeEnvio',
        width: '10%'
    },
    {
        title: 'Fecha estimada de entrega',
        dataIndex: 'fechaEstimadaEntrega',
        width: '10%'
    },
    {
        title: 'Total',
        dataIndex: 'total',
        width: '5%'
    },
    {
        title: 'Estado',
        dataIndex: 'estadoOrden',
        width: '10%'
    },
    {
        title: 'Action',
        key: 'action',
        width: '15%'
    },
];

const data = [];
const reclamoAsociado = [];
const productosAsociados = [];
const selectedOrder = ref(null)

const cargarOrdenes = () => {
    loading.value = true
    OrdenDeCompraController.getOrdenByEmpresaId(empresaId.value)
        .then((response) => {
            console.log(response.data)
            data.value = response.data
            loading.value = false;
        })
        .catch((error) => {
            loading.value = false;
            console.error('Error al obtener la lista de prductos:', error);
        });
}

const verReclamo = (data) => {
    reclamoAsociado.value = data
    console.log(reclamoAsociado.value)
    openViewReclamo.value = true
};

const verProductos = (data) => {
    productosAsociados.value = data
    console.log(productosAsociados.value)
    openViewProducto.value = true
};

const CambiarEstado = (data) => {
    if (data.estadoOrden == 'Enviado') {
        checked.value = true;
        checked2.value = true;
    }
     if (data.estadoOrden == 'Retirar') {
        checked.value = true;
        checked2.value = true;
        checked3.value = true;
    } 
    if (data.estadoOrden == 'Entregado'){
        checked.value = true;
        checked2.value = true;
        checked3.value = true;
        checked4.value = true;
    }
    selectedOrder.value = data
    openEstado.value = true
    console.log(data)
};

const handleEstado = () => {
    console.log(selectedOrder.value)
    const estadoOrden = ref('')

    if (checked4.value == true) {
        estadoOrden.value = 'Entregado'
    } else if (checked3.value == true) {
        estadoOrden.value = 'Retirar'
    } else if (checked2.value == true) {
        estadoOrden.value = 'Enviado'
    } else {
        checked.value = false;
        checked2.value = false;
        checked3.value = false;
        checked4.value = false;
        estadoOrden.value = 'Pendiente'
        return
    }

    const data = {
        id:selectedOrder.value.id,
        medioDePago:selectedOrder.value.medioDePago,
        direccionDeEnvio: selectedOrder.value.direccionDeEnvio,
        fechaEstimadaEntrega: selectedOrder.value.fechaEstimadaEntrega,
        total: selectedOrder.value.total,
        estadoOrden: estadoOrden.value,
        fecha: selectedOrder.value.fecha,
        ClienteId: selectedOrder.value.clienteId
    }
    console.log(data)
    OrdenDeCompraController.editarOrden(data)
    .then(() => {
            console.log('Exito')
            cargarOrdenes();
            openEstado.value = false;
        })
        .catch((error) => {
            console.error('Error :', error);
        });
};

const handleCancelEstado = () => {
        checked.value = false;
        checked2.value = false;
        checked3.value = false;
        checked4.value = false;

};
onMounted(() => {
    cargarOrdenes();
});
</script>