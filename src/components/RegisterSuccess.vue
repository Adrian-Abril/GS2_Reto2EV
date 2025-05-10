<template>
    <div class="success-overlay">
      <div class="success-card">
        <div class="success-icon">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="64" height="64">
            <circle cx="12" cy="12" r="11" fill="#27ae60" />
            <path d="M9 16.2L4.8 12l-1.4 1.4L9 19 21 7l-1.4-1.4L9 16.2z" fill="white"/>
          </svg>
        </div>
        
        <h2 class="success-title">¡Registro Exitoso!</h2>
        
        <p class="success-message">
          Gracias por registrarte en FarMed, {{ userData.name }}. Tu cuenta ha sido creada correctamente.
        </p>
        
        <div class="user-details">
          <div class="detail-item">
            <span class="detail-label">Email:</span>
            <span class="detail-value">{{ userData.email }}</span>
          </div>
          <div v-if="userData.phone" class="detail-item">
            <span class="detail-label">Teléfono:</span>
            <span class="detail-value">{{ userData.phone }}</span>
          </div>
        </div>
        
        <p class="next-steps">
          Hemos enviado un correo de confirmación a tu dirección de email. 
          Por favor, verifica tu cuenta haciendo clic en el enlace del correo.
        </p>
        
        <button @click="$emit('continue')" class="continue-button">
          Continuar al inicio de sesión
        </button>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'RegisterSuccess',
    props: {
      userData: {
        type: Object,
        required: true
      }
    }
  };
  </script>
  
  <style lang="scss">
    @import '@/assets/styles/components/register-success.scss';
  </style>