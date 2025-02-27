<template>
    <div class="product-card">
      <img :src="image" :alt="name" />
      <h3>{{ name }}</h3>
      <p class="price">{{ price }}</p>
      <button>Ver más</button>
    </div>
  </template>
  
  <script setup lang="ts">
  defineProps<{
    name: string;
    price: string;
    image: string;
  }>();
  </script>
  
  <style scoped lang="scss">
@use "../assets/styles/variables" as *;
@use "../assets/styles/mixins" as *;

.product-card {
    background: white;
    padding: 15px;
    border-radius: 10px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    text-align: center;
    width: 180px;
  
    img {
      width: 100%;
      height: auto;
      border-radius: 8px;
    }
  
    h3 {
      font-size: 16px;
      margin: 10px 0;
    }
  
    .price {
      font-size: 14px;
      font-weight: bold;
      color: #007bff;
    }
  
    button {
      margin-top: 10px;
      padding: 8px 12px;
      background-color: #007bff;
      color: white;
      border: none;
      cursor: pointer;
      border-radius: 5px;
  
      &:hover {
        background-color: #0056b3;
      }
    }
  }
  
  </style>
  