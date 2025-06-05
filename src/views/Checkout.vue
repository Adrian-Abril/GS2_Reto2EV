<template>
    <div class="checkout-container">
      <h1>Finalizar Compra</h1>
      
      <div v-if="productos.length === 0" class="checkout-vacio">
        <p>No hay productos en tu carrito</p>
        <button @click="volverATienda" class="btn-volver">Volver a la tienda</button>
      </div>
      
      <div v-else class="checkout-contenido">
        <div class="checkout-pasos">
          <div class="paso" :class="{ activo: pasoActual === 1 }">
            <span class="paso-numero">1</span>
            <span class="paso-texto">Dirección de envío</span>
          </div>
          <div class="paso" :class="{ activo: pasoActual === 2 }">
            <span class="paso-numero">2</span>
            <span class="paso-texto">Método de pago</span>
          </div>
          <div class="paso" :class="{ activo: pasoActual === 3 }">
            <span class="paso-numero">3</span>
            <span class="paso-texto">Revisar pedido</span>
          </div>
        </div>
        
        <!-- Paso 1: Dirección de envío -->
        <div v-if="pasoActual === 1" class="formulario-paso">
          <h2>Dirección de envío</h2>
          <form @submit.prevent="avanzarPaso" class="formulario-direccion">
            <div class="campo-formulario">
              <label for="nombre">Nombre completo</label>
              <input 
                type="text" 
                id="nombre" 
                v-model="direccion.nombre" 
                required
                placeholder="Ej: Juan Pérez"
              >
            </div>
            
            <div class="campo-formulario">
              <label for="email">Email</label>
              <input 
                type="email" 
                id="email" 
                v-model="direccion.email" 
                required
                placeholder="Ej: juanperez@correo.com"
              >
            </div>
            
            <div class="campo-formulario">
              <label for="telefono">Teléfono</label>
              <input 
                type="tel" 
                id="telefono" 
                v-model="direccion.telefono" 
                required
                placeholder="Ej: 612345678"
              >
            </div>
            
            <div class="campo-formulario">
              <label for="calle">Dirección</label>
              <input 
                type="text" 
                id="calle" 
                v-model="direccion.calle" 
                required
                placeholder="Ej: Calle Principal 123"
              >
            </div>
            
            <div class="campo-formulario grupo">
              <div class="subcampo">
                <label for="codigoPostal">Código Postal</label>
                <input 
                  type="text" 
                  id="codigoPostal" 
                  v-model="direccion.codigoPostal" 
                  required
                  placeholder="Ej: 28001"
                >
              </div>
              
              <div class="subcampo">
                <label for="ciudad">Ciudad</label>
                <input 
                  type="text" 
                  id="ciudad" 
                  v-model="direccion.ciudad" 
                  required
                  placeholder="Ej: Madrid"
                >
              </div>
            </div>
            
            <div class="campo-formulario">
              <label for="provincia">Provincia</label>
              <select id="provincia" v-model="direccion.provincia" required>
                <option value="" disabled selected>Selecciona una provincia</option>
                <option v-for="provincia in provincias" :key="provincia" :value="provincia">
                  {{ provincia }}
                </option>
              </select>
            </div>
            
            <div class="campo-formulario checkbox">
              <input type="checkbox" id="guardarDireccion" v-model="guardarDireccion">
              <label for="guardarDireccion">Guardar esta dirección para futuras compras</label>
            </div>
            
            <div class="botones-navegacion">
              <button @click="volverATienda" type="button" class="btn-secundario">Volver a la tienda</button>
              <button type="submit" class="btn-primario">Continuar a método de pago</button>
            </div>
          </form>
        </div>
        
        <!-- Paso 2: Método de pago -->
        <div v-if="pasoActual === 2" class="formulario-paso">
          <h2>Método de pago</h2>
          <form @submit.prevent="avanzarPaso" class="formulario-pago">
            <div class="opciones-pago">
              <div class="opcion-pago" 
                   :class="{ seleccionado: metodoPago === 'tarjeta' }"
                   @click="metodoPago = 'tarjeta'">
                <div class="opcion-radio">
                  <input 
                    type="radio" 
                    id="tarjeta" 
                    name="metodoPago" 
                    value="tarjeta" 
                    v-model="metodoPago"
                  >
                  <label for="tarjeta">Tarjeta de crédito/débito</label>
                </div>
                <div class="iconos-tarjetas">
                  <span class="icono-tarjeta visa"></span>
                  <span class="icono-tarjeta mastercard"></span>
                  <span class="icono-tarjeta amex"></span>
                </div>
              </div>
              
              <div class="opcion-pago" 
                   :class="{ seleccionado: metodoPago === 'paypal' }"
                   @click="metodoPago = 'paypal'">
                <div class="opcion-radio">
                  <input 
                    type="radio" 
                    id="paypal" 
                    name="metodoPago" 
                    value="paypal" 
                    v-model="metodoPago"
                  >
                  <label for="paypal">PayPal</label>
                </div>
                <div class="iconos-tarjetas">
                  <span class="icono-tarjeta paypal"></span>
                </div>
              </div>
              
              <div class="opcion-pago" 
                   :class="{ seleccionado: metodoPago === 'transferencia' }"
                   @click="metodoPago = 'transferencia'">
                <div class="opcion-radio">
                  <input 
                    type="radio" 
                    id="transferencia" 
                    name="metodoPago" 
                    value="transferencia" 
                    v-model="metodoPago"
                  >
                  <label for="transferencia">Transferencia bancaria</label>
                </div>
              </div>
            </div>
            
            <!-- Campos para tarjeta de crédito -->
            <div v-if="metodoPago === 'tarjeta'" class="detalles-pago tarjeta">
              <div class="campo-formulario">
                <label for="nombreTarjeta">Nombre en la tarjeta</label>
                <input 
                  type="text" 
                  id="nombreTarjeta" 
                  v-model="pago.tarjeta.nombre" 
                  required
                  placeholder="Ej: JUAN PEREZ"
                >
              </div>
              
              <div class="campo-formulario">
                <label for="numeroTarjeta">Número de tarjeta</label>
                <input 
                  type="text" 
                  id="numeroTarjeta" 
                  v-model="pago.tarjeta.numero" 
                  required
                  placeholder="XXXX XXXX XXXX XXXX"
                  maxlength="19"
                  @input="formatearNumeroTarjeta"
                >
              </div>
              
              <div class="campo-formulario grupo">
                <div class="subcampo">
                  <label for="fechaExpiracion">Fecha de expiración</label>
                  <input 
                    type="text" 
                    id="fechaExpiracion" 
                    v-model="pago.tarjeta.fechaExpiracion" 
                    required
                    placeholder="MM/AA"
                    maxlength="5"
                    @input="formatearFechaExpiracion"
                  >
                </div>
                
                <div class="subcampo">
                  <label for="cvv">CVV</label>
                  <input 
                    type="text" 
                    id="cvv" 
                    v-model="pago.tarjeta.cvv" 
                    required
                    placeholder="123"
                    maxlength="4"
                  >
                </div>
              </div>
            </div>
            
            <!-- Campos para PayPal -->
            <div v-if="metodoPago === 'paypal'" class="detalles-pago paypal">
              <p class="instrucciones-paypal">
                Al hacer clic en "Continuar", serás redirigido a PayPal para completar tu compra de forma segura.
              </p>
            </div>
            
            <!-- Campos para transferencia bancaria -->
            <div v-if="metodoPago === 'transferencia'" class="detalles-pago transferencia">
              <div class="info-transferencia">
                <p>Por favor, realiza una transferencia con los siguientes datos:</p>
                <ul>
                  <li><strong>Banco:</strong> Banco Farmacia</li>
                  <li><strong>IBAN:</strong> ES12 3456 7890 1234 5678 9012</li>
                  <li><strong>BIC/SWIFT:</strong> FARMABCXXX</li>
                  <li><strong>Beneficiario:</strong> Farmacia Online</li>
                  <li><strong>Concepto:</strong> Pedido #{{ generarNumeroPedido() }}</li>
                </ul>
                <p>Una vez realizada la transferencia, enviaremos tu pedido.</p>
              </div>
            </div>
            
            <div class="botones-navegacion">
              <button @click="pasoActual = 1" type="button" class="btn-secundario">Volver a dirección</button>
              <button type="submit" class="btn-primario">Continuar a revisar pedido</button>
            </div>
          </form>
        </div>
        
        <!-- Paso 3: Revisar pedido -->
        <div v-if="pasoActual === 3" class="formulario-paso">
          <h2>Revisar pedido</h2>
          
          <div class="resumen-pedido">
            <div class="seccion-resumen">
              <h3>Productos</h3>
              <div class="productos-resumen">
                <div v-for="(producto, index) in productos" :key="index" class="producto-resumen">
                  <div class="producto-resumen-imagen">
                    <img :src="producto.imagen" :alt="producto.nombre">
                  </div>
                  <div class="producto-resumen-info">
                    <h4>{{ producto.nombre }}</h4>
                    <p class="producto-cantidad">Cantidad: {{ producto.cantidad }}</p>
                  </div>
                  <div class="producto-resumen-precio">
                    <p>{{ formatoMoneda(producto.precio * producto.cantidad) }}</p>
                  </div>
                </div>
              </div>
            </div>
            
            <div class="seccion-resumen">
              <h3>Dirección de envío</h3>
              <div class="direccion-resumen">
                <p><strong>{{ direccion.nombre }}</strong></p>
                <p>{{ direccion.calle }}</p>
                <p>{{ direccion.codigoPostal }}, {{ direccion.ciudad }}, {{ direccion.provincia }}</p>
                <p>Email: {{ direccion.email }}</p>
                <p>Teléfono: {{ direccion.telefono }}</p>
              </div>
            </div>
            
            <div class="seccion-resumen">
              <h3>Método de pago</h3>
              <div class="pago-resumen">
                <template v-if="metodoPago === 'tarjeta'">
                  <p><strong>Tarjeta de crédito/débito</strong></p>
                  <p>Terminada en {{ pago.tarjeta.numero.slice(-4) }}</p>
                </template>
                <template v-else-if="metodoPago === 'paypal'">
                  <p><strong>PayPal</strong></p>
                </template>
                <template v-else>
                  <p><strong>Transferencia bancaria</strong></p>
                </template>
              </div>
            </div>
            
            <div class="total-resumen">
              <div class="linea-total">
                <span>Subtotal:</span>
                <span>{{ formatoMoneda(calcularSubtotal) }}</span>
              </div>
              <div class="linea-total">
                <span>Gastos de envío:</span>
                <span>{{ calcularSubtotal > 50 ? 'Gratis' : formatoMoneda(gastosEnvio) }}</span>
              </div>
              <div class="linea-total">
                <span>IVA (4%):</span>
                <span>{{ formatoMoneda(calcularIVA) }}</span>
              </div>
              <div class="linea-total total-final">
                <span>Total:</span>
                <span>{{ formatoMoneda(calcularTotal) }}</span>
              </div>
            </div>
          </div>
          
          <div class="condiciones">
            <div class="campo-formulario checkbox">
              <input type="checkbox" id="aceptarCondiciones" v-model="aceptarCondiciones">
              <label for="aceptarCondiciones">Acepto los términos y condiciones de compra</label>
            </div>
            <div v-if="errorCondiciones" class="error-mensaje">
              Debes aceptar los términos y condiciones para continuar
            </div>
          </div>
          
          <div class="botones-navegacion">
            <button @click="pasoActual = 2" type="button" class="btn-secundario">Volver a método de pago</button>
            <button @click="finalizarCompra" :disabled="!aceptarCondiciones" class="btn-primario">Finalizar compra</button>
          </div>
        </div>
        
        <!-- Paso 4: Pedido confirmado -->
        <div v-if="pasoActual === 4" class="confirmacion-pedido">
          <div class="confirmacion-icono">
            <span class="icono-exito">✓</span>
          </div>
          <h2>¡Pedido realizado con éxito!</h2>
          <p class="numero-pedido">Pedido #{{ numeroPedido }}</p>
          <p class="mensaje-confirmacion">
            Hemos enviado un correo de confirmación a <strong>{{ direccion.email }}</strong> con los detalles de tu pedido.
          </p>
          <p class="mensaje-tiempo">
            Tu pedido será procesado en las próximas 24 horas. Recibirás actualizaciones sobre el estado de tu envío.
          </p>
          <div class="botones-confirmacion">
            <button @click="volverATienda" class="btn-primario">Volver a la tienda</button>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'Checkout',
    data() {
      return {
        productos: [],
        pasoActual: 1,
        numeroPedido: '',
        direccion: {
          nombre: '',
          email: '',
          telefono: '',
          calle: '',
          codigoPostal: '',
          ciudad: '',
          provincia: ''
        },
        provincias: [
          'Álava', 'Albacete', 'Alicante', 'Almería', 'Asturias', 'Ávila', 'Badajoz', 
          'Barcelona', 'Burgos', 'Cáceres', 'Cádiz', 'Cantabria', 'Castellón', 'Ciudad Real', 
          'Córdoba', 'Cuenca', 'Girona', 'Granada', 'Guadalajara', 'Guipúzcoa', 'Huelva', 
          'Huesca', 'Islas Baleares', 'Jaén', 'La Coruña', 'La Rioja', 'Las Palmas', 
          'León', 'Lérida', 'Lugo', 'Madrid', 'Málaga', 'Murcia', 'Navarra', 'Orense', 
          'Palencia', 'Pontevedra', 'Salamanca', 'Santa Cruz de Tenerife', 'Segovia', 
          'Sevilla', 'Soria', 'Tarragona', 'Teruel', 'Toledo', 'Valencia', 'Valladolid', 
          'Vizcaya', 'Zamora', 'Zaragoza'
        ],
        guardarDireccion: false,
        metodoPago: 'tarjeta',
        pago: {
          tarjeta: {
            nombre: '',
            numero: '',
            fechaExpiracion: '',
            cvv: ''
          }
        },
        gastosEnvio: 4.99,
        aceptarCondiciones: false,
        errorCondiciones: false
      }
    },
    computed: {
      calcularSubtotal() {
        return this.productos.reduce((total, item) => {
          return total + (item.precio * item.cantidad);
        }, 0);
      },
      calcularIVA() {
        return this.calcularSubtotal * 0.04; // IVA reducido del 4% para medicamentos
      },
      calcularTotal() {
        let total = this.calcularSubtotal + this.calcularIVA;
        
        // Añadir gastos de envío si el subtotal es menor a 50€
        if (this.calcularSubtotal < 50) {
          total += this.gastosEnvio;
        }
        
        return total;
      }
    },
    methods: {
      formatoMoneda(valor) {
        return new Intl.NumberFormat('es-ES', {
          style: 'currency',
          currency: 'EUR'
        }).format(valor);
      },
      volverATienda() {
        this.$router.push('/tienda');
      },
      avanzarPaso() {
        this.pasoActual += 1;
      },
      formatearNumeroTarjeta() {
        // Eliminar espacios y caracteres no numéricos
        let numero = this.pago.tarjeta.numero.replace(/\D/g, '');
        
        // Añadir espacios cada 4 dígitos
        let numeroFormateado = '';
        for (let i = 0; i < numero.length; i++) {
          if (i > 0 && i % 4 === 0) {
            numeroFormateado += ' ';
          }
          numeroFormateado += numero[i];
        }
        
        this.pago.tarjeta.numero = numeroFormateado;
      },
      formatearFechaExpiracion() {
        // Eliminar caracteres no numéricos
        let fecha = this.pago.tarjeta.fechaExpiracion.replace(/\D/g, '');
        
        // Formatear como MM/AA
        if (fecha.length > 2) {
          fecha = fecha.substring(0, 2) + '/' + fecha.substring(2, 4);
        }
        
        this.pago.tarjeta.fechaExpiracion = fecha;
      },
      generarNumeroPedido() {
        const caracteres = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
        let codigo = '';
        for (let i = 0; i < 8; i++) {
          codigo += caracteres.charAt(Math.floor(Math.random() * caracteres.length));
        }
        return codigo;
      },
      async finalizarCompra() {
        if (!this.aceptarCondiciones) {
          this.errorCondiciones = true;
          return;
        }
        
        this.errorCondiciones = false;
        this.numeroPedido = this.generarNumeroPedido();
        
         const payload = {
          productos: this.productos.map(p => ({
            medicamentoId: p.id,
            cantidad: p.cantidad,
            precio: p.precio
          }))
        };

        try{
          const response = await fetch('http://localhost:5000/api/compras/finalizar', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(payload)
          });

          if(!response.ok) throw new Error(`Error al realizar la compra: ${response.status} ${response.statusText}`)
          const data = await response.json();
          alert("Compra realizada con exito, ID de compra: " + data.id);
          this.productos = [];
          this.$router.push('/perfil');
        }catch(error){
          console.error("Error al registrar la compra", error);
          alert("Error al realizar la compra")
        }


        // Vaciar el carrito
        localStorage.removeItem('carrito');
        
        // Mostrar confirmación
        this.pasoActual = 4;
      },
      cargarProductosDesdeStorage() {
        const carritoGuardado = localStorage.getItem('carrito');
        if (carritoGuardado) {
          this.productos = JSON.parse(carritoGuardado);
        }
      }
    },
    created() {
      this.cargarProductosDesdeStorage();
      
      // Redirigir a la tienda si no hay productos
      if (this.productos.length === 0) {
        this.$router.push('/tienda');
      }
      
      // Cargar dirección guardada si existe
      const direccionGuardada = localStorage.getItem('direccion');
      if (direccionGuardada) {
        this.direccion = JSON.parse(direccionGuardada);
        this.guardarDireccion = true;
      }
    }
  }
  </script>
  
  <style>
  @import '@/assets/styles/pages/checkout.scss';
  </style>