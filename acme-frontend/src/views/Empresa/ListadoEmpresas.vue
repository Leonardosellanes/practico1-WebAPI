<template>
    <div>
      <h1>Empresas</h1>
      <ul>
        <li v-for="empresa in empresas" :key="empresa.rut">
          {{ empresa.nombre }} - {{ empresa.rut }}
        </li>
      </ul>
    </div>
  </template>
  
  <script>
    import EmpresasController from '../../services/EmpresasController';


  export default {
    data() {
      return {
        empresas: [],
      };
    },
    mounted() {
      this.obtenerEmpresas();
    },
    methods: {
      async obtenerEmpresas() {
        try {
          const response = await EmpresasController.get();
          this.empresas = response.data;
        } catch (error) {
          console.error('Error al obtener empresas:', error);
        }
      },
    },

  };
  </script>
 
  