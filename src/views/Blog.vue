<template>
  <div class="about-us-container">
    <section class="about-section">
      <div class="about-content">
        <h1 class="section-title">Sobre Nosotros</h1>
        <div class="content-wrapper">
          <div class="image-container">
            <img src="../assets/images/1.png" alt="Farmacia Farmed" class="about-image" />
          </div>
          <div class="text-container">
            <h2 class="subtitle">Bienvenidos a Farmed</h2>
            <p class="description">
              En Farmed, nos dedicamos a cuidar de tu salud y bienestar desde hace más de 15 años. Nacimos con la misión de proporcionar medicamentos y productos farmacéuticos de la más alta calidad, con un servicio personalizado y profesional.
            </p>
            <p class="description">
              Nuestro equipo está formado por farmacéuticos altamente cualificados y personal asistente que se mantiene en constante actualización para ofrecerte la mejor atención y asesoramiento en cada visita.
            </p>
            <h3 class="values-title">Nuestros Valores</h3>
            <ul class="values-list">
              <li><span class="value-highlight">Compromiso:</span> Con la salud y el bienestar de nuestros clientes.</li>
              <li><span class="value-highlight">Calidad:</span> En todos nuestros productos y servicios.</li>
              <li><span class="value-highlight">Profesionalidad:</span> En cada interacción y consejo farmacéutico.</li>
              <li><span class="value-highlight">Cercanía:</span> Trato personalizado adaptado a las necesidades de cada cliente.</li>
              <li><span class="value-highlight">Innovación:</span> Constante actualización en productos y servicios farmacéuticos.</li>
            </ul>
          </div>
        </div>
      </div>
    </section>

    <section class="contact-section" id="contacto">
      <div class="contact-content">
        <h2 class="section-title">Contacto</h2>
        <div class="contact-wrapper">
          <div class="contact-info">
            <div class="info-item">
              <i class="icon location-icon"></i>
              <div class="info-text">
                <h4>Dirección</h4>
                <p>Av. Principal 123, Ciudad</p>
              </div>
            </div>
            <div class="info-item">
              <i class="icon phone-icon"></i>
              <div class="info-text">
                <h4>Teléfono</h4>
                <p>+34 912 345 678</p>
              </div>
            </div>
            <div class="info-item">
              <i class="icon email-icon"></i>
              <div class="info-text">
                <h4>Email</h4>
                <p>info@farmed.com</p>
              </div>
            </div>
            <div class="info-item">
              <i class="icon hours-icon"></i>
              <div class="info-text">
                <h4>Horario</h4>
                <p>Lunes a Viernes: 9:00 - 20:00</p>
                <p>Sábados: 10:00 - 14:00</p>
              </div>
            </div>
          </div>
          <div class="contact-form">
            <h3>Envíanos un mensaje</h3>
            <form @submit.prevent="submitForm">
              <div class="form-group">
                <label for="name">Nombre</label>
                <input type="text" id="name" v-model="formData.name" required />
              </div>
              <div class="form-group">
                <label for="email">Email</label>
                <input type="email" id="email" v-model="formData.email" required />
              </div>
              <div class="form-group">
                <label for="phone">Teléfono</label>
                <input type="tel" id="phone" v-model="formData.phone" />
              </div>
              <div class="form-group">
                <label for="message">Mensaje</label>
                <textarea id="message" v-model="formData.message" required></textarea>
              </div>
              <button type="submit" class="submit-button">Enviar Mensaje</button>
            </form>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: 'AboutUs',
  data() {
    return {
      formData: {
        name: '',
        email: '',
        phone: '',
        message: ''
      }
    }
  },
  methods: {
    submitForm() {
      // Aquí puedes implementar la lógica para enviar el formulario
      console.log('Formulario enviado', this.formData);
      // Resetear formulario después de enviar
      this.formData = {
        name: '',
        email: '',
        phone: '',
        message: ''
      };
      // Mostrar mensaje de confirmación
      alert('Mensaje enviado correctamente. Nos pondremos en contacto contigo pronto.');
    }
  }
}
</script>

<style lang="scss">
  @import '@/assets/styles/pages/blog.scss';
</style>