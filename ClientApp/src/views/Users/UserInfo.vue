<template>
  <div class="container">
    <div class="d-flex mt-3 justify-content-center">
      <b-button
        v-if="showDiv === 'detailInfo'"
        @click="changeDiv('editInfo')"
        variant="success"
        class="mr-3"
      >Edit</b-button>
      <b-button
        v-if="showDiv==='editInfo'"
        @click="changeDiv('detailInfo')"
        variant="success"
      >Detail</b-button>
    </div>
    <div class="ss-title">
      <h2 class="title">{{ showTitle }}</h2>
    </div>
    <div v-if="showDiv==='editInfo'">
      <b-form-checkbox
        v-model="showInputPass"
        value="true"
        unchecked-value="false"
      >Thay đổi mật khẩu</b-form-checkbox>
      <div class="row" v-if="showInputPass==='true'">
        <b-form-group class="col-md-6" label="Mật khẩu">
          <b-form-input
            :class="errors.has('password') || password === '' ? 'is-invalid' : 'is-valid'"
            name="password"
            data-vv-as="Mật khẩu"
            v-validate="'required|min:6'"
            placeholder
            v-model="password"
            type="password"
            ref="password"
          ></b-form-input>
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('password') == true"
          >{{errors.first('password')}}</span>
        </b-form-group>
        <b-form-group class="col-md-6" label="Nhập lại mật khẩu">
          <b-form-input
            :class="errors.has('rePassword') || rePassword === '' ? 'is-invalid' : 'is-valid'"
            v-validate="'required|confirmed:password'"
            v-model="rePassword"
            type="password"
            name="rePassword"
            data-vv-as="Nhập lại mật khẩu"
          ></b-form-input>
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('rePassword') == true"
          >{{errors.first('rePassword')}}</span>
        </b-form-group>
      </div>
      <div class="row">
        <b-form-group class="col-md-6" label="Họ tên">
          <b-form-input
            :class="errors.has('fullName') || fullName === '' ? 'is-invalid' : 'is-valid'"
            data-vv-as="Họ tên"
            v-validate="'required'"
            v-model="fullName"
            name="fullName"
          ></b-form-input>
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('fullName') == true"
          >{{errors.first('fullName')}}</span>
        </b-form-group>
        <b-form-group class="col-md-6" label="Ngày sinh">
          <b-form-datepicker
            :class="errors.has('birthDt') || birthDt === '' ? 'is-invalid' : 'is-valid'"
            v-validate="'required'"
            data-vv-as="Ngày sinh"
            name="birthDt"
            v-model="birthDt"
            today-button
            reset-button
            close-button
            locale="vi"
            placeholder="Chọn ngày"
          ></b-form-datepicker>
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('birthDt') == true"
          >{{errors.first('birthDt')}}</span>
        </b-form-group>
      </div>
      <div class="row">
        <b-form-group class="col-md-6" label="Email">
          <b-form-input
            :class="errors.has('email') || email === '' ? 'is-invalid' : 'is-valid'"
            v-validate="'required|email'"
            type="email"
            v-model="email"
            name="email"
          ></b-form-input>
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('email') == true"
          >{{errors.first('email')}}</span>
        </b-form-group>
        <b-form-group class="col-md-6" label="SĐT">
          <b-form-input
            :class="errors.has('telNo') || telNo === '' ? 'is-invalid' : 'is-valid'"
            data-vv-as="SĐT"
            v-validate="{ required: true,max: 10, regex: /(09|01[2|6|8|9])+([0-9]{8})\b/g }"
            name="telNo"
            v-model="telNo"
          ></b-form-input>
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('telNo') == true"
          >{{errors.first('telNo')}}</span>
        </b-form-group>
      </div>

      <b-button pill variant="success" @click="updateUser()" class="ml-auto d-block">Cập nhật</b-button>
    </div>
    <div v-if="showDiv === 'detailInfo'">
      <div class="content-details table-responsive">
        <table class="table table-striped table-hover">
          <tbody>
            <tr>
              <th>Id</th>
              <td>{{ userInfo.userId }}</td>
            </tr>
            <tr>
              <th>Tên</th>
              <td>{{ userInfo.fullName }}</td>
            </tr>
            <tr>
              <th>Leader</th>
              <td>{{ userInfo.leaderName }}</td>
            </tr>
            <tr>
              <th>Chức vụ</th>
              <td>{{ userInfo.userTypeName }}</td>
            </tr>
            <tr>
              <th>Ngày tham gia công ty</th>
              <td>{{ joinDt }}</td>
            </tr>
            <tr>
              <th>Ngày sinh</th>
              <td>{{ birthDt }}</td>
            </tr>
            <tr>
              <th>Email</th>
              <td>{{ userInfo.email }}</td>
            </tr>
            <tr>
              <th>SDT</th>
              <td>{{ userInfo.telNo }}</td>
            </tr>
            <tr>
              <th>Ngày nghỉ phép còn lại</th>
              <td>{{ userInfo.restDay }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data () {
    return {
      showDiv: 'detailInfo',
      showInputPass: 'false',
      userInfo: '',
      userId: '',
      password: '',
      rePassword: '',
      fullName: '',
      leaderId: '',
      leaderList: [],
      userTypeId: null,
      userTypeList: [{ value: null, text: 'Chọn', disabled: true }],
      joinDt: '',
      birthDt: '',
      email: '',
      telNo: ''
    }
  },
  watch: {
    userInfo () {
      this.userId = this.userInfo.userId
      this.fullName = this.userInfo.fullName
      this.leaderId = this.userInfo.leaderId
      this.userTypeId = this.userInfo.userTypeId
      this.joinDt = this.formatDateTime(this.userInfo.joinDt)
      this.birthDt = this.formatDateTime(this.userInfo.birthDt)
      this.email = this.userInfo.email
      this.telNo = this.userInfo.telNo
      this.restDay = this.userInfo.restDay
    }
  },
  created () {
    this.loadData()
  },
  methods: {
    changeDiv (name) {
      this.showDiv = name
    },
    formatDateTime (dttime) {
      // format : yyyy-MM-dd
      let format = dttime.split('T')
      return format[0]
    },
    updateUser () {
      this.$validator.validateAll().then(result => {
        if (result) {
          // validation passed succesfully
          let data = {
            UserId: this.userId,
            FullName: this.fullName,
            BirthDt: new Date(this.birthDt),
            Email: this.email,
            TelNo: this.telNo
          }
          this.axios
            .post('api/Client/UpdateUser', data)
            .then(res => {
              // console.log(res)
              if (res.data === true) {
                this.$swal('Ok!', 'Sửa thành công', 'success')
                this.showDiv = 'detailInfo'
              } else this.$swal('Thất bại!', 'Vui lòng kiểm tra lại', 'error')
            })
            .catch(err => {
              console.error(err)
            })          
        }
      })
    },
    loadData () {
      this.axios
        .get('admin/api/Admin/GetUserById', {
          params: {
            userId: this.$cookies.get('userData').userId
          }
        })
        .then(res => {
          this.userInfo = res.data
        })
    }
  },
  computed: {
    showTitle () {
      let title = ''
      if (this.showDiv === 'detailInfo') {
        title = 'Thông tin của bạn'
      }
      if (this.showDiv === 'editInfo') {
        title = 'Chỉnh sửa thông tin'
      }
      return title
    }
  }
}
</script>

<style lang="scss" scope>
</style>
