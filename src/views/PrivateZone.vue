<template>
    <div class="private-zone">
      <div class="sidebar">
        <div class="sidebar-header">
          <div class="pharmacy-logo">
            <img src="@/assets/images/1.png" alt="Logo FARMED" />
          </div>
          <h2>FARMED</h2>
        </div>
        
        <nav class="sidebar-nav">
          <div 
            v-for="(item, index) in menuItems" 
            :key="index" 
            class="nav-item"
            :class="{ active: activeSection === item.id }"
            @click="setActiveSection(item.id)"
          >
            <div class="icon-container">
              <i :class="item.icon"></i>
            </div>
            <span>{{ item.text }}</span>
          </div>
        </nav>
        
        <div class="sidebar-footer">
          <div class="nav-item" @click="logout">
            <div class="icon-container">
              <i class="fa-solid fa-right-from-bracket"></i>
            </div>
            <span>Cerrar sesión</span>
          </div>
        </div>
      </div>
      
      <div class="main-content">
        <DashboardHeader 
          :user="userData" 
          :title="currentSectionTitle" 
          @toggle-notifications="toggleNotifications"
        />
        
        <div class="content-container">
          <Dashboard v-if="activeSection === 'dashboard'" :user="userData" />
          <Prescriptions v-if="activeSection === 'prescriptions'" :prescriptions="userPrescriptions" />
          <MedicalHistory v-if="activeSection === 'history'" :history="userMedicalHistory" />
          <MyOrders v-if="activeSection === 'orders'" :orders="userOrders" />
          <Appointments v-if="activeSection === 'appointments'" :appointments="userAppointments" />
          <ProfileSettings v-if="activeSection === 'profile'" :user="userData" @update-profile="updateUserProfile" />
        </div>
      </div>
      
      <NotificationsPanel 
        v-if="showNotifications" 
        :notifications="userNotifications" 
        @close="toggleNotifications" 
        @mark-read="markNotificationRead"
      />
    </div>
  </template>
  
  <script>
  import DashboardHeader from '@/components/private/DashboardHeader.vue';
  import Dashboard from '@/components/private/Dashboard.vue';
  import Prescriptions from '@/components/private/Prescriptions.vue';
  import MedicalHistory from '@/components/private/MedicalHistory.vue';
  import MyOrders from '@/components/private/MyOrders.vue';
  import Appointments from '@/components/private/Appointments.vue';
  import ProfileSettings from '@/components/private/ProfileSettings.vue';
  import NotificationsPanel from '@/components/private/NotificationsPanel.vue';
  
  // Utilizando Font Awesome que parece estar ya instalado en tu proyecto
  // basado en el componente footer.vue que incluye clases como fa-solid fa-phone
  
  export default {
    name: 'PrivateZone',
    components: {
      DashboardHeader,
      Dashboard,
      Prescriptions,
      MedicalHistory,
      MyOrders,
      Appointments,
      ProfileSettings,
      NotificationsPanel
    },
    data() {
      return {
        activeSection: 'dashboard',
        showNotifications: false,
        userData: {
          id: 1,
          name: 'María García',
          email: 'maria.garcia@example.com',
          avatar: null,
          role: 'cliente',
          phone: '600123456',
          address: 'Calle Principal 123, Madrid',
          birthdate: '1985-05-15'
        },
        menuItems: [
          { id: 'dashboard', text: 'Inicio', icon: 'fa-solid fa-home' },
          { id: 'prescriptions', text: 'Mis Recetas', icon: 'fa-solid fa-prescription' },
          { id: 'history', text: 'Historial Médico', icon: 'fa-solid fa-clipboard-list' },
          { id: 'orders', text: 'Mis Pedidos', icon: 'fa-solid fa-shopping-bag' },
          { id: 'appointments', text: 'Citas', icon: 'fa-solid fa-calendar' },
          { id: 'profile', text: 'Mi Perfil', icon: 'fa-solid fa-user' }
        ],
        userPrescriptions: [
          {
            id: 'RX100234',
            medication: 'Ibuprofeno 600mg',
            doctor: 'Dr. Martínez',
            date: '2025-02-15',
            expiryDate: '2025-05-15',
            instructions: 'Tomar 1 comprimido cada 8 horas',
            status: 'active',
            refillsLeft: 2
          },
          {
            id: 'RX100235',
            medication: 'Amoxicilina 500mg',
            doctor: 'Dra. Ruiz',
            date: '2025-03-01',
            expiryDate: '2025-03-10',
            instructions: 'Tomar 1 cápsula cada 12 horas durante 7 días',
            status: 'completed',
            refillsLeft: 0
          }
        ],
        userMedicalHistory: [
          {
            date: '2025-02-15',
            type: 'consultation',
            provider: 'Dr. Martínez - Medicina General',
            notes: 'Dolor de cabeza y fiebre. Posible gripe estacional.'
          },
          {
            date: '2025-01-10',
            type: 'test',
            provider: 'Laboratorio Farmed',
            notes: 'Análisis de sangre completo. Resultados normales.'
          }
        ],
        userOrders: [
          {
            id: 'ORD10045',
            date: '2025-03-05',
            status: 'entregado',
            amount: 32.50,
            items: [
              { name: 'Paracetamol 500mg', quantity: 1, price: 5.95 },
              { name: 'Vitamina C 1000mg', quantity: 1, price: 12.50 },
              { name: 'Crema hidratante facial', quantity: 1, price: 14.05 }
            ],
            tracking: 'TR123456789ES'
          },
          {
            id: 'ORD10028',
            date: '2025-02-28',
            status: 'en-proceso',
            amount: 45.75,
            items: [
              { name: 'Omeprazol 20mg', quantity: 1, price: 8.25 },
              { name: 'Colirio hidratante', quantity: 2, price: 11.90 },
              { name: 'Complemento alimenticio', quantity: 1, price: 25.60 }
            ],
            tracking: null
          }
        ],
        userAppointments: [
          {
            id: 'APT2025031',
            type: 'Consulta farmacéutica',
            date: '2025-03-15',
            time: '10:30',
            pharmacist: 'Lucía Fernández',
            status: 'programada',
            notes: 'Revisión de medicación actual y posibles interacciones'
          },
          {
            id: 'APT2025022',
            type: 'Seguimiento tratamiento',
            date: '2025-03-05',
            time: '17:00',
            pharmacist: 'Carlos Jiménez',
            status: 'completada',
            notes: 'Control de presión arterial y revisión de medicación para hipertensión'
          }
        ],
        userNotifications: [
          {
            id: 1,
            title: 'Pedido entregado',
            message: 'Tu pedido #ORD10045 ha sido entregado correctamente.',
            date: '2025-03-05',
            time: '14:32',
            read: true,
            type: 'order'
          },
          {
            id: 2,
            title: 'Recordatorio de cita',
            message: 'Recuerda tu cita de consulta farmacéutica mañana a las 10:30.',
            date: '2025-03-14',
            time: '09:00',
            read: false,
            type: 'appointment'
          },
          {
            id: 3,
            title: 'Receta próxima a caducar',
            message: 'Tu receta de Ibuprofeno 600mg caducará en 7 días.',
            date: '2025-05-08',
            time: '08:00',
            read: false,
            type: 'prescription'
          }
        ]
      };
    },
    computed: {
      currentSectionTitle() {
        const section = this.menuItems.find(item => item.id === this.activeSection);
        return section ? section.text : '';
      }
    },
    methods: {
      setActiveSection(sectionId) {
        this.activeSection = sectionId;
        this.showNotifications = false;
      },
      toggleNotifications() {
        this.showNotifications = !this.showNotifications;
      },
      markNotificationRead(notificationId) {
        const notification = this.userNotifications.find(n => n.id === notificationId);
        if (notification) {
          notification.read = true;
        }
      },
      updateUserProfile(updatedProfile) {
        this.userData = { ...this.userData, ...updatedProfile };
        // Aquí se podría añadir la lógica para actualizar en el backend
      },
      logout() {
        // Lógica para cerrar sesión
        localStorage.removeItem('authToken');
        this.$router.push('/login');
      }
    }
  };
  </script>
  
  <style lang="scss">
  // Variables
  $primary-color: #2e7d32; // Verde farmacia
  $secondary-color: #e8f5e9; // Verde claro
  $accent-color: #1b5e20; // Verde oscuro
  $danger-color: #d32f2f; // Rojo para alertas
  $text-primary: #333333;
  $text-secondary: #666666;
  $text-light: #999999;
  $border-color: #e0e0e0;
  $bg-light: #f8f9fa;
  $white: #ffffff;
  $sidebar-width: 260px;
  $header-height: 70px;
  $box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  $border-radius: 8px;
  $transition: all 0.3s ease;
  
  .private-zone {
    display: flex;
    min-height: 100vh;
    background-color: $bg-light;
    font-family: 'Roboto', 'Segoe UI', sans-serif;
    
    .sidebar {
      width: $sidebar-width;
      background-color: $white;
      box-shadow: $box-shadow;
      display: flex;
      flex-direction: column;
      position: fixed;
      height: 100vh;
      z-index: 100;
      
      .sidebar-header {
        padding: 1.5rem;
        display: flex;
        align-items: center;
        border-bottom: 1px solid $border-color;
        
        .pharmacy-logo {
          width: 40px;
          height: 40px;
          margin-right: 1rem;
          
          img {
            width: 100%;
            height: 100%;
            object-fit: contain;
          }
        }
        
        h2 {
          color: $primary-color;
          font-weight: 700;
          margin: 0;
          font-size: 1.5rem;
        }
      }
      
      .sidebar-nav {
        flex: 1;
        padding: 1.5rem 0;
        
        .nav-item {
          display: flex;
          align-items: center;
          padding: 0.875rem 1.5rem;
          color: $text-secondary;
          cursor: pointer;
          transition: $transition;
          
          &:hover {
            background-color: rgba($primary-color, 0.05);
            color: $primary-color;
          }
          
          &.active {
            background-color: rgba($primary-color, 0.1);
            color: $primary-color;
            font-weight: 600;
            border-left: 4px solid $primary-color;
            
            .icon-container {
              color: $primary-color;
            }
          }
          
          .icon-container {
            width: 24px;
            height: 24px;
            margin-right: 0.875rem;
            display: flex;
            align-items: center;
            justify-content: center;
            color: $text-secondary;
          }
          
          span {
            font-size: 0.95rem;
          }
        }
      }
      
      .sidebar-footer {
        padding: 1rem 0;
        border-top: 1px solid $border-color;
        
        .nav-item {
          color: $text-secondary;
          
          &:hover {
            color: $danger-color;
            
            .icon-container {
              color: $danger-color;
            }
          }
        }
      }
    }
    
    .main-content {
      flex: 1;
      margin-left: $sidebar-width;
      
      .content-container {
        padding: 1.5rem;
        margin-top: $header-height;
      }
    }
  }
  
  // Estilos para componentes comunes en la zona privada
  .private-card {
    background-color: $white;
    border-radius: $border-radius;
    box-shadow: $box-shadow;
    padding: 1.5rem;
    margin-bottom: 1.5rem;
    
    .card-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.25rem;
      
      h3 {
        font-size: 1.25rem;
        font-weight: 600;
        color: $text-primary;
        margin: 0;
      }
      
      .card-actions {
        display: flex;
        gap: 0.5rem;
      }
    }
    
    .empty-state {
      text-align: center;
      padding: 2rem 0;
      color: $text-light;
      
      .empty-icon {
        font-size: 3rem;
        margin-bottom: 1rem;
        opacity: 0.5;
      }
      
      p {
        margin-bottom: 1.5rem;
        font-size: 1rem;
      }
    }
  }
  
  // Estilos para botones
  .btn {
    padding: 0.5rem 1rem;
    border-radius: 4px;
    font-weight: 500;
    cursor: pointer;
    transition: $transition;
    border: none;
    font-size: 0.9rem;
    display: inline-flex;
    align-items: center;
    gap: 0.5rem;
    
    &.btn-primary {
      background-color: $primary-color;
      color: $white;
      
      &:hover {
        background-color: $accent-color;
      }
    }
    
    &.btn-outline {
      background-color: transparent;
      color: $primary-color;
      border: 1px solid $primary-color;
      
      &:hover {
        background-color: rgba($primary-color, 0.05);
      }
    }
    
    &.btn-danger {
      background-color: $danger-color;
      color: $white;
      
      &:hover {
        background-color: darken($danger-color, 10%);
      }
    }
    
    &.btn-text {
      background-color: transparent;
      color: $primary-color;
      padding: 0.5rem;
      
      &:hover {
        background-color: rgba($primary-color, 0.05);
      }
    }
    
    .btn-icon {
      width: 18px;
      height: 18px;
    }
  }
  
  // Estilos para estados y badges
  .status-badge {
    padding: 0.25rem 0.75rem;
    border-radius: 20px;
    font-size: 0.8rem;
    font-weight: 500;
    text-transform: capitalize;
    
    &.active, &.programada {
      background-color: rgba($primary-color, 0.1);
      color: $primary-color;
    }
    
    &.completed, &.entregado {
      background-color: rgba(76, 175, 80, 0.1);
      color: #4caf50;
    }
    
    &.en-proceso, &.pending {
      background-color: rgba(255, 152, 0, 0.1);
      color: #ff9800;
    }
    
    &.expired, &.cancelled {
      background-color: rgba(244, 67, 54, 0.1);
      color: #f44336;
    }
  }
  
  // Estilos responsivos
  @media (max-width: 992px) {
    .private-zone {
      .sidebar {
        transform: translateX(-100%);
        transition: transform 0.3s ease;
        
        &.active {
          transform: translateX(0);
        }
      }
      
      .main-content {
        margin-left: 0;
      }
    }
  }
  
  // Animaciones
  @keyframes fadeIn {
    from { opacity: 0; }
    to { opacity: 1; }
  }
  
  .fade-in {
    animation: fadeIn 0.3s ease;
  }
  </style>