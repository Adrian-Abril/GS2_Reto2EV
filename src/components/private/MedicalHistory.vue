<template>
    <div class="medical-history">
      <div class="history-header private-card">
        <div class="section-info">
          <h2>Mi Historial Médico</h2>
          <p>Consulta tu registro de atenciones médicas y análisis clínicos</p>
        </div>
        <div class="section-actions">
          <button class="btn btn-primary">
            <i class="fa-solid fa-plus"></i>
            <span>Añadir registro</span>
          </button>
        </div>
      </div>
      
      <div class="history-filters">
        <div class="filter-group">
          <label>Tipo:</label>
          <select v-model="filters.type" class="filter-select">
            <option value="all">Todos</option>
            <option value="consultation">Consultas</option>
            <option value="test">Análisis clínicos</option>
            <option value="medication">Medicamentos</option>
            <option value="vaccine">Vacunas</option>
          </select>
        </div>
        
        <div class="filter-group">
          <label>Período:</label>
          <select v-model="filters.period" class="filter-select">
            <option value="all">Todo</option>
            <option value="last-month">Último mes</option>
            <option value="last-3-months">Últimos 3 meses</option>
            <option value="last-6-months">Últimos 6 meses</option>
            <option value="last-year">Último año</option>
          </select>
        </div>
        
        <div class="search-box">
          <i class="fa-solid fa-search search-icon"></i>
          <input type="text" v-model="filters.search" placeholder="Buscar en historial..." class="search-input">
        </div>
      </div>
      
      <div class="history-timeline">
        <div v-if="filteredHistory.length === 0" class="empty-state private-card">
          <i class="fa-solid fa-clipboard-list empty-icon"></i>
          <p>No se encontraron registros que coincidan con los filtros</p>
          <button class="btn btn-primary" @click="resetFilters">Restablecer filtros</button>
        </div>
        
        <div v-else>
          <div v-for="(yearGroup, year) in groupedHistory" :key="year" class="year-group">
            <div class="year-header">
              <h3>{{ year }}</h3>
              <div class="year-line"></div>
            </div>
            
            <div v-for="(monthGroup, month) in yearGroup" :key="month" class="month-group">
              <div class="month-header">
                <h4>{{ getMonthName(month) }}</h4>
              </div>
              
              <div class="history-items">
                <div v-for="(item, index) in monthGroup" :key="index" class="history-item private-card">
                  <div class="item-date">
                    <div class="date-circle">
                      {{ getDayFromDate(item.date) }}
                    </div>
                  </div>
                  
                  <div class="item-content">
                    <div class="item-header">
                      <div class="item-type-badge" :class="item.type">
                        <i :class="getIconForType(item.type)"></i>
                        <span>{{ getTypeText(item.type) }}</span>
                      </div>
                      <div class="item-actions">
                        <button class="btn btn-text">
                          <i class="fa-solid fa-download"></i>
                        </button>
                        <button class="btn btn-text">
                          <i class="fa-solid fa-ellipsis-vertical"></i>
                        </button>
                      </div>
                    </div>
                    
                    <h3 class="item-provider">{{ item.provider }}</h3>
                    <p class="item-notes">{{ item.notes }}</p>
                    
                    <div v-if="item.results" class="item-results">
                      <h4>Resultados:</h4>
                      <ul class="results-list">
                        <li v-for="(result, resultIndex) in item.results" :key="resultIndex">
                          <span class="result-name">{{ result.name }}:</span>
                          <span class="result-value" :class="{ 'value-alert': result.status === 'alert' }">
                            {{ result.value }} {{ result.unit }}
                          </span>
                          <span v-if="result.reference" class="result-reference">
                            (Ref: {{ result.reference }})
                          </span>
                        </li>
                      </ul>
                    </div>
                    
                    <div v-if="item.medications" class="item-medications">
                      <h4>Medicamentos recetados:</h4>
                      <ul class="medications-list">
                        <li v-for="(med, medIndex) in item.medications" :key="medIndex">
                          {{ med.name }} - {{ med.dosage }}
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'MedicalHistory',
    props: {
      history: {
        type: Array,
        default: () => []
      }
    },
    data() {
      return {
        filters: {
          type: 'all',
          period: 'all',
          search: ''
        },
        monthNames: [
          'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
          'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
        ]
      };
    },
    computed: {
      filteredHistory() {
        if (!this.history) return [];
        
        let result = [...this.history];
        
        // Filtrar por tipo
        if (this.filters.type !== 'all') {
          result = result.filter(item => item.type === this.filters.type);
        }
        
        // Filtrar por período
        if (this.filters.period !== 'all') {
          const now = new Date();
          const monthsAgo = {
            'last-month': 1,
            'last-3-months': 3,
            'last-6-months': 6,
            'last-year': 12
          }[this.filters.period];
          
          if (monthsAgo) {
            const cutoffDate = new Date();
            cutoffDate.setMonth(now.getMonth() - monthsAgo);
            
            result = result.filter(item => {
              const itemDate = new Date(item.date);
              return itemDate >= cutoffDate;
            });
          }
        }
        
        // Filtrar por búsqueda
        if (this.filters.search) {
          const searchTerm = this.filters.search.toLowerCase();
          result = result.filter(item => 
            (item.provider && item.provider.toLowerCase().includes(searchTerm)) ||
            (item.notes && item.notes.toLowerCase().includes(searchTerm)) ||
            (item.results && item.results.some(r => 
              r.name.toLowerCase().includes(searchTerm) || 
              r.value.toString().includes(searchTerm)
            )) ||
            (item.medications && item.medications.some(m => 
              m.name.toLowerCase().includes(searchTerm) || 
              m.dosage.toLowerCase().includes(searchTerm)
            ))
          );
        }
        
        // Ordenar por fecha (más reciente primero)
        result.sort((a, b) => {
          return new Date(b.date) - new Date(a.date);
        });
        
        return result;
      },
      groupedHistory() {
        const groupedByYear = {};
        
        this.filteredHistory.forEach(item => {
          const date = new Date(item.date);
          const year = date.getFullYear();
          const month = date.getMonth();
          
          if (!groupedByYear[year]) {
            groupedByYear[year] = {};
          }
          
          if (!groupedByYear[year][month]) {
            groupedByYear[year][month] = [];
          }
          
          groupedByYear[year][month].push(item);
        });
        
        // Ordenar años y meses de forma descendente (más reciente primero)
        const sortedByYear = {};
        Object.keys(groupedByYear)
          .sort((a, b) => b - a)
          .forEach(year => {
            sortedByYear[year] = {};
            
            Object.keys(groupedByYear[year])
              .sort((a, b) => b - a)
              .forEach(month => {
                sortedByYear[year][month] = groupedByYear[year][month];
              });
          });
        
        return sortedByYear;
      }
    },
    methods: {
      getMonthName(monthIndex) {
        return this.monthNames[monthIndex];
      },
      getDayFromDate(dateString) {
        const date = new Date(dateString);
        return date.getDate();
      },
      getTypeText(type) {
        const typeMap = {
          'consultation': 'Consulta médica',
          'test': 'Análisis clínico',
          'medication': 'Medicamento',
          'vaccine': 'Vacuna'
        };
        return typeMap[type] || type;
      },
      getIconForType(type) {
        const iconMap = {
          'consultation': 'fa-solid fa-stethoscope',
          'test': 'fa-solid fa-vial',
          'medication': 'fa-solid fa-pills',
          'vaccine': 'fa-solid fa-syringe'
        };
        return iconMap[type] || 'fa-solid fa-circle-info';
      },
      resetFilters() {
        this.filters = {
          type: 'all',
          period: 'all',
          search: ''
        };
      }
    }
  };
  </script>
  
  <style>
  @import '@/assets/styles/components/medical-history.scss';
  </style>