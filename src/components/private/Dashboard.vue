<template>
  <div class="dashboard">
    <div class="welcome-card private-card">
      <div class="welcome-message">
        <h2>Bienvenido/a, {{ user.name.split(' ')[0] }}</h2>
        <p>¿Cómo podemos ayudarte hoy con tu salud?</p>
      </div>
      <div class="quick-actions">
        <button class="action-btn">
          <i class="fa-solid fa-prescription"></i>
          <span>Nueva receta</span>
        </button>
        <button class="action-btn">
          <i class="fa-solid fa-calendar-plus"></i>
          <span>Agendar cita</span>
        </button>
        <button class="action-btn">
          <i class="fa-solid fa-cart-shopping"></i>
          <span>Comprar</span>
        </button>
      </div>
    </div>
    
    <div class="dashboard-grid">
      <!-- Medicamentos actuales -->
      <div class="dashboard-card private-card">
        <div class="card-header">
          <h3>Mis Medicamentos Actuales</h3>
          <div class="card-actions">
            <button class="btn btn-text">Ver todos</button>
          </div>
        </div>
        
        <div v-if="activeMedications.length" class="medications-list">
          <div v-for="(medication, index) in activeMedications" :key="index" class="medication-item">
            <div class="medication-icon">
              <i class="fa-solid fa-pills"></i>
            </div>
            <div class="medication-details">
              <h4>{{ medication.name }}</h4>
              <p>{{ medication.dosage }}</p>
              <div class="medication-schedule">
                <span v-for="(time, i) in medication.schedule" :key="i" class="schedule-badge">
                  {{ time }}
                </span>
              </div>
            </div>
            <div class="medication-status">
              <div class="remaining-pills">
                <span class="pill-count">{{ medication.remaining }}</span>
                <span class="pill-label">restantes</span>
              </div>
              <button v-if="medication.refillable" class="btn btn-outline btn-small">Reponer</button>
            </div>
          </div>
        </div>
        
        <div v-else class="empty-state">
          <i class="fa-solid fa-prescription-bottle empty-icon"></i>
          <p>No tienes medicamentos activos actualmente</p>
          <button class="btn btn-primary">Añadir medicamento</button>
        </div>
      </div>
      
      <!-- Próximas citas -->
      <div class="dashboard-card private-card">
        <div class="card-header">
          <h3>Próximas Citas</h3>
          <div class="card-actions">
            <button class="btn btn-text">Ver todas</button>
          </div>
        </div>
        
        <div v-if="upcomingAppointments.length" class="appointments-list">
          <div v-for="(appointment, index) in upcomingAppointments" :key="index" class="appointment-item">
            <div class="appointment-date">
              <div class="date-box">
                <span class="date-month">{{ appointment.monthShort }}</span>
                <span class="date-day">{{ appointment.day }}</span>
              </div>
              <span class="appointment-time">{{ appointment.time }}</span>
            </div>
            
            <div class="appointment-details">
              <h4>{{ appointment.type }}</h4>
              <p>{{ appointment.with }}</p>
              <span class="status-badge" :class="appointment.status">
                {{ appointment.status }}
              </span>
            </div>
            
            <div class="appointment-actions">
              <button class="btn btn-outline btn-small">Modificar</button>
            </div>
          </div>
        </div>
        
        <div v-else class="empty-state">
          <i class="fa-solid fa-calendar empty-icon"></i>
          <p>No tienes citas programadas</p>
          <button class="btn btn-primary">Agendar cita</button>
        </div>
      </div>
      
      <!-- Últimos pedidos -->
      <div class="dashboard-card private-card">
        <div class="card-header">
          <h3>Últimos Pedidos</h3>
          <div class="card-actions">
            <button class="btn btn-text">Ver todos</button>
          </div>
        </div>
        
        <div v-if="recentOrders.length" class="orders-list">
          <div v-for="(order, index) in recentOrders" :key="index" class="order-item">
            <div class="order-info">
              <h4>Pedido #{{ order.id }}</h4>
              <p>{{ order.date }}</p>
              <span class="status-badge" :class="order.status">{{ order.statusText }}</span>
            </div>
            <div class="order-amount">
              {{ order.amount.toFixed(2) }}€
            </div>
            <div class="order-actions">
              <button class="btn btn-text btn-small">
                <i class="fa-solid fa-eye"></i>
                <span>Detalles</span>
              </button>
            </div>
          </div>
        </div>
        
        <div v-else class="empty-state">
          <i class="fa-solid fa-shopping-bag empty-icon"></i>
          <p>No has realizado pedidos recientemente</p>
          <button class="btn btn-primary">Ir a la tienda</button>
        </div>
      </div>
      
      <!-- Recordatorios de salud -->
      <div class="dashboard-card private-card">
        <div class="card-header">
          <h3>Recordatorios de Salud</h3>
        </div>
        
        <div class="health-reminders">
          <div class="reminder-item">
            <div class="reminder-icon water">
              <i class="fa-solid fa-droplet"></i>
            </div>
            <div class="reminder-content">
              <h4>Hidratación diaria</h4>
              <div class="progress-bar">
                <div class="progress" style="width: 65%"></div>
              </div>
              <p>4/8 vasos de agua</p>
            </div>
          </div>
          
          <div class="reminder-item">
            <div class="reminder-icon medications">
              <i class="fa-solid fa-pills"></i>
            </div>
            <div class="reminder-content">
              <h4>Toma de medicamentos</h4>
              <div class="progress-bar">
                <div class="progress" style="width: 100%"></div>
              </div>
              <p>Todos los medicamentos tomados hoy</p>
            </div>
          </div>
          
          <div class="reminder-item">
            <div class="reminder-icon checkup">
              <i class="fa-solid fa-stethoscope"></i>
            </div>
            <div class="reminder-content">
              <h4>Revisión anual</h4>
              <p>Hace 10 meses desde tu última revisión general</p>
              <button class="btn btn-outline btn-small">Programar</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Dashboard',
  props: {
    user: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      activeMedications: [
        {
          name: 'Ibuprofeno 600mg',
          dosage: '1 comprimido / 8h',
          schedule: ['8:00', '16:00', '00:00'],
          remaining: 12,
          refillable: true
        },
        {
          name: 'Paracetamol 1g',
          dosage: '1 comprimido / 12h',
          schedule: ['9:00', '21:00'],
          remaining: 5,
          refillable: true
        },
        {
          name: 'Amoxicilina 500mg',
          dosage: '1 cápsula / 8h con las comidas',
          schedule: ['8:00', '14:00', '20:00'],
          remaining: 6,
          refillable: false
        }
      ],
      upcomingAppointments: [
        {
          type: 'Consulta farmacéutica',
          date: '2025-03-15',
          time: '10:30',
          with: 'Lucía Fernández',
          status: 'programada',
          day: '15',
          monthShort: 'MAR'
        },
        {
          type: 'Control de presión arterial',
          date: '2025-03-22',
          time: '17:00',
          with: 'Carlos Jiménez',
          status: 'programada',
          day: '22',
          monthShort: 'MAR'
        }
      ],
      recentOrders: [
        {
          id: 'ORD10045',
          date: '05/03/2025',
          status: 'delivered',
          statusText: 'Entregado',
          amount: 32.50
        },
        {
          id: 'ORD10028',
          date: '28/02/2025',
          status: 'processing',
          statusText: 'En proceso',
          amount: 45.75
        }
      ]
    };
  }
};
</script>

<style>
@import '@/assets/styles/components/dashboard.scss';
</style>