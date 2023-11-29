<template>
  <div class="container mx-auto mt-10 flex justify-center items-center">
    <div v-if="empresas.length > 0">
      <form @submit.prevent="submitRegistroCliente" class="max-w-md mx-auto bg-white p-8 border rounded-md">
      <h2 class="text-3xl font-semibold mb-8">Registro Cliente</h2>

      <div class="mb-4">
        <label for="nombre" class="block text-sm font-medium text-gray-600">Nombre:</label>
        <input v-model="name" type="text" id="nombre" name="nombre" class="mt-1 p-2 w-full border rounded-md" required />
      </div>

      <div class="mb-4">
        <label for="lname" class="block text-sm font-medium text-gray-600">Apellido:</label>
        <input v-model="lname" type="text" id="lname" name="lname" class="mt-1 p-2 w-full border rounded-md" required />
      </div>

      <div class="mb-4">
        <label for="email" class="block text-sm font-medium text-gray-600">Correo Electrónico:</label>
        <input v-model="email" type="email" id="email" name="email" class="mt-1 p-2 w-full border rounded-md" required />
      </div>

      <div class="mb-4">
        <label for="password" class="block text-sm font-medium text-gray-600">Contraseña:</label>
        <input v-model="password" type="password" id="password" name="password" class="mt-1 p-2 w-full border rounded-md" required />
      </div>

      <div class="mb-4">
        <label for="shipAddress" class="block text-sm font-medium text-gray-600">Dirección de Envío:</label>
        <input v-model="shipAddress" type="text" id="shipAddress" name="shipAddress" class="mt-1 p-2 w-full border rounded-md" required />
      </div>

      <button type="submit" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
        Registrarse
      </button>
    </form>
    </div>
    <div v-else>
      <p>No hay empresas disponibles. Contacta al administrador.</p>
    <button class="bg-red-700 hover:bg-red-700 text-white font-bold py-2 px-4 rounded" @click="redirectToHome">Ir a la página de inicio</button>
    </div>

  </div>
</template>

<script>
import AuthController from '../../services/AuthController';
import EmpresasController from '../../services/EmpresasController';

export default {
  data() {
    return {
      name: '',
      lname: '',
      email: '',
      password: '',
      shipAddress: '',
      empresas: [],
    };
  },
  mounted() {
    // Obtener la lista de empresas al cargar el componente
    this.loadEmpresas();
  },
  methods: {
    loadEmpresas() {
      // Obtener la lista de empresas
      EmpresasController.getAll()
        .then((response) => {
          // Almacenar las empresas en el estado del componente
          this.empresas = response.data;
        })
        .catch((error) => {
          console.error('Error al cargar empresas:', error);
        });
    },
    submitRegistroCliente() {
      const data = {
        name: this.name,
        lname: this.lname,
        email: this.email,
        password: this.password,
        shipAddress: this.shipAddress,
        // ... otros campos si es necesario
      };

      AuthController.createCliente(data)
        .then((response) => {
          console.log('Registro de cliente exitoso:', response.data);
          this.$router.push('/Home');
        })
        .catch((error) => {
          console.error('Error al registrar cliente:', error);
        });
    },
  },
  methods: {
  redirectToHome() {
    this.$router.push('/Home');
    window.location.reload();
  },
}
};

</script>

  