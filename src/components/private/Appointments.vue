<template>
    <div class="appointments">
      <div class="appointments-header private-card">
        <div class="section-info">
          <h2>Mis Citas</h2>
          <p>Gestiona tus citas farmacéuticas y servicios de atención</p>
        </div>
        <div class="section-actions">
          <button class="btn btn-primary">
            <i class="fa-solid fa-calendar-plus"></i>
            <span>Nueva cita</span>
          </button>
        </div>
      </div>
      
      <div class="appointments-tabs">
        <button 
          class="tab-button" 
          :class="{ active: activeTab === 'upcoming' }"
          @click="activeTab = 'upcoming'"
        >
          Próximas citas
        </button>
        <button 
          class="tab-button" 
          :class="{ active: activeTab === 'past' }"
          @click="activeTab = 'past'"
        >
          Citas pasadas
        </button>
      </div>
      
      <div class="appointments-filters">
        <div class="filter-group">
          <label>Tipo:</label>
          <select v-model="filters.type" class="filter-select">
            <option value="all">Todos</option>
            <option value="Consulta farmacéutica">Consulta farmacéutica</option>
            <option value="Seguimiento tratamiento">Seguimiento de tratamiento</option>
            <option value="Control presión">Control de presión</option>
            <option value="Vacunación">Vacunación</option>
          </select>
        </div>
        
        <div class="search-box">
          <i class="fa-solid fa-search search-icon"></i>
          <input type="text" v-model="filters.search" placeholder="Buscar citas..." class="search-input">
        </div>
      </div>
      
      <div class="appointments-list">
        <div v-if="filteredAppointments.length === 0" class="empty-state private-card">
          <i class="fa-solid fa-calendar-xmark empty-icon"></i>
          <p v-if="activeTab === 'upcoming'">No tienes citas programadas</p>
          <p v-else>No hay citas pasadas</p>
          <button class="btn btn-primary">Agendar una cita</button>
        </div>
        
        <div v-else class="appointments-grid">
          <div v-for="(appointment, index) in filteredAppointments" :key="index" class="appointment-card private-card">
            <div class="appointment-date">
              <span class="date-month">{{ getMonthShort(appointment.date) }}</span>
              <span class="date-day">{{ getDayFromDate(appointment.date) }}</span>
              <span class="date-time">{{ appointment.time }}</span>
            </div>
            
            <div class="appointment-content">
              <h3 class="appointment-type">{{ appointment.type }}</h3>
              <p class="appointment-with">
                <i class="fa-solid fa-user-md"></i>
                {{ appointment.pharmacist }}
              </p>
              <p v-if="appointment.notes" class="appointment-notes">{{ appointment.notes }}</p>
              <div class="appointment-status">
                <span class="status-badge" :class="appointment.status">
                  {{ getStatusText(appointment.status) }}
                </span>
              </div>
            </div>
            
            <div class="appointment-actions">
              <div v-if="isUpcoming(appointment)">
                <button class="btn btn-outline btn-block">
                  <i class="fa-solid fa-pen-to-square"></i>
                  Modificar
                </button>
                <button class="btn btn-danger btn-block">
                  <i class="fa-solid fa-calendar-xmark"></i>
                  Cancelar
                </button>
              </div>
              <div v-else>
                <button class="btn btn-outline btn-block">
                  <i class="fa-solid fa-calendar-plus"></i>
                  Agendar similar
                </button>
                <button class="btn btn-primary btn-block">
                  <i class="fa-solid fa-star"></i>
                  Valorar
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      <div v-if="activeTab === 'upcoming' && showCalendarView" class="calendar-view private-card">
        <div class="calendar-header">
          <h3>Vista de calendario</h3>
          <div class="calendar-navigation">
            <button class="btn btn-text" @click="previousMonth">
              <i class="fa-solid fa-chevron-left"></i>
            </button>
            <span class="current-month">{{ currentMonthName }} {{ currentYear }}</span>
            <button class="btn btn-text" @click="nextMonth">
              <i class="fa-solid fa-chevron-right"></i>
            </button>
          </div>
        </div>
        
        <div class="calendar-grid">
          <div class="calendar-days">
            <div v-for="day in weekDays" :key="day" class="day-header">{{ day }}</div>
          </div>
          
          <div class="calendar-dates">
            <div 
              v-for="date in calendarDates" 
              :key="date.dateString"
              class="date-cell"
              :class="{
                'other-month': !date.currentMonth,
                'today': date.isToday,
                'has-appointments': hasAppointmentOnDate(date.dateString)
              }"
            >
              <span class="date-number">{{ date.day }}</span>
              <div v-if="hasAppointmentOnDate(date.dateString)" class="date-indicator">
                <span 
                  v-for="(apt, aptIndex) in getAppointmentsForDate(date.dateString)" 
                  :key="aptIndex" 
                  class="appointment-dot" 
                  :title="apt.type + ' - ' + apt.time"
                ></span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'Appointments',
    props: {
      appointments: {
        type: Array,
        default: () => []
      }
    },
    data() {
      return {
        activeTab: 'upcoming',
        filters: {
          type: 'all',
          search: ''
        },
        showCalendarView: true,
        currentDate: new Date(),
        currentMonth: new Date().getMonth(),
        currentYear: new Date().getFullYear(),
        weekDays: ['Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb', 'Dom'],
        monthNames: [
          'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
          'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
        ]
      };
    },
    computed: {
      filteredAppointments() {
        if (!this.appointments) return [];
        
        let result = [...this.appointments];
        
        // Filtrar por estado (tab activa)
        if (this.activeTab === 'upcoming') {
          result = result.filter(appointment => this.isUpcoming(appointment));
        } else {
          result = result.filter(appointment => !this.isUpcoming(appointment));
        }
        
        // Filtrar por tipo
        if (this.filters.type !== 'all') {
          result = result.filter(appointment => appointment.type === this.filters.type);
        }
        
        // Filtrar por búsqueda
        if (this.filters.search) {
          const searchTerm = this.filters.search.toLowerCase();
          result = result.filter(appointment => 
            appointment.type.toLowerCase().includes(searchTerm) ||
            appointment.pharmacist.toLowerCase().includes(searchTerm) ||
            (appointment.notes && appointment.notes.toLowerCase().includes(searchTerm))
          );
        }
        
        // Ordenar por fecha y hora
        result.sort((a, b) => {
          const dateA = new Date(`${a.date} ${a.time}`);
          const dateB = new Date(`${b.date} ${b.time}`);
          
          if (this.activeTab === 'upcoming') {
            return dateA - dateB; // Próximas: más cercanas primero
          } else {
            return dateB - dateA; // Pasadas: más recientes primero
          }
        });
        
        return result;
      },
      currentMonthName() {
        return this.monthNames[this.currentMonth];
      },
      calendarDates() {
        const dates = [];
        
        // Primer día del mes actual (0 = Domingo, 1 = Lunes, ..., 6 = Sábado)
        const firstDay = new Date(this.currentYear, this.currentMonth, 1).getDay();
        // Ajustar para que la semana comience en lunes (0 = Lunes, ..., 6 = Domingo)
        const firstDayAdjusted = firstDay === 0 ? 6 : firstDay - 1;
        
        // Último día del mes actual
        const lastDay = new Date(this.currentYear, this.currentMonth + 1, 0).getDate();
        
        // Último día del mes anterior
        const prevMonthLastDay = new Date(this.currentYear, this.currentMonth, 0).getDate();
        
        // Días del mes anterior para completar la primera semana
        for (let i = firstDayAdjusted - 1; i >= 0; i--) {
          const day = prevMonthLastDay - i;
          const month = this.currentMonth === 0 ? 11 : this.currentMonth - 1;
          const year = this.currentMonth === 0 ? this.currentYear - 1 : this.currentYear;
          
          dates.push({
            day,
            month,
            year,
            currentMonth: false,
            isToday: this.isToday(year, month, day),
            dateString: this.formatDateString(year, month, day)
          });
        }
        
        // Días del mes actual
        for (let i = 1; i <= lastDay; i++) {
          dates.push({
            day: i,
            month: this.currentMonth,
            year: this.currentYear,
            currentMonth: true,
            isToday: this.isToday(this.currentYear, this.currentMonth, i),
            dateString: this.formatDateString(this.currentYear, this.currentMonth, i)
          });
        }
        
        // Días del mes siguiente para completar la última semana
        const remainingDays = 42 - dates.length; // 6 semanas * 7 días = 42
        for (let i = 1; i <= remainingDays; i++) {
          const month = this.currentMonth === 11 ? 0 : this.currentMonth + 1;
          const year = this.currentMonth === 11 ? this.currentYear + 1 : this.currentYear;
          
          dates.push({
            day: i,
            month,
            year,
            currentMonth: false,
            isToday: this.isToday(year, month, i),
            dateString: this.formatDateString(year, month, i)
          });
        }
        
        return dates;
      }
    },
    methods: {
      isUpcoming(appointment) {
        const now = new Date();
        const appointmentDate = new Date(`${appointment.date} ${appointment.time}`);
        return appointmentDate >= now || appointment.status === 'programada';
      },
      getMonthShort(dateString) {
        const date = new Date(dateString);
        const monthIndex = date.getMonth();
        return this.monthNames[monthIndex].substring(0, 3).toUpperCase();
      },
      getDayFromDate(dateString) {
        const date = new Date(dateString);
        return date.getDate();
      },
      getStatusText(status) {
        const statusMap = {
          'programada': 'Programada',
          'completada': 'Completada',
          'cancelada': 'Cancelada',
          'reprogramada': 'Reprogramada'
        };
        return statusMap[status] || status;
      },
      previousMonth() {
        if (this.currentMonth === 0) {
          this.currentMonth = 11;
          this.currentYear--;
        } else {
          this.currentMonth--;
        }
      },
      nextMonth() {
        if (this.currentMonth === 11) {
          this.currentMonth = 0;
          this.currentYear++;
        } else {
          this.currentMonth++;
        }
      },
      isToday(year, month, day) {
        const today = new Date();
        return (
          today.getDate() === day &&
          today.getMonth() === month &&
          today.getFullYear() === year
        );
      },
      formatDateString(year, month, day) {
        // Formato: YYYY-MM-DD
        return `${year}-${String(month + 1).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
      },
      hasAppointmentOnDate(dateString) {
        return this.getAppointmentsForDate(dateString).length > 0;
      },
      getAppointmentsForDate(dateString) {
        if (!this.appointments) return [];
        
        return this.appointments.filter(appointment => {
          const aptDate = new Date(appointment.date);
          const calDate = new Date(dateString);
          
          return (
            aptDate.getDate() === calDate.getDate() &&
            aptDate.getMonth() === calDate.getMonth() &&
            aptDate.getFullYear() === calDate.getFullYear() &&
            this.isUpcoming(appointment)
          );
        });
      }
    }
  };
  </script>
  
  <style>
  @import '@/assets/styles/components/appointments.scss';
  </style>