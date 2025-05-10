<template>
  <div class="dashboard-header">
    <div class="header-title">
      <h1>{{ title }}</h1>
    </div>
    
    <div class="header-actions">
      <div class="search-bar">
        <div class="search-icon">
          <i class="fa-solid fa-search"></i>
        </div>
        <input type="text" placeholder="Buscar medicamentos, recetas..." />
      </div>
      
      <div class="notification-bell" @click="$emit('toggle-notifications')">
        <i class="fa-solid fa-bell"></i>
        <span v-if="unreadNotificationsCount > 0" class="notification-badge">
          {{ unreadNotificationsCount > 9 ? '9+' : unreadNotificationsCount }}
        </span>
      </div>
      
      <div class="user-profile">
        <div v-if="user.avatar" class="avatar">
          <img :src="user.avatar" :alt="user.name" />
        </div>
        <div v-else class="avatar-placeholder">
          {{ userInitials }}
        </div>
        <span class="user-name">{{ user.name }}</span>
      </div>
    </div>
  </div>
</template>

<script>
// Usamos Font Awesome que ya está disponible en el proyecto

export default {
  name: 'DashboardHeader',
  components: {
    // No necesitamos importar componentes para los iconos de Font Awesome
  },
  props: {
    user: {
      type: Object,
      required: true
    },
    title: {
      type: String,
      default: 'Dashboard'
    },
    notifications: {
      type: Array,
      default: () => []
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
    unreadNotificationsCount() {
      if (!this.notifications) return 0;
      return this.notifications.filter(n => !n.read).length;
    }
  }
};
</script>

<style>
@import '@/assets/styles/components/dashboard-header.scss';
</style>