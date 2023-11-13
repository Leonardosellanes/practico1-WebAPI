import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import categoria from '../views/Categoria/categoria.vue'
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
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
