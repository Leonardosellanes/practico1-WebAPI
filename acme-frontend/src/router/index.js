import { createRouter, createWebHistory } from 'vue-router';
import { createStore } from 'vuex';
import axios from 'axios';
import store from '../views/Login/store.js';

const requireAuth = (to, from, next) => {
    if (!store.getters.isAuthenticated) {
      // Redirigir a la página de inicio de sesión si el usuario no está autenticado
      next('/login');
    } else {
      next();
    }
  };
//beforeEnter: requireAuth, usar para bloquear


const routes = [
    {
        path: '/',
        name: 'Layaout',
        component: () => import('../layout/index.vue'),
        redirect: '/Home',
        meta: {
            title: 'Layaout',
        },
        children: [
            {
                path: '/Home',
                name: 'Home',
                component: () => import('../views/Productos/listadoHome.vue'),
                meta: {
                    title: 'Home',
                },
            },
            {
                path: '/login',
                name: 'Login',
                component: () => import('../views/Login/Login.vue'),
                meta: {
                    title: 'Iniciar Sesión',
                },
            },

            {
                path: '/Categorias',
                name: 'Categorias',
                component: () => import('../views/Categoria/categoria.vue'),
                meta: {
                    title: 'Categorias',
                },
            },

            {
                path: '/Productos',
                name: 'Productos',
                component: () => import('../views/Productos/producto.vue'),
                meta: {
                    title: 'Productos',
                }
            },
            {
                path: '/Productos/Product/:id',
                name: 'Product',
                component: () => import('../views/Productos/vistaProducto.vue'),
                meta: {
                    title: 'Product',
                },

            },
            { 
                path: '/Empresas',
                name: 'Empresas',
                component: () => import('../views/Empresa/ListadoEmpresas.vue'),
                meta: {
                    title: 'Empresas',
                },
            },

            {
                path: '/Sucursales/:id',
                name: 'Sucursales',
                component: () => import('../views/Sucursal/ListadoSucursales.vue'),
                meta: {
                    title: 'Sucursales',
                },
            },
            {
                path: '/registro',
                name: 'Registro',
                component: () => import('../views/Login/Registro.vue'),
                meta: {
                  title: 'Registro',
                },
                children: [
                  {
                    path: 'cliente',
                    name: 'RegistroCliente',
                    component: () => import('../views/Login/RegistroCliente.vue'),
                    meta: {
                      title: 'Registro Cliente',
                    },
                  },
                  {
                    path: 'administrador',
                    name: 'RegistroAdministrador',
                    component: () => import('../views/Login/RegistroAdministrador.vue'),
                    meta: {
                      title: 'Registro Administrador',
                    },
                  },
                ],
              },
            
        ],
    },
];



const router = createRouter({
    history: createWebHistory(),
    routes,
  });
  
  export default router;





