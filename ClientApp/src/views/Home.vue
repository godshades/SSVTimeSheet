<template>
  <div class="home">    
    <a href="#/dang-nhap" class="mb-3 btn btn-success">Đăng xuất</a>
    <regist-time></regist-time>
  </div>
</template>

<style>
.home {
  padding-top: 100px;
}
</style>

<script>
// @ is an alias to /src
import RegistTime from './Users/RegistTime.vue'

export default {
  name: 'home',
  components: {
    RegistTime
  }
}
</script>
