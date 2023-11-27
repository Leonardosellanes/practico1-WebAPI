<template>
    <div class=" w-full h-full space-y-10">
        <a-space direction="vertical" style="width: 100%;">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Bienvenido"
                sub-title="aqui tienes nuestra seleccion de productos" />
                <div class="w-full flex justify-center" v-if="loading">
                    <a-spin :indicator="indicator" @spinning="true" />
                </div>
            <a-list :grid="{ xs: 1, sm: 2, md: 4, lg: 4, xl: 6, xxl: 5 }" :data-source="data" v-else>
                <template #renderItem="{ item }">
                    <a-list-item>
                        <a-card hoverable style="width: 300px" @click="cardClicked(item)">
                            <template #cover>
                                <img alt="example" style="object-fit: cover; width: 100%; height: 170px;"
                                :src="'data:image/png;base64,' + item.base64" />
                            </template>
                            <template #actions>
                                <a-button type="primary" shape="round" >
                                    Ver Más
                                </a-button>
                            </template>
                            <a-card-meta :title="item.titulo" :description="'$' + item.precio">
                                
                            </a-card-meta>
                        </a-card>
                    </a-list-item>
                </template>
            </a-list>
        </a-space>
    </div>
</template>

<script setup>
import { ref, onMounted,h } from 'vue';
import ProductosController from '../../services/ProductosController';
import EmpresasController from '../../services/EmpresasController';
import { useRouter } from 'vue-router';
import { LoadingOutlined } from '@ant-design/icons-vue';

const data = ref([])
const router = useRouter();
const loading = ref(false)
const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});

const cargarProductos = () => {
    loading.value = true
    const dataProductos = EmpresasController.getById(1)
        .then((response) => {
            console.log(response.data.productos)
            data.value = response.data.productos.reverse();
            loading.value = false
        })
        .catch((error) => {
            loading.value = false
            console.error('Error al obtener la lista de categorias:', error);
        });
}

const cardClicked = (item) => {
    router.push({ name: 'Product', params: { id: item.id } });
}

onMounted(() => {
    cargarProductos()
});
</script>
