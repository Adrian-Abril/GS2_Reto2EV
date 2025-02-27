<template>
    <div class="novedades">
      <div class="novedades__container">
        <h1 class="novedades__title">Novedades</h1>
        <p class="novedades__description">
          Descubre los productos más recientes y destacados de nuestra tienda. Cada uno seleccionado para ofrecerte la mejor calidad y experiencia.
        </p>
        <div class="novedades__content">
          <div v-for="(item, index) in novedades" :key="index" class="novedades__cajetilla">
            <div class="novedades__caja">
              <img :src="item.imagen" :alt="item.nombre" class="novedades__imagen" />
              <div class="novedades__info">
                <h3 class="novedades__nombre">{{ item.nombre }}</h3>
                <p class="novedades__descripcion">{{ item.descripcion }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref } from 'vue';
  
  const novedades = ref([
    { nombre: 'Producto 1', imagen: '/images/producto1.jpg', descripcion: 'Descripción del producto 1' },
    { nombre: 'Producto 2', imagen: '/images/producto2.jpg', descripcion: 'Descripción del producto 2' },
    { nombre: 'Producto 3', imagen: '/images/producto3.jpg', descripcion: 'Descripción del producto 3' },
    { nombre: 'Producto 4', imagen: '/images/producto4.jpg', descripcion: 'Descripción del producto 4' },
    { nombre: 'Producto 5', imagen: '/images/producto5.jpg', descripcion: 'Descripción del producto 5' },
    { nombre: 'Producto 6', imagen: '/images/producto6.jpg', descripcion: 'Descripción del producto 6' }
  ]);
  </script>
  
  <style scoped lang="scss">
  @use '../assets/styles/novedades.scss' as *;
  </style>
  