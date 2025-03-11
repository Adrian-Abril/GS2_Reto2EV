<template>
    <div class="my-orders">
      <div class="orders-header private-card">
        <div class="section-info">
          <h2>Mis Pedidos</h2>
          <p>Historial de compras y seguimiento de pedidos</p>
        </div>
        <div class="section-actions">
          <button class="btn btn-primary">
            <i class="fa-solid fa-cart-shopping"></i>
            <span>Realizar nuevo pedido</span>
          </button>
        </div>
      </div>
      
      <div class="orders-filters">
        <div class="filter-group">
          <label>Estado:</label>
          <select v-model="filters.status" class="filter-select">
            <option value="all">Todos</option>
            <option value="en-proceso">En proceso</option>
            <option value="enviado">Enviado</option>
            <option value="entregado">Entregado</option>
            <option value="cancelado">Cancelado</option>
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
          <input type="text" v-model="filters.search" placeholder="Buscar pedidos..." class="search-input">
        </div>
      </div>
      
      <div class="orders-list">
        <div v-if="filteredOrders.length === 0" class="empty-state private-card">
          <i class="fa-solid fa-shopping-cart empty-icon"></i>
          <p>No se encontraron pedidos que coincidan con los filtros</p>
          <button class="btn btn-primary" @click="resetFilters">Restablecer filtros</button>
        </div>
        
        <div v-for="(order, index) in filteredOrders" :key="index" class="order-card private-card">
          <div class="order-header">
            <div class="order-info">
              <h3>Pedido #{{ order.id }}</h3>
              <div class="order-meta">
                <span class="order-date">
                  <i class="fa-solid fa-calendar"></i>
                  {{ formatDate(order.date) }}
                </span>
                <span class="status-badge" :class="order.status">
                  {{ getStatusText(order.status) }}
                </span>
              </div>
            </div>
            <div class="order-amount">
              <span class="amount-label">Total:</span>
              <span class="amount-value">{{ formatPrice(order.amount) }}</span>
            </div>
          </div>
          
          <div class="order-content">
            <div class="order-items">
              <h4>Productos</h4>
              <ul class="items-list">
                <li v-for="(item, itemIndex) in order.items" :key="itemIndex" class="item">
                  <div class="item-name">{{ item.name }}</div>
                  <div class="item-details">
                    <span class="item-quantity">{{ item.quantity }} x</span>
                    <span class="item-price">{{ formatPrice(item.price) }}</span>
                  </div>
                </li>
              </ul>
            </div>
            
            <div class="order-actions">
              <h4>Acciones</h4>
              <div class="action-buttons">
                <button class="btn btn-outline btn-block">
                  <i class="fa-solid fa-receipt"></i>
                  Ver factura
                </button>
                <button v-if="order.status === 'entregado'" class="btn btn-outline btn-block">
                  <i class="fa-solid fa-rotate"></i>
                  Repetir pedido
                </button>
                <button v-if="order.status === 'en-proceso' || order.status === 'enviado'" class="btn btn-outline btn-block">
                  <i class="fa-solid fa-truck-fast"></i>
                  Seguimiento
                </button>
                <button v-if="order.status === 'en-proceso'" class="btn btn-danger btn-block">
                  <i class="fa-solid fa-ban"></i>
                  Cancelar
                </button>
              </div>
            </div>
          </div>
          
          <div class="order-footer" v-if="order.tracking">
            <div class="tracking-info">
              <i class="fa-solid fa-truck"></i>
              <span>Nº de seguimiento: <strong>{{ order.tracking }}</strong></span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'MyOrders',
    props: {
      orders: {
        type: Array,
        default: () => []
      }
    },
    data() {
      return {
        filters: {
          status: 'all',
          period: 'all',
          search: ''
        }
      };
    },
    computed: {
      filteredOrders() {
        if (!this.orders) return [];
        
        let result = [...this.orders];
        
        // Filtrar por estado
        if (this.filters.status !== 'all') {
          result = result.filter(order => order.status === this.filters.status);
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
            
            result = result.filter(order => {
              const orderDate = new Date(this.parseOrderDate(order.date));
              return orderDate >= cutoffDate;
            });
          }
        }
        
        // Filtrar por búsqueda
        if (this.filters.search) {
          const searchTerm = this.filters.search.toLowerCase();
          result = result.filter(order => 
            order.id.toLowerCase().includes(searchTerm) ||
            (order.items && order.items.some(item => 
              item.name.toLowerCase().includes(searchTerm)
            ))
          );
        }
        
        // Ordenar por fecha (más reciente primero)
        result.sort((a, b) => {
          const dateA = new Date(this.parseOrderDate(a.date));
          const dateB = new Date(this.parseOrderDate(b.date));
          return dateB - dateA;
        });
        
        return result;
      }
    },
    methods: {
      parseOrderDate(dateStr) {
        // Convertir formato DD/MM/YYYY a YYYY-MM-DD para ordenación
        const parts = dateStr.split('/');
        return `${parts[2]}-${parts[1]}-${parts[0]}`;
      },
      formatDate(dateStr) {
        // Mantenemos el formato DD/MM/YYYY tal como está
        return dateStr;
      },
      formatPrice(price) {
        return `${price.toFixed(2)}€`;
      },
      getStatusText(status) {
        const statusMap = {
          'en-proceso': 'En proceso',
          'enviado': 'Enviado',
          'entregado': 'Entregado',
          'cancelado': 'Cancelado'
        };
        return statusMap[status] || status;
      },
      resetFilters() {
        this.filters = {
          status: 'all',
          period: 'all',
          search: ''
        };
      }
    }
  };
  </script>
  
  <style>
  @import '@/assets/styles/components/my-orders.scss';
  </style>