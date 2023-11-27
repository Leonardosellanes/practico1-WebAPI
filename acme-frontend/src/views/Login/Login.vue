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
import axios from 'axios';

const store = useStore();
const formData = ref({
  email: '',
  password: '',
});

const login = async () => {
  try {
    const response = await axios.post('/api/Auth/Login', {
      username: formData.email,
      password: formData.password,
    });

    const token = response.data.token;

    store.commit('setAuthToken', token);

  } catch (error) {
    console.error('Error de inicio de sesión:', error.response.data);
  }
};
</script>

