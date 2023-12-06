<template>
  <div v-if="!isLoggedIn" class="container mx-auto mt-10 flex justify-center items-center">
    <div v-if="empresas.length > 0">
      <form @submit.prevent="submitRegistroCliente"
        class="max-w-md mx-auto bg-white p-8 border rounded-md flex flex-col space-y-2">
        <h2 class="text-3xl font-semibold mb-8">Registro Cliente</h2>


        <label for="nombre" class="block text-sm font-medium text-gray-600">Nombre:</label>
        <input v-model="name" type="text" id="nombre" name="nombre" class="mt-1 p-2 w-30 border rounded-md" required />



        <label for="lname" class="block text-sm font-medium text-gray-600">Apellido:</label>
        <input v-model="lname" type="text" id="lname" name="lname" class="mt-1 p-2 w-30 border rounded-md" required />



        <label for="email" class="block text-sm font-medium text-gray-600">Correo Electrónico:</label>
        <input v-model="email" type="email" id="email" name="email" class="mt-1 p-2 w-30 border rounded-md" required />



        <label for="password" class="block text-sm font-medium text-gray-600">Contraseña:</label>
        <input v-model="password" type="password" id="password" name="password" class="mt-1 p-2 w-30 border rounded-md"
          required />



        <label for="shipAddress" class="block text-sm font-medium text-gray-600">Dirección de Envío:</label>
        <input v-model="shipAddress" type="text" id="shipAddress" name="shipAddress"
          class="mt-1 p-2 w-30 border rounded-md" required />



        <label for="empresa" class="block text-sm font-medium text-gray-600">Empresa:</label>
        <a-select ref="select" v-model:value="empresasSelected" style="width: 100%" :options="options"
          @change="handleChange">
        </a-select>


        <a-button type="primary" html-type="submit" class="w-full">
          Registrarse
        </a-button>
      </form>
    </div>
    <div v-else>
      <p>No hay empresas disponibles. Contacta al administrador.</p>
      <button class="bg-red-700 hover:bg-red-700 text-white font-bold py-2 px-4 rounded" @click="redirectToHome">Ir a la
        página de inicio</button>
    </div>

  </div>
</template>

<script setup>
import AuthController from '../../services/AuthController';
import EmpresasController from '../../services/EmpresasController';
import { ref, watch, onMounted } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const isLoggedIn = ref(false); // Asegúrate de obtener este valor de tu estado de autenticación
const name = ref('');
const lname = ref('')
const email = ref('')
const password = ref('')
const shipAddress = ref('')
const empresasSelected = ref('');
const empresas = ref([]);
const options = ref([]);

// Obtener la lista de empresas al cargar el componente
onMounted(() => {
  loadEmpresas();
});

const loadEmpresas = () => {
  // Obtener la lista de empresas
  EmpresasController.get()
    .then((response) => {
      // Almacenar las empresas en el estado del componente
      empresas.value = response.data;

      options.value = response.data.map((empresa) => ({
        value: empresa.id,
        label: empresa.nombre,
      }));
    })
    .catch((error) => {
      console.error('Error al cargar empresas:', error);
    });
};

const submitRegistroCliente = () => {
  const data = {
    name: name.value,
    lName: lname.value,
    shipAddress: shipAddress.value,
    password: password.value,
    email: email.value,
    empresaId: empresasSelected.value
  };

  AuthController.createCliente(data)
    .then((response) => {
      console.log('Registro de cliente exitoso:', response.data);
      router.push('/Home');
    })
    .catch((error) => {
      console.error('Error al registrar cliente:', error);
    });
};

const redirectToHome = () => {
  router.push('/Home');
};
</script>
