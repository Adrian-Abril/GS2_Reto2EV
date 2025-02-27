
<template>
  <div class="login">
        <div class="login__container">
            <h1 class="login__title">Iniciar Sesión</h1>
            <form class="login__form">
                <div class="login__field">
                    <label for="email" class="login__label">Correo Electrónico</label>
                    <input type="email" id="email" class="login__input" placeholder="Introduce tu correo" required>
                </div>
                <div class="login__field">
                    <label for="password" class="login__label">Contraseña</label>
                    <input type="password" id="password" class="login__input" placeholder="Introduce tu contraseña" required>
                </div>
                <button type="submit" class="login__button">Acceder</button>
                <a href="#" class="login__link">¿Olvidaste tu contraseña?</a>
                <router-link to="/register" class="login__link">Registrarse</router-link>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
</script>

<style scoped lang="scss">
@use "@/assets/styles/variables" as *;
@use "@/assets/styles/mixins" as *;

// Estilos base
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: Arial, sans-serif;
}

body {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: $background-color; // Variable global
}

// Contenedor principal del login
.login {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  padding-top: 100px;

  &__container {
    width: 90%;
    max-width: 400px;
    background: #fff;
    padding: 2rem;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    text-align: center;
  }

  &__title {
    font-size: 1.8rem;
    margin-bottom: 1.5rem;
    color: $primary-color;
  }

  &__form {
    display: flex;
    flex-direction: column;
  }

  &__field {
    display: flex;
    flex-direction: column;
    margin-bottom: 1.2rem;
  }

  &__label {
    font-size: 0.9rem;
    margin-bottom: 0.3rem;
    text-align: left;
    color: #555;
  }

  &__input {
    padding: 0.8rem;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 1rem;
    transition: border 0.3s;

    &:focus {
      border-color: $primary-color;
      outline: none;
    }
  }

  &__button {
    background: $primary-color;
    color: white;
    padding: 0.8rem;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 1rem;
    transition: background 0.3s;

    &:hover {
      background: darken($primary-color, 10%);
    }
  }

  &__link {
    margin-top: 1rem;
    display: block;
    font-size: 0.9rem;
    color: $secondary-color;
    text-decoration: none;

    &:hover {
      text-decoration: underline;
    }
  }
}

// Responsive para pantallas más grandes
@media (min-width: 768px) {
  .login {
    &__container {
      padding: 2.5rem;
    }

    &__title {
      font-size: 2rem;
    }

    &__input {
      font-size: 1.1rem;
    }

    &__button {
      font-size: 1.1rem;
    }
  }
}

</style>