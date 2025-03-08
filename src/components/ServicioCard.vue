<template>
    <div class="servicio-card">
      <div class="servicio-icono">
        <svg v-if="servicio.icono === 'pill'" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="36" height="36" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M10.5 20.5 3 13c-1.4-1.4-1.4-3.6 0-5s3.6-1.4 5 0l.5.5"></path>
          <path d="m13 7.5 7.5 7.5c1.4 1.4 1.4 3.6 0 5s-3.6 1.4-5 0L8 12.5"></path>
        </svg>
        <svg v-else-if="servicio.icono === 'chat'" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="36" height="36" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z"></path>
        </svg>
        <svg v-else-if="servicio.icono === 'heart'" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="36" height="36" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z"></path>
        </svg>
        <svg v-else-if="servicio.icono === 'skin'" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="36" height="36" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M6 8c.5 4 2 7 6 8.5C16 15 17.5 12 18 8"></path>
          <path d="M12 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5Z"></path>
          <path d="M12 21a7.5 7.5 0 0 0 7.5-7.5c0-1-.2-1.9-.6-2.7"></path>
          <path d="M4.5 13.8c-.4-.8-.6-1.7-.6-2.7A7.5 7.5 0 0 1 12 3.5c1 0 1.9.2 2.7.6"></path>
        </svg>
        <svg v-else xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="36" height="36" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M4.5 17H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h16a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2h-.5"></path>
          <path d="M12 15H7a2 2 0 0 0-2 2v4h10v-4a2 2 0 0 0-2-2h-1Z"></path>
        </svg>
      </div>
      <div class="servicio-contenido">
        <h3>{{ servicio.titulo }}</h3>
        <p>{{ servicio.descripcion }}</p>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'ServicioCard',
    props: {
      servicio: {
        type: Object,
        required: true
      }
    }
  }
  </script>
  
  <style lang="scss">
  @import '@/assets/styles/components/servicio-card.scss';
  </style>