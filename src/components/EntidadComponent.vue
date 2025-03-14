<template>
    <div class="entidad-component">
      <!-- Formulario para añadir nueva entidad -->
      <div class="form-container">
        <h2>Añadir Nueva Entidad</h2>
        <form @submit.prevent="agregarEntidad" class="form-grid">
          <div class="form-group">
            <label for="nombre">Nombre:</label>
            <input 
              type="text" 
              id="nombre" 
              v-model="nuevaEntidad.nombre" 
              required 
              class="form-control"
            >
          </div>
          
          <div class="form-group">
            <label for="descripcion">Descripción:</label>
            <textarea 
              id="descripcion" 
              v-model="nuevaEntidad.descripcion" 
              class="form-control"
            ></textarea>
          </div>
          
          <div class="form-group">
            <label for="categoria">Categoría:</label>
            <select 
              id="categoria" 
              v-model="nuevaEntidad.categoria" 
              class="form-control"
            >
              <option value="categoria1">Categoría 1</option>
              <option value="categoria2">Categoría 2</option>
              <option value="categoria3">Categoría 3</option>
            </select>
          </div>
          
          <div class="form-group">
            <label for="fecha">Fecha:</label>
            <input 
              type="date" 
              id="fecha" 
              v-model="nuevaEntidad.fecha" 
              class="form-control"
            >
          </div>
          
          <div class="form-group">
            <label for="activo">Estado:</label>
            <div class="toggle-container">
              <input 
                type="checkbox" 
                id="activo" 
                v-model="nuevaEntidad.activo"
              >
              <label for="activo" class="toggle-label">{{ nuevaEntidad.activo ? 'Activo' : 'Inactivo' }}</label>
            </div>
          </div>
          
          <div class="form-actions">
            <button type="submit" class="btn-primary">Guardar</button>
            <button type="button" @click="resetForm" class="btn-secondary">Cancelar</button>
          </div>
        </form>
      </div>
  
      <!-- Filtros de búsqueda -->
      <div class="filters-container">
        <h2>Filtros</h2>
        <div class="filters-grid">
          <div class="filter-group">
            <input 
              type="text" 
              v-model="filtros.busqueda" 
              placeholder="Buscar por nombre..." 
              class="form-control"
            >
          </div>
          
          <div class="filter-group">
            <select v-model="filtros.categoria" class="form-control">
              <option value="">Todas las categorías</option>
              <option value="categoria1">Categoría 1</option>
              <option value="categoria2">Categoría 2</option>
              <option value="categoria3">Categoría 3</option>
            </select>
          </div>
          
          <div class="filter-group">
            <select v-model="filtros.estado" class="form-control">
              <option value="">Todos los estados</option>
              <option value="true">Activo</option>
              <option value="false">Inactivo</option>
            </select>
          </div>
          
          <div class="filter-actions">
            <button @click="aplicarFiltros" class="btn-secondary">Aplicar filtros</button>
            <button @click="limpiarFiltros" class="btn-outline">Limpiar</button>
          </div>
        </div>
      </div>
  
      <!-- Tabla de listado de entidades -->
      <div class="table-container">
        <h2>Listado de Entidades</h2>
        
        <div v-if="cargando" class="loading">
          <div class="spinner"></div>
          <p>Cargando datos...</p>
        </div>
        
        <div v-else-if="entidades.length === 0" class="empty-state">
          <p>No se encontraron entidades. Intenta añadir una nueva o ajustar los filtros.</p>
        </div>
        
        <div v-else class="responsive-table">
          <table>
            <thead>
              <tr>
                <th @click="ordenarPor('id')" class="sortable">
                  ID
                  <span v-if="ordenacion.campo === 'id'" class="sort-icon">
                    {{ ordenacion.direccion === 'asc' ? '↑' : '↓' }}
                  </span>
                </th>
                <th @click="ordenarPor('nombre')" class="sortable">
                  Nombre
                  <span v-if="ordenacion.campo === 'nombre'" class="sort-icon">
                    {{ ordenacion.direccion === 'asc' ? '↑' : '↓' }}
                  </span>
                </th>
                <th>Descripción</th>
                <th @click="ordenarPor('categoria')" class="sortable">
                  Categoría
                  <span v-if="ordenacion.campo === 'categoria'" class="sort-icon">
                    {{ ordenacion.direccion === 'asc' ? '↑' : '↓' }}
                  </span>
                </th>
                <th @click="ordenarPor('fecha')" class="sortable">
                  Fecha
                  <span v-if="ordenacion.campo === 'fecha'" class="sort-icon">
                    {{ ordenacion.direccion === 'asc' ? '↑' : '↓' }}
                  </span>
                </th>
                <th>Estado</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="entidad in entidadesFiltradas" :key="entidad.id">
                <td>{{ entidad.id }}</td>
                <td>{{ entidad.nombre }}</td>
                <td class="descripcion-cell">
                  <div class="truncate-text">{{ entidad.descripcion }}</div>
                </td>
                <td>{{ entidad.categoria }}</td>
                <td>{{ formatearFecha(entidad.fecha) }}</td>
                <td>
                  <span 
                    class="estado-badge" 
                    :class="entidad.activo ? 'estado-activo' : 'estado-inactivo'"
                  >
                    {{ entidad.activo ? 'Activo' : 'Inactivo' }}
                  </span>
                </td>
                <td class="actions-cell">
                  <button @click="editarEntidad(entidad)" class="btn-icon edit">
                    <span class="icon">✏️</span>
                  </button>
                  <button @click="eliminarEntidad(entidad.id)" class="btn-icon delete">
                    <span class="icon">🗑️</span>
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        
        <!-- Paginación -->
        <div class="pagination">
          <button 
            :disabled="paginaActual === 1" 
            @click="cambiarPagina(paginaActual - 1)" 
            class="btn-page"
          >
            Anterior
          </button>
          
          <span class="page-info">
            Página {{ paginaActual }} de {{ totalPaginas }}
          </span>
          
          <button 
            :disabled="paginaActual === totalPaginas" 
            @click="cambiarPagina(paginaActual + 1)" 
            class="btn-page"
          >
            Siguiente
          </button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  // No es necesario importar axios directamente aquí
  // Utilizaremos el servicio para manejar las peticiones API
  
  export default {
    name: 'EntidadComponent',
    data() {
      return {
        entidades: [],
        nuevaEntidad: {
          nombre: '',
          descripcion: '',
          categoria: 'categoria1',
          fecha: '',
          activo: true
        },
        entidadEditando: null,
        cargando: true,
        error: null,
        filtros: {
          busqueda: '',
          categoria: '',
          estado: ''
        },
        ordenacion: {
          campo: 'id',
          direccion: 'asc'
        },
        paginaActual: 1,
        elementosPorPagina: 5
      }
    },
    computed: {
      entidadesFiltradas() {
        let resultado = [...this.entidades];
        
        // Aplicar filtros
        if (this.filtros.busqueda) {
          const busqueda = this.filtros.busqueda.toLowerCase();
          resultado = resultado.filter(e => 
            e.nombre.toLowerCase().includes(busqueda) || 
            e.descripcion.toLowerCase().includes(busqueda)
          );
        }
        
        if (this.filtros.categoria) {
          resultado = resultado.filter(e => e.categoria === this.filtros.categoria);
        }
        
        if (this.filtros.estado !== '') {
          const estadoBoolean = this.filtros.estado === 'true';
          resultado = resultado.filter(e => e.activo === estadoBoolean);
        }
        
        // Aplicar ordenación
        resultado.sort((a, b) => {
          let valorA = a[this.ordenacion.campo];
          let valorB = b[this.ordenacion.campo];
          
          // Manejar fechas
          if (this.ordenacion.campo === 'fecha') {
            valorA = new Date(valorA);
            valorB = new Date(valorB);
          }
          
          if (this.ordenacion.direccion === 'asc') {
            return valorA > valorB ? 1 : -1;
          } else {
            return valorA < valorB ? 1 : -1;
          }
        });
        
        // Calcular paginación
        const inicio = (this.paginaActual - 1) * this.elementosPorPagina;
        const fin = inicio + this.elementosPorPagina;
        
        return resultado.slice(inicio, fin);
      },
      totalPaginas() {
        return Math.ceil(this.entidades.length / this.elementosPorPagina);
      }
    },
    methods: {
      async cargarEntidades() {
        this.cargando = true;
        try {
          // Utilizamos datos de ejemplo ya que no hay conexión real a API
          this.cargarDatosEjemplo();
        } catch (error) {
          console.error('Error al cargar entidades:', error);
          this.error = 'No se pudieron cargar las entidades. Intenta de nuevo más tarde.';
        } finally {
          this.cargando = false;
        }
      },
      
      cargarDatosEjemplo() {
        // Datos de ejemplo para demostración
        this.entidades = [
          { 
            id: 1, 
            nombre: 'Entidad de ejemplo 1', 
            descripcion: 'Esta es una descripción de ejemplo para la primera entidad.', 
            categoria: 'categoria1', 
            fecha: '2023-01-15', 
            activo: true 
          },
          { 
            id: 2, 
            nombre: 'Entidad de ejemplo 2', 
            descripcion: 'Descripción más corta para la segunda entidad.', 
            categoria: 'categoria2', 
            fecha: '2023-02-20', 
            activo: false 
          },
          { 
            id: 3, 
            nombre: 'Entidad importante', 
            descripcion: 'Esta entidad es muy importante y tiene una descripción más larga para probar cómo se muestra en la tabla.', 
            categoria: 'categoria1', 
            fecha: '2023-03-10', 
            activo: true 
          },
          { 
            id: 4, 
            nombre: 'Otra entidad', 
            descripcion: 'Descripción de otra entidad.', 
            categoria: 'categoria3', 
            fecha: '2023-04-05', 
            activo: true 
          },
          { 
            id: 5, 
            nombre: 'Entidad inactiva', 
            descripcion: 'Esta entidad está inactiva.', 
            categoria: 'categoria2', 
            fecha: '2023-05-12', 
            activo: false 
          },
          { 
            id: 6, 
            nombre: 'Entidad reciente', 
            descripcion: 'Esta es la entidad más reciente.', 
            categoria: 'categoria3', 
            fecha: '2023-06-01', 
            activo: true 
          }
        ];
      },
      
      async agregarEntidad() {
        try {
          if (this.entidadEditando) {
            // Actualizar entidad existente
            // En un caso real, aquí iría una llamada a axios.put
            const index = this.entidades.findIndex(e => e.id === this.entidadEditando.id);
            if (index !== -1) {
              this.entidades[index] = {
                ...this.entidadEditando,
                ...this.nuevaEntidad
              };
            }
            this.entidadEditando = null;
          } else {
            // Crear nueva entidad
            // En un caso real, aquí iría una llamada a axios.post
            const nuevaEntidadCompleta = {
              id: this.entidades.length > 0 ? Math.max(...this.entidades.map(e => e.id)) + 1 : 1,
              ...this.nuevaEntidad
            };
            
            this.entidades.push(nuevaEntidadCompleta);
          }
          
          this.resetForm();
        } catch (error) {
          console.error('Error al guardar la entidad:', error);
          alert('No se pudo guardar la entidad. Intenta de nuevo más tarde.');
        }
      },
      
      editarEntidad(entidad) {
        this.entidadEditando = entidad;
        this.nuevaEntidad = { ...entidad };
        // Scroll hasta el formulario
        document.querySelector('.form-container').scrollIntoView({ behavior: 'smooth' });
      },
      
      async eliminarEntidad(id) {
        if (confirm('¿Estás seguro de que deseas eliminar esta entidad?')) {
          try {
            // En un caso real, aquí iría una llamada a axios.delete
            this.entidades = this.entidades.filter(e => e.id !== id);
          } catch (error) {
            console.error('Error al eliminar la entidad:', error);
            alert('No se pudo eliminar la entidad. Intenta de nuevo más tarde.');
          }
        }
      },
      
      resetForm() {
        this.nuevaEntidad = {
          nombre: '',
          descripcion: '',
          categoria: 'categoria1',
          fecha: '',
          activo: true
        };
        this.entidadEditando = null;
      },
      
      formatearFecha(fecha) {
        if (!fecha) return '';
        const f = new Date(fecha);
        return f.toLocaleDateString();
      },
      
      ordenarPor(campo) {
        if (this.ordenacion.campo === campo) {
          this.ordenacion.direccion = this.ordenacion.direccion === 'asc' ? 'desc' : 'asc';
        } else {
          this.ordenacion.campo = campo;
          this.ordenacion.direccion = 'asc';
        }
      },
      
      aplicarFiltros() {
        this.paginaActual = 1;
      },
      
      limpiarFiltros() {
        this.filtros = {
          busqueda: '',
          categoria: '',
          estado: ''
        };
        this.paginaActual = 1;
      },
      
      cambiarPagina(pagina) {
        if (pagina >= 1 && pagina <= this.totalPaginas) {
          this.paginaActual = pagina;
        }
      }
    },
    created() {
      this.cargarEntidades();
    }
  }
  </script>
  
  <style scoped>
  .entidad-component {
    display: flex;
    flex-direction: column;
    gap: 30px;
  }
  
  h2 {
    margin-bottom: 20px;
    color: #2c3e50;
    font-size: 1.5rem;
  }
  
  /* Estilos para el formulario */
  .form-container {
    background-color: #f8f9fa;
    border-radius: 8px;
    padding: 20px;
    border: 1px solid #e9ecef;
  }
  
  .form-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    gap: 20px;
  }
  
  .form-group {
    display: flex;
    flex-direction: column;
  }
  
  .form-control {
    padding: 6px;
    border: 1px solid #ced4da;
    border-radius: 4px;
    font-size: 1rem;
    transition: border-color 0.15s ease-in-out;
  }
  
  .form-control:focus {
    border-color: #4a6cf7;
    outline: none;
  }
  
  textarea.form-control {
    min-height: 100px;
    resize: vertical;
  }
  
  .toggle-container {
    display: flex;
    align-items: center;
    gap: 10px;
  }
  
  .toggle-label {
    font-size: 0.9rem;
  }
  
  .form-actions {
    grid-column: 1 / -1;
    display: flex;
    gap: 10px;
    justify-content: flex-end;
  }
  
  /* Estilos para los filtros */
  .filters-container {
    background-color: #f8f9fa;
    border-radius: 8px;
    padding: 20px;
    border: 1px solid #e9ecef;
  }
  
  .filters-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    gap: 15px;
    align-items: end;
  }
  
  .filter-actions {
    display: flex;
    gap: 10px;
  }
  
  /* Estilos para la tabla */
  .table-container {
    background-color: #fff;
    border-radius: 8px;
    padding: 20px;
    border: 1px solid #e9ecef;
  }
  
  .responsive-table {
    overflow-x: auto;
  }
  
  table {
    width: 100%;
    border-collapse: collapse;
    margin-bottom: 20px;
  }
  
  th, td {
    padding: 12px 15px;
    text-align: left;
    border-bottom: 1px solid #e9ecef;
  }
  
  thead {
    background-color: #f8f9fa;
  }
  
  th {
    font-weight: 600;
  }
  
  .sortable {
    cursor: pointer;
    position: relative;
  }
  
  .sort-icon {
    margin-left: 5px;
  }
  
  .descripcion-cell {
    max-width: 300px;
  }
  
  .truncate-text {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    max-width: 300px;
  }
  
  .estado-badge {
    display: inline-block;
    padding: 4px 8px;
    border-radius: 4px;
    font-size: 0.85rem;
    font-weight: 500;
  }
  
  .estado-activo {
    background-color: #e6f7e6;
    color: #28a745;
  }
  
  .estado-inactivo {
    background-color: #f8d7da;
    color: #dc3545;
  }
  
  .actions-cell {
    white-space: nowrap;
    width: 100px;
  }
  
  /* Botones */
  .btn-primary {
    background-color: #4a6cf7;
    color: white;
    border: none;
    padding: 10px 15px;
    border-radius: 4px;
    cursor: pointer;
    font-weight: 500;
    transition: background-color 0.2s;
  }
  
  .btn-primary:hover {
    background-color: #3755d7;
  }
  
  .btn-secondary {
    background-color: #6c757d;
    color: white;
    border: none;
    padding: 10px 15px;
    border-radius: 4px;
    cursor: pointer;
    font-weight: 500;
    transition: background-color 0.2s;
  }
  
  .btn-secondary:hover {
    background-color: #5a6268;
  }
  
  .btn-outline {
    background-color: transparent;
    color: #6c757d;
    border: 1px solid #6c757d;
    padding: 9px 14px;
    border-radius: 4px;
    cursor: pointer;
    font-weight: 500;
    transition: all 0.2s;
  }
  
  .btn-outline:hover {
    background-color: #6c757d;
    color: white;
  }
  
  .btn-icon {
    background: none;
    border: none;
    font-size: 1.2rem;
    cursor: pointer;
    padding: 5px;
    margin: 0 2px;
    border-radius: 4px;
  }
  
  .btn-icon.edit:hover {
    background-color: #e9ecef;
  }
  
  .btn-icon.delete:hover {
    background-color: #f8d7da;
  }
  
  /* Estilos para la paginación */
  .pagination {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 15px;
    margin-top: 20px;
  }
  
  .btn-page {
    padding: 8px 12px;
    border: 1px solid #ced4da;
    background-color: #fff;
    border-radius: 4px;
    cursor: pointer;
    transition: all 0.2s;
  }
  
  .btn-page:hover:not(:disabled) {
    background-color: #f8f9fa;
    border-color: #4a6cf7;
  }
  
  .btn-page:disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
  
  .page-info {
    font-size: 0.9rem;
    color: #6c757d;
  }
  
  /* Estados de carga y vacío */
  .loading {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 40px 0;
  }
  
  .spinner {
    border: 4px solid rgba(0, 0, 0, 0.1);
    width: 36px;
    height: 36px;
    border-radius: 50%;
    border-left-color: #4a6cf7;
    animation: spin 1s linear infinite;
    margin-bottom: 15px;
  }
  
  @keyframes spin {
    0% { transform: rotate(0deg); }
    100% { transform: rotate(360deg); }
  }
  
  .empty-state {
    text-align: center;
    padding: 40px 0;
    color: #6c757d;
  }
  
  /* Estilos responsivos */
  @media (max-width: 768px) {
    .form-grid, .filters-grid {
      grid-template-columns: 1fr;
    }
    
    .form-actions, .filter-actions {
      justify-content: center;
    }
    
    .responsive-table {
      font-size: 0.9rem;
    }
    
    th, td {
      padding: 10px;
    }
    
    .truncate-text {
      max-width: 150px;
    }
  }
  </style>