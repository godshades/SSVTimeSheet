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
              <li>
                <router-link class="dropdown-item" :to="'/thong-tin-user'" tag="a">Thông tin của bạn</router-link>
              </li>
              <li v-if="permission == 4 || permission == 5 || permission == 6">
                <router-link class="dropdown-item" :to="'/admin'" tag="a">Vào trang quản trị</router-link>
              </li>
              <b-dropdown-item @click="logOut()">Đăng xuất</b-dropdown-item>
            </b-nav-item-dropdown>
          </b-navbar-nav>
        </div>
      </b-navbar>
    </div>
    <router-view></router-view>
  </div>
</template>

<style lang="css" scope>
.home {
  padding-bottom: 100px;
}
.ss-title {
  margin-bottom: 30px;
  margin-top: 30px;
}
.ss-title .title {
  text-align: center;
  font-size: 30px;
}
.vue__time-picker .custom-select.is-invalid {
  border-color: #dc3545;
  padding-right: calc(0.75em + 2.3125rem);
}
.vue__time-picker .custom-select.is-valid {
  border-color: #28a745;
  padding-right: calc(0.75em + 2.3125rem);
}
.form-control:disabled {
  cursor: not-allowed;
}
</style>

<script>
export default {
  name: 'home',
  data () {
    return {
      showComponent: 'registTime',
      userName: this.$cookies.get('userData').fullName,
      permission: this.$cookies.get('userData').userTypeId
    }
  },
  methods: {
    logOut () {
      this.$cookies.remove('token')
      this.$cookies.remove('userData')
      this.$router.push('/dang-nhap')
    }
  }
}
</script>
