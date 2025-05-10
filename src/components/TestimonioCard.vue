<template>
    <div class="testimonio-card">
      <div class="testimonio-cabecera">
        <div class="testimonio-avatar">
          {{ iniciales }}
        </div>
        <div class="testimonio-info">
          <h4>{{ testimonio.nombre }}</h4>
          <div class="testimonio-estrellas">
            <span v-for="i in 5" :key="i" class="estrella" :class="{ 'activa': i <= testimonio.puntuacion }">★</span>
          </div>
        </div>
      </div>
      <div class="testimonio-contenido">
        <p>{{ testimonio.texto }}</p>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'TestimonioCard',
    props: {
      testimonio: {
        type: Object,
        required: true
      }
    },
    computed: {
      iniciales() {
        const nombres = this.testimonio.nombre.split(' ');
        if (nombres.length > 1) {
          return `${nombres[0][0]}${nombres[1][0]}`;
        }
        return nombres[0][0];
      }
    }
  }
  </script>
  
  <style lang="scss">
  @import '@/assets/styles/components/testimonio-card.scss';
  </style>