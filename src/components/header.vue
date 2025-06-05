<template>
  <header class="header">
    <div class="header-container">
      <!-- Logo y nombre -->
      <div class="brand">
        <router-link to="/" class="logo-link">
          <div class="logo">
            <img src="@/assets/images/1.png" alt="Logo FARMED" />
          </div>
          <h1 class="brand-name">FARMED</h1>
        </router-link>
      </div>
      
      <!-- Navegación principal -->
      <nav class="main-nav" :class="{ 'active': menuActive }">
        <ul class="nav-list">
          <li class="nav-item">
            <router-link to="/" class="nav-link">Inicio</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/tienda" class="nav-link">Medicamentos</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/oportunidades" class="nav-link">Trabaja Con Nosotros</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/blog" class="nav-link">Sobre nosotros</router-link>
          </li>
        </ul>
      </nav>
      
      <!-- Acciones del usuario -->
      <div class="user-actions">
        <div class="user-action">
          <router-link to="/login" class="action-link">
            <div class="icon-container">
              <!-- SVG de usuario directamente en el template -->
              <svg class="action-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path>
                <circle cx="12" cy="7" r="4"></circle>
              </svg>
            </div>
            <span class="action-text">Mi cuenta</span>
          </router-link>
        </div>
        
        <div class="user-action">
          <router-link to="/carrito" class="action-link">
            <div class="cart-container">
              <!-- SVG de carrito directamente en el template -->
              <svg class="action-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <circle cx="9" cy="21" r="1"></circle>
                <circle cx="20" cy="21" r="1"></circle>
                <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6"></path>
              </svg>
              <span v-if="cartItemsCount > 0" class="cart-badge">{{ cartItemsCount }}</span>
            </div>
            <span class="action-text">Mi cesta</span>
          </router-link>
        </div>
      </div>
      
      <!-- Botón de menú móvil -->
      <div class="mobile-menu-toggle" @click="toggleMenu">
        <span></span>
        <span></span>
        <span></span>
      </div>
    </div>
  </header>
</template>

<script>
export default {
  name: 'Header',
  data() {
    return {
      menuActive: false,
      cartItemsCount: 0
    }
  },
  methods: {
    toggleMenu() {
      this.menuActive = !this.menuActive;
      // Evitar scroll cuando el menú está abierto en móvil
      document.body.style.overflow = this.menuActive ? 'hidden' : '';
    },
    updateCartCount() {
      const carritoGuardado = localStorage.getItem('carrito');
      if (carritoGuardado) {
        const carrito = JSON.parse(carritoGuardado);
        this.cartItemsCount = carrito.reduce((total, item) => total + item.cantidad, 0);
      } else {
        this.cartItemsCount = 0;
      }
    }
  },
  created() {
    this.updateCartCount();
    
    // Actualizar el contador cuando cambia el carrito
    window.addEventListener('storage', (event) => {
      if (event.key === 'carrito') {
        this.updateCartCount();
      }
    });
  },
  mounted() {
    // Evento personalizado para actualizar el carrito cuando se añade un producto
    this.$root.$on('cart-updated', () => {
      this.updateCartCount();
    });
  },
  beforeDestroy() {
    // Limpiar listeners
    this.$root.$off('cart-updated');
    window.removeEventListener('storage');
  }
}
</script>

<style>
@import '@/assets/styles/components/header.scss';
</style>