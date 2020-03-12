<template>
  <div class="login">
    <div class="container col-md-3">
        <img class="mb-4" src="@/assets/logo saishunkan.jpg" />
        <h1 class="h3 mb-3">Đăng nhập</h1>
        <div class="col-md-12 mb-3">
          <input type="text" class="form-control" placeholder="Tên đăng nhập" v-model="username" />
        </div>
        <div class="col-md-12 mb-3">
          <input type="password" class="form-control" placeholder="Mật khẩu" v-model="password" />
        </div>
        <button class="btn btn-success" type="submit" @click="signIn">Sign in</button>
      <a href="#" target="_blank" rel="noopener">Đăng kí tài khoản mới</a>.
    </div>
  </div>
</template>

<script>
  import axios from 'axios'

  export default {
    name: 'login',
    props: {
      msg: String
    },
    data() {
      return {
        username: '',
        password: ''
      }
    },
    methods: {
      signIn: function () {
        var ttt = JSON.stringify({
          AA: this.username
          //password: this.password
        });
        let frm = new FormData();
        frm.append("aa", "tesst");
        axios({
          method: 'post',
          url: '/loginUser/loginUserss',
          params: { 'vinhnc': 'bbb', 'aa': 'cc' },
          headers: {
            'Content-Type': 'application/json'
          }
        })
          .then(function (response) {
            console.log(response)
          })
          .catch(function (error) {
            console.log(error);
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  img {
    width: 60px;
  }

  h3 {
    margin: 40px 0 0;
  }

  ul {
    list-style-type: none;
    padding: 0;
  }

  li {
    display: inline-block;
    margin: 0 10px;
  }

  a {
    color: #42b983;
  }
</style>
