<template>
  <div v-if="!isLoggedIn" class="container mx-auto mt-10 flex justify-center">
    <div class="">
      <h2 class="text-3xl font-semibold mb-8">Registro de Empresa</h2>

      <form @submit.prevent="submitForm" class="max-w-md mx-auto bg-white p-8 border rounded-md flex flex-col">
        <!-- Campos de Registro de Empresa -->
        <div class="mb-4">
          <label for="nombreEmpresa" class="block text-sm font-medium text-gray-600">Nombre de la Empresa:</label>
          <input v-model="empresa.nombre" type="text" id="nombreEmpresa" name="nombreEmpresa" class="mt-1 p-2 w-56 border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="rutEmpresa" class="block text-sm font-medium text-gray-600">RUT de la Empresa:</label>
          <input v-model="empresa.rut" type="text" id="rutEmpresa" name="rutEmpresa" class="mt-1 p-2 w-56 border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <!-- Campos de Registro de Administrador -->
        <div class="mb-4">
          <label for="nombreAdmin" class="block text-sm font-medium text-gray-600">Nombre del Administrador:</label>
          <input v-model="admin.nombre" type="text" id="nombreAdmin" name="nombreAdmin" class="mt-1 p-2 w-56 border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="apellidoAdmin" class="block text-sm font-medium text-gray-600">Apellido del Administrador:</label>
          <input v-model="admin.apellido" type="text" id="apellidoAdmin" name="apellidoAdmin" class="mt-1 p-2 w-56 border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="emailAdmin" class="block text-sm font-medium text-gray-600">Correo Electrónico del Administrador:</label>
          <input v-model="admin.email" type="email" id="emailAdmin" name="emailAdmin" class="mt-1 p-2 w-56 border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <div class="mb-4">
          <label for="passwordAdmin" class="block text-sm font-medium text-gray-600">Contraseña del Administrador:</label>
          <input v-model="admin.password" type="password" id="passwordAdmin" name="passwordAdmin" class="mt-1 p-2 w-56 border rounded-md focus:outline-none focus:border-green-500" />
        </div>
  
        <a-button type="primary" html-type="submit" class="w-full">
          Registrar Empresa
        </a-button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import AuthController from '../../services/AuthController';
import EmpresasController from '../../services/EmpresasController';
import { useRouter } from 'vue-router';
import { message } from 'ant-design-vue';

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

const router = useRouter();
let isLoggedIn = false;

const submitForm = () => {
  try {
    EmpresasController.create(empresa.value.nombre, empresa.value.rut).then((response) => {
      if(response.status == 200){
        const empresaId = response.data.id;
        const body = {
          empresaId: empresaId,
          name: admin.value.nombre,
          lname: admin.value.apellido,
          email: admin.value.email,
          password: admin.value.password
        }
        AuthController.createManager(body).then((response) => {
          if(response.status == 200){
            router.push('/login');
          }
          else{
            message.error('Ha ocurrido un error al registrar usuario');
          }
        })
      }else{
        message.error('Ha ocurrido un error al registrar empresa');
      }
    })

  } catch (error) {
    message.error('Error al registrar empresa y administrador');
  }
};

</script>

<style scoped>
</style>