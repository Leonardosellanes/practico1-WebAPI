<template>
  <div>
    <form @submit.prevent="login">
      <label>Email:</label>
      <input v-model="formData.email" type="text" />

      <label>Contraseña:</label>
      <input v-model="formData.password" type="password" />

      <button type="submit">Iniciar Sesión</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useStore } from 'vuex';
import AuthController from '../../services/AuthController';

const store = useStore();
const formData = ref({
  email: '',
  password: '',
});

const login = () => {

  const data = {
    email: formData.value.email,
    password: formData.value.password,
  }

  console.log(formData)
  AuthController.Login(data)
    .then((response) => {
      const token = response.data.token;
      store.commit('setAuthToken', token);
    })
    .catch((error) => {
      console.error('Error de inicio de sesión:', error.response.data);
    });





};
</script>

