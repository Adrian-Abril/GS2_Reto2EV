<template>
    <div class="prescriptions">
      <div class="prescriptions-header private-card">
        <div class="section-info">
          <h2>Mis Recetas</h2>
          <p>Gestiona tus recetas médicas electrónicas</p>
        </div>
        <div class="section-actions">
          <button class="btn btn-primary">
            <i class="fa-solid fa-plus"></i>
            <span>Subir nueva receta</span>
          </button>
        </div>
      </div>
      
      <div class="prescriptions-filters">
        <div class="filter-group">
          <label>Estado:</label>
          <select v-model="filters.status" class="filter-select">
            <option value="all">Todos</option>
            <option value="active">Activas</option>
            <option value="expired">Caducadas</option>
            <option value="completed">Completadas</option>
          </select>
        </div>
        
        <div class="filter-group">
          <label>Ordenar por:</label>
          <select v-model="filters.sort" class="filter-select">
            <option value="date">Fecha (reciente primero)</option>
            <option value="name">Nombre del medicamento</option>
            <option value="expiry">Fecha de caducidad</option>
          </select>
        </div>
        
        <div class="search-box">
          <i class="fa-solid fa-search search-icon"></i>
          <input type="text" v-model="filters.search" placeholder="Buscar recetas..." class="search-input">
        </div>
      </div>
      
      <div class="prescriptions-list">
        <div v-if="filteredPrescriptions.length === 0" class="empty-state private-card">
          <i class="fa-solid fa-clipboard-list empty-icon"></i>
          <p>No se encontraron recetas que coincidan con los filtros</p>
          <button class="btn btn-primary" @click="resetFilters">Restablecer filtros</button>
        </div>
        
        <div v-for="(prescription, index) in filteredPrescriptions" :key="index" class="prescription-card private-card">
          <div class="prescription-header">
            <div class="prescription-info">
              <span class="prescription-id">{{ prescription.id }}</span>
              <span class="status-badge" :class="prescription.status">
                {{ getStatusText(prescription.status) }}
              </span>
            </div>
            <div class="prescription-actions">
              <button class="btn btn-text">
                <i class="fa-solid fa-download"></i>
              </button>
              <button class="btn btn-text">
                <i class="fa-solid fa-ellipsis-vertical"></i>
              </button>
            </div>
          </div>
          
          <div class="prescription-content">
            <div class="medication-details">
              <h3>{{ prescription.medication }}</h3>
              <p class="prescription-doctor">Recetado por: {{ prescription.doctor }}</p>
              <div class="prescription-dates">
                <span class="date-item">
                  <i class="fa-solid fa-calendar-plus"></i>
                  <span>Fecha de emisión: {{ formatDate(prescription.date) }}</span>
                </span>
                <span class="date-item">
                  <i class="fa-solid fa-calendar-xmark"></i>
                  <span>Fecha de caducidad: {{ formatDate(prescription.expiryDate) }}</span>
                </span>
              </div>
              <div class="prescription-instructions">
                <h4>Instrucciones:</h4>
                <p>{{ prescription.instructions }}</p>
              </div>
            </div>
            
            <div class="prescription-actions-box">
              <div class="refills-info">
                <span class="refills-count">{{ prescription.refillsLeft }}</span>
                <span class="refills-label">Recambios<br>restantes</span>
              </div>
              <button
                v-if="prescription.status === 'active' && prescription.refillsLeft > 0"
                class="btn btn-primary btn-block"
              >
                Solicitar recambio
              </button>
              <button
                v-else-if="prescription.status === 'expired'"
                class="btn btn-outline btn-block"
              >
                Renovar receta
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'Prescriptions',
    props: {
      prescriptions: {
        type: Array,
        default: () => []
      }
    },
    data() {
      return {
        filters: {
          status: 'all',
          sort: 'date',
          search: ''
        }
      };
    },
    computed: {
      filteredPrescriptions() {
        if (!this.prescriptions) return [];
        
        let result = [...this.prescriptions];
        
        // Filtrar por estado
        if (this.filters.status !== 'all') {
          result = result.filter(prescription => prescription.status === this.filters.status);
        }
        
        // Filtrar por búsqueda
        if (this.filters.search) {
          const searchTerm = this.filters.search.toLowerCase();
          result = result.filter(prescription => 
            prescription.medication.toLowerCase().includes(searchTerm) ||
            prescription.id.toLowerCase().includes(searchTerm) ||
            prescription.doctor.toLowerCase().includes(searchTerm)
          );
        }
        
        // Ordenar
        result.sort((a, b) => {
          switch(this.filters.sort) {
            case 'name':
              return a.medication.localeCompare(b.medication);
            case 'expiry':
              return new Date(a.expiryDate) - new Date(b.expiryDate);
            case 'date':
            default:
              return new Date(b.date) - new Date(a.date);
          }
        });
        
        return result;
      }
    },
    methods: {
      formatDate(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString('es-ES', {
          day: '2-digit',
          month: '2-digit',
          year: 'numeric'
        });
      },
      getStatusText(status) {
        const statusMap = {
          'active': 'Activa',
          'expired': 'Caducada',
          'completed': 'Completada'
        };
        return statusMap[status] || status;
      },
      resetFilters() {
        this.filters = {
          status: 'all',
          sort: 'date',
          search: ''
        };
      }
    }
  };
  </script>
  
  <style>
  @import '@/assets/styles/components/prescriptions.scss';
  </style>