<template>
  <div v-if="!isLoggedIn" class="container mx-auto mt-10 flex justify-center items-center">
    <form @submit.prevent="login" class="max-w-md mx-auto bg-white p-8 border rounded-md">
      <h2 class="text-3xl font-semibold mb-8">Iniciar Sesión</h2>

      <div class="mb-4">
        <label for="email" class="block text-sm font-medium text-gray-600">Email:</label>
        <input v-model="formData.email" type="text" id="email" name="email" class="mt-1 p-2 w-full border rounded-md" />
      </div>

      <div class="mb-4">
        <label for="password" class="block text-sm font-medium text-gray-600">Contraseña:</label>
        <input v-model="formData.password" type="password" id="password" name="password" class="mt-1 p-2 w-full border rounded-md" />
      </div>

      <button type="submit" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
        Iniciar Sesión
      </button>

      <div v-if="error" class="text-red-500 mt-4">
        {{ error }}
      </div>
    </form>
  </div>
  <router-link v-else to="/Home">
    Ya has iniciado sesión..
  </router-link>
</template>

<script setup>
import { ref, watch } from 'vue';
import { useStore } from 'vuex';
//import store from '../services/store.js';
import AuthController from '../../services/AuthController';
import { useRouter } from 'vue-router';


const router = useRouter();
const store = useStore();
const formData = ref({
  email: '',
  password: '',
});
const error = ref(null);
const isLoggedIn = ref(store.getters.isAuthenticated);

const login = () => {
  const data = {
    Email: formData.value.email,
    Password: formData.value.password,
  };

  AuthController.Login(data)
    .then((response) => {
      console.log(response.data)
      const token = response.data
      store.commit('setToken', token);
      isLoggedIn.value = sessionStorage.getItem('idUsuario') != null
      if (isLoggedIn) {
        
        router.push('/Productos').then(() => {
    window.location.reload();
  });
        
      }
      formData.value.email = '';
      formData.value.password = '';
      error.value = null;
    })
    .catch((error) => {
      console.error('Error de inicio de sesión:', error);
      error.value = 'Inicio de sesión fallido. Verifica tus credenciales.';
    });
};

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

