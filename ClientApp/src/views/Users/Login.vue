<template>
  <div class="background-login">
    <div class="login-page">
      <div class="form">
        <form class="login-form">
          <div class="border-input">
            <input type="text" v-model="userid" required />
            <div class="input-placeholder">UserId</div>
          </div>
          <div class="border-input">
            <input type="password" v-model="password" required />
            <div class="input-placeholder">Password</div>
          </div>
          <b-form-checkbox
            class="mb-3"
            v-model="rememberMe"
            value="true"
            unchecked-value="false"
            >Nhớ mật khẩu</b-form-checkbox
          >
          <button class="btn-login" type="button" @click="signIn()">
            Đăng nhập
          </button>
          <p class="message">Chưa có tài khoản? <a href="#">Đăng kí mới</a></p>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'login',
  props: {
    msg: String
  },
  data () {
    return {
      userid: '',
      password: '',
      rememberMe: false
    }
  },

  methods: {
    signIn () {
      let data = {
        UserId: this.userid,
        Password: this.password
      }
      this.axios({
        method: 'post',
        url: 'api/UserLogin/Login',
        data: data
      })
        .then(res => {
          if (res.data.statusCode === 400) {
            this.$toastr.error(
              'Id hoặc mật khẩu không chính xác',
              'Xin kiểm tra lại!'
            )
          } else {
            let token = res.data.token
            this.$cookies.set('token', token)
            this.$cookies.set('userData', res.data.userData)
            // console.log("signIn -> res.data.userData", res.data.userData)
            this.$router.push({ name: 'home' }).catch(err => {})            
            this.$toastr.success(
              `Chào mừng ${res.data.userData.name} đến với Saishunkan System`,
              'Wellcome'
            )
          }
        })
        .catch(err => {})
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
@import url(https://fonts.googleapis.com/css?family=Roboto:300);
.background-login {
  background: #2f4895; /* fallback for old browsers */
  background: linear-gradient(to left, #f3eeee, #54a3bd);
  font-family: "Roboto", sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  height: 100vh;
}
.login-page {
  width: 360px;
  padding: 8% 0 0;
  margin: auto;
}

.form {
  position: relative;
  z-index: 1;
  background: #ffffff;
  max-width: 360px;
  margin: 0 auto 100px;
  padding: 45px;
  box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);
}

.form input {
  font-family: "Roboto", sans-serif;
  outline: 0;
  width: 100%;
  border: 0;
  border-bottom: 1px solid #80868b;
  padding: 5px;
  box-sizing: border-box;
  font-size: 14px;
}

.form .btn-login {
  font-family: "Roboto", sans-serif;
  text-transform: uppercase;
  outline: 0;
  background: #3b65a1;
  width: 100%;
  border: 0;
  padding: 10px;
  color: #ffffff;
  font-size: 14px;
  cursor: pointer;
}

.form .btn-login:hover,
.form .btn-login:active,
.form .btn-login:focus {
  background: #304996;
}

.form .message {
  margin: 15px 0 0;
  color: #b3b3b3;
  font-size: 12px;
  text-align: center;
}

.form .message a {
  color: #3b65a1;
  text-decoration: none;
}

.form .register-form {
  display: none;
}
input {
  color: #3b65a1;
}
.login-form .border-input {
  position: relative;
  margin-bottom: 30px;
  transition: all 0.3s ease;
}
.login-form .border-input::after {
  content: "";
  display: block;
  position: absolute;
  left: 50%;
  bottom: 0;
  transform: translateX(-50%);
  height: 3px;
  width: 0;
  background-color: #3b65a1;
  transition: all 0.3s ease;
}
.login-form .border-input:focus-within::after {
  width: 100%;
}
.border-input .input-placeholder {
  position: absolute;
  pointer-events: none;
  color: #80868b;
  left: 5px;
  bottom: 5px;
  transition: all 0.3s ease;
}
.login-form input:focus + .input-placeholder,
.login-form input:valid + .input-placeholder {
  bottom: 35px;
  font-size: 12px;
  color: #3b65a1;
  font-weight: bold;
}
</style>
