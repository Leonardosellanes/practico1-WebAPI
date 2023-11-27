<template>
    <div>
      <h2>Registro Empleado</h2>
      <form @submit.prevent="submitRegistroEmpleado">
        <!-- Agrega aquí los campos del formulario para el empleado -->
        <label for="nombre">Nombre:</label>
        <input type="text" v-model="nombre" required />
  
        <label for="email">Email:</label>
        <input type="email" v-model="email" required />
  
        <!-- Otros campos del formulario -->
  
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
        address: '',
        email: '',
      };
    },
    methods: {
      async submitRegistroEmpleado() {
        try {
          const response = await axios.post('/api/registro/empleado', {
            name: this.name,
            lname: this.lname,
            address: this.address,
            // ... otros campos
          });
  
          console.log('Registro de empleado exitoso:', response.data);
          this.$router.push('/registro-exitoso');
        } catch (error) {
          console.error('Error al registrar empleado:', error);
        }
      },
    },
  };
  </script>
  