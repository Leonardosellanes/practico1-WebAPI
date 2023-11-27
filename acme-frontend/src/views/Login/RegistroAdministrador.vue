<template>
  <div>
    <h2>Registro Administrador</h2>
    <form @submit.prevent="submitRegistroAdministrador">
      <!-- Agrega aquí los campos del formulario para el administrador -->
      <label for="nombre">Nombre:</label>
      <input type="text" v-model="name" required />

      <label for="lname">Apellido:</label>
      <input type="text" v-model="lname" required />

      <label for="email">Email:</label>
      <input type="email" v-model="email" required />

      <label for="password">Contraseña:</label>
      <input type="password" v-model="password" required />

      <label for="address">Dirección:</label>
      <input type="text" v-model="address" required />

      <button type="submit">Registrarse</button>
    </form>
  </div>
</template>
  
<script>
import axios from 'axios';
import AuthController from '../../services/AuthController';

export default {
  data() {
    return {
      name: '',
      lname: '',
      email: '',
      password: '',
      address: '',
    };
  },
  methods: {
    submitRegistroAdministrador() {

      const data = {
        name: this.name,
        lname: this.lname,
        email: this.email,
        password: this.password,
        address: this.address,
        // ... otros campos si es necesario
      }

      AuthController.createAdmin(data)
        .then(() => {
          console.log('Registro de administrador exitoso:', response.data);
          this.$router.push('/Home');
        })
        .catch((error) => {
          console.error('Error al registrar administrador:', error);
        });
    },
  },
};
</script>
  