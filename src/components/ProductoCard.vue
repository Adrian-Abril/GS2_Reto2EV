<template>
  <div class="producto-card">
    <div class="producto-imagen">
      <img :src="producto.imagen" :alt="producto.nombre">
    </div>
    <div class="producto-contenido">
      <h3 class="producto-titulo">{{ producto.nombre }}</h3>
      <p class="producto-descripcion">{{ producto.descripcion }}</p>
      <div class="producto-footer">
        <span class="producto-precio">{{ formatearPrecio(producto.precio) }}</span>
        <button class="btn-agregar" @click="agregarAlCarrito">
          <span class="icono-carrito">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <circle cx="9" cy="21" r="1"></circle>
              <circle cx="20" cy="21" r="1"></circle>
              <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6"></path>
            </svg>
          </span>
          Añadir
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ProductoCard',
  props: {
    producto: {
      type: Object,
      required: true
    }
  },
  methods: {
    formatearPrecio(precio) {
      return `${precio.toFixed(2)}€`;
    },
    agregarAlCarrito() {
      // Obtener el carrito actual del localStorage
      let carrito = [];
      const carritoGuardado = localStorage.getItem('carrito');
      
      if (carritoGuardado) {
        carrito = JSON.parse(carritoGuardado);
      }
      
      // Verificar si el producto ya está en el carrito
      const indiceProducto = carrito.findIndex(item => item.id === this.producto.id);
      
      if (indiceProducto >= 0) {
        // Si ya existe, incrementar cantidad
        carrito[indiceProducto].cantidad += 1;
      } else {
        // Si no existe, añadirlo con cantidad 1
        carrito.push({
          id: this.producto.id,
          nombre: this.producto.nombre,
          precio: this.producto.precio,
          imagen: this.producto.imagen,
          cantidad: 1
        });
      }
      
      // Guardar el carrito actualizado
      localStorage.setItem('carrito', JSON.stringify(carrito));
      
      // Emitir evento para actualizar contador en header y mostrar notificación
      this.$emit('producto-agregado', {
        mensaje: `${this.producto.nombre} agregado al carrito`,
        tipo: 'exito'
      });
      
      // Disparar evento personalizado para que el header se entere
      this.$root.$emit('cart-updated');
    }
  }
}
</script>

<style lang="scss">
.producto-card {
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  height: 100%;
  display: flex;
  flex-direction: column;
  
  &:hover {
    transform: translateY(-5px);
    box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
  }
  
  .producto-imagen {
    height: 200px;
    width: 100%;
    overflow: hidden;
    
    img {
      width: 100%;
      height: 100%;
      object-fit: cover;
      transition: transform 0.5s ease;
      
      &:hover {
        transform: scale(1.05);
      }
    }
  }
  
  .producto-contenido {
    padding: 1.5rem;
    display: flex;
    flex-direction: column;
    flex-grow: 1;
  }
  
  .producto-titulo {
    font-size: 1.2rem;
    font-weight: 600;
    margin: 0 0 0.5rem;
    color: #333;
  }
  
  .producto-descripcion {
    font-size: 0.9rem;
    color: #666;
    margin-bottom: 1.5rem;
    flex-grow: 1;
    display: -webkit-box;
    -webkit-line-clamp: 3;
    -webkit-box-orient: vertical;
    overflow: hidden;
  }
  
  .producto-footer {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-top: auto;
  }
  
  .producto-precio {
    font-size: 1.3rem;
    font-weight: 700;
    color: #2e7d32;
  }
  
  .btn-agregar {
    background-color: #2e7d32;
    color: white;
    border: none;
    padding: 0.6rem 1.2rem;
    border-radius: 4px;
    font-weight: 500;
    cursor: pointer;
    display: flex;
    align-items: center;
    gap: 0.5rem;
    transition: background-color 0.2s;
    
    &:hover {
      background-color: #1b5e20;
    }
    
    .icono-carrito {
      display: flex;
      align-items: center;
    }
  }
}
</style>