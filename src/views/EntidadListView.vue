<template>
    <div class="entidad-list">
      <h1>Gestión de Entidades</h1>
      <div class="container">
        <EntidadComponent />
      </div>
    </div>
  </template>
  
  <script>
  import EntidadComponent from '@/components/EntidadComponent.vue'
  
  export default {
    name: 'EntidadListView',
    components: {
      EntidadComponent
    }
  }
  </script>
  
  <style scoped>
  .entidad-list {
    padding: 20px;
    max-width: 1200px;
    margin: 0 auto;
  }
  
  h1 {
    margin-bottom: 30px;
    color: #2c3e50;
    text-align: center;
  }
  
  .container {
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    padding: 20px;
  }
  
  @media (max-width: 768px) {
    .entidad-list {
      padding: 10px;
    }
    
    .container {
      padding: 15px;
    }
  }
  </style>