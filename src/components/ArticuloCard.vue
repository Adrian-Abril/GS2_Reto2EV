<template>
    <div class="articulo-card">
      <div class="articulo-imagen">
        <img :src="articulo.imagen" :alt="articulo.titulo">
      </div>
      <div class="articulo-contenido">
        <span class="articulo-fecha">{{ articulo.fecha }}</span>
        <h3 class="articulo-titulo">{{ articulo.titulo }}</h3>
        <p class="articulo-extracto">{{ articulo.extracto }}</p>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'ArticuloCard',
    props: {
      articulo: {
        type: Object,
        required: true
      }
    }
  }
  </script>
  
  <style lang="scss">
  @import '@/assets/styles/components/articulo-card.scss';
  </style>