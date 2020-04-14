<template>
  <div>
    <b-modal id="modal-list-user" hide-footer size="lg">
      <div class="ss-title">
        <h2 class="title">{{ showTitle }}</h2>
      </div>
      <div v-if="showPopup==='editUser' || showPopup==='addUser'">
        <div class="row" v-if="showPopup==='addUser'">
          <b-form-group class="col-md-6" label="ID Nhân viên">
            <b-form-input
              placeholder="VN"
              v-model="userId"
              :class="errors.has('userId') || userId === '' ? 'is-invalid' : 'is-valid'"
              name="userId"
              data-vv-as="ID Nhân viên"
              v-validate="'required'"
            ></b-form-input>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('userId') == true"
            >{{errors.first('userId')}}</span>
          </b-form-group>
          <b-form-group class="col-md-6" label="Mật khẩu">
            <b-form-input
              v-model="password"
              :class="errors.has('password') || password === '' ? 'is-invalid' : 'is-valid'"
              name="password"
              data-vv-as="Mật khẩu"
              v-validate="'required|min:6'"
            ></b-form-input>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('password') == true"
            >{{errors.first('password')}}</span>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Họ tên">
            <b-form-input
              v-model="fullName"
              :class="errors.has('fullName') || fullName === '' ? 'is-invalid' : 'is-valid'"
              name="fullName"
              data-vv-as="Họ tên"
              v-validate="'required'"
            ></b-form-input>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('fullName') == true"
            >{{errors.first('fullName')}}</span>
          </b-form-group>
          <b-form-group class="col-md-6" label="Leader">
            <b-form-select
              v-model="leaderId"
              :options="leaderList"
              :class="errors.has('leaderId') || leaderId === '' ? 'is-invalid' : 'is-valid'"
              name="leaderId"
              data-vv-as="Leader"
              v-validate="'required'"
            ></b-form-select>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('leaderId') == true"
            >{{errors.first('leaderId')}}</span>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Phân quyền">
            <b-form-select
              v-model="userTypeId"
              :options="userTypeList"
              :class="errors.has('userTypeId') || userTypeId === null ? 'is-invalid' : 'is-valid'"
              name="userTypeId"
              data-vv-as="Phân quyền"
              v-validate="'required'"
            ></b-form-select>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('userTypeId') == true"
            >{{errors.first('userTypeId')}}</span>
          </b-form-group>
          <b-form-group class="col-md-6" label="Ngày sinh">
            <b-form-datepicker
              v-model="birthDt"
              today-button
              reset-button
              close-button
              locale="vi"
              placeholder="Chọn ngày"
              :class="errors.has('birthDt') || birthDt === '' ? 'is-invalid' : 'is-valid'"
              name="birthDt"
              data-vv-as="Ngày sinh"
              v-validate="'required'"
            ></b-form-datepicker>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('birthDt') == true"
            >{{errors.first('birthDt')}}</span>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Ngày vào công ty">
            <b-form-datepicker
              v-model="joinDt"
              today-button
              reset-button
              close-button
              locale="vi"
              placeholder="Chọn ngày"
              :class="errors.has('joinDt') || joinDt === '' ? 'is-invalid' : 'is-valid'"
              name="joinDt"
              data-vv-as="Ngày vào công ty"
              v-validate="'required'"
            ></b-form-datepicker>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('joinDt') == true"
            >{{errors.first('joinDt')}}</span>
          </b-form-group>
          <b-form-group class="col-md-6" label="Email">
            <b-form-input
              type="email"
              v-model="email"
              :class="errors.has('email') || email === '' ? 'is-invalid' : 'is-valid'"
              name="email"
              data-vv-as="email"
              v-validate="'required|email'"
            ></b-form-input>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('email') == true"
            >{{errors.first('email')}}</span>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="SĐT">
            <b-form-input
              v-model="telNo"
              :class="errors.has('telNo') || telNo === '' ? 'is-invalid' : 'is-valid'"
              name="telNo"
              data-vv-as="SĐT"
              v-validate="{ required: true,max: 10, regex: /(09|01[2|6|8|9])+([0-9]{8})\b/g }"
            ></b-form-input>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('telNo') == true"
            >{{errors.first('telNo')}}</span>
          </b-form-group>
          <b-form-group class="col-md-6" label="Ngày nghỉ phép còn lại">
            <b-form-input
              v-model="restDay"
              :class="errors.has('restDay') || restDay === '' ? 'is-invalid' : 'is-valid'"
              name="restDay"
              data-vv-as="Ngày nghỉ phép"
              v-validate="'required'"
            ></b-form-input>
            <span
              class="invalid-feedback d-block"
              v-if="errors.has('restDay') == true"
            >{{errors.first('restDay')}}</span>
          </b-form-group>
        </div>
        <b-button
          @click="sendUser()"
          pill
          variant="success"
          class="ml-auto d-block"
        >{{showPopup==='editUser'?'Cập nhật': 'Thêm'}}</b-button>
      </div>
    </b-modal>
  </div>
</template>

<script>
export default {
  data () {
    return {
      reLoad: '',
      userId: '',
      password: '',
      fullName: '',
      leaderId: '',
      leaderList: [],
      userTypeId: null,
      userTypeList: [{ value: null, text: 'Chọn', disabled: true }],
      joinDt: '',
      birthDt: '',
      email: '',
      telNo: '',
      restDay: '0'
    }
  },
  props: ['userInfo', 'showPopup'],
  created () {
    this.loadData()
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
  methods: {
    sendUser () {
      this.$validator.validateAll().then(result => {
        if (result) {
          // validation passed succesfully
          let data = {
            UserId: this.userId,
            Password: this.password,
            FullName: this.fullName,
            LeaderId: this.leaderId,
            UserTypeId: this.userTypeId,
            JoinDt: new Date(this.joinDt + 'UTC'),
            BirthDt: new Date(this.birthDt + 'UTC'),
            Email: this.email,
            TelNo: this.telNo,
            RestDay: parseInt(this.restDay)
          }
          if (this.showPopup === 'addUser') {
            this.axios
              .post('api/Admin/AddUser', data)
              .then(res => {
                // console.log(res)
                if (res.data === 2) {
                  this.$emit('reloadComponent')
                  this.$swal('Ok!', 'Thêm thành công', 'success')
                } else if (res.data === 1) {
                  this.$toastr.error('Id nhân viên đã tồn tại', 'Thất bại')
                } else { this.$swal('Thất bại!', 'Vui lòng kiểm tra lại', 'error') }
              })
              .catch(err => {
                console.error(err)
              })
          }
          if (this.showPopup === 'editUser') {
            this.axios
              .post('api/Admin/EditUser', data)
              .then(res => {
                // console.log(res)
                if (res.data === true) {
                  this.$emit('reloadComponent')
                  this.$swal('Ok!', 'Sửa thành công', 'success')
                } else { this.$swal('Thất bại!', 'Vui lòng kiểm tra lại', 'error') }
              })
              .catch(err => {
                console.error(err)
              })
          }
        }
      })
    },
    formatDateTime (dttime) {
      // format : yyyy-MM-dd
      let format = dttime.split('T')
      return format[0]
    },
    loadData () {
      this.axios
        .get('api/Admin/GetAllLeader ')
        .then(res => {
          for (const key in res.data) {
            if (res.data.hasOwnProperty(key)) {
              if (
                res.data[key].userId === this.$cookies.get('userData').userId
              ) {
                this.leaderId = res.data[key].userId
              }
              this.leaderList.push({
                value: res.data[key].userId,
                text: res.data[key].fullName
              })
            }
          }
        })
        .catch(err => {
          console.log(err)
        })
      this.axios
        .get('api/Admin/GetAllUserType')
        .then(res => {
          for (const key in res.data) {
            if (res.data.hasOwnProperty(key)) {
              this.userTypeList.push({
                value: res.data[key].typeValue,
                text: res.data[key].typeValueName
              })
            }
          }
        })
        .catch(err => {
          console.log(err)
        })
    }
  },
  computed: {
    showTitle () {
      let title = ''
      if (this.showPopup === 'addUser') {
        title = 'Thêm nhân viên'
      }
      if (this.showPopup === 'editUser') {
        title = 'Sửa nhân viên'
      }
      return title
    }
  }
}
</script>

<style scoped>
</style>
