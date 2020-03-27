<template>
  <div class="home">
    <div>
      <b-navbar type="dark" variant="success" class="mb-3">
        <div class="container">
          <b-navbar-nav>
            <b-nav-item href="/">Home</b-nav-item>
          </b-navbar-nav>
          <!-- Navbar dropdowns -->
          <b-navbar-nav>
            <b-nav-item-dropdown :text="userName" class="ml-auto">
              <b-dropdown-item href="#">Thông tin của bạn</b-dropdown-item>
              <b-dropdown-item v-if="permission != 1" href="/admin">Vào trang quản trị</b-dropdown-item>
              <b-dropdown-item @click="logOut()">Đăng xuất</b-dropdown-item>
            </b-nav-item-dropdown>
          </b-navbar-nav>
        </div>
      </b-navbar>
    </div>
    <regist-time :key="componentRegistTime" v-on:changeComponentEvent = "refreshComponent"></regist-time>
  </div>
</template>

<style></style>

<script>
// @ is an alias to /src
import RegistTime from './Users/RegistTime.vue'

export default {
  name: 'home',
  data () {
    return {
      componentRegistTime: 0,
      userName: this.$cookies.get('userData').name,
      permission: this.$cookies.get('userData').typeId   
    }
  },
  components: {
    RegistTime
  },
  methods: {
    logOut () {
      this.$cookies.remove('token')
      this.$cookies.remove('userData')
      this.$router.push('/dang-nhap')
    },
    refreshComponent () {
      this.componentRegistTime += 1
      // hàm thay đổi component
    }
  }
}
</script>
