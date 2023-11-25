<template>
  <div class="border-type h-16 w-full flex items-center bg-white justify-between fixed z-10 ">
    <div class=" w-1/6 h-full">
      <router-link to="/Home" class="flex justify-center w-10 h-14 ml-20" @click.native="toHome">
        <img src="/images/Logo.png" alt="Logo" />
      </router-link>
    </div>
    <div class=" w-4/6 h-full flex justify-center items-center">
      <a-menu v-model:selectedKeys="current" mode="horizontal" :items="items" />
    </div>
    <div class="w-1/6 h-full flex justify-end items-center mr-8">
      <a-space>
      <ShoppingCartOutlined :style="{fontSize: '24px', color: 'gray'}"/>
      <a-dropdown :trigger="['click']">
        <a-avatar style="color: #f56a00; background-color: #fde3cf">U</a-avatar>
        <template #overlay>
          <a-menu>
            <a-menu-item key="0">
              <a href="http://www.alipay.com/">Perfil</a>
            </a-menu-item>
            <a-menu-divider />
            <a-menu-item key="3">Cerrar Sesion</a-menu-item>
          </a-menu>
        </template>
      </a-dropdown>
    </a-space>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import {ShoppingCartOutlined } from '@ant-design/icons-vue'

const router = useRouter();
const current = ref(['Home']);
const items = ref([
  {
    key: 'Home',
    label: 'Home',
    onClick: () => {
      router.push('/Home');
    },
  },
  {
    key: 'Productos',
    label: 'Productos',
    onClick: () => {
      router.push('/Productos');
    },
  },
  {
    key: 'Categorias',
    label: 'Categorias',
    onClick: () => {
      router.push('/Categorias');
    },
  }
  
]);

const toHome = () => {
    current.value[0] = 'Home';  // o current.value = [];
};

</script>

<style scoped>
.border-type {
  border-bottom: 1px solid lightgray;
}
</style>
