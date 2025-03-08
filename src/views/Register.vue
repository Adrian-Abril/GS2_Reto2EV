<template>
  <div class="register-container">
    <div class="register-card">
      <div class="logo-container">
        <h1 class="logo">FarMed</h1>
        <div class="logo-underline"></div>
        <p class="slogan">Tu farmacia de confianza</p>
      </div>
      
      <h2 class="form-title">Crear Cuenta</h2>
      
      <form @submit.prevent="handleRegister" class="register-form">
        <div class="form-group">
          <label for="name">Nombre completo</label>
          <input 
            type="text" 
            id="name" 
            v-model="formData.name" 
            required 
            placeholder="Tu nombre y apellidos"
          />
        </div>
        
        <div class="form-group">
          <label for="email">Correo electrónico</label>
          <input 
            type="email" 
            id="email" 
            v-model="formData.email" 
            required 
            placeholder="correo@ejemplo.com"
          />
        </div>
        
        <div class="form-group">
          <label for="phone">Teléfono (opcional)</label>
          <input 
            type="tel" 
            id="phone" 
            v-model="formData.phone" 
            placeholder="Tu número de teléfono"
          />
        </div>
        
        <div class="form-group">
          <label for="password">Contraseña</label>
          <input 
            :type="showPassword ? 'text' : 'password'" 
            id="password" 
            v-model="formData.password" 
            required 
            placeholder="Crea una contraseña"
          />
          <p class="password-hint">La contraseña debe tener al menos 6 caracteres</p>
        </div>
        
        <div class="form-group">
          <label for="confirmPassword">Confirmar contraseña</label>
          <input 
            :type="showConfirmPassword ? 'text' : 'password'" 
            id="confirmPassword" 
            v-model="formData.confirmPassword" 
            required 
            placeholder="Repite tu contraseña"
          />
        </div>
        
        <div class="terms-container">
          <input type="checkbox" id="terms" v-model="formData.termsAccepted" required />
          <label for="terms">
            Acepto los <a href="#" class="terms-link">términos y condiciones</a> y la <a href="#" class="terms-link">política de privacidad</a>
          </label>
        </div>
        
        <button type="submit" class="register-button">
          Crear Cuenta
        </button>
      </form>
      
      <div class="divider"></div>
      
      <div class="login-link-container">
        <p>¿Ya tienes una cuenta?</p>
        <router-link to="/login" class="login-link">Inicia sesión aquí</router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Register',
  data() {
    return {
      formData: {
        name: '',
        email: '',
        phone: '',
        password: '',
        confirmPassword: '',
        termsAccepted: false
      },
      showPassword: false,
      showConfirmPassword: false,
      errorMessage: ''
    };
  },
  methods: {
    validateForm() {
      if (this.formData.password.length < 6) {
        alert('La contraseña debe tener al menos 6 caracteres.');
        return false;
      }
      
      if (this.formData.password !== this.formData.confirmPassword) {
        alert('Las contraseñas no coinciden.');
        return false;
      }
      
      if (!this.formData.termsAccepted) {
        alert('Debes aceptar los términos y condiciones.');
        return false;
      }
      
      return true;
    },
    handleRegister() {
      if (!this.validateForm()) {
        return;
      }
      
      // En un caso real, aquí enviarías los datos al servidor
      console.log('Registro exitoso:', this.formData);
      
      // Redirigir al login o mostrar mensaje de éxito
      alert('Cuenta creada con éxito. Por favor, inicia sesión.');
      this.$router.push('/login');
    }
  }
};
</script>

<style lang="scss">
@import '@/assets/styles/pages/register.scss';
</style>