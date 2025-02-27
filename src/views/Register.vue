<template>
    <div class="register">
      <div class="register__container">
        <h1 class="register__title">Crear Cuenta</h1>
        <form class="register__form">
          <div class="register__field">
            <label for="name" class="register__label">Nombre</label>
            <input type="text" id="name" class="register__input" placeholder="Introduce tu nombre" required>
          </div>
          <div class="register__field">
            <label for="surname" class="register__label">Apellidos</label>
            <input type="text" id="surname" class="register__input" placeholder="Introduce tus apellidos" required>
          </div>
          <div class="register__field">
            <label for="email" class="register__label">Correo Electrónico</label>
            <input type="email" id="email" class="register__input" placeholder="Introduce tu correo" required>
          </div>
          <div class="register__field">
            <label for="password" class="register__label">Contraseña</label>
            <input type="password" id="password" class="register__input" placeholder="Introduce tu contraseña" required>
          </div>
          <div class="register__field">
            <label for="confirm-password" class="register__label">Confirmar Contraseña</label>
            <input type="password" id="confirm-password" class="register__input" placeholder="Repite tu contraseña" required>
          </div>
          <button type="submit" class="register__button">Registrarse</button>
          <router-link to="/login" class="register__link">¿Ya tienes cuenta? Inicia sesión</router-link>
        </form>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  </script>
  
  <style scoped lang="scss">
@use "@/assets/styles/variables" as *;
@use "@/assets/styles/mixins" as *;

.register {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f9f9f9;

  &__container {
    background: white;
    padding: 3rem;
    border-radius: 12px;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
    width: 100%;
    max-width: 500px; // Tamaño de la caja
    text-align: center;
  }

  &__title {
    font-size: 2rem;
    margin-bottom: 1.5rem;
    color: #333;
  }

  &__form {
    display: flex;
    flex-direction: column;
    gap: 1.2rem;
  }

  &__field {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
  }

  &__label {
    font-size: 1rem;
    color: #555;
    margin-bottom: 0.4rem;
    width: 100%;
    max-width: 350px; // Alineado con los inputs
    text-align: left; // Asegura alineación con el input
  }

  &__input {
    width: 100%;
    max-width: 350px; // Tamaño estándar fijo
    padding: 0.8rem;
    border: 1px solid #ccc;
    border-radius: 6px;
    font-size: 1rem;
  }

  &__button {
    background: #007bff;
    color: white;
    padding: 0.9rem;
    border: none;
    border-radius: 6px;
    font-size: 1rem;
    cursor: pointer;
    transition: background 0.3s ease;
    max-width: 350px;
    margin: 0 auto;

    &:hover {
      background: #0056b3;
    }
  }

  &__link {
    display: block;
    margin-top: 1rem;
    font-size: 1rem;
    color: #007bff;
    text-decoration: none;

    &:hover {
      color: #0056b3;
    }
  }
}

  </style>
  