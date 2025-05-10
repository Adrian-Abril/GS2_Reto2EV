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
          imagen: 'https://static.vecteezy.com/system/resources/previews/036/094/758/non_2x/ai-generated-plastic-supplement-multivitamin-bottles-on-transparent-background-free-png.png',
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