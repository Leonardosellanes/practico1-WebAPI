// src/router/index.js

import { createRouter, createWebHistory } from 'vue-router';


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
        ],
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;

