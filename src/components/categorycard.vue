

<template>
    <div class="category-card">
      <router-link :to="link">
        <div class="icon" :style="{ backgroundColor: bgColor }">
          <img :src="icon" :alt="name" />
        </div>
        <h3>{{ name }}</h3>
        <div class="underline"></div>
      </router-link>
    </div>
  </template>
  
  <script setup lang="ts">
  defineProps<{
    name: string;
    icon: string;
    link: string;
    bgColor: string;
  }>();
  </script>
  
  <style scoped lang="scss">
@use "../assets/styles/variables" as *;
@use "../assets/styles/mixins" as *;

.category-card {
  text-align: center;
  width: 120px; // Reducimos el tamaño para que ocupen menos espacio

  a {
    text-decoration: none;
    color: inherit;
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .icon {
    width: 60px; // Más pequeño
    height: 60px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;

    img {
      width: 28px; // Ajustamos el icono
      height: 28px;
    }
  }

  h3 {
    font-size: 14px; // Texto más pequeño
    margin: 8px 0;
    font-weight: 600;
  }

  .underline {
    width: 30px; // Línea más pequeña
    height: 2px;
    background-color: #666;
    margin-top: 4px;
  }
}

  </style>
  