<template>
    <footer class="footer">
      <div class="container">
        <p>&copy; 2024 FARMED. Todos los derechos reservados.</p>
        <nav>
          <router-link to="/terminos">Términos y condiciones</router-link>
          <router-link to="/privacidad">Política de privacidad</router-link>
          <router-link to="/contacto">Contacto</router-link>
        </nav>
      </div>
    </footer>
  </template>
  
  <script setup lang="ts">
  </script>
  
  <style scoped lang="scss">
  @use "../assets/styles/variables" as *;
  @use "../assets/styles/mixins" as *;
  
.footer {
    background-color: #333;
    color: white;
    text-align: center;
    padding: 20px 0;
  
    .container {
      width: 90%;
      max-width: 1200px;
      margin: 0 auto;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  
    nav {
      display: flex;
      gap: 15px;
  
      a {
        color: white;
        text-decoration: none;
        font-size: 14px;
  
        &:hover {
          text-decoration: underline;
        }
      }
    }
  }
  
  </style>
  