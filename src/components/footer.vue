<template>
  <footer class="footer">
    <div class="container">
      <div class="footer-links">
        <nav>
          <router-link to="/home">Inicio</router-link>
          <router-link to="/tienda">Productos</router-link>
          <router-link to="/blog">Contacto</router-link>
        </nav>
      </div>
      <div class="footer-info">
        <div class="contact">
          <p><i class="fa-solid fa-phone"></i> 900 123 456</p>
          <p><i class="fa-solid fa-envelope"></i> info@farmed.com</p>
          <p><i class="fa-solid fa-location-dot"></i> Av. Salud 123, Madrid</p>
        </div>
      </div>
      <div class="footer-legal">
        <router-link to="/terminos">Términos y condiciones</router-link>
        <router-link to="/privacidad">Política de privacidad</router-link>
        <router-link to="/blog">Contacto</router-link>
      </div>
      <p class="copyright">&copy; {{ new Date().getFullYear() }} FARMED. Todos los derechos reservados.</p>
    </div>
  </footer>
</template>

<script setup lang="ts">
// No se requiere lógica adicional para este componente simplificado
</script>

<style scoped lang="scss">
@use '@/assets/styles/components/footer.scss';
</style>