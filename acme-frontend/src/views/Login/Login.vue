<template>
  <div v-if="!isLoggedIn" class="container mx-auto mt-10 flex justify-center items-center">
    <a-form :model="formData" name="basic" :label-col="{ span: 8 }" :wrapper-col="{ span: 16 }" autocomplete="off"
      @finish="login" class="max-w-md mx-auto bg-white p-8 border rounded-md">
      <h2 class="text-3xl font-semibold mb-8">Iniciar Sesión</h2>
      <a-form-item label="Email" name="email" :rules="[{ required: true, message: 'Ingresa tu correo' }]">
        <a-input v-model:value="formData.email" />
      </a-form-item>

      <a-form-item label="Contraseña" name="password" :rules="[{ required: true, message: 'Ingresa tu contraseña' }]">
        <a-input-password v-model:value="formData.password" />
      </a-form-item>


      <a-form-item :wrapper-col="{ offset: 8, span: 16 }">
        <a-button type="primary" html-type="submit">Iniciar Sesión</a-button>
      </a-form-item>
    </a-form>
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
import { message } from 'ant-design-vue';


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
    .catch(() => {
      message.error('Credenciales incorrectas')
    });
};

</script>

