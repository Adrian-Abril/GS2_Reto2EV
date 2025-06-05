<template>
    <div class="tienda-container">
      <h1>Nuestra Tienda</h1>
      
      <div class="tienda-controles">
        <div class="buscador">
          <input 
            type="text" 
            v-model="busqueda" 
            placeholder="Buscar productos..." 
            class="input-busqueda"
          >
        </div>
        
        <div class="filtros">
          <select v-model="filtroCategoria" class="select-filtro">
            <option value="">Todas las categorías</option>
            <option v-for="categoria in categorias" :key="categoria" :value="categoria">
              {{ categoria }}
            </option>
          </select>
          
          <select v-model="ordenarPor" class="select-filtro">
            <option value="nombre-asc">Nombre (A-Z)</option>
            <option value="nombre-desc">Nombre (Z-A)</option>
            <option value="precio-asc">Precio (Menor a Mayor)</option>
            <option value="precio-desc">Precio (Mayor a Menor)</option>
          </select>
        </div>
      </div>
      
      <div v-if="productosFiltrados.length === 0" class="no-productos">
        <p>No se encontraron productos que coincidan con tu búsqueda.</p>
      </div>
      
      <div v-else class="productos-grid">
        <ProductoCard 
          v-for="producto in productosFiltrados" 
          :key="producto.id" 
          :producto="producto"
          @producto-agregado="mostrarNotificacion"
          @mostrar-notificacion="mostrarNotificacion"
        />
      </div>
      
      <div v-if="notificacion.visible" class="notificacion" :class="notificacion.tipo">
        <p>{{ notificacion.mensaje }}</p>
      </div>
    </div>
  </template>

<script>

import axios from 'axios';
import ProductoCard from '@/components/ProductoCard.vue';

export default {
  name: 'Tienda',
  components: {
    ProductoCard
  },
  data() {
    return {
      productos: [],
      busqueda: '',
      filtroCategoria: '',
      ordenarPor: '',
      notificacion: {
        visible: false,
        mensaje: '',
        tipo: 'exito'
      }
    };
  },
  computed: {
    categorias(){
      return [... new Set(this.productos.map(p => p.categoria))];
    },
    productosFiltrados(){
      let resultado = [...this.productos];
      if(this.busqueda){
        const termino = this.busqueda.toLowerCase();
        resultado = resultado.filter(p => 
          p.nombre.toLowerCase().includes(termino) || 
          p.descripcion.toLowerCase().includes(termino)
        );
      }

      if( this.filtroCategoria){
        resultado = resultado.filter(p => p.categoria === this.filtroCategoria);
      }

      switch(this.ordenarPor){
        case 'nombre-asc':
          resultado.sort((a, b) => a.nombre.localeCompare(b.nombre));
          break;
        case 'nombre-desc':
          resultado.sort((a, b) => b.nombre.localeCompare(a.nombre));
          break;
        case 'precio-asc':
          resultado.sort((a, b) => a.precio - b.precio);
          break;
        case 'precio-desc':
          resultado.sort((a, b) => b.precio - a.precio);
          break;
      }

      return resultado;
    }
  },
  methods: {
    mostrarNotificacion(data){
      this.notificacion = {
        visible: true,
        mensaje: data.mensaje || 'Producto agregado al carrito',
        tipo: data.tipo || 'exito'
      }
      setTimeout(() => {
        this.notificacion.visible = false;
      }, 3000);
    }
  },
  mounted(){
    axios.get("http://localhost:5000/api/medicamentos")
      .then(response => {
        this.productos = response.data;
      })
      .catch(error => {
        console.log(error);
      });
  }
  }
</script>

<style lang="scss">
@import '@/assets/styles/pages/tienda.scss';
</style>