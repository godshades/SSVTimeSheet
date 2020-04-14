<template>
  <div class="admin-index">
    <div class="d-flex">
      <div class="menu-left side-navbar">
        <div class="side-navbar-wrapper">
          <!-- Sidebar Header    -->
          <div
            class="sidenav-header d-flex align-items-center justify-content-center"
          >
            <!-- User Info-->
            <div class="sidenav-header-inner text-center">
              <img
                src="https://d19m59y37dris4.cloudfront.net/dashboard/1-4-7/img/avatar-7.jpg"
                alt="person"
                class="img-fluid rounded-circle mCS_img_loaded"
              />
              <h2 class="h5">{{userName}}</h2>
              <span>{{position}}</span>
            </div>
            <!-- Small Brand information, appears on minimized sidebar-->
            <div class="sidenav-header-logo">
              <a href="index.html" class="brand-small text-center">
                <strong>B</strong><strong class="text-primary">D</strong></a
              >
            </div>
          </div>
          <!-- Sidebar Navigation Menus-->
          <div class="main-menu">
            <h5 class="sidenav-heading">Main</h5>
            <ul id="side-main-menu" class="side-menu list-unstyled">
              <li>
                <a href="/">
                  <font-awesome-icon icon="home" /> Đi đến trang chủ
                </a>
              </li>
              <li>
                <a href="#">
                  <font-awesome-icon icon="check" />Yêu cầu chưa duyệt
                  <div class="badge badge-info ml-auto">{{countRequire}}</div></a
                >
              </li>
              <li>
                <a href="#">
                  <font-awesome-icon icon="table" />Yêu cầu đã duyệt
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="content-right">
        <nav class="main-header navbar navbar-expand navbar-light bg-success">
          <!-- Left navbar links -->
          <ul class="navbar-nav">
            <li class="nav-item">
              <a class="nav-link" data-widget="pushmenu" href="#"
                ><font-awesome-icon icon="bars"
              /></a>
            </li>
            <li class="nav-item d-none d-sm-inline-block">
              <a href="/admin/index" class="nav-link">Dashboard</a>
            </li>
            <li class="nav-item d-none d-sm-inline-block">
              <a @click.stop.prevent="logOut()" href="#" class="nav-link">Đăng xuất</a>
            </li>
          </ul>
          <!-- SEARCH FORM -->
          <form class="form-inline ml-3">
            <div class="input-group input-group-sm">
              <input
                class="form-control form-control-navbar"
                type="search"
                placeholder="Search"
                aria-label="Search"
              />
              <div class="input-group-append">
                <button class="btn btn-navbar" type="submit">
                  <font-awesome-icon icon="search" />
                </button>
              </div>
            </div>
          </form>
        </nav>
        <div class="content-dashboard">
          <ManagerRequirement v-on:refreshComponent="loadData()"/>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import ManagerRequirement from './ManagerRequirement.vue'
export default {
  data () {
    return {
      countRequire: '',
      userName: this.$cookies.get('userData').name,
      position: this.$cookies.get('userData').typeId === 2 ? 'Leader' : 'Manager'
    }
  },
  components: {
    ManagerRequirement
  },
  methods: {
    loadData () {
      let leaderId = this.$cookies.get('userData').userId
      let sttapprove = this.$cookies.get('userData').typeId === 2 ? 1 : 3
      this.axios
        .get('/api/RegistTime/SttApprove', {
          params: {
            leaderId: leaderId,
            sttapprove: sttapprove
          }
        })
        .then(res => {
          this.countRequire = res.data
        })
    },
    logOut () {
      this.$cookies.remove('token')
      this.$cookies.remove('userData')
      this.$router.push('/dang-nhap')
    }
  },
  created () {
    this.loadData()
  }
}
</script>
<style lang="css">
::-webkit-scrollbar {
  width: 5px;
}
/* Track */
::-webkit-scrollbar-track {
  box-shadow: inset 0 0 5px grey;
  border-radius: 10px;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background-color: #33b35a;
  border-radius: 10px;
}

.menu-left {
  position: fixed;
  top: 0;
  left: 0;
  width: 200px;
  height: 100%;
  width: 250px;
  text-align: left;
  background: #393836;
  color: #fff;
  z-index: 999;
  overflow-y: auto;
  -webkit-transition: all 0.3s;
  transition: all 0.3s;
}

.content-right {
  width: calc(100% - 250px);
  position: absolute;
  top: 0;
  right: 0;
  -webkit-transition: width 0.3s linear;
  transition: width 0.3s linear;
  background-color: #f4f7fa;
  min-height: 100vh;
  padding-bottom: 50px;
}
.side-navbar .sidenav-header {
  background: #292929;
  padding: 14px;
  margin-bottom: 10px;
}
.side-navbar .sidenav-header img {
  width: 50px;
  height: 50px;
}
.side-navbar .sidenav-header h2 {
  margin: 10px 0 0;
  text-transform: uppercase;
  letter-spacing: 0.1em;
}
.side-navbar .sidenav-header span {
  font-size: 0.8em;
  color: #666;
  display: inline-block;
}
.side-navbar a.brand-small {
  text-transform: uppercase;
  font-weight: 500;
  padding: 0;
}
.side-navbar .brand-small {
  display: none;
}
.side-navbar a.brand-small strong {
  font-size: 1.5rem;
}
.side-navbar .sidenav-heading {
  font-weight: 700;
  color: #adb5bd;
  font-size: 0.9rem;
  text-transform: uppercase;
  letter-spacing: 0.1em;
  margin-left: 10px;
  margin-top: 10px;
  margin-bottom: 10px;
}
.side-navbar li {
  position: relative;
}
.side-navbar li a {
  position: relative;
  display: inline-block;
  font-size: 0.9rem;
  font-weight: 300;
  color: #fff;
  text-decoration: none;
  width: 100%;
  padding: 12px;
}
.side-navbar svg {
  font-size: inherit;
  margin-right: 10px;
}

.side-navbar li a:focus,
.side-navbar li a:hover {
  background: #33b35a;
  color: #fff;
  text-decoration: none;
}
</style>
