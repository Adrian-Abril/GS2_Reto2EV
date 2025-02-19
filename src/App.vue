<script setup lang="ts">
import { RouterView } from 'vue-router';
import Header from '@/components/Header.vue';
import Login from '@/components/Login.vue';
</script>

<template>
  <Header />
  <main>
    <RouterView />
  </main>
</template>

<style scoped lang="scss">
</style>
