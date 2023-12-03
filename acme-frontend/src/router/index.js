import { createRouter, createWebHistory } from 'vue-router';
import { createStore } from 'vuex';
import axios from 'axios';
import store from '../services/store.js';

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
                beforeEnter: (to, from, next) => {
                    if (store.getters.isAuthenticated) {
                       next('/Home');
                    } else {
                       next();
                    }
                 },
                meta: {
                    title: 'Iniciar Sesión',
                },
            },

            {
                path: '/registro',
                name: 'Registro',
                component: () => import('../views/Login/Registro.vue'),
                beforeEnter: (to, from, next) => {
                    if (store.getters.isAuthenticated) {
                       next('/Home');
                    } else {
                       next();
                    }
                 },
                meta: {
                  title: 'Registro',
                },
            },
            {
                path: '/registroCliente',
                name: 'RegistroCliente',
                component: () => import('../views/Login/RegistroCliente.vue'),
                beforeEnter: (to, from, next) => {
                    if (store.getters.isAuthenticated) {
                       next('/Home');
                    } else {
                       next();
                    }
                 },
                meta: {
                  title: 'Registro Cliente',
                },
            },          
            {
                path: '/registroEmpresa',
                name: 'RegistroEmpresa',
                component: () => import('../views/Login/RegistroEmpresa.vue'),
                beforeEnter: (to, from, next) => {
                    if (store.getters.isAuthenticated) {
                       next('/Home');
                    } else {
                       next();
                    }
                 },
                meta: {
                  title: 'Registro Empresa',
                },
              },         
            {
                path: '/registroEmpleado',
                name: 'RegistroEmpleado',
                component: () => import('../views/Login/RegistroEmpleado.vue'),
                beforeEnter: (to, from, next) => {
                    if (store.getters.isAuthenticated) {
                       next('/Home');
                    } else {
                       next();
                    }
                 },
                meta: {
                  title: 'Registro Empleado',
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
                path: '/Perfil',
                name: 'Perfil',
                component: () => import('../views/Usuario/perfil.vue'),
                beforeEnter: (to, from, next) => {
                    if (store.getters.isAuthenticated) {
                       next('/Home');
                    } else {
                       next();
                    }
                 },
                meta: {
                    title: 'Perfil',
                },
            },
            {
                path: '/Reclamos',
                name: 'Reclamos',
                component: () => import('../views/Reclamos/listadoReclamos.vue'),
                meta: {
                    title: 'Reclamos',
                },
            },
            {
                path: '/Ordenes',
                name: 'Ordenes',
                component: () => import('../views/OrdenesCompra/Ordenes.vue'),
                meta: {
                    title: 'Reclamos',
                },
            },

            {
                path: '/Carrito',
                name: 'Carrito',
                component: () => import('../views/Carrito/CarritoView.vue'),
                meta: {
                    title: 'Carrito',
                },
            },
            {
                path: '/Empleados',
                name: 'Empleados',
                component: () => import('../views/Usuario/ListadoEmpleados.vue'),
                meta: {
                    title: 'Carrito',
                },
            },
        ],
    },
];



const router = createRouter({
    history: createWebHistory(),
    routes,
  });
  
  export default router;

