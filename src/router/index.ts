import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Login from '../views/Login.vue';
import Pedidos from '../views/Pedidos.vue';
import Perfil from '../views/Perfil.vue';
import Register from '../views/Register.vue';

const routes = [
  { path: '/', component: Home, meta: { public: true } },
  { path: '/login', component: Login, meta: { public: true } },
  { path: '/pedidos', component: Pedidos, meta: { requiresAuth: true } },
  { path: '/perfil', component: Perfil, meta: { requiresAuth: true } },
  { path: '/register', component: Register, meta: { public: true } },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Protegemos rutas privadas (esto se mejorará más adelante con autenticación)
router.beforeEach((to, from, next) => {
  const isAuthenticated = false; // Esto se conectará con Pinia después
  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login');
  } else {
    next();
  }
});

export default router;
