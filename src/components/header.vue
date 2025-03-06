<template>
  <header class="header">
    <div class="container">
      <div class="logo">
        <router-link to="/" class="logo__link">
          <img src="/src/assets/images/1.png" alt="FARMED Logo" class="logo__image" />
          <div>
            <h1>FARMED</h1>
            <span>Somos farmacéuticos</span>
          </div>
        </router-link>
      </div>

      <!-- Botón hamburguesa -->
      <button class="menu-toggle" @click="toggleMenu" :class="{ open: isMenuOpen }">
        <div></div>
        <div></div>
        <div></div>
      </button>

      <!-- Menú de navegación -->
      <nav class="nav" :class="{ 'nav--open': isMenuOpen }">
        <ul>
          <li><router-link to="/" @click="closeMenu">Categorías</router-link></li>
          <li><router-link to="/medicamentos" @click="closeMenu">Medicamentos</router-link></li>
          <li><router-link to="/novedades" @click="closeMenu">Novedades</router-link></li>
          <li><router-link to="/oportunidades" @click="closeMenu">Oportunidades</router-link></li>
          <li><router-link to="/blog" @click="closeMenu">Blog</router-link></li>
        </ul>
      </nav>

      <!-- Acciones (login y carrito) -->
      <div class="actions">
        <router-link to="/login" class="login">Iniciar sesión</router-link>
        <button class="cart"></button>
      </div>
    </div>
  </header>
</template>

<script setup lang="ts">
import { ref } from "vue";

const isMenuOpen = ref(false);

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};

const closeMenu = () => {
  isMenuOpen.value = false;
};
</script>

<style scoped lang="scss">
@use '@/../../assets/styles/components/header.scss' as *;

.logo__link {
  display: flex;
  align-items: center;
  text-decoration: none;
  color: inherit;
}

.logo__image {
  cursor: pointer;
}
</style>
