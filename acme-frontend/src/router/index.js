// src/router/index.js

import { createRouter, createWebHistory } from 'vue-router';


const routes = [
    {
        path: '/',
        name: 'Home',
        component: () => import('../layout/index.vue'),
        meta: {
            title: 'Home',
        },
        children: [

            {
                path: '/Categorias',
                name: 'Categorias',
                component: () => import('../views/Categoria/categoria.vue'),
                meta: {
                    title: 'Categorias',
                },
            },

            {
              path: '/Empresas',
              name: 'Empresas',
              component: () => import('../views/Empresa/ListadoEmpresas.vue'),
              meta: {
                  title: 'Categorias',
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

