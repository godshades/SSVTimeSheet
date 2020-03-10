<template>
  <div class="send-mail">
    <div class="container">
      <div class="py-5 text-center">
        <img v-bind:src="img_logo" style="width:50px" />
        <h4 class="mb-3">Mẫu gửi báo cáo hằng ngày</h4>
      </div>
      <div class="row">
        <div class="col-md-6 mb-3">
          <label for="firstName">Tên :</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"></span>
            </div>
            <input type="text" class="form-control" id="firstName" placeholder="" value="" required>
          </div>
        </div>
        <div class="col-md-6 mb-3">
          <label for="lastName">Địa chỉ mail :</label>
          <input type="text" class="form-control" id="lastName" placeholder="" value="" required readonly>
        </div>
        <div class="col-md-12 mb-3">
          <label for="username">Mail người nhận :</label>
          <input type="text" class="form-control" id="username" placeholder="" required>
        </div>
        <div class="col-md-12 mb-3">
          <label for="email">Nội dung công việc hôm nay :<span class="text-muted"></span></label>
          <input type="email" class="form-control" id="email" placeholder="" required>
        </div>
        <div class="col-md-9 mb-3">
          <label for="address">Tiến độ công việc <span class="text-muted">(Task-Percent)</span> :</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"></span>
            </div>
            <input type="text" class="form-control" id="address2" placeholder="" required>
          </div>
        </div>
        <div class="col-md-3 mb-3">
          <span class="btn btn-success" id="btnAdd">Thêm</span>
          <span class="btn btn-danger" id="btnDel">Xóa</span>
        </div>
        <div class="col-md-12 mb-3">
          <label for="username">Mail người nhận :</label>
          <input type="text" class="form-control" id="username" placeholder="" required>
        </div>
        <div class="col-md-12 mb-3">
          <label for="address2">Dự kiến kế hoạch tiếp theo : </label>
          <input type="text" class="form-control" id="address2" placeholder="" required>
        </div>
      </div>
      <div class="form-group text-center">
        <button class="btn btn-success" type="submit" v-on:click="sendMail">Gửi mail</button>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios'
  export default {
    data() {
      return {
        img_logo: 'https://timviec365.vn/pictures/2019/11/27/avatar185497.jpg'
      }
    },
    methods: {
      sendMail: function () {
        axios({
          method: 'post',
          url: '/sendMail/sendDataToMail',
        })
          .then(function (response) {
            console.log(response);
          })
          .catch(function (error) {
            console.log(error);
          });
      }
    }
  }
</script>
