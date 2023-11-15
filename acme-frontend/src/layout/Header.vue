<template>
  <div class="border-type h-16 w-full flex items-center bg-white justify-between fixed z-10 ">
    <div class=" w-1/6 h-full">
      <router-link to="/" class="flex justify-center w-10 h-14 ml-20" @click.native="toHome">
        <img src="/images/Logo.png" alt="Logo" />
      </router-link>
    </div>
    <div class=" w-4/6 h-full flex justify-center items-center">
      <a-menu v-model:selectedKeys="current" mode="horizontal" :items="items" />
    </div>
    <div class="w-1/6 h-full flex justify-end items-center mr-8">
      
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
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const current = ref(['']);
const items = ref([
  {
    key: 'Categorias',
    label: 'Categorias',
    onClick: () => {
      router.push('/Categorias');
    },
  },
]);

const toHome = () => {
    current.value[0] = '';  // o current.value = [];
};

</script>

<style scoped>
.border-type {
  border-bottom: 1px solid lightgray;
}
</style>
