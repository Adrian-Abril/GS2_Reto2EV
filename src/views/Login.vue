
<template>
    <div class="login-container">
      <div v-if="!isLoggedIn" class="login-card">
        <div class="logo-container">
          <h1 class="logo">FarMed</h1>
          <div class="logo-underline"></div>
          <p class="slogan">Tu farmacia de confianza</p>
        </div>
        
        <h2 class="form-title">Iniciar Sesión</h2>
        
        <form @submit.prevent="handleLogin" class="login-form" method="post">
          <div class="form-group">
            <label for="email">Correo electrónico</label>
            <input 
              type="email" 
              id="email" 
              v-model="credentials.email" 
              required 
              placeholder="correo@ejemplo.com"
            />
          </div>
          
          <div class="form-group">
            <label for="password">Contraseña</label>
            <div class="password-container">
              <input 
                :type="showPassword ? 'text' : 'password'" 
                id="password" 
                v-model="credentials.password" 
                required 
                placeholder="TuContraseña"
              />
              <button 
                type="button" 
                class="toggle-password" 
                @click="showPassword = !showPassword"
              >
                <span v-if="showPassword">🔒</span>
                <span v-else>👁️</span>
              </button>
            </div>
          </div>
          
          <div class="form-options">
            <div class="remember-container">
              <input type="checkbox" id="remember" v-model="credentials.remember" />
              <label for="remember">Recordarme</label>
            </div>
            <a href="#" class="forgot-password">¿Olvidaste tu contraseña?</a>
          </div>
          
          <button type="submit" class="login-button">
            Iniciar Sesión
          </button>
          
          <div v-if="errorMessage" class="error-message">
            {{ errorMessage }}
          </div>
        </form>
        
        <div class="form-footer">
          <p>¿No tienes una cuenta?</p>
          <router-link to="/register" class="register-link">Regístrate aquí</router-link>
        </div>
        <div class="form-footer">
        <router-link to="/privatezone" class="register-link">ZonaPrivada</router-link>
      </div>
      </div>
      
      <UserAccount v-else @logout="handleLogout" :user="user" />
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  import UserAccount from '@/components/UserAccount.vue';
  
  export default {
    name: 'Login',
    components: {
      UserAccount
    },
    data() {
      return {
        credentials: {
          email: '',
          password: '',
          remember: false
        },
        showPassword: false,
        isLoading: false,
        isLoggedIn: false,
        errorMessage: '',
        user: null
      };
    },
    created() {
      // Verificar si hay datos en localStorage para auto-login
      const savedUser = localStorage.getItem('farmed_user');
      if (savedUser) {
        try {
          this.user = JSON.parse(savedUser);
          this.isLoggedIn = true;
        } catch (error) {
          localStorage.removeItem('farmed_user');
        }
      }
    },
    methods: {
      async handleLogin() {
        this.isLoading = true;
        this.errorMessage = '';
        console.log(this.credentials)
        try{
          const response = await axios.post('http://localhost:5000/api/auth/login', {
            correo: this.credentials.email,
            password: this.credentials.password
          }, { withCredentials: true});

          this.user = response.data;
          this.isLoggedIn = true;
            localStorage.setItem("rol", JSON.stringify(this.user.rol));
            localStorage.setItem('farmed_user', JSON.stringify(this.user));
            // Guardar en localStorage si la opción "recordarme" está activada
            if (this.credentials.remember) {
              localStorage.setItem('farmed_user', JSON.stringify(this.user));
            }
            if(this.user.rol == "admin"){
              alert("Bienvenido Admin");
            }
            
            // Limpiar formulario
            this.credentials = {
              email: '',
              password: '',
              remember: false
            };
        }catch(error){
          if(error.response)
          {
            console.error("respuesta de error", error.response);
            this.errorMessage = error.response.status == 401 ? "Credenciales incorrectas" : `Error del servidor ${error.response.status}`;
          }else{
            console.error("Error desconocido", error);
            this.errorMessage = "Credenciales incorrectas";
          }
          
        }

        this.isLoading = false;
      },
      handleLogout() {
        this.isLoggedIn = false;
        this.user = null;
        localStorage.removeItem('farmed_user');
        axios.post('http://localhost:5000/api/auth/logout', {}, { withCredentials: true })
      }
    }
  };
  </script>
  
  <style lang="scss">
@use '@/assets/styles/pages/login.scss' as *;
  </style>