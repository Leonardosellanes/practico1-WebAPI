<template>
    <div>
      <h2>Registro Cliente</h2>
      <form @submit.prevent="submitRegistroCliente">
        <!-- Agrega aquí los campos del formulario para el cliente -->
        <label for="nombre">Nombre:</label>
        <input type="text" v-model="name" required />
  
        <label for="lname">Apellido:</label>
        <input type="text" v-model="lname" required />
  
        <label for="email">Email:</label>
        <input type="email" v-model="email" required />
  
        <label for="password">Contraseña:</label>
        <input type="password" v-model="password" required />
  
        <label for="shipAddress">Dirección de Envío:</label>
        <input type="text" v-model="shipAddress" required />
  
        <button type="submit">Registrarse</button>
      </form>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    data() {
      return {
        name: '',
        lname: '',
        email: '',
        password: '',
        shipAddress: '',
      };
    },
    methods: {
      async submitRegistroCliente() {
        try {
          const response = await axios.post('/api/RegisterCliente', {
            name: this.name,
            lname: this.lname,
            email: this.email,
            password: this.password,
            shipAddress: this.shipAddress,
            // ... otros campos si es necesario
          });
  
          console.log('Registro de cliente exitoso:', response.data);
          this.$router.push('/registro-exitoso');
        } catch (error) {
          console.error('Error al registrar cliente:', error);
        }
      },
    },
  };
  </script>
  
  