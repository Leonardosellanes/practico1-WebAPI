<template>
  <div class="border-type h-16 w-full flex items-center bg-white justify-between fixed z-10">
    <div class="w-1/6 h-full">
      <router-link to="/Home" class="flex justify-center w-10 h-14 ml-20" @click.native="toHome">
        <img src="/images/Logo.png" alt="Logo" />
      </router-link>
    </div>
    <div class="w-4/6 h-full flex justify-center items-center">
      <a-menu v-model:selectedKeys="current" mode="horizontal" v-if="isLoggedIn == true && rol == 'USER'">
        <a-menu-item v-for="item in itemsUser" :key="item.key" :onClick="item.onClick">
          {{ item.label }}
        </a-menu-item>
      </a-menu>
      <a-menu v-model:selectedKeys="current" mode="horizontal" v-else-if="isLoggedIn == true && rol == 'MANAGER'">
        <a-menu-item v-for="item in itemsManager" :key="item.key" :onClick="item.onClick">
          {{ item.label }}
        </a-menu-item>
      </a-menu>
      <a-menu v-model:selectedKeys="current" mode="horizontal" v-else-if="isLoggedIn == true && rol == 'ADMIN'">
        <a-menu-item v-for="item in itemsAdmin" :key="item.key" :onClick="item.onClick">
          {{ item.label }}
        </a-menu-item>
      </a-menu>
      <a-menu v-model:selectedKeys="current" mode="horizontal" v-else-if="isLoggedIn == true && rol == 'EMPLEADO'">
        <a-menu-item v-for="item in itemsEmpleado" :key="item.key" :onClick="item.onClick">
          {{ item.label }}
        </a-menu-item>
      </a-menu>
    </div>
    <div class="w-1/6 h-full flex justify-end items-center mr-8">
      <a-space>
        <router-link v-if="isLoggedIn == false" to="/login">
          <button class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
            Login
          </button>
        </router-link>
        <router-link v-if="isLoggedIn == false" to="/registro">
          <button class="bg-purple-500 hover:bg-purple-700 text-white font-bold py-2 px-4 rounded">
            Registro
          </button>
        </router-link>
        <router-link v-if="isLoggedIn == true && rol == 'USER'" to="/Carrito">
          <ShoppingCartOutlined :style="{ fontSize: '24px', color: 'gray' }" />
        </router-link>
        <a-dropdown v-if="isLoggedIn == true" :trigger="['click']">
          <a-avatar style="color: #f56a00; background-color: #fde3cf">U</a-avatar>
          <template #overlay>
            <a-menu>
              <a-menu-item key="0">
                <a @click="toProfile">Perfil</a>
              </a-menu-item>
              <a-menu-divider />
              <a-menu-item key="3" @click="handleAuthentication" :style="{ color: 'red' }">
                {{ 'Cerrar Sesión' }}
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
const empresaId = ref(sessionStorage.getItem('empresaId'))
const isLoggedIn = ref(sessionStorage.getItem('idUsuario') != null);
const rol = ref(sessionStorage.getItem('rol'))
const current = ref(['Home']);
const itemsUser = ref([
  {
    key: 'Home',
    label: 'Home',
    onClick: () => {
      router.push('/Home');
    },
  },
]);

const itemsManager = ref([
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
    key: 'Sucursales',
    label: 'Sucursales',
    onClick: () => {
      router.push(`/Sucursales/${empresaId.value}`);
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

const itemsEmpleado = ref([
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
    key: 'Sucursales',
    label: 'Sucursales',
    onClick: () => {
      router.push(`/Sucursales/${empresaId.value}`);
    },
  }
]);

const itemsAdmin = ref([
  {
    key: 'Empresas',
    label: 'Empresas',
    onClick: () => {
      router.push('/Empresas');
    },
  }
]);


// const canViewItem = (item) => {

//   console.log('Item:', item);
//   console.log('isLoggedIn:', isLoggedIn.value);

//   // Si el elemento requiere inicio de sesión y el usuario no está logeado, no se muestra
//   if (item.showWhenLoggedIn && !isLoggedIn.value) {
//     console.log('No se muestra porque el usuario no está conectado.');
//     return false;
//   }

//   // Si el elemento tiene roles especificados
//   if (item.rol && item.rol.length > 0) {
//     const userRoles = sessionStorage.getItem('rol') || ''; // Si 'rol' es null o undefined, se establece como una cadena vacía
//     console.log('Roles permitidos:', item.rol);
//     console.log('Roles del usuario:', userRoles);

//     // Verifica si el usuario tiene al menos uno de los roles especificados
//     const canView = item.rol.some(role => userRoles.includes(role));
//     console.log('Se muestra:', canView);

//     return canView;
//   }

//   // Si no hay restricciones de roles y el elemento no requiere inicio de sesión, se muestra
//   if (!item.showWhenLoggedIn) {
//     console.log('Se muestra porque no hay restricciones de roles.');
//     return true;
//   }

//   // En cualquier otro caso, no se muestra
//   console.log('No se muestra por alguna razón no identificada.');
//   return false;
// };

// watch(
//   () => sessionStorage.getItem('idUsuario'),
//   (newValue) => {
//     isLoggedIn.value = newValue !== null;
//   }
// );



console.log(rol.value)

const handleAuthentication = () => {
  if (isLoggedIn.value) {
    store.commit('clearToken');
    isLoggedIn.value = false;
    router.push('/login')
    .then(() => {
    window.location.reload();
  });
  } else {
    router.push('/login');
  }
};

const toHome = () => {
  current.value[0] = 'Home';
};

const toProfile = () => {
  current.value[0] = '';
  router.push('/Perfil');
};

</script>

<style scoped>
.border-type {
  border-bottom: 1px solid lightgray;
}
</style>