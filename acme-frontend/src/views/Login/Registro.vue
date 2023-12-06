<template>
  <div v-if="!isLoggedIn" class="max-w-md mx-auto bg-white p-8 border rounded-md text-center flex flex-col">
   
      <h1 class="text-3xl font-semibold mb-8">Registro Acme</h1>

      <router-link to="/registroCliente" class="mb-4 inline-block w-auto">
        <a-button class="w-full">
          Registrarse como Cliente
        </a-button>
      </router-link>

      <router-link to="/registroEmpresa" class="mb-4 inline-block w-auto">
        <a-button class="w-full">
          Registrarse como Empresa
        </a-button>
      </router-link>

      <router-view />
   
  </div>
</template>

<script setup>
import { ref, onMounted ,computed } from 'vue';
import { useStore } from 'vuex';


import { useRouter } from 'vue-router';

const store = useStore();
const router = useRouter();
const isLoggedIn = computed(() => store.getters.isAuthenticated);

/*onMounted(() => {
  isLoggedIn = store.getters.isAuthenticated;
  if (!store.getters.isAuthenticated) {
  router.push('/Home');
}
  if (!isLoggedIn.value) {
    router.push('/Home');
  }
});*/
</script>
