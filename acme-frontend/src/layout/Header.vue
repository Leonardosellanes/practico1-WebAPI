<template>
  <div class="border-type h-16 w-full flex items-center bg-white justify-between fixed z-10">
    <div class="w-1/6 h-full">
      <router-link to="/Home" class="flex justify-center w-10 h-14 ml-20" @click.native="toHome">
        <img src="/images/Logo.png" alt="Logo" />
      </router-link>
    </div>
    <div class="w-4/6 h-full flex justify-center items-center">
      <a-menu v-model:selectedKeys="current" mode="horizontal" :items="items" />
    </div>
    <div class="w-1/6 h-full flex justify-end items-center mr-8">
      <a-space>

        <router-link v-if="!isLoggedIn" to="/Login">
          <button class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
            Login
          </button>
        </router-link>

        <router-link v-if="!isLoggedIn" to="/registro">
          <button class="bg-purple-500 hover:bg-purple-700 text-white font-bold py-2 px-4 rounded">
            Registro
          </button>
        </router-link>
        <router-link to="/Carrito">
          <ShoppingCartOutlined :style="{ fontSize: '24px', color: 'gray' }" />
        </router-link>
        <a-dropdown :trigger="['click']">
          <a-avatar style="color: #f56a00; background-color: #fde3cf">U</a-avatar>
          <template #overlay>
            <a-menu>
              <a-menu-item key="0">
                <a @click="toProfile">Perfil</a>
              </a-menu-item>
              <a-menu-divider />
              <a-menu-item key="3" @click="handleAuthentication">
                {{ isLoggedIn ? 'Cerrar Sesión' : 'Iniciar Sesión' }}
              </a-menu-item>
            </a-menu>
          </template>
        </a-dropdown>
      </a-space>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { ShoppingCartOutlined } from '@ant-design/icons-vue';
import { useStore } from 'vuex';

const router = useRouter();
const store = useStore();
const isLoggedIn = ref(false);
const current = ref(['Home']);
const items = ref([
  {
    key: 'Home',
    label: 'Home',
    onClick: () => {
      router.push('/Home');
    },
  },
  {
    key: 'Productos',
    label: 'Productos',
    onClick: () => {
      router.push('/Productos');
    },
  },
  {
    key: 'Categorias',
    label: 'Categorias',
    onClick: () => {
      router.push('/Categorias');
    },
  },
  {
    key: 'Ordenes',
    label: 'Ordenes',
    onClick: () => {
      router.push('/Ordenes');
    },
  },
  {
    key: 'Reclamos',
    label: 'Reclamos',
    onClick: () => {
      router.push('/Reclamos');
    },
  },
  {
    key: 'Empresas',
    label: 'Empresas',
    onClick: () => {
      router.push('/Empresas');
    },
  },
  {
    key: 'Sucursales',
    label: 'Sucursales',
    onClick: () => {
      router.push(`/Sucursales/${empresaId}`);
    },
  },
  {
    key: 'Empleados',
    label: 'Empleados',
    onClick: () => {
      router.push(`/Empleados`);
    },
  }

]);
const empresaId = (1)

const handleAuthentication = () => {
  if (isLoggedIn) {
    // Realizar lógica de cierre de sesión
    store.commit('clearToken');
    router.push('/');
  } else {
    // Redirigir a la página de inicio de sesión
    router.push('/login');
  }
};

const goToLogin = () => {
  router.push('/login');
};


const toHome = () => {
  current.value[0] = 'Home';
};
const toProfile = () => {
  current.value[0] = '';
  router.push('/Perfil')
};

</script>

<style scoped>
.border-type {
  border-bottom: 1px solid lightgray;
}
</style>



