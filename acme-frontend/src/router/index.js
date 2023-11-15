import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import categoria from '../views/Categoria/categoria.vue';
import ListadoEmpresas from '../views/Empresa/ListadoEmpresas.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/Categorias',
    name: 'Categorias',
    component: categoria,
  },
  {
    path: '/Empresas',
    name: 'Empresas',
    component: ListadoEmpresas,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
