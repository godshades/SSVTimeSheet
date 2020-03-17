<template>
  <div class="home">
    <Login msg="Login"/>
  </div>
</template>

<style>
  body {
    background: #2f4895; /* fallback for old browsers */
    background: linear-gradient(to left, #f3eeee, #54a3bd);
    font-family: "Roboto", sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
  }
</style>

<script>
// @ is an alias to /src
import Login from './Users/Login.vue'

export default {
  name: 'home',
  components: {
    Login
  }
}
</script>
