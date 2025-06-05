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
  import ProductoCard from '@/components/ProductoCard.vue';
  
  export default {
    name: 'Tienda',
    components: {
      ProductoCard
    },
    data() {
      return {
        productos: [
          {
            id: 1,
            nombre: 'Paracetamol 500mg',
            descripcion: 'Analgésico y antipirético para el alivio del dolor leve o moderado y estados febriles.',
            precio: 4.95,
            categoria: 'Medicamentos',
            imagen: 'https://www.farmaciasdirect.es/cdn/shop/files/paracetamol-normon-500-mg-20-comprimidos.webp?v=1725054603'
          },
          {
            id: 2,
            nombre: 'Ibuprofeno 600mg',
            descripcion: 'Antiinflamatorio no esteroideo (AINE) para el tratamiento del dolor y la inflamación.',
            precio: 5.99,
            categoria: 'Medicamentos',
            imagen: 'https://www.kernpharma.com/sites/default/files/styles/max_575x575/public/productos/imagenes/ibuprofeno-kern-pharma-600-mg-comprimidos-recubiertos-envase-clinico-6337.jpg?itok=oHEo1vrh'
          },
          {
            id: 3,
            nombre: 'Omeprazol 20mg',
            descripcion: 'Inhibidor de la bomba de protones para reducir la producción de ácido en el estómago.',
            precio: 8.50,
            categoria: 'Medicamentos',
            imagen: 'https://anefp.org/sites/default/files/medicamentos-a-z/Kern/7127048_FOTO.jpg'
          },
          {
            id: 4,
            nombre: 'Amoxicilina 500mg',
            descripcion: 'Antibiótico de amplio espectro para el tratamiento de infecciones bacterianas.',
            precio: 12.75,
            categoria: 'Medicamentos',
            imagen: 'https://media.istockphoto.com/id/2096790710/es/foto/caja-gen%C3%A9rica-y-pastillas-antibi%C3%B3ticas-de-amoxicilina-fondo-blanco.jpg?s=612x612&w=0&k=20&c=TpL5ukMyaDUQVJ8m-sNoSBqFkflM_FSZQQjcx978Go4='
          },
          {
            id: 5,
            nombre: 'Vitamina C 1000mg',
            descripcion: 'Suplemento alimenticio para reforzar el sistema inmunológico y prevenir resfriados.',
            precio: 9.95,
            categoria: 'Vitaminas y Suplementos',
            imagen: 'https://m.media-amazon.com/images/I/81JrTCGGNSL.jpg'
          },
          {
            id: 6,
            nombre: 'Complejo B',
            descripcion: 'Suplemento con vitaminas del grupo B para el metabolismo energético y el sistema nervioso.',
            precio: 11.50,
            categoria: 'Vitaminas y Suplementos',
            imagen: 'https://media.istockphoto.com/id/487920346/es/foto/c%C3%A1psulas-de-vitamina-b-b1-y-b2-b6-b12.jpg?s=612x612&w=0&k=20&c=yCzNlWi1F_vJ3G0oDBVMDqVenNvnAqb4SwbTpZQkgKs='
          },
          {
            id: 7,
            nombre: 'Loratadina 10mg',
            descripcion: 'Antihistamínico para aliviar los síntomas de alergias como rinitis y urticaria.',
            precio: 7.25,
            categoria: 'Medicamentos',
            imagen: 'https://www.kernpharma.com/sites/default/files/productos/imagenes/loratadina-kern-pharma-efg-10-mg-20-compr-5091.jpeg'
          },
          {
            id: 8,
            nombre: 'Termómetro Digital',
            descripcion: 'Termómetro de alta precisión para medir la temperatura corporal de forma rápida y segura.',
            precio: 15.99,
            categoria: 'Accesorios Médicos',
            imagen: 'https://static.vecteezy.com/system/resources/thumbnails/019/615/957/small_2x/digital-thermometer-temperature-png.png'
          },
          {
            id: 9,
            nombre: 'Tensiómetro de Brazo',
            descripcion: 'Dispositivo para medir la presión arterial de forma precisa y sencilla en el hogar.',
            precio: 42.95,
            categoria: 'Accesorios Médicos',
            imagen: 'https://png.pngtree.com/png-vector/20231120/ourmid/pngtree-digital-blood-pressure-monitor-electronic-digital-png-image_10574715.png'
          },
          {
            id: 10,
            nombre: 'Crema Hidratante Facial',
            descripcion: 'Crema dermatológica para hidratar y proteger la piel del rostro, especial para pieles sensibles.',
            precio: 18.50,
            categoria: 'Dermocosmética',
            imagen: 'https://img.freepik.com/psd-gratis/contenedor-crema-levitante-estudio-bodegon_23-2150807017.jpg'
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
            id: 12,
            nombre: 'Alcohol Isopropílico 70%',
            descripcion: 'Solución antiséptica para desinfección de superficies y pequeñas heridas.',
            precio: 3.95,
            categoria: 'Cuidado Personal',
            imagen: 'https://m.media-amazon.com/images/I/71xr9TOc2ML.jpg'
          },
          {
            id: 13,
            nombre: 'Vendas Elásticas',
            descripcion: 'Pack de vendas elásticas de diferentes tamaños para lesiones musculares y articulares.',
            precio: 8.75,
            categoria: 'Primeros Auxilios',
            imagen: 'https://www.bimedica.com/imagenes_productos/556.png'
          },
          {
            id: 14,
            nombre: 'Agua Micelar',
            descripcion: 'Limpiador facial suave para todo tipo de pieles, elimina maquillaje e impurezas.',
            precio: 13.25,
            categoria: 'Dermocosmética',
            imagen: 'https://www.trnd.com/es/proyectos/skinactive-agua-micelar/micelar-project-teaser-mobile.jpg'
          },
          {
            id: 15,
            nombre: 'Mascarillas FFP2',
            descripcion: 'Pack de 10 mascarillas de alta filtración para protección respiratoria.',
            precio: 14.50,
            categoria: 'Protección',
            imagen: 'https://www.sumhiprot.com/wp-content/uploads/2485.png'
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
            id: 17,
            nombre: 'Aceite de CBD 5%',
            descripcion: 'Aceite de cannabidiol para aliviar dolores, reducir la ansiedad y mejorar el sueño.',
            precio: 29.99,
            categoria: 'Medicina Natural',
            imagen: 'https://laiol.es/cdn/shop/files/retoqueCBD.png?v=1712924885'
          },
          {
            id: 18,
            nombre: 'Báscula de Baño Digital',
            descripcion: 'Báscula de precisión para control de peso con pantalla LCD y capacidad hasta 180kg.',
            precio: 24.50,
            categoria: 'Accesorios Médicos',
            imagen: 'https://mibricolaje.com/21247-large_default/bascula-de-bano-digital-150-kg-vidrio-templado-larryhouse.jpg'
          },
          {
            id: 19,
            nombre: 'Tiras Reactivas Glucosa',
            descripcion: 'Pack de 50 tiras reactivas compatibles con glucómetros estándar para medir niveles de azúcar.',
            precio: 27.75,
            categoria: 'Control Diabetes',
            imagen: 'https://www.accu-chek.com.co/sites/g/files/iut231/f/styles/image_300x400/public/tira-reactivas-active-300x300-web.png?itok=3qzC-ug4'
          },
          {
            id: 20,
            nombre: 'Gel Aloe Vera 100%',
            descripcion: 'Gel calmante e hidratante para irritaciones, quemaduras solares y piel sensible.',
            precio: 8.99,
            categoria: 'Dermocosmética',
            imagen: 'https://m.media-amazon.com/images/I/81nUQxkThML.jpg'
          },
          {
            id: 21,
            nombre: 'Colágeno Hidrolizado',
            descripcion: 'Suplemento para mejorar la salud de piel, cabello, uñas y articulaciones.',
            precio: 22.95,
            categoria: 'Vitaminas y Suplementos',
            imagen: 'https://www.natysal.com/424-large_default/colageno-hidrolizado-silicio-vitamina-D.jpg'
          },
          {
            id: 22,
            nombre: 'Oxímetro de Pulso',
            descripcion: 'Dispositivo para medir la saturación de oxígeno en sangre y frecuencia cardíaca.',
            precio: 29.95,
            categoria: 'Accesorios Médicos',
            imagen: 'https://www.anahuac.mx/mexico/sites/default/files/inline-images/El-oximetro-de-pulso-y-el-COVID-19-2.jpg'
          },
          {
            id: 23,
            nombre: 'Pasta Dental Sensibilidad',
            descripcion: 'Dentífrico especial para dientes sensibles con efecto remineralizante.',
            precio: 6.50,
            categoria: 'Higiene Bucal',
            imagen: 'https://www.colgate.es/content/dam/cp-sites/oral-care/oral-care-center/es-es/product-detail-pages/toothpaste/2021/colgate-sensitive-alivio-inmediato-proteccion-diaria.png'
          },
          {
            id: 24,
            nombre: 'Aceite Esencial Lavanda',
            descripcion: 'Aceite 100% puro para aromaterapia, relajación y bienestar emocional.',
            precio: 12.25,
            categoria: 'Aromaterapia',
            imagen: 'https://www.pranarom.es/cdn/shop/files/aebio-esit20-lavanda-10ml-pranarom-01.jpg?v=1709631993&width=2362'
          },
          {
            id: 25,
            nombre: 'Crema Muscular Árnica',
            descripcion: 'Crema antiinflamatoria natural para dolores musculares y articulares.',
            precio: 14.75,
            categoria: 'Medicina Natural',
            imagen: 'https://m.media-amazon.com/images/I/61a3aV5-aiL._AC_UF894,1000_QL80_.jpg'
          },
          {
            id: 26,
            nombre: 'Jeringas Desechables',
            descripcion: 'Pack de 20 jeringas estériles de 5ml para uso médico o veterinario.',
            precio: 9.25,
            categoria: 'Accesorios Médicos',
            imagen: 'https://png.pngtree.com/png-clipart/20210912/original/pngtree-syringe-needle-png-image_6731465.jpg'
          },
          {
            id: 27,
            nombre: 'Vitamina D3 2000UI',
            descripcion: 'Suplemento para fortalecer huesos, dientes y sistema inmunitario.',
            precio: 15.50,
            categoria: 'Vitaminas y Suplementos',
            imagen: 'https://m.media-amazon.com/images/I/71mocSq5MYL._AC_UF894,1000_QL80_.jpg'
          },
          {
            id: 28,
            nombre: 'Estetoscopio Profesional',
            descripcion: 'Instrumento médico de alta calidad para auscultación cardíaca y pulmonar.',
            precio: 45.99,
            categoria: 'Equipamiento Médico',
            imagen: 'https://img.freepik.com/foto-gratis/estetoscopio-aislado-sobre-superficie-blanca_181624-11424.jpg'
          },
          {
            id: 29,
            nombre: 'Kit Primeros Auxilios',
            descripcion: 'Kit completo con vendas, tijeras, pinzas, gasas y más para emergencias.',
            precio: 32.50,
            categoria: 'Primeros Auxilios',
            imagen: 'https://cdnx.jumpseller.com/isoprevent/image/18914467/resize/610/610?1644946133'
          },
          {
            id: 30,
            nombre: 'Cepillo Dental Eléctrico',
            descripcion: 'Cepillo con tecnología ultrasónica para una limpieza profunda y cuidado de encías.',
            precio: 39.95,
            categoria: 'Higiene Bucal',
            imagen: 'https://s.alicdn.com/@sc04/kf/HTB1EX.oXPLuK1Rjy0Fh760pdFXaL.png_720x720q50.png'
          }
        ],
        busqueda: '',
        filtroCategoria: '',
        ordenarPor: 'nombre-asc',
        notificacion: {
          visible: false,
          mensaje: '',
          tipo: 'exito'
        }
      };
    },
    computed: {
      categorias() {
        // Obtener todas las categorías únicas
        return [...new Set(this.productos.map(p => p.categoria))];
      },
      productosFiltrados() {
        let resultado = [...this.productos];
        
        // Aplicar filtro de búsqueda
        if (this.busqueda) {
          const terminoBusqueda = this.busqueda.toLowerCase();
          resultado = resultado.filter(producto => 
            producto.nombre.toLowerCase().includes(terminoBusqueda) || 
            producto.descripcion.toLowerCase().includes(terminoBusqueda)
          );
        }
        
        // Aplicar filtro de categoría
        if (this.filtroCategoria) {
          resultado = resultado.filter(producto => 
            producto.categoria === this.filtroCategoria
          );
        }
        
        // Aplicar ordenamiento
        switch (this.ordenarPor) {
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
      mostrarNotificacion(data) {
        this.notificacion = {
          visible: true,
          mensaje: data.mensaje || 'Producto agregado al carrito',
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
  @import '@/assets/styles/pages/tienda.scss';
  </style>