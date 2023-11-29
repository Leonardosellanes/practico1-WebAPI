<template>
    <div class=" w-full h-full space-y-10 ">
        <a-space direction="vertical" style="width: 100%; ">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Perfil de usuario">
                <template #extra>
                    <a-modal v-model:open="open" title="Realizar reclamo" :confirm-loading="confirmLoading" @ok="reclamoOk">
                        <a-space direction="vertical" style="width: 100%;">
                            <a-textarea v-model:value="descripcionReclamo" :rows="4" placeholder="Ingrese su reclamo"
                                allow-clear />
                        </a-space>
                    </a-modal>
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
                </template>
            </a-page-header>
            <div class="w-full flex justify-between">
                <a-card title="Tu informacion" :bordered="false" style="width: 500px">
                    <template #extra><a href="#">
                            <EditOutlined />
                            editar
                        </a>
                    </template>
                    <p>Card content</p>
                    <p>Card content</p>
                    <p>Card content</p>
                </a-card>
                <a-table :columns="columns" :data-source="data.value" class="w-full px-6" v-if="loading == false">
                    <template #title>Tus ordenes de compra</template>
                    <template #bodyCell="{ column, record }">
                        <template v-if="column.key === 'action'">
                            <span>
                                <a @click="realizarReclamo(record)" v-if="record.rcs == null">Realizar reclamo</a>
                                <a @click="verReclamo(record.rcs)" v-else>ver Reclamo</a>
                                <a-divider type="vertical" />
                                <a @click="verProductos(record.carritos)">Ver productos</a>
                            </span>
                        </template>
                    </template>
                </a-table>

            </div>
        </a-space>

    </div>
</template>

<script setup>
import { EditOutlined } from '@ant-design/icons-vue';
import { ref, onMounted, h, computed } from 'vue';
import OrdenDeCompraController from '../../services/OrdenDeCompraController';
import ReclamosController from '../../services/ReclamosController'

const idUsuario = ref(sessionStorage.getItem('idUsuario'));
const loading = ref(false);
const confirmLoading = ref(false);
const open = ref(false);
const openViewReclamo = ref(false);
const openViewProducto = ref(false);
const descripcionReclamo = ref('');
const idOrden = ref();
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

const cargarOrdenes = () => {
    loading.value = true
    OrdenDeCompraController.getOrdenByUserId(idUsuario.value)
        .then((response) => {
            data.value = response.data.reverse()
            loading.value = false;
        })
        .catch((error) => {
            loading.value = false;
            console.error('Error al obtener la lista de prductos:', error);
        });
}

const realizarReclamo = (data) => {
    idOrden.value = data.id
    open.value = true

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

const reclamoOk = () => {
    confirmLoading.value = true

    const data = {
        id: 0,
        descripcion: descripcionReclamo.value,
        fecha: new Date(),
        empresaId: 1,
        ocId: idOrden.value
    }

    ReclamosController.createReclamo(data)
        .then(() => {
            confirmLoading.value = false;
            open.value = false
            cargarOrdenes()
        })
        .catch((error) => {
            confirmLoading.value = false;
            open.value = false
            console.error('Error al obtener la lista de prductos:', error);
        });
}

onMounted(() => {
    cargarOrdenes();
});
</script>