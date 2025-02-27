<template>
  <header class="header">
    <div class="container">
      <div class="logo">
        <img src="/src/assets/images/1.png" alt="VOLUNMED Logo" class="logo__image" />
      <div>
          <h1>FARMED</h1>
          <span>Somos farmacéuticos</span>
        </div>
      </div>
      <nav class="nav">
        <ul>
          <li><router-link to="/">Categorías</router-link></li>
          <li><router-link to="/medicamentos">Medicamentos</router-link></li>
          <li><router-link to="/promociones">Promociones</router-link></li>
          <li><router-link to="/novedades">Novedades</router-link></li>
          <li><router-link to="/oportunidades">Oportunidades</router-link></li>
          <li><router-link to="/marcas">Marcas</router-link></li>
          <li><router-link to="/blog">Blog</router-link></li>
        </ul>
      </nav>
      <div class="actions">
        <router-link to="/login" color="primary" block class="login">Iniciar sesión</router-link>
        <button class="cart"></button>
      </div>
    </div>
  </header>
</template>

<script setup lang="ts">
</script>

<style scoped lang="scss">
@use "../assets/styles/variables" as *;
@use "../assets/styles/mixins" as *;

.header {
  background-color: #ffffff;
  padding: 15px 0;
  box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);

  .container {
    width: 90%;
    max-width: 1200px;
    margin: 0 auto;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  .logo {
    display: flex;
    align-items: center;
    gap: 10px;

    &__image {
      width: 100px;
      height: auto;
    }

    h1 {
      font-size: 24px;
      font-weight: bold;
      color: #333;
      margin: 0;
    }

    span {
      font-size: 14px;
      color: #777;
    }
  }

  .nav {
    ul {
      list-style: none;
      display: flex;
      gap: 20px;
      padding: 0;
      margin: 0;
      flex-grow: 1;

      li {
        a {
          text-decoration: none;
          font-size: 16px;
          color: #333;
          transition: color 0.3s ease;

          &:hover {
            color: #007bff;
          }
        }
      }
    }
  }

  .actions {
    display: flex;
    align-items: center;
    gap: 15px;

    .login {
      background-color: #007bff;
      color: white;
      font-size: 14px;
      padding: 10px 15px;
      border: none;
      cursor: pointer;
      border-radius: 5px;
      transition: background 0.3s ease;

      &:hover {
        background-color: #0056b3;
      }
    }

    .cart {
      width: 30px;
      height: 30px;
      background-size: cover;
      border: none;
      cursor: pointer;
    }
  }
}

</style>
