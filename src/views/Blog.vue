<template>
    <div class="about">
      <div class="about__container">
        <h1 class="about__title">Quiénes Somos</h1>
        <p class="about__description">
          En <strong>Farmed</strong>, nos dedicamos a garantizar y suministrar medicamentos a través de nuestra 
          <strong>tienda online</strong>, enfocándonos en aquellas personas con dificultades de movilidad, ya sea por 
          una <strong>urgencia inmediata</strong> o por <strong>comodidad</strong>.
        </p>
        <div class="about__content">
          <div class="about__section">
            <h2 class="about__subtitle">Nuestra Misión</h2>
            <p class="about__text">
              Nuestro objetivo es proporcionar un servicio <strong>rápido, seguro y accesible</strong> de venta de 
              medicamentos online, asegurando que cada paciente reciba lo que necesita sin salir de casa.
            </p>
          </div>
          <div class="about__section">
            <h2 class="about__subtitle">¿Cómo Funciona?</h2>
            <ul class="about__list">
              <li class="about__item">Explora nuestra tienda y encuentra los medicamentos que necesitas.</li>
              <li class="about__item">Realiza tu pedido de forma sencilla y segura.</li>
              <li class="about__item">Recibe los medicamentos en la puerta de tu hogar con entrega rápida.</li>
            </ul>
          </div>
          <div class="about__section">
            <h2 class="about__subtitle">Compromiso con la Salud</h2>
            <p class="about__text">
              En <strong>Farmed</strong>, trabajamos con farmacias autorizadas para garantizar la calidad y seguridad 
              de cada medicamento, cumpliendo con todas las regulaciones sanitarias vigentes.
            </p>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  </script>
  
  <style scoped lang="scss">
@use "@/assets/styles/variables" as *;
@use "@/assets/styles/mixins" as *;

.about {
    display: flex;
    justify-content: center;
    align-items: center;
    height: auto;
    padding: 4rem 1.5rem;
    background-color: #f9f9f9;
  
    &__container {
      background: white;
      padding: 3rem;
      border-radius: 12px;
      box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
      width: 100%;
      max-width: 800px; // Más ancho para mayor legibilidad
      text-align: center;
    }
  
    &__title {
      font-size: 2rem;
      margin-bottom: 1rem;
      color: #007bff;
    }
  
    &__description {
      font-size: 1.2rem;
      color: #333;
      margin-bottom: 2rem;
    }
  
    &__content {
      text-align: left;
    }
  
    &__section {
      margin-bottom: 2rem;
    }
  
    &__subtitle {
      font-size: 1.5rem;
      color: #007bff;
      margin-bottom: 0.5rem;
    }
  
    &__text {
      font-size: 1rem;
      color: #555;
    }
  
    &__list {
      list-style: none;
      padding: 0;
    }
  
    &__item {
      font-size: 1rem;
      color: #444;
      margin-bottom: 0.5rem;
      position: relative;
      padding-left: 1.5rem;
  
      &::before {
        content: "✔";
        position: absolute;
        left: 0;
        color: #007bff;
        font-weight: bold;
      }
    }
  }
 
  </style>
  