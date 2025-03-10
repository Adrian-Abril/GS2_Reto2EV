<template>
    <section class="hero-banner">
      <div class="hero-container">
        <div class="hero-content">
          <h1>Farmacia <span>Farmed</span></h1>
          <h2>Tu salud es nuestra prioridad</h2>
          <p>Disponemos de una amplia gama de productos farmacéuticos, consejos profesionales y servicios personalizados para cuidar tu bienestar.</p>
          <div class="hero-buttons">
            <router-link to="/tienda" class="btn btn-primary">Ver Productos</router-link>
            <router-link to="/blog" class="btn btn-secondary">Contactar</router-link>
          </div>
        </div>
        <div class="hero-image">
          <img src="https://img.freepik.com/free-photo/modern-pharmacy-with-cheerful-clients_23-2147888274.jpg" alt="Farmed">
        </div>
      </div>
    </section>
  </template>
  
  <script>
  export default {
    name: 'HeroBanner'
  }
  </script>
  
  <style lang="scss">
  @import '@/assets/styles/components/hero-banner.scss';
  </style>