<template>
  <section class="promo-banner">
    <div class="container">
      <h1>Tu farmacia online de confianza</h1>
      <p>Descubre los mejores productos de salud y bienestar con entrega rápida y segura.</p>
      <button class="cta-button">Explorar Tienda</button>
    </div>
  </section>

  <section class="categories">
    <h2>Categorías principales</h2>
    <div class="category-list">
      <categorycard 
        name="Medicamentos"
        icon="@/assets/icons/medicamentos.svg"
        link="/medicamentos"
        bgColor="#A7D7C5"
      />
      <categorycard 
        name="Higiene"
        icon="@/assets/icons/higiene.svg"
        link="/higiene"
        bgColor="#A5C8D2"
      />
      <categorycard 
        name="Cosmética"
        icon="@/assets/icons/cosmetica.svg"
        link="/cosmetica"
        bgColor="#F5A9A9"
      />
      <categorycard 
        name="Nutrición y Dietética"
        icon="@/assets/icons/nutricion.svg"
        link="/nutricion"
        bgColor="#E2C299"
      />
    </div>
  </section>

  <section class="featured-products">
    <h2>Productos Destacados</h2>
    <div class="product-list">
      <productcard 
        name="Paracetamol 500mg"
        price="5.99€"
        image="@/assets/images/paracetamol.jpg"
      />
      <productcard 
        name="Crema Hidratante"
        price="12.50€"
        image="@/assets/images/crema.jpg"
      />
      <productcard 
        name="Vitaminas Complejo B"
        price="8.99€"
        image="@/assets/images/vitaminas.jpg"
      />
    </div>
  </section>

  <section class="testimonials">
    <h2>Lo que dicen nuestros clientes</h2>
    <div class="testimonial-list">
      <div class="testimonial">
        <p>"Increíble servicio, la entrega fue rápida y los productos de alta calidad."</p>
        <span>- Laura M.</span>
      </div>
      <div class="testimonial">
        <p>"FARMED tiene los mejores precios y la atención al cliente es excelente."</p>
        <span>- Carlos R.</span>
      </div>
      <div class="testimonial">
        <p>"Me encanta la variedad de productos y lo fácil que es comprar en la web."</p>
        <span>- Andrea G.</span>
      </div>
    </div>
  </section>

  <section class="faq">
    <h2>Preguntas Frecuentes</h2>
    <div class="faq-list">
      <div class="faq-item">
        <h3>¿Cuánto tiempo tarda el envío?</h3>
        <p>El tiempo de entrega estándar es de 24 a 48 horas en días hábiles.</p>
      </div>
      <div class="faq-item">
        <h3>¿Puedo devolver un producto?</h3>
        <p>Sí, puedes solicitar una devolución dentro de los 14 días posteriores a la compra.</p>
      </div>
      <div class="faq-item">
        <h3>¿Cómo puedo contactar con soporte?</h3>
        <p>Puedes escribirnos a soporte@farmed.com o llamar al 900-123-456.</p>
      </div>
    </div>
  </section>

  <section class="store-info">
    <div class="container">
      <h2>Sobre FARMED</h2>
      <p>
        En <strong>FARMED</strong> nos especializamos en ofrecer productos farmacéuticos de calidad, 
        con un enfoque en el bienestar y la salud de nuestros clientes. Nuestro compromiso es 
        brindar la mejor atención y un servicio confiable para garantizar tu satisfacción.
      </p>
      <p>
        Descubre nuestra amplia selección de medicamentos, productos de higiene, cosmética y 
        nutrición. ¡Tu bienestar es nuestra prioridad!
      </p>
    </div>
  </section>
</template>

<script setup lang="ts">
import categorycard from '../components/categorycard.vue';
import productcard from '@/components/productcard.vue';
</script>

<style scoped lang="scss">
@use '../assets/styles/pages/home.scss' as *;
</style>
