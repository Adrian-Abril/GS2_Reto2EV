<template>
    <div class="user-account-container">
      <div class="user-header">
        <div class="user-avatar">
          <span>{{ userInitials }}</span>
        </div>
        <div class="user-info">
          <h2 class="user-name">{{ user.name }}</h2>
          <p class="user-email">{{ user.email }}</p>
          <span class="user-role">{{ userRoleText }}</span>
        </div>
      </div>
  
      <div class="account-sections">
        <div class="welcome-section">
          <h3>¡Bienvenido a tu cuenta de Farmed!</h3>
          <p>Desde aquí podrás gestionar tus pedidos, recetas y disfrutar de beneficios exclusivos.</p>
        </div>
        
        <div class="quick-actions">
          <h3>Acciones rápidas</h3>
          <div class="actions-grid">
            <div class="action-card">
              <i class="action-icon orders-icon"></i>
              <h4>Mis Pedidos</h4>
              <p>Revisa el estado de tus pedidos actuales e históricos</p>
            </div>
            
            <div class="action-card">
              <i class="action-icon prescriptions-icon"></i>
              <h4>Recetas</h4>
              <p>Gestiona tus recetas médicas y reabastécelas</p>
            </div>
            
            <div class="action-card">
              <i class="action-icon appointments-icon"></i>
              <h4>Citas</h4>
              <p>Agenda una consulta con nuestros especialistas</p>
            </div>
            
            <div class="action-card">
              <i class="action-icon profile-icon"></i>
              <h4>Mi Perfil</h4>
              <p>Actualiza tus datos personales y preferencias</p>
            </div>
          </div>
        </div>
  
        <div class="latest-orders">
          <h3>Pedidos recientes</h3>
          <div v-if="hasOrders" class="orders-list">
            <div v-for="(order, index) in orders" :key="index" class="order-item">
              <div class="order-info">
                <h4>Pedido #{{ order.id }}</h4>
                <p>{{ order.date }}</p>
                <span class="order-status" :class="order.status">{{ order.status }}</span>
              </div>
              <div class="order-amount">
                {{ order.amount }}€
              </div>
            </div>
          </div>
          <div v-else class="no-orders">
            <i class="empty-icon"></i>
            <p>Aún no tienes pedidos realizados</p>
            <button class="shop-button">Ir a la tienda</button>
          </div>
        </div>
      </div>
  
      <div class="account-footer">
        <button @click="$emit('logout')" class="logout-button">
          <i class="logout-icon"></i>
          Cerrar sesión
        </button>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'UserAccount',
    props: {
      user: {
        type: Object,
        required: true
      }
    },
    data() {
      return {
        orders: [
          {
            id: '10045',
            date: '05/03/2025',
            status: 'entregado',
            amount: '32.50'
          },
          {
            id: '10028',
            date: '28/02/2025',
            status: 'en-proceso',
            amount: '45.75'
          }
        ]
      }
    },
    computed: {
      userInitials() {
        if (!this.user || !this.user.name) return '?';
        
        return this.user.name
          .split(' ')
          .map(name => name.charAt(0).toUpperCase())
          .slice(0, 2)
          .join('');
      },
      userRoleText() {
        if (!this.user || !this.user.role) return 'Cliente';
        
        const roles = {
          'cliente': 'Cliente',
          'farmaceutico': 'Farmacéutico',
          'admin': 'Administrador'
        };
        
        return roles[this.user.role] || 'Cliente';
      },
      hasOrders() {
        return this.orders && this.orders.length > 0;
      }
    }
  };
  </script>
  
  <style lang="scss">
  @use '@/assets/styles/components/user-account.scss';
  </style>