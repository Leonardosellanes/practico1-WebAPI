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
        name: 'Layout',
        component: () => import('../layout/index.vue'),
        redirect: '/Home',
        meta: {
            title: 'Layout',
        },
        children: [
            {
                path: '/Home',
                name: 'Home',
                component: () => import('../views/Productos/listadoHome.vue'),
                meta: {
                    title: 'Home',
                },
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next('/Ordenes')
                        }
                        if(rol == 'USER'){
                            next();
                        }
                    } else {
                       next('/login');
                    }
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
                path: '/registro',
                name: 'Registro',
                component: () => import('../views/Login/Registro.vue'),
                meta: {
                  title: 'Registro',
                },
            },
            {
                path: '/registroCliente',
                name: 'RegistroCliente',
                component: () => import('../views/Login/RegistroCliente.vue'),
                meta: {
                  title: 'Registro Cliente',
                },
            },          
            {
                path: '/registroEmpresa',
                name: 'RegistroEmpresa',
                component: () => import('../views/Login/RegistroEmpresa.vue'),
                meta: {
                  title: 'Registro Empresa',
                },
              },         
            {
                path: '/registroEmpleado',
                name: 'RegistroEmpleado',
                component: () => import('../views/Login/RegistroEmpleado.vue'),
                meta: {
                  title: 'Registro Empleado',
                },
            },
            {
                path: '/Categorias',
                name: 'Categorias',
                component: () => import('../views/Categoria/categoria.vue'),
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next()
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
                 },
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
                },
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next('/Ordenes')
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
                },
            },
            {
                path: '/Productos/Product/:id',
                name: 'Product',
                component: () => import('../views/Productos/vistaProducto.vue'),
                meta: {
                    title: 'Product',
                },
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next()
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
                },

            },
            { 
                path: '/Empresas',
                name: 'Empresas',
                component: () => import('../views/Empresa/ListadoEmpresas.vue'),
                meta: {
                    title: 'Empresas',
                },
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next()
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next('/Ordenes')
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
                },
            },

            {
                path: '/Sucursales/:id',
                name: 'Sucursales',
                component: () => import('../views/Sucursal/ListadoSucursales.vue'),
                meta: {
                    title: 'Sucursales',
                },
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN' || rol == 'MANAGER'){
                            next()
                        }
                        if(rol == 'EMPLEADO' ){
                            next('/Ordenes')
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
                },
            },

              { 
                path: '/Perfil',
                name: 'Perfil',
                component: () => import('../views/Usuario/perfil.vue'),
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        next();
                    } else {
                       next('/login');
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
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next()
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
                },
            },
            {
                path: '/Ordenes',
                name: 'Ordenes',
                component: () => import('../views/OrdenesCompra/Ordenes.vue'),
                meta: {
                    title: 'Reclamos',
                },
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next()
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
                },
            },

            {
                path: '/Carrito',
                name: 'Carrito',
                component: () => import('../views/Carrito/CarritoView.vue'),
                meta: {
                    title: 'Carrito',
                },
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO' || rol == 'MANAGER'){
                            next('/Ordenes')
                        }
                        if(rol == 'USER'){
                            next();
                        }
                    } else {
                       next('/login');
                    }
                },
            },
            {
                path: '/Empleados',
                name: 'Empleados',
                component: () => import('../views/Usuario/ListadoEmpleados.vue'),
                beforeEnter: (to, from, next) => {
                    const rol = sessionStorage.getItem('rol');
                    if (rol != null) {
                        if(rol == 'ADMIN'){
                            next('/Empresas')
                        }
                        if(rol == 'EMPLEADO'){
                            next('/Ordenes')
                        }
                        if(rol == 'MANAGER'){
                            next();
                        }
                        if(rol == 'USER'){
                            next('/Home');
                        }
                    } else {
                       next('/login');
                    }
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

