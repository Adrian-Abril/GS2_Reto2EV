<template>
  <header class="header">
    <div class="container">
      <div class="logo">
        <h1>VOLUNMED</h1>
        <span>Somos farmacéuticos</span>
      </div>
      <nav class="nav">
        <ul>
          <li><router-link to="/">Categorías</router-link></li>
          <li><router-link to="/medicamentos">Medicamentos</router-link></li>
          <li><router-link to="/promociones">Promociones</router-link></li>
          <li><router-link to="/novedades">Novedades</router-link></li>
          <li><router-link to="/oportunidades">Oportunidades</router-link></li>
          <li><router-link to="/marcas">Marcas</router-link></li>
          <li><router-link to="/blog">Blog</router-link></li>
        </ul>
      </nav>
      <div class="actions">
        <button class="login">Iniciar sesión</button>
        <button class="cart"></button>
      </div>
    </div>
  </header>
</template>

<script setup lang="ts">
</script>

<style scoped lang="scss">
@use '@/assets/styles/header' as *;
</style>
