<template>
  <div class="border-type h-16 w-full flex items-center bg-white justify-between fixed z-10">
    <div class="w-1/6 h-full">
      <router-link to="/Home" class="flex justify-center w-10 h-14 ml-20" @click.native="toHome">
        <img src="/images/Logo.png" alt="Logo" />
      </router-link>
    </div>
    <div class="w-4/6 h-full flex justify-center items-center">
      <a-menu v-model:selectedKeys="current" mode="horizontal">
        <a-menu-item v-for="item in items" :key="item.key" :onClick="item.onClick" v-if="canViewItem(items)">
          {{ item.label }}
        </a-menu-item>
      </a-menu>
    </div>
    <div class="w-1/6 h-full flex justify-end items-center mr-8">
      <a-space>
        <router-link v-if="!isLoggedIn" to="/login">
          <button class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
            Login
          </button>
        </router-link>
        <router-link v-if="!isLoggedIn" to="/registro">
          <button class="bg-purple-500 hover:bg-purple-700 text-white font-bold py-2 px-4 rounded">
            Registro
          </button>
        </router-link>



        <router-link v-if="isLoggedIn" to="/Carrito">
          <ShoppingCartOutlined :style="{ fontSize: '24px', color: 'gray' }" />
        </router-link>
        <a-dropdown v-if="isLoggedIn" :trigger="['click']">
          <a-avatar style="color: #f56a00; background-color: #fde3cf">U</a-avatar>
          <template #overlay>
            <a-menu>
              <a-menu-item key="0">
                <a @click="toProfile">Perfil</a>
              </a-menu-item>
              <a-menu-divider />
              <a-menu-item key="3" @click="handleAuthentication" :style="{ color: !isLoggedIn.value ? 'red' : 'black' }">
                {{ !isLoggedIn.value? 'Cerrar Sesión' : 'Iniciar Sesión' }}
              </a-menu-item>
            </a-menu>
          </template>
        </a-dropdown>
      </a-space>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { ShoppingCartOutlined } from '@ant-design/icons-vue';

const router = useRouter();
const store = useStore();
const isLoggedIn = ref(store.getters.isAuthenticated);
const current = ref(['Home']);
const items = ref([
  {
    key: 'Home',
    label: 'Home',
    onClick: () => {
      router.push('/Home');
    },
    showWhenLoggedIn: false,
    rol: [],
  },
  {
    key: 'Productos',
    label: 'Productos',
    onClick: () => {
      router.push('/Productos');
    },
    showWhenLoggedIn: true,
    rol: ['ADMIN','USER']
  },
  {
    key: 'Categorias',
    label: 'Categorias',
    onClick: () => {
      router.push('/Categorias');
    },
    showWhenLoggedIn: true,
    rol: ['ADMIN','USER']
  },
  {
    key: 'Ordenes',
    label: 'Ordenes',
    onClick: () => {
      router.push('/Ordenes');
    },
    showWhenLoggedIn: true,
    rol: ['ADMIN','USER']
  },
  {
    key: 'Reclamos',
    label: 'Reclamos',
    onClick: () => {
      router.push('/Reclamos');
    },
    showWhenLoggedIn: true,
    rol: ['ADMIN','USER']
  },
  {
    key: 'Empresas',
    label: 'Empresas',
    onClick: () => {
      router.push('/Empresas');
    },
    showWhenLoggedIn: true,
    rol: ['ADMIN','USER']
  },
  {
    key: 'Sucursales',
    label: 'Sucursales',
    onClick: () => {
      router.push(`/Sucursales/${empresaId.value}`);
    },
    showWhenLoggedIn: true,
    rol: ['USER']
  },
  {
    key: 'Empleados',
    label: 'Empleados',
    onClick: () => {
      router.push(`/Empleados`);
    },
    showWhenLoggedIn: true,
    rol: ['USER']
  }

]);


const canViewItem = (item) => {
  console.log('Item:', item);
  console.log('isLoggedIn:', isLoggedIn.value);

  // Si el elemento requiere inicio de sesión y el usuario no está logeado, no se muestra
  if (item.showWhenLoggedIn && !isLoggedIn.value) {
    console.log('No se muestra porque el usuario no está conectado.');
    return false;
  }

  // Si el elemento tiene roles especificados
  if (item.rol && item.rol.length > 0) {
    const userRoles = store.getters.rol; // Cambiado de userRoles a userRole
    console.log('Roles permitidos:', item.rol);
    console.log('Roles del usuario:', userRoles);
    
    // Verifica si el usuario tiene al menos uno de los roles especificados
    const canView = item.rol.some(role => userRoles.includes(role));
    console.log('Se muestra:', canView);
    
    return canView;
  }

  // Si no hay restricciones de roles y el elemento no requiere inicio de sesión, se muestra
  if (!item.showWhenLoggedIn) {
    console.log('Se muestra porque no hay restricciones de roles.');
    return true;
  }

  // En cualquier otro caso, no se muestra
  console.log('No se muestra por alguna razón no identificada.');
  return false;
};

watch(
  () => store.getters.isAuthenticated,
  (newValue) => {
    isLoggedIn.value = newValue;
  }
);

const empresaId = ref(sessionStorage.getItem('empresaId'))

const handleAuthentication = () => {
  if (isLoggedIn.value) {
    store.commit('clearToken');
    isLoggedIn.value = false; 
    router.push('/login');
    window.location.reload();
  } else {
    router.push('/login');
  }
};

/*const handleLogout = () => {
  store.commit('clearToken');
  isLoggedIn.value = false;
  router.push('/');
};*/

const handleLogout = () => {
  store.dispatch('logoutUser');
  window.location.reload(); 
};

const toHome = () => {
  current.value[0] = 'Home';
};

const toProfile = () => {
  current.value[0] = '';
  router.push('/Perfil');
};

const handleSession = () => {
  isLoggedIn.value = store.getters.isAuthenticated;
};

onMounted(() => {
  handleSession();
});

</script>

<style scoped>
.border-type {
  border-bottom: 1px solid lightgray;
}/*
const userRole = computed(() => store.getters.userRole);

// En tu template, muestra u oculta elementos según el rol
<a-menu-item v-if="userRole === 'ADMIN'" key="Admin">Administrador</a-menu-item>
<a-menu-item v-if="userRole === 'USER'" key="User">Usuario</a-menu-item>
<!-- Agrega más lógica según sea necesario para otros roles --> 
*/
</style>