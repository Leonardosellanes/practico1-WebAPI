<template>
    <div class="app-admin-wrap-layout-2 w-full h-screen">
        <Header></Header>
        <main>
            <div class="main-content-wrap flex flex-col flex-grow print-area pt-24">
                <div>
                    <router-view></router-view>
                </div>
            </div>
        </main>
    </div>
</template>

<script setup>
import { RouterView } from 'vue-router';
import Header from './Header.vue';
</script>

<style lang="scss" scoped>
.app-admin-wrap-layout-2 {

    .main-content-wrap {
        width: calc(100% - 120px);
        height: 100%;
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