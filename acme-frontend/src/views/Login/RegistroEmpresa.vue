  <template>
    <div class="container mx-auto mt-10 flex justify-center items-center">
      <div class="text-center">
        <h2 class="text-3xl font-semibold mb-8">Registro de Empresa</h2>
  
        <form @submit.prevent="submitForm" class="max-w-md mx-auto bg-white p-8 border rounded-md shadow-md">
        <!-- Campos de Registro de Empresa -->
        <div class="mb-4">
          <label for="nombreEmpresa" class="block text-sm font-medium text-gray-600">Nombre de la Empresa:</label>
          <input v-model="empresa.nombre" type="text" id="nombreEmpresa" name="nombreEmpresa" class="mt-1 p-2 w-full border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="rutEmpresa" class="block text-sm font-medium text-gray-600">RUT de la Empresa:</label>
          <input v-model="empresa.rut" type="text" id="rutEmpresa" name="rutEmpresa" class="mt-1 p-2 w-full border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <!-- Campos de Registro de Administrador -->
        <div class="mb-4">
          <label for="nombreAdmin" class="block text-sm font-medium text-gray-600">Nombre del Administrador:</label>
          <input v-model="admin.nombre" type="text" id="nombreAdmin" name="nombreAdmin" class="mt-1 p-2 w-full border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="apellidoAdmin" class="block text-sm font-medium text-gray-600">Apellido del Administrador:</label>
          <input v-model="admin.apellido" type="text" id="apellidoAdmin" name="apellidoAdmin" class="mt-1 p-2 w-full border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="emailAdmin" class="block text-sm font-medium text-gray-600">Correo Electrónico del Administrador:</label>
          <input v-model="admin.email" type="email" id="emailAdmin" name="emailAdmin" class="mt-1 p-2 w-full border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="passwordAdmin" class="block text-sm font-medium text-gray-600">Contraseña del Administrador:</label>
          <input v-model="admin.password" type="password" id="passwordAdmin" name="passwordAdmin" class="mt-1 p-2 w-full border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <button type="submit" class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
          Registrar Empresa
        </button>
      </form>
      </div>
    </div>
  </template>

  <script setup>
  import { ref } from 'vue';
  import AuthController from '../../services/AuthController';
  
  const empresa = ref({
    nombre: '',
    rut: '',
  });
  
  const admin = ref({
    nombre: '',
    apellido: '',
    email: '',
    password: '',
  });
  
  const submitForm = async () => {
    try {
      const response = await AuthController.createEmpresa({
        nombreEmpresa: empresa.value.nombre,
        RUTEmpresa: empresa.value.rut,
        NombreAdmin: admin.value.nombre,
        ApellidoAdmin: admin.value.apellido,
        EmailAdmin: admin.value.email,
        PasswordAdmin: admin.value.password,
      });
      console.log('Respuesta del servidor:', response.data);
      //Redirigir?
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Axios Error Details:", error.response);
        }
      return StatusCode(StatusCodes.Status500InternalServerError, "Error al registrar empresa y administrador");
      }

  };
  </script>
  <style scoped>
  </style>
  