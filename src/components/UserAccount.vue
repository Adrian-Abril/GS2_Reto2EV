<template>
    <div class="user-account-container">
      <div class="user-header">
        <div class="user-avatar">
          <span>{{ userInitials }}</span>
        </div>
        <div class="user-info">
          <h2 class="user-name">{{ user.nombre }}</h2>
          <p class="user-email">{{ user.correo }}</p>
          <span class="user-role">{{ userRoleText }}</span>
        </div>
      </div>
      <!-- USUARIO ADMIN -->
      <div class="account-sections">
        <div v-if="isAdmin" class="admin-panel">
          <h3>Panel de Administración</h3>
          <div class="admin-actions">
            <button @click="cargarUsuarios" class="admin-card">Usuarios</button>
            <button @click="cargarMedicamentos" class="admin-card">Medicamentos</button>
            <button @click="cargarPedidos" class="admin-card">Pedidos</button>
          </div>

          <div v-if="selectedAdminSection === 'usuarios'" >
            <h3>Usuarios</h3>
            <ul>
              <li v-for="user in users" :key="user.id">
                <input v-model="user.nombre" placeholder="Nombre del usuario">
                <input v-model="user.correo" placeholder="Correo del usuario">
                <select v-model="user.rol">
                  <option value="cliente">Cliente</option>
                  <option value="farmaceutico">Farmacéutico</option>
                  <option value="admin">Administrador</option>
                </select>
                <input type="checkbox" v-model="user.activo"> Activo

                <button @click="guardarUsuario(user.id, user)">Guardar</button>
                <button @click="eliminarUsuario(user.id)">Eliminar</button>
              </li>
            </ul>
          </div>

          <div v-if="selectedAdminSection === 'medicamentos'" >
            <h3>Medicamentos</h3>
            <ul>
              <li v-for="med in medicamentos" :key="med.id">
                <input v-model="med.nombre" placeholder="Nombre del medicamento">
                <input v-model="med.precio" placeholder="Precio del medicamento">
                <button @click="guardarMedicamento(med.id, med)">Guardar</button>
              </li>
            </ul>
          </div>

          <div v-if="selectedAdminSection === 'pedidos'" >
            <h3>Pedidos</h3>
            <ul>
              <li v-for="ped in pedidos" :key="ped.id">
                <!-- Pedido: #{{ ped.id }} - {{ new Date(ped.fecha).toLocaleDateString() }} - Total: {{ ped.total }}€ -->
                <input v-model="ped.id" type="number" placeholder="ID del pedido">
                <input v-model="ped.fecha" type="date" readonly>
                <input v-model="ped.total" placeholder="Total del pedido">
                <button @click="guardarPedido(ped.id, ped)">Guardar</button>
                <button @click="eliminarPedido(ped.id)">Eliminar</button>
              </li>
            </ul>
          </div>

        </div>
        <!-- USUARIO NORMAL -->
        <div v-else>
          <div class="welcome-section">
            <h3>¡Bienvenido a tu cuenta de Farmed!</h3>
            <p>Desde aquí podrás gestionar tus pedidos, recetas y disfrutar de beneficios exclusivos.</p>
          </div>
          
          <div class="quick-actions">
            <h3>Acciones rápidas</h3>
            <div class="actions-grid">
              <div class="action-card">
                <i class="action-icon orders-icon"></i>
                <h4>Mis Pedidos</h4>
                <p>Revisa el estado de tus pedidos actuales e históricos</p>
              </div>
              
              <div class="action-card">
                <i class="action-icon prescriptions-icon"></i>
                <h4>Recetas</h4>
                <p>Gestiona tus recetas médicas y reabastécelas</p>
              </div>
              
              <div class="action-card">
                <i class="action-icon appointments-icon"></i>
                <h4>Citas</h4>
                <p>Agenda una consulta con nuestros especialistas</p>
              </div>
              
              <div class="action-card">
                <i class="action-icon profile-icon"></i>
                <h4>Mi Perfil</h4>
                <p>Actualiza tus datos personales y preferencias</p>
              </div>
            </div>
          </div>
          <div class="latest-orders">
            <h3>Pedidos recientes</h3>
            <div v-if="hasOrders" class="orders-list">
              <div v-for="(order, index) in orders" :key="index" class="order-item">
                <div class="order-info">
                  <h4>Pedido #{{ order.id }}</h4>
                  <p>{{ order.date }}</p>
                  <span class="order-status" :class="order.status">{{ order.status }}</span>
                </div>
                <div class="order-amount">
                  {{ order.amount }}€
                </div>
              </div>
            </div>
            <div v-else class="no-orders">
              <i class="empty-icon"></i>
              <p>Aún no tienes pedidos realizados</p>
              <button class="shop-button">Ir a la tienda</button>
            </div>
          </div>
        </div>
      </div>
  
      <div class="account-footer">
        <button @click="$emit('logout')" class="logout-button">
          <i class="logout-icon"></i>
          Cerrar sesión
        </button>
      </div>
    </div>
  </template>
  
  <script>
  import router from '@/router';
  import axios from 'axios';


  export default {
    name: 'UserAccount',
    props: {
      user: {
        type: Object,
        required: true
      }
    },
    data() {
      return {
        selectedAdminSection: null, //cambio a usuarios, medicamentos o pedidos
        orders: [],
        users: [],
        pedidos: [],
        medicamentos: [],


      }
    },
    computed: {
      isAdmin(){
        return this.user.rol === 'admin';
      },
      userInitials() {
        if (!this.user || !this.user.nombre) return '?';
        
        return this.user.nombre
          .split(' ')
          .map(name => name.charAt(0).toUpperCase())
          .slice(0, 2)
          .join('');
      },
      userRoleText() {
        if (!this.user || !this.user.rol) return 'Cliente';
        
        const roles = {
          'cliente': 'Cliente',
          'farmaceutico': 'Farmacéutico',
          'admin': 'Administrador'
        };
        
        return roles[this.user.rol] || 'Cliente';
      },
      hasOrders() {
        return this.orders && this.orders.length > 0;
      }
    },
    mounted(){
      this.obtenerPedidos();
    },
    methods: {
      async cargarUsuarios() {
        this.selectedAdminSection = 'usuarios';
        try{
          const res = await axios.get('http://localhost:5000/api/usuarios', { withCredentials: true });
          this.users = res.data;
        }catch(error){
          console.log("Error al obtener los usuarios " + error)
        }
      },
      async guardarUsuario(id, u){
        try{
          await axios.put(`http://localhost:5000/api/usuarios/${id}`, u, { withCredentials: true });
          console.log('id del usuario' + id)
          console.log('Usuario guardado:', u);
        }catch(error){
          console.log("Error al guardar el usuario " + error)
        }
      },
      async eliminarUsuario(id){
        if(!confirm("Estas seguro de eliminar el usuario?")){
            return;
        }
        try{
          await axios.delete(`http://localhost:5000/api/usuarios/${id}`, { withCredentials: true });
          this.users = this.users.filter(u => u.id !== id);
          console.log('Usuario eliminado:', id);
        }catch(error){
          console.log("Error al eliminar el usuario " + error)
        }
      },

      async cargarMedicamentos(){
        this.selectedAdminSection = 'medicamentos';
        try{
          const res = await axios.get('http://localhost:5000/api/medicamentos', { withCredentials: true });
          this.medicamentos = res.data;
        }catch(error){
          console.log("Error al obtener los medicamentos " + error)
        }
      },
      async guardarMedicamento(id, m){
        try{
          await axios.put(`http://localhost:5000/api/medicamentos/${id}`, m, { withCredentials: true });
          console.log('Medicamento guardado:', m);
        }catch(error){
          console.log("Error al guardar el medicamento " + error)
        }
      },
      async eliminarMedicamento(id){
        if(!confirm("Estas seguro de eliminar el medicamento?")){
            return;
        }
        try{
          await axios.delete(`http://localhost:5000/api/medicamentos/${id}`, { withCredentials: true });
          this.medicamentos = this.medicamentos.filter(m => m.id !== id);
          console.log('Medicamento eliminado:', id);
        }catch(error){
          console.log("Error al medicamento el usuario " + error)
        }
      },
      async cargarPedidos(){
        this.selectedAdminSection = 'pedidos';
        try{
          const res = await axios.get('http://localhost:5000/api/compras', { withCredentials: true });
          this.pedidos = res.data;
        }catch(error){
          console.log("Error al obtener los pedidos " + error)
        }
      },async guardarPedido(id, p){
        try{

          const body = {
            id: p.id,
            fecha: p.fecha,
            precioUnitario: p.total
          }
          await axios.put(`http://localhost:5000/api/detallecompra/${id}`, body, { withCredentials: true });
          console.log('Pedido guardado:', p);
        }catch(error){
          console.log("Error al guardar el pedido " + error)
        }
      },
      async eliminarPedido(id){
        if(!confirm("Estas seguro de eliminar el pedido?")){
            return;
        }
        try{
          await axios.delete(`http://localhost:5000/api/compras/${id}`, { withCredentials: true });
          this.pedidos = this.pedidos.filter(p => p.id !== id);
          console.log('pedido eliminado:', id);
        }catch(error){
          console.log("Error al eliminar el pedido " + error)
        }
      },
      async obtenerPedidos(){
        try{
          const response = await axios.get("http://localhost:5000/api/compras", { withCredentials: true });
          this.orders = response.data.map(c => ({
            id: c.id,
            date: new Date(c.fecha).toLocaleDateString(),
            status: "procesado",
            amount: c.total || "0.00",
          }));
        }catch(error){
          console.log(error);
          this.orders = [];
        }
      }
    }
  };
  </script>
  
  <style lang="scss">
  @use '@/assets/styles/components/user-account.scss';
  </style>