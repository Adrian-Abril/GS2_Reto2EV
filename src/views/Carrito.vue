<template>
    <div class="carrito-container">
      <h1>Mi Carrito de Compras</h1>
      
      <div v-if="productos.length === 0" class="carrito-vacio">
        <p>Tu carrito está vacío</p>
        <button @click="volverATienda" class="btn-volver">Volver a la tienda</button>
      </div>
      
      <div v-else class="carrito-contenido">
        <div class="productos-lista">
          <div v-for="(producto, index) in productos" :key="index" class="producto-item">
            <div class="producto-imagen">
              <img :src="producto.imagen" :alt="producto.nombre">
            </div>
            <div class="producto-info">
              <h3>{{ producto.nombre }}</h3>
              <p class="producto-descripcion">{{ producto.descripcion }}</p>
              <p class="producto-precio">{{ formatoMoneda(producto.precio) }}</p>
            </div>
            <div class="producto-cantidad">
              <button @click="disminuirCantidad(index)" class="btn-cantidad">-</button>
              <span>{{ producto.cantidad }}</span>
              <button @click="aumentarCantidad(index)" class="btn-cantidad">+</button>
            </div>
            <div class="producto-subtotal">
              <p>{{ formatoMoneda(producto.precio * producto.cantidad) }}</p>
            </div>
            <div class="producto-eliminar">
              <button @click="eliminarProducto(index)" class="btn-eliminar">
                <span class="icono-eliminar">×</span>
              </button>
            </div>
          </div>
        </div>
        
        <div class="carrito-resumen">
          <div class="resumen-detalle">
            <div class="detalle-fila">
              <span>Subtotal:</span>
              <span>{{ formatoMoneda(calcularSubtotal) }}</span>
            </div>
            <div class="detalle-fila">
              <span>IVA (21%):</span>
              <span>{{ formatoMoneda(calcularIVA) }}</span>
            </div>
            <div class="detalle-fila total">
              <span>Total:</span>
              <span>{{ formatoMoneda(calcularTotal) }}</span>
            </div>
          </div>
          
          <div class="botones-accion">
            <button @click="volverATienda" class="btn-secundario">Seguir comprando</button>
            <button @click="procederAlPago" class="btn-primario">Proceder al pago</button>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'Carrito',
    data() {
      return {
        productos: [],
      }
    },
    computed: {
      calcularSubtotal() {
        return this.productos.reduce((total, item) => {
          return total + (item.precio * item.cantidad);
        }, 0);
      },
      calcularIVA() {
        return this.calcularSubtotal * 0.21;
      },
      calcularTotal() {
        return this.calcularSubtotal + this.calcularIVA;
      }
    },
    methods: {
      formatoMoneda(valor) {
        return new Intl.NumberFormat('es-ES', {
          style: 'currency',
          currency: 'EUR'
        }).format(valor);
      },
      aumentarCantidad(index) {
        this.productos[index].cantidad += 1;
        this.actualizarCarritoEnStorage();
      },
      disminuirCantidad(index) {
        if (this.productos[index].cantidad > 1) {
          this.productos[index].cantidad -= 1;
          this.actualizarCarritoEnStorage();
        }
      },
      eliminarProducto(index) {
        this.productos.splice(index, 1);
        this.actualizarCarritoEnStorage();
      },
      volverATienda() {
        this.$router.push('/tienda');
      },
      procederAlPago() {
        this.$router.push('/checkout');
      },
      cargarProductosDesdeStorage() {
        const carritoGuardado = localStorage.getItem('carrito');
        if (carritoGuardado) {
          this.productos = JSON.parse(carritoGuardado);
        }
      },
      actualizarCarritoEnStorage() {
        localStorage.setItem('carrito', JSON.stringify(this.productos));
      }
    },
    created() {
      this.cargarProductosDesdeStorage();
    }
  }
  </script>
  
  <style>
  @import '@/assets/styles/pages/carrito.scss';
  </style>