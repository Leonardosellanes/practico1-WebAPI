<template>
  <div v-if="!isLoggedIn" class="container mx-auto mt-10 flex justify-center items-center">
    <form @submit.prevent="submitRegistroEmpleado" class="max-w-md mx-auto bg-white p-8 border rounded-md">
      <h2 class="text-3xl font-semibold mb-8">Registro Empleado</h2>

      <div class="mb-4">
        <label for="nombre" class="block text-sm font-medium text-gray-600">Nombre:</label>
        <input v-model="name" type="text" id="nombre" name="nombre" class="mt-1 p-2 w-full border rounded-md" required />
      </div>

      <div class="mb-4">
        <label for="email" class="block text-sm font-medium text-gray-600">Correo Electrónico:</label>
        <input v-model="email" type="email" id="email" name="email" class="mt-1 p-2 w-full border rounded-md" required />
      </div>

      <!-- Agrega aquí otros campos del formulario para el empleado -->

      <button type="submit" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
        Registrarse
      </button>
    </form>
  </div>
</template>
  
<script>
import axios from 'axios';
import { ref, watch } from 'vue';
import { useStore } from 'vuex';


import { useRouter } from 'vue-router';

export default {
  data() {
    return {
      name: '',
      email: '',
      // ... otros campos del formulario para el empleado
    };
  },
  methods: {
    async submitRegistroEmpleado() {
      try {
        const store = useStore();
        const router = useRouter();
        const empresaId = store.state.empresaId; // Obtén el ID de la empresa del estado de Vuex

        const response = await axios.post('/api/registro/empleado', {
          name: this.name,
          email: this.email,
          empresaId: empresaId, // Asigna el ID de la empresa al empleado
          // ... otros campos del formulario para el empleado
        });

        console.log('Registro de empleado exitoso:', response.data);
        router.push('/registro-exitoso');
      } catch (error) {
        console.error('Error al registrar empleado:', error);
      }
    },
  },
  setup() {
    const store = useStore();
    const isLoggedIn = ref(false);
    const router = useRouter();

  /*onMounted(() => {
  isLoggedIn = store.getters.isAuthenticated;
  if (!store.getters.isAuthenticated) {
  router.push('/');
}
  if (isLoggedIn) {
    router.push('/Home');
  }
});*/
    return {
      isLoggedIn,
    };
  },
};
</script>
