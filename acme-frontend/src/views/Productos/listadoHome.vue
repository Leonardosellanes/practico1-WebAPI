<template>
    <div class="w-full h-full space-y-10">
        <a-space direction="vertical" style="width: 100%;">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Bienvenido"
                sub-title="Aquí tienes nuestra selección de productos">
                <template #extra>
                    <a-input-search v-model:value="searchText" placeholder="input search text" enter-button
                        @change="onSearch" />
                </template>
            </a-page-header>
            <div class="w-full flex justify-center" v-if="loading">
                <a-spin :indicator="indicator" @spinning="true" />
            </div>
            <a-list :grid="{ xs: 1, sm: 2, md: 4, lg: 4, xl: 6, xxl: 5 }" :data-source="filtered" v-else>
                <template #renderItem="{ item }">
                    <a-list-item>
                        <a-card hoverable style="width: 300px" @click="cardClicked(item)">
                            <template #cover>
                                <img alt="example" style="object-fit: cover; width: 100%; height: 170px;"
                                    :src="'data:image/png;base64,' + item.base64" />
                            </template>
                            <template #actions>
                                <a-button type="primary" shape="round">
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
import { ref, onMounted, h, computed } from 'vue';
import ProductosController from '../../services/ProductosController';
import EmpresasController from '../../services/EmpresasController';
import { useRouter } from 'vue-router';
import { LoadingOutlined } from '@ant-design/icons-vue';
import { compileString } from 'sass';

const data = ref([]);
const filtered = ref([])
const router = useRouter();
const loading = ref(false);
const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});
const searchText = ref("");


const onSelect = option => {
    console.log('select', option);
};

const cargarProductos = () => {
    loading.value = true;
    EmpresasController.getById(1)
        .then((response) => {
            data.value = response.data.productos.reverse();
            loading.value = false;
            filtered.value = data.value;
        })
        .catch((error) => {
            loading.value = false;
            console.error('Error al obtener la lista de prductos:', error);
        });
};

const cardClicked = (item) => {
    router.push({ name: 'Product', params: { id: item.id } });
};

const filterProductsBySearchText = (products) => {
    console.log(searchText.value.toLocaleLowerCase())

    filtered.value = products.filter((product) =>
        product.titulo.toLowerCase().includes(searchText.value.toLocaleLowerCase())
    );
};

const onSearch = () => {
    if (searchText.value != "") {
        filterProductsBySearchText(data.value)
    } else {
        filtered.value = data.value
    }

};

onMounted(() => {
    cargarProductos();
});
</script>
  