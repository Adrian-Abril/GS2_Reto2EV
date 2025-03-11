<template>
  <div class="notifications-panel">
    <div class="panel-header">
      <h3>Notificaciones</h3>
      <button class="close-btn" @click="$emit('close')">
        <i class="fa-solid fa-xmark"></i>
      </button>
    </div>
    
    <div class="panel-content">
      <div v-if="notifications.length === 0" class="empty-notifications">
        <div class="empty-icon">
          <i class="fa-solid fa-bell-slash"></i>
        </div>
        <p>No tienes notificaciones</p>
      </div>
      
      <div v-else class="notifications-list">
        <div 
          v-for="notification in notifications" 
          :key="notification.id" 
          class="notification-item"
          :class="{ 'unread': !notification.read }"
          @click="markAsRead(notification.id)"
        >
          <div class="notification-icon" :class="notification.type">
            <i v-if="notification.type === 'order'" class="fa-solid fa-shopping-bag"></i>
            <i v-else-if="notification.type === 'appointment'" class="fa-solid fa-calendar"></i>
            <i v-else-if="notification.type === 'prescription'" class="fa-solid fa-prescription"></i>
            <i v-else class="fa-solid fa-circle-exclamation"></i>
          </div>
          
          <div class="notification-content">
            <div class="notification-header">
              <h4>{{ notification.title }}</h4>
              <span class="notification-time">{{ formatNotificationTime(notification) }}</span>
            </div>
            <p>{{ notification.message }}</p>
          </div>
        </div>
      </div>
    </div>
    
    <div class="panel-footer">
      <button 
        class="mark-all-read"
        @click="markAllAsRead"
        :disabled="!hasUnreadNotifications"
      >
        Marcar todas como leídas
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'NotificationsPanel',
  props: {
    notifications: {
      type: Array,
      default: () => []
    }
  },
  computed: {
    hasUnreadNotifications() {
      return this.notifications.some(notification => !notification.read);
    }
  },
  methods: {
    markAsRead(notificationId) {
      this.$emit('mark-read', notificationId);
    },
    markAllAsRead() {
      this.notifications.forEach(notification => {
        if (!notification.read) {
          this.markAsRead(notification.id);
        }
      });
    },
    formatNotificationTime(notification) {
      const today = new Date();
      const notificationDate = new Date(`${notification.date} ${notification.time}`);
      
      // Si es hoy, mostrar solo la hora
      if (
        today.getDate() === notificationDate.getDate() &&
        today.getMonth() === notificationDate.getMonth() &&
        today.getFullYear() === notificationDate.getFullYear()
      ) {
        return notification.time;
      }
      
      // Si es ayer
      const yesterday = new Date(today);
      yesterday.setDate(yesterday.getDate() - 1);
      if (
        yesterday.getDate() === notificationDate.getDate() &&
        yesterday.getMonth() === notificationDate.getMonth() &&
        yesterday.getFullYear() === notificationDate.getFullYear()
      ) {
        return `Ayer, ${notification.time}`;
      }
      
      // Para otras fechas
      return `${notification.date}, ${notification.time}`;
    }
  }
};
</script>

<style>
@import '@/assets/styles/components/notifications-panel.scss';
</style>