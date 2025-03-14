import axios from 'axios';

// URL base de la API - cambiar según tu configuración
const API_URL = 'https://tu-api.com/api';

export default {
  // Obtener todas las entidades
  async getEntidades() {
    try {
      const response = await axios.get(`${API_URL}/entidades`);
      return response.data;
    } catch (error) {
      console.error('Error al obtener entidades:', error);
      throw error;
    }
  },
  
  // Obtener una entidad por ID
  async getEntidadById(id) {
    try {
      const response = await axios.get(`${API_URL}/entidades/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error al obtener entidad con ID ${id}:`, error);
      throw error;
    }
  },
  
  // Crear una nueva entidad
  async createEntidad(entidad) {
    try {
      const response = await axios.post(`${API_URL}/entidades`, entidad);
      return response.data;
    } catch (error) {
      console.error('Error al crear entidad:', error);
      throw error;
    }
  },
  
  // Actualizar una entidad existente
  async updateEntidad(id, entidad) {
    try {
      const response = await axios.put(`${API_URL}/entidades/${id}`, entidad);
      return response.data;
    } catch (error) {
      console.error(`Error al actualizar entidad con ID ${id}:`, error);
      throw error;
    }
  },
  
  // Eliminar una entidad
  async deleteEntidad(id) {
    try {
      const response = await axios.delete(`${API_URL}/entidades/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error al eliminar entidad con ID ${id}:`, error);
      throw error;
    }
  },
  
  // Búsqueda filtrada de entidades
  async searchEntidades(params) {
    try {
      const response = await axios.get(`${API_URL}/entidades/search`, { params });
      return response.data;
    } catch (error) {
      console.error('Error al buscar entidades:', error);
      throw error;
    }
}