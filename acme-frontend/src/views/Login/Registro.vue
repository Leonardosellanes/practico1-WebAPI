<template>
  <div v-if="!isLoggedIn" class="container mx-auto mt-10 flex justify-center items-center">
    <div class="text-center">
      <h1 class="text-3xl font-semibold mb-8">Registro Acme</h1>

      <router-link to="/registroCliente" class="mb-4 inline-block">
        <button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
          Registrarse como Cliente
        </button>
      </router-link>

      <router-link to="/registroEmpresa" class="mb-4 inline-block">
        <button class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
          Registrarse como Empresa
        </button>
      </router-link>

      <router-view />
    </div>
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
