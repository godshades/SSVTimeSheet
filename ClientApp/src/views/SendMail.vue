<template>
  <div class="send-mail">
    <div class="container" style="text-align:left">
      <div class="py-5 text-center">
        <img src="@/assets/logo saishunkan.jpg" style="width:60px" />
        <h4 class="mb-3">Mẫu gửi báo cáo hằng ngày</h4>
      </div>
      <div class="row">
        <div class="col-md-12 mb-3">
          <p v-if="errors.length">
            <ul>
              <li style="color:red" v-for="error in errors">{{ error }}</li>
            </ul>
          </p>
        </div>
        <div class="col-md-6 mb-3">
          <label for="firstName">Tên :</label>
          <input type="text" class="form-control" v-model="usersname" />
        </div>
        <div class="col-md-6 mb-3">
          <label for="lastName">Địa chỉ mail :</label>
          <input type="text" class="form-control" v-model="mail" />
        </div>
        <div class="col-md-12 mb-3">
          <label for="username">Mail người nhận :</label>
          <input type="text" class="form-control" v-model="mailsender" />
        </div>
        <div class="col-md-12 mb-3">
          <label for="username">Mật khẩu - bảo mật cao nhé :))</label>
          <input type="password" class="form-control" v-model="password" />
        </div>
        <div class="col-md-12 mb-3">
          <label for="email">Nội dung công việc hôm nay :</label>
          <input type="email" class="form-control" v-model="content" />
        </div>
        <div class="col-md-12 mb-3">
          <label for="address">
            Tiến độ công việc
            <span class="text-muted">(VD: 1 Test 50%)</span> :
          </label>
          <textarea class="form-control" aria-label="With textarea" v-model="task"></textarea>
        </div>
        <div class="col-md-12 mb-3">
          <label for="username">Vấn đề phát sinh :</label>
          <input type="text" class="form-control" v-model="err" />
        </div>
        <div class="col-md-12 mb-3">
          <label for="address2">Dự kiến kế hoạch tiếp theo :</label>
          <input type="text" class="form-control" v-model="contentTm" />
        </div>
      </div>
      <div class="form-group text-center">
        <button class="btn btn-success" type="submit" @click="sendMail">Gửi mail</button>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios'

  export default {
    name: 'send-mail',
    data() {
      return {
        errors: [],
        usersname: '',
        mail: '',
        password: '',
        mailsender: '',
        content: '',
        task: '',
        err: '',
        contentTm: ''
      }
    },
    methods: {
      sendMail: function () {
        this.errors = [];

        if (!this.usersname) {
          this.errors.push('Tên chưa nhập ');
        }
        if (!this.mail) {
          this.errors.push('Mail chưa có thông tin ');
        }
        if (!this.password) {
          this.errors.push('Chưa có pass word ');
        }
        if (!this.mailsender) {
          this.errors.push('Mail người nhận chưa nhập ');
        }
        if (this.mail && this.mailsender && this.mail == this.mailsender) {
          this.errors.push('Mail người gửi và người nhận phải khác nhau')
        }
        if (!this.content) {
          this.errors.push('Nội dung chưa nhập ');
        }
        if (!this.task) {
          this.errors.push('Tiến độ công việc chưa nhập ');
        }
        if (!this.contentTm) {
          this.errors.push('Kế hoạch tiếp theo chưa nhập ');
        }
        if (this.usersname && this.mail && this.password && this.mailsender && this.content && this.task && this.contentTm && this.mail != this.mailsender) {
          var users = JSON.stringify({
            Name: this.usersname,
            Mail: this.mail,
            Pass: this.password,
            Mailsender: this.mailsender,
            Content: this.content,
            Percent: this.task,
            Err: this.err,
            Tmcontent: this.contentTm
          });
          axios({
            method: 'post',
            url: '/sendMail/sendDataToMail',
            data: users,
            headers: {
              'Content-Type': 'application/json'
            }
          })
            .then(function (response) {
              if (response.data == true) {
                alert('Gửi mail thành công')
              } else {
                alert('Gửi mail thất bại, kiểm tra lại dữ liệu')               
              }
            })
            .catch(function (error) {
              console.log(error);
            });
        }
      }
    }
  }

</script>
