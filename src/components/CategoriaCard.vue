<template>
    <div class="categoria-card">
              <div class="categoria-imagen">
          <img :src="categoria.imagen" :alt="categoria.nombre">
        </div>
        <div class="categoria-contenido">
          <h3>{{ categoria.nombre }}</h3>
          <span class="ver-productos">Ver productos</span>
        </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'CategoriaCard',
    props: {
      categoria: {
        type: Object,
        required: true
      }
    }
  }
  </script>
  
  <style lang="scss">
  @import '@/assets/styles/components/categoria-card.scss';
  </style>