<template>
    <div class="app-admin-wrap-layout-2 w-full h-screen bg-[#F8F8F8]">
        <Header></Header>
        <main>
            <div class="main-content-wrap flex flex-col flex-grow print-area pt-24">
                <router-view></router-view>
            </div>
        </main>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { RouterView } from 'vue-router';
import Header from './Header.vue';
import ProductosController from '../services/ProductosController';

const data = ref([])

const cargarProductos = () => {
    const dataProductos = ProductosController.getProductos()
        .then((response) => {
            data.value = response.data.reverse();
        })
        .catch((error) => {
            console.error('Error al obtener la lista de categorias:', error);
        });
}

onMounted(() => {
    cargarProductos()
});
</script>

<style lang="scss" scoped>
.app-admin-wrap-layout-2 {
    .main-content-wrap {
        width: calc(100% - 20px);
        height: auto;
        margin-left: 1%;
        transition: all 0.24s ease-in-out;

        .main-content-body {
            min-height: calc(100vh - 80px);
        }

        &.full {
            width: 100%;
            margin-left: 0px;
            transition: all 0.24s ease-in-out;
        }

        @media screen and (max-width: 991px) {
            width: 100%;
            margin-left: 0px;
            padding-right: 16px;
            padding-left: 16px;
        }
    }
}
</style>