<template>
  <div class="home-container">
    <!-- Hero Banner -->
    <HeroBanner />
    
    <!-- Servicios Destacados -->
    <section class="servicios-section">
      <div class="container">
        <h2 class="section-title">Nuestros Servicios</h2>
        <div class="servicios-grid">
          <ServicioCard 
            v-for="servicio in servicios" 
            :key="servicio.id" 
            :servicio="servicio" 
          />
        </div>
      </div>
    </section>
    
    <!-- Categorías Populares -->
    <section class="categorias-section">
      <div class="container">
        <h2 class="section-title">Categorías Populares</h2>
        <div class="categorias-slider">
          <CategoriaCard 
            v-for="categoria in categorias" 
            :key="categoria.id" 
            :categoria="categoria" 
          />
        </div>
      </div>
    </section>
    
    <!-- Productos Destacados -->
    <section class="productos-destacados-section">
      <div class="container">
        <div class="section-header">
          <h2 class="section-title">Productos Destacados</h2>
          <router-link to="/tienda" class="ver-todos">Ver todos los productos</router-link>
        </div>
        <div class="productos-carousel">
          <ProductoCard 
            v-for="producto in productosDestacados" 
            :key="producto.id" 
            :producto="producto"
            @producto-agregado="mostrarNotificacion"
          />
        </div>
      </div>
    </section>
    
    <!-- Promociones -->
    <section class="promociones-section">
      <div class="container">
        <div class="promociones-grid">
          <div class="promocion-card promocion-grande">
            <div class="promocion-content">
              <h3>Ofertas de Primavera</h3>
              <p>Descuentos de hasta el 30% en productos seleccionados</p>
              <router-link to="/promociones" class="btn-promocion">Ver ofertas</router-link>
            </div>
            <div class="promocion-imagen">
              <img src="https://img.freepik.com/free-photo/pharmacist-working-pharmacy-shop-drugstore_1303-25662.jpg" alt="Promoción especial">
            </div>
          </div>
          <div class="promocion-card">
            <div class="promocion-content">
              <h3>Envío Gratis</h3>
              <p>En pedidos superiores a 30€</p>
            </div>
          </div>
          <div class="promocion-card">
            <div class="promocion-content">
              <h3>Recogida en Tienda</h3>
              <p>¡Listo en solo 30 minutos!</p>
            </div>
          </div>
        </div>
      </div>
    </section>
    
    <!-- Blog y Consejos -->
    <section class="blog-section">
      <div class="container">
        <h2 class="section-title">Blog de Salud y Bienestar</h2>
        <div class="blog-grid">
          <ArticuloCard 
            v-for="articulo in articulos" 
            :key="articulo.id" 
            :articulo="articulo" 
          />
        </div>
      </div>
    </section>
    
    <!-- Testimonios -->
    <section class="testimonios-section">
      <div class="container">
        <h2 class="section-title">Lo que dicen nuestros clientes</h2>
        <div class="testimonios-carousel">
          <TestimonioCard 
            v-for="testimonio in testimonios" 
            :key="testimonio.id" 
            :testimonio="testimonio" 
          />
        </div>
      </div>
    </section>
    
    <!-- Suscripción al Newsletter -->
    <section class="newsletter-section">
      <div class="container">
        <div class="newsletter-container">
          <div class="newsletter-content">
            <h2>Mantente Informado</h2>
            <p>Suscríbete a nuestro boletín para recibir consejos de salud, ofertas especiales y novedades.</p>
          </div>
          <form class="newsletter-form" @submit.prevent="suscribirNewsletter">
            <input 
              type="email" 
              v-model="emailNewsletter" 
              placeholder="Tu correo electrónico" 
              required
              class="newsletter-input"
            >
            <button type="submit" class="newsletter-button">Suscribirse</button>
          </form>
        </div>
      </div>
    </section>
    
    <!-- Notificación -->
    <div v-if="notificacion.visible" class="notificacion" :class="notificacion.tipo">
      <p>{{ notificacion.mensaje }}</p>
    </div>
  </div>
</template>

<script>
import HeroBanner from '@/components/HeroBanner.vue';
import ServicioCard from '@/components/ServicioCard.vue';
import CategoriaCard from '@/components/CategoriaCard.vue';
import ProductoCard from '@/components/ProductoCard.vue';
import ArticuloCard from '@/components/ArticuloCard.vue';
import TestimonioCard from '@/components/TestimonioCard.vue';

export default {
  name: 'Home',
  components: {
    HeroBanner,
    ServicioCard,
    CategoriaCard,
    ProductoCard,
    ArticuloCard,
    TestimonioCard
  },
  data() {
    return {
      servicios: [
        {
          id: 1,
          titulo: 'Dispensación de Medicamentos',
          descripcion: 'Entrega de medicamentos prescritos con consejos profesionales para su correcto uso.',
          icono: 'pill'
        },
        {
          id: 2,
          titulo: 'Consejo Farmacéutico',
          descripcion: 'Nuestros farmacéuticos te aconsejan sobre el uso de medicamentos y problemas de salud menores.',
          icono: 'chat'
        },
        {
          id: 3,
          titulo: 'Control de Presión Arterial',
          descripcion: 'Medición y seguimiento de la presión arterial con análisis de resultados por profesionales.',
          icono: 'heart'
        },
        {
          id: 4,
          titulo: 'Análisis de Piel',
          descripcion: 'Evaluación personalizada del tipo de piel y recomendación de productos específicos.',
          icono: 'skin'
        }
      ],
      categorias: [
        {
          id: 1,
          nombre: 'Medicamentos',
          imagen: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSWbFX6gnr9tZXJZLqNtcm86EeC1YdY8RodXxUMIQfCqwA68Ck5EXU_xPj3pBLP8Arv8Hk&usqp=CAU',
          enlace: '/tienda?categoria=Medicamentos'
        },
        {
          id: 2,
          nombre: 'Dermocosmética',
          imagen: 'https://www.zschimmer-schwarz.es/app/uploads/2019/11/Dermocosm%C3%A9tica.jpg',
          enlace: '/tienda?categoria=Dermocosmética'
        },
        {
          id: 3,
          nombre: 'Vitaminas y Suplementos',
          imagen: 'data:image/jpeg;base64,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',
          enlace: '/tienda?categoria=Vitaminas%20y%20Suplementos'
        },
        {
          id: 4,
          nombre: 'Higiene Bucal',
          imagen: 'https://www.clinicadentalsedi.es/wp-content/uploads/2019/10/consejos-para-una-higiene-dental-de-hierro-1.jpg',
          enlace: '/tienda?categoria=Higiene%20Bucal'
        },
        {
          id: 5,
          nombre: 'Primeros Auxilios',
          imagen: 'https://www.poligonosindustrialesasturias.com/udecontrol_datos/objetos/2432.jpg',
          enlace: '/tienda?categoria=Primeros%20Auxilios'
        }
      ],
      productosDestacados: [
       {
            id: 5,
            nombre: 'Vitamina C 1000mg',
            descripcion: 'Suplemento alimenticio para reforzar el sistema inmunológico y prevenir resfriados.',
            precio: 9.95,
            categoria: 'Vitaminas y Suplementos',
            imagen: 'https://m.media-amazon.com/images/I/81JrTCGGNSL.jpg'
        },
        {
            id: 11,
            nombre: 'Protector Solar SPF 50+',
            descripcion: 'Protección solar de amplio espectro contra rayos UVA y UVB, resistente al agua.',
            precio: 21.99,
            categoria: 'Dermocosmética',
            imagen: 'https://media-pierre-fabre.wedia-group.com/api/wedia/dam/transform/u5wa3z31qn5se8zaxwqp7kemidz43diywws4kqe/pf_square/u5wa3z31qn5se8zaxwqp7kemidz43diywws4kqe?t=resize&width=800&height=800'
        },
        {
            id: 16,
            nombre: 'Probióticos Digestivos',
            descripcion: 'Suplemento con bacterias beneficiosas para mejorar la salud digestiva e intestinal.',
            precio: 19.95,
            categoria: 'Vitaminas y Suplementos',
            imagen: 'https://www.nutergia.es/wp-content/uploads/2023/05/ERGYPHILUS-Conf-600x800.png'
        },
        {
            id: 25,
            nombre: 'Crema Muscular Árnica',
            descripcion: 'Crema antiinflamatoria natural para dolores musculares y articulares.',
            precio: 14.75,
            categoria: 'Medicina Natural',
            imagen: 'https://m.media-amazon.com/images/I/61a3aV5-aiL._AC_UF894,1000_QL80_.jpg'
        },
      ],
      articulos: [
        {
          id: 1,
          titulo: 'Cómo fortalecer el sistema inmunológico de forma natural',
          extracto: 'Descubre alimentos y hábitos que pueden ayudarte a mejorar tus defensas y prevenir enfermedades comunes.',
          fecha: '12 Marzo 2025',
          imagen: 'https://www.clinicasanmiguel.es/wp-content/uploads/2024/12/sistemainmune.jpg',
          enlace: '/blog/fortalecer-sistema-inmunologico'
        },
        {
          id: 2,
          titulo: 'Consejos para cuidar la piel en primavera',
          extracto: 'La primavera trae cambios que afectan a nuestra piel. Te explicamos cómo adaptarte a la nueva estación.',
          fecha: '5 Marzo 2025',
          imagen: 'https://www.seasonsmexico.com/cdn/shop/articles/Banner_4b02153f-f2fc-402b-9018-ecd55ee4fbda.jpg?v=1647372636',
          enlace: '/blog/cuidado-piel-primavera'
        },
        {
          id: 3,
          titulo: 'Todo lo que debes saber sobre las alergias estacionales',
          extracto: 'Síntomas, prevención y tratamientos para sobrellevar la temporada de alergias con la mayor comodidad.',
          fecha: '28 Febrero 2025',
          imagen: 'https://www.misistemainmune.es/wp-content/uploads/2021/03/Alergias-respiratorias-scaled.jpeg',
          enlace: '/blog/alergias-estacionales'
        }
      ],
      testimonios: [
        {
          id: 1,
          nombre: 'María García',
          texto: 'Excelente atención al cliente. El farmacéutico me dio consejos muy útiles para tratar mi alergia. ¡Muy profesionales!',
          puntuacion: 5
        },
        {
          id: 2,
          nombre: 'Carlos Martínez',
          texto: 'Pedí mis medicamentos a través de la web y la entrega fue rápida y puntual. El servicio online funciona perfectamente.',
          puntuacion: 4
        },
        {
          id: 3,
          nombre: 'Laura Sánchez',
          texto: 'Me encanta el surtido de productos naturales y ecológicos. Además, siempre tienen ofertas interesantes.',
          puntuacion: 5
        }
      ],
      emailNewsletter: '',
      notificacion: {
        visible: false,
        mensaje: '',
        tipo: 'exito'
      }
    };
  },
  methods: {
    suscribirNewsletter() {
      // Aquí iría la lógica para guardar el email en una base de datos
      this.mostrarNotificacion({
        mensaje: `¡Gracias por suscribirte a nuestro newsletter!`,
        tipo: 'exito'
      });
      this.emailNewsletter = '';
    },
    mostrarNotificacion(data) {
      this.notificacion = {
        visible: true,
        mensaje: data.mensaje || 'Acción completada con éxito',
        tipo: data.tipo || 'exito'
      };
      
      // Ocultar la notificación después de 3 segundos
      setTimeout(() => {
        this.notificacion.visible = false;
      }, 3000);
    }
  }
}
</script>

<style>
@import '@/assets/styles/pages/home.scss';
</style>