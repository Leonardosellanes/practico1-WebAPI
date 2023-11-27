<template>
  <div>
    <h1>Página de Registro</h1>
    <router-link to="/registro/cliente">
      <button>Registrarse como Cliente</button>
    </router-link>
    <router-link to="/registro/administrador">
      <button>Registrarse como Administrador</button>
    </router-link>

    <!-- Contenido específico del registro según la selección del usuario -->
    <router-view />
  </div>
</template>
    