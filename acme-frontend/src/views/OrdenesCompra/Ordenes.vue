<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Ordenes de compra"></a-page-header>
        <a-table :columns="columns" :data-source="data.value" class="w-full px-6" v-if="loading == false">
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
</template>

<script setup>
import { ref, onMounted, h, computed } from 'vue';
import EmpresasController from '../../services/EmpresasController';


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
    EmpresasController.getById(1)
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

onMounted(() => {
    cargarOrdenes();
});
</script>