<template>
    <div class="profile-settings">
      <div class="profile-header private-card">
        <div class="section-info">
          <h2>Mi Perfil</h2>
          <p>Gestiona tu información personal y preferencias</p>
        </div>
      </div>
      
      <div class="settings-grid">
        <div class="personal-info private-card">
          <div class="card-header">
            <h3>Información Personal</h3>
            <button v-if="!editingPersonal" class="btn btn-text" @click="startEditingPersonal">
              <i class="fa-solid fa-pencil"></i>
              Editar
            </button>
          </div>
          
          <div v-if="!editingPersonal" class="info-content">
            <div class="profile-avatar">
              <div v-if="user.avatar" class="avatar">
                <img :src="user.avatar" :alt="user.name" />
              </div>
              <div v-else class="avatar-placeholder">
                {{ userInitials }}
              </div>
            </div>
            
            <div class="info-details">
              <div class="info-item">
                <label>Nombre completo:</label>
                <span>{{ user.name }}</span>
              </div>
              <div class="info-item">
                <label>Correo electrónico:</label>
                <span>{{ user.email }}</span>
              </div>
              <div class="info-item">
                <label>Teléfono:</label>
                <span>{{ user.phone || 'No especificado' }}</span>
              </div>
              <div class="info-item">
                <label>Dirección:</label>
                <span>{{ user.address || 'No especificada' }}</span>
              </div>
              <div class="info-item">
                <label>Fecha de nacimiento:</label>
                <span>{{ formatDate(user.birthdate) || 'No especificada' }}</span>
              </div>
            </div>
          </div>
          
          <form v-else class="edit-form" @submit.prevent="savePersonalInfo">
            <div class="form-row">
              <div class="avatar-upload">
                <div class="avatar-preview">
                  <div v-if="personalForm.avatar" class="avatar">
                    <img :src="personalForm.avatar" alt="Avatar preview" />
                  </div>
                  <div v-else class="avatar-placeholder">
                    {{ userInitials }}
                  </div>
                </div>
                <div class="avatar-buttons">
                  <button type="button" class="btn btn-outline btn-small">
                    <i class="fa-solid fa-upload"></i>
                    Subir foto
                  </button>
                  <button 
                    v-if="personalForm.avatar" 
                    type="button" 
                    class="btn btn-outline btn-small"
                    @click="personalForm.avatar = null"
                  >
                    <i class="fa-solid fa-trash"></i>
                    Eliminar
                  </button>
                </div>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group">
                <label for="name">Nombre completo</label>
                <input 
                  type="text" 
                  id="name" 
                  v-model="personalForm.name" 
                  class="form-control"
                  required
                />
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group">
                <label for="email">Correo electrónico</label>
                <input 
                  type="email" 
                  id="email" 
                  v-model="personalForm.email" 
                  class="form-control"
                  required
                />
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group">
                <label for="phone">Teléfono</label>
                <input 
                  type="tel" 
                  id="phone" 
                  v-model="personalForm.phone" 
                  class="form-control"
                />
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group">
                <label for="address">Dirección</label>
                <textarea 
                  id="address" 
                  v-model="personalForm.address" 
                  class="form-control"
                  rows="2"
                ></textarea>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group">
                <label for="birthdate">Fecha de nacimiento</label>
                <input 
                  type="date" 
                  id="birthdate" 
                  v-model="personalForm.birthdate" 
                  class="form-control"
                />
              </div>
            </div>
            
            <div class="form-actions">
              <button type="button" class="btn btn-outline" @click="cancelEditingPersonal">
                Cancelar
              </button>
              <button type="submit" class="btn btn-primary">
                Guardar cambios
              </button>
            </div>
          </form>
        </div>
        
        <div class="security-settings private-card">
          <div class="card-header">
            <h3>Seguridad</h3>
            <button v-if="!editingPassword" class="btn btn-text" @click="startEditingPassword">
              <i class="fa-solid fa-key"></i>
              Cambiar contraseña
            </button>
          </div>
          
          <div v-if="!editingPassword" class="security-info">
            <div class="info-item">
              <label>Contraseña:</label>
              <span>••••••••</span>
            </div>
            <div class="info-item">
              <label>Último cambio:</label>
              <span>Hace 3 meses</span>
            </div>
          </div>
          
          <form v-else class="edit-form" @submit.prevent="changePassword">
            <div class="form-row">
              <div class="form-group">
                <label for="current-password">Contraseña actual</label>
                <div class="password-input">
                  <input 
                    :type="showPassword.current ? 'text' : 'password'" 
                    id="current-password" 
                    v-model="passwordForm.current" 
                    class="form-control"
                    required
                  />
                  <button 
                    type="button"
                    class="password-toggle"
                    @click="showPassword.current = !showPassword.current"
                  >
                    <i :class="showPassword.current ? 'fa-solid fa-eye-slash' : 'fa-solid fa-eye'"></i>
                  </button>
                </div>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group">
                <label for="new-password">Nueva contraseña</label>
                <div class="password-input">
                  <input 
                    :type="showPassword.new ? 'text' : 'password'" 
                    id="new-password" 
                    v-model="passwordForm.new" 
                    class="form-control"
                    required
                  />
                  <button 
                    type="button"
                    class="password-toggle"
                    @click="showPassword.new = !showPassword.new"
                  >
                    <i :class="showPassword.new ? 'fa-solid fa-eye-slash' : 'fa-solid fa-eye'"></i>
                  </button>
                </div>
                <div class="password-strength" v-if="passwordForm.new">
                  <div class="strength-bar">
                    <div 
                      class="strength-indicator" 
                      :style="{ width: passwordStrength.percent + '%' }"
                      :class="passwordStrength.level"
                    ></div>
                  </div>
                  <span class="strength-text">{{ passwordStrength.text }}</span>
                </div>
              </div>
            </div>
            
            <div class="form-row">
              <div class="form-group">
                <label for="confirm-password">Confirmar nueva contraseña</label>
                <div class="password-input">
                  <input 
                    :type="showPassword.confirm ? 'text' : 'password'" 
                    id="confirm-password" 
                    v-model="passwordForm.confirm" 
                    class="form-control"
                    required
                  />
                  <button 
                    type="button"
                    class="password-toggle"
                    @click="showPassword.confirm = !showPassword.confirm"
                  >
                    <i :class="showPassword.confirm ? 'fa-solid fa-eye-slash' : 'fa-solid fa-eye'"></i>
                  </button>
                </div>
                <span 
                  v-if="passwordForm.new && passwordForm.confirm && passwordForm.new !== passwordForm.confirm" 
                  class="error-message"
                >
                  Las contraseñas no coinciden
                </span>
              </div>
            </div>
            
            <div class="form-actions">
              <button type="button" class="btn btn-outline" @click="cancelEditingPassword">
                Cancelar
              </button>
              <button 
                type="submit" 
                class="btn btn-primary"
                :disabled="!canSubmitPassword"
              >
                Cambiar contraseña
              </button>
            </div>
          </form>
        </div>
        
        <div class="notification-preferences private-card">
          <div class="card-header">
            <h3>Preferencias de notificaciones</h3>
          </div>
          
          <div class="preferences-list">
            <div class="preference-item">
              <div class="preference-info">
                <h4>Recordatorios de citas</h4>
                <p>Recibe alertas antes de tus citas programadas</p>
              </div>
              <div class="toggle-switch">
                <input 
                  type="checkbox" 
                  id="appointment-reminders" 
                  v-model="preferences.appointmentReminders"
                  class="toggle-input" 
                />
                <label 
                  for="appointment-reminders" 
                  class="toggle-label"
                ></label>
              </div>
            </div>
            
            <div class="preference-item">
              <div class="preference-info">
                <h4>Recordatorios de medicación</h4>
                <p>Recibe alertas para tomar tus medicamentos a tiempo</p>
              </div>
              <div class="toggle-switch">
                <input 
                  type="checkbox" 
                  id="medication-reminders" 
                  v-model="preferences.medicationReminders"
                  class="toggle-input" 
                />
                <label 
                  for="medication-reminders" 
                  class="toggle-label"
                ></label>
              </div>
            </div>
            
            <div class="preference-item">
              <div class="preference-info">
                <h4>Actualizaciones de pedidos</h4>
                <p>Recibe notificaciones sobre el estado de tus pedidos</p>
              </div>
              <div class="toggle-switch">
                <input 
                  type="checkbox" 
                  id="order-updates" 
                  v-model="preferences.orderUpdates"
                  class="toggle-input" 
                />
                <label 
                  for="order-updates" 
                  class="toggle-label"
                ></label>
              </div>
            </div>
            
            <div class="preference-item">
              <div class="preference-info">
                <h4>Ofertas especiales</h4>
                <p>Recibe información sobre promociones y descuentos</p>
              </div>
              <div class="toggle-switch">
                <input 
                  type="checkbox" 
                  id="promotions" 
                  v-model="preferences.promotions"
                  class="toggle-input" 
                />
                <label 
                  for="promotions" 
                  class="toggle-label"
                ></label>
              </div>
            </div>
            
            <div class="preference-item">
              <div class="preference-info">
                <h4>Consejos de salud</h4>
                <p>Recibe consejos personalizados para mejorar tu salud</p>
              </div>
              <div class="toggle-switch">
                <input 
                  type="checkbox" 
                  id="health-tips" 
                  v-model="preferences.healthTips"
                  class="toggle-input" 
                />
                <label 
                  for="health-tips" 
                  class="toggle-label"
                ></label>
              </div>
            </div>
          </div>
          
          <div class="preferences-actions">
            <button class="btn btn-primary" @click="savePreferences">
              Guardar preferencias
            </button>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'ProfileSettings',
    props: {
      user: {
        type: Object,
        required: true
      }
    },
    data() {
      return {
        editingPersonal: false,
        editingPassword: false,
        personalForm: {
          name: '',
          email: '',
          phone: '',
          address: '',
          birthdate: '',
          avatar: null
        },
        passwordForm: {
          current: '',
          new: '',
          confirm: ''
        },
        showPassword: {
          current: false,
          new: false,
          confirm: false
        },
        preferences: {
          appointmentReminders: true,
          medicationReminders: true,
          orderUpdates: true,
          promotions: false,
          healthTips: true
        }
      };
    },
    computed: {
      userInitials() {
        if (!this.user || !this.user.name) return '?';
        
        return this.user.name
          .split(' ')
          .map(name => name.charAt(0).toUpperCase())
          .slice(0, 2)
          .join('');
      },
      passwordStrength() {
        const password = this.passwordForm.new;
        
        if (!password) {
          return {
            level: '',
            text: '',
            percent: 0
          };
        }
        
        // Criterios de fortaleza
        const lengthValid = password.length >= 8;
        const hasLower = /[a-z]/.test(password);
        const hasUpper = /[A-Z]/.test(password);
        const hasDigit = /\d/.test(password);
        const hasSpecial = /[!@#$%^&*(),.?":{}|<>]/.test(password);
        
        // Calcular puntaje
        let score = 0;
        if (lengthValid) score += 1;
        if (hasLower) score += 1;
        if (hasUpper) score += 1;
        if (hasDigit) score += 1;
        if (hasSpecial) score += 1;
        
        // Determinar nivel y texto
        let level, text;
        switch (score) {
          case 0:
          case 1:
            level = 'weak';
            text = 'Débil';
            break;
          case 2:
          case 3:
            level = 'medium';
            text = 'Media';
            break;
          case 4:
            level = 'strong';
            text = 'Fuerte';
            break;
          case 5:
            level = 'very-strong';
            text = 'Muy fuerte';
            break;
        }
        
        return {
          level,
          text,
          percent: (score / 5) * 100
        };
      },
      canSubmitPassword() {
        return (
          this.passwordForm.current &&
          this.passwordForm.new &&
          this.passwordForm.confirm &&
          this.passwordForm.new === this.passwordForm.confirm &&
          this.passwordStrength.percent >= 40 // Al menos contraseña media
        );
      }
    },
    created() {
      this.resetPersonalForm();
    },
    methods: {
      formatDate(dateString) {
        if (!dateString) return '';
        
        const date = new Date(dateString);
        return date.toLocaleDateString('es-ES', {
          day: '2-digit',
          month: '2-digit',
          year: 'numeric'
        });
      },
      startEditingPersonal() {
        this.resetPersonalForm();
        this.editingPersonal = true;
      },
      cancelEditingPersonal() {
        this.editingPersonal = false;
        this.resetPersonalForm();
      },
      resetPersonalForm() {
        this.personalForm = {
          name: this.user.name || '',
          email: this.user.email || '',
          phone: this.user.phone || '',
          address: this.user.address || '',
          birthdate: this.user.birthdate || '',
          avatar: this.user.avatar || null
        };
      },
      savePersonalInfo() {
        // Simular actualización de datos personales
        // En un entorno real, esto haría una llamada a la API
        this.$emit('update-profile', { ...this.personalForm });
        this.editingPersonal = false;
      },
      startEditingPassword() {
        this.passwordForm = {
          current: '',
          new: '',
          confirm: ''
        };
        this.showPassword = {
          current: false,
          new: false,
          confirm: false
        };
        this.editingPassword = true;
      },
      cancelEditingPassword() {
        this.editingPassword = false;
        this.passwordForm = {
          current: '',
          new: '',
          confirm: ''
        };
      },
      changePassword() {
        // Simular cambio de contraseña
        // En un entorno real, esto haría una llamada a la API
        alert('Contraseña actualizada correctamente');
        this.editingPassword = false;
      },
      savePreferences() {
        // Simular guardado de preferencias
        // En un entorno real, esto haría una llamada a la API
        alert('Preferencias guardadas correctamente');
      }
    }
  };
  </script>
  
  <style>
  @import '@/assets/styles/components/profile-settings.scss';
  </style>