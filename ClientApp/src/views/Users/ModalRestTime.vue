<template>
  <div>
    <b-modal id="register-time" hide-footer size="lg">
      <div class="ss-title">
        <h2 class="title">Đăng ký lịch tại công ty</h2>
      </div>
      <div>
        <div class="row">
          <b-form-group
            class="col-md-6"
            label="Phân loại thời gian"
            label-for="classifyTime"
          >
            <b-form-select
              v-model="classifySelected"
              :options="classifyTime"
              :state="validateRequired(classifySelected)"
            ></b-form-select>
          </b-form-group>
          <b-form-group class="col-md-6" label="Người nhận">
            <b-form-select
              v-model="leadSelected"
              :options="LeaderSelect"
              :state="validateRequired(leadSelected)"
            ></b-form-select>
          </b-form-group>
        </div>
        <div v-if="classifySelected != 2">
          <div class="row">
            <b-form-group class="col-md-6" label="Giờ bắt đầu">
              <vue-timepicker
                v-model.lazy="startTime"
                :input-class="{
                  'custom-select': true,
                  'is-invalid':
                    !validateRequired(startTime) || handleShortTime == 0,
                  'is-valid':
                    validateRequired(startTime) && handleShortTime != 0
                }"
                hide-clear-button
                placeholder="Chọn giờ"
              >
              </vue-timepicker>
            </b-form-group>
            <b-form-group class="col-md-6" label="Giờ kết thúc">
              <vue-timepicker
                v-model.lazy="endTime"
                :input-class="{
                  'custom-select': true,
                  'is-invalid':
                    !validateRequired(endTime) || handleShortTime == 0,
                  'is-valid': validateRequired(endTime) && handleShortTime != 0
                }"
                hide-clear-button
                placeholder="Chọn giờ"
              ></vue-timepicker>
            </b-form-group>
          </div>
          <div class="row">
            <b-form-group class="col-md-6" label="Số ngày làm">
              <b-form-input
                v-model.lazy="handleShortTime"
                disabled
              ></b-form-input>
            </b-form-group>

            <b-form-group class="col-md-6" label="Ngày đăng ký làm">
              <b-form-input :value="dataDate" disabled></b-form-input>
            </b-form-group>
          </div>
          <b-form-group label="Ghi chú">
            <b-form-textarea
              v-model="noteTime"
              placeholder="Viết vào đây..."
              rows="3"
              max-rows="6"
            ></b-form-textarea>
          </b-form-group>

          <b-button
            @click="sendWorkTime()"
            pill
            variant="success"
            class="ml-auto d-block"
            >Gửi</b-button
          >
        </div>
      </div>
      <div v-if="classifySelected == 2">
        <div class="row">
          <b-form-group class="col-md-6" label="Giờ bắt đầu">
            <vue-timepicker
              v-model.lazy="startTime"
              :input-class="{
                'custom-select': true,
                'is-invalid':
                  !validateRequired(startTime) || handleShortTime == 0,
                'is-valid': validateRequired(startTime) && handleShortTime != 0
              }"
              hide-clear-button
              placeholder="Chọn giờ"
            >
            </vue-timepicker>
          </b-form-group>
          <b-form-group class="col-md-6" label="Giờ kết thúc">
            <vue-timepicker
              v-model.lazy="endTime"
              :input-class="{
                'custom-select': true,
                'is-invalid':
                  !validateRequired(endTime) || handleShortTime == 0,
                'is-valid': validateRequired(endTime) && handleShortTime != 0
              }"
              hide-clear-button
              placeholder="Chọn giờ"
            ></vue-timepicker>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Ngày bắt đầu">
            <b-form-datepicker
              :value="dataDate"
              locale="vi"
              disabled
            ></b-form-datepicker>
          </b-form-group>
          <b-form-group class="col-md-6" label="Ngày kết thúc">
            <b-form-datepicker
              v-model="endDate"
              :state="validateRequired(endDate) && handleLongTime !== '0'"
              today-button
              reset-button
              close-button
              locale="vi"
              placeholder="Chọn ngày"
            ></b-form-datepicker>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Số ngày nghỉ">
            <b-form-input v-model.lazy="handleLongTime" disabled></b-form-input>
          </b-form-group>
          <b-form-group class="col-md-6" label="Lý do nghỉ" label-for="Reason">
            <b-form-select
              v-model="reasonSelected"
              :options="reasonList"
              :state="validateRequired(reasonSelected)"
            ></b-form-select>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Họ tên người liên hệ">
            <b-form-input
              v-model="nameContact"
              :state="validateRequired(nameContact)"
            ></b-form-input>
          </b-form-group>
          <b-form-group class="col-md-6" label="SĐT người liên hệ">
            <b-form-input
              v-model.lazy="phoneContact"
              :state="
                validateRequired(phoneContact) &&
                  checkPhone(phoneContact) == true
              "
            ></b-form-input>
          </b-form-group>
        </div>

        <b-form-group label="Ghi chú">
          <b-form-textarea
            v-model="noteTime"
            placeholder="Viết vào đây..."
            rows="3"
            max-rows="6"
          ></b-form-textarea>
        </b-form-group>
        <b-button
          @click="sendRestTime()"
          pill
          variant="success"
          class="ml-auto d-block"
          >Gửi</b-button
        >
      </div>
    </b-modal>
  </div>
</template>

<script>
import VueTimepicker from 'vue2-timepicker'
import 'vue2-timepicker/dist/VueTimepicker.css'
export default {
  components: {
    VueTimepicker
  },
  props: {
    dataDate: {
      type: String
    }
  },
  data () {
    return {
      leadSelected: null,
      LeaderSelect: [],
      classifySelected: null,
      classifyTime: [
        { value: null, text: 'Chọn', disabled: true },
        { value: 1, text: 'Làm thêm' },
        { value: 2, text: 'Xin nghỉ' }
      ],
      reasonSelected: 0,
      reasonList: [{ value: 0, text: 'Chọn', disabled: true }],
      nameContact: '',
      phoneContact: '',
      startTime: '',
      endTime: '',
      time: '',
      endDate: '',
      restDate: '',
      noteTime: ''
    }
  },
  created () {
    this.loadData()
  },
  watch: {
    dataDate: function () {
      this.endDate = this.dataDate
    }
  },
  methods: {
    loadData () {
      let typeId = this.$cookies.get('userData').typeId
      let leadUser = this.$cookies.get('userData').leadUser
      this.axios
        .get('/api/RegistTime/GetAllLeader', {
          params: {
            typeId: typeId
          }
        })
        .then(res => {
          for (const key in res.data) {
            if (res.data.hasOwnProperty(key)) {
              if (res.data[key].userId === leadUser) {
                this.leadSelected = leadUser
              }
              this.LeaderSelect.push({
                value: res.data[key].userId,
                text: res.data[key].name
              })
            }
          }
        })
        .catch(err => {
          console.error(err)
        })
      this.axios.get('/api/RegistTime/GetReason').then(res => {
        for (const key in res.data) {
          if (res.data.hasOwnProperty(key)) {
            this.reasonList.push({
              value: res.data[key].typeId,
              text: res.data[key].typeName
            })
          }
        }
      })
    },
    validateRequired (name) {
      if (name != null && name !== '' && name != 0) {
        return true
      } else return false
    },
    sendAxiosTime (requestUrl, data) {
      this.axios({
        method: 'post',
        url: requestUrl,
        data: data
      })
        .then(res => {
          if (res.data === false) {
            this.$toastr.error(
              'Thêm thất bại, vui lòng kiểm tra lại',
              'Lỗi rồi!'
            )
          } else {
            this.$emit('changComponent') // dùng sự kiện để gọi hàm refresh ở component cha
            this.$toastr.success('Thêm thành công', 'Ngon lành')
          }
        })
        .catch(err => {})
    },
    sendWorkTime () {
      let data = {
        UserId: this.$cookies.get('userData').userId,
        LeaderId: this.leadSelected,
        ClassifyTime: this.classifySelected,
        StartTime: new Date(this.dataDate + ',' + this.startTime + ' UTC'),
        EndTime: new Date(this.dataDate + ',' + this.endTime + ' UTC'),
        Time: this.time,
        Note: this.noteTime
      }
      let requestUrl = '/api/RegistTime/InsertTime'
      // if (
      //   this.leadSelected === null ||
      //   this.classifySelected === null ||
      //   this.startTime === '' ||
      //   this.endTime === ''
      // ) {
      //   this.$toastr.error(
      //     'Vui lòng chọn đầy đủ các trường',
      //     'Xin kiểm tra lại !!!'
      //   )
      // } else if (this.handleShortTime === 0) {
      //   this.$toastr.error(
      //     'Giờ kết thúc phải lớn hơn giờ bắt đầu',
      //     'Xin kiểm tra lại !!!'
      //   )
      // } else if (this.classifySelected !== 1) {
      //   if (
      //     this.reasonSelected === 0 ||
      //     this.nameContact === '' ||
      //     this.phoneContact === ''
      //   ) {
      //     this.$toastr.error(
      //       'Vui lòng chọn đầy đủ các trường',
      //       'Xin kiểm tra lại !!!'
      //     )
      //   } else if (this.checkPhone(this.phoneContact) === false) {
      //     this.$toastr.error(
      //       'SDT không đúng định dạng',
      //       'Xin kiểm tra lại !!!'
      //     )
      //   } else {
      //     this.sendAxiosTime(requestUrl, data)
      //   }
      // } else {
      this.sendAxiosTime(requestUrl, data)
      // }
    },
    sendRestTime () {
      let data = {
        UserId: this.$cookies.get('userData').userId,
        LeaderId: this.leadSelected,
        ClassifyTime: this.classifySelected,
        StartTime: new Date(this.dataDate + ',' + this.startTime + ' UTC'),
        EndTime: new Date(this.endDate + ',' + this.endTime + ' UTC'),
        // Khởi tạo ngày tháng rồi cộng 2 chuỗi string ngày với giờ lại
        ReasonId: this.reasonSelected,
        NameContact: this.nameContact,
        PhoneContact: this.phoneContact,
        Time: this.restDate,
        Note: this.noteTime
      }
      let requestUrl = '/api/RegistTime/InsertTime'
      if (
        this.leadSelected === null ||
        this.classifySelected === null ||
        this.endDate === ''
      ) {
        this.$toastr.error(
          'Vui lòng chọn đầy đủ các trường',
          'Xin kiểm tra lại !!!'
        )
      } else if (this.handleLongTime === '0') {
        this.$toastr.error(
          'Ngày kết thúc phải lớn hơn ngày bắt đầu',
          'Xin kiểm tra lại !!!'
        )
      } else if (this.checkPhone(this.phoneContact) === false) {
        this.$toastr.error('SDT không đúng định dạng', 'Xin kiểm tra lại !!!')
      } else {
        this.sendAxiosTime(requestUrl, data)
      }
    },
    handleTime (starttime, endtime) {
      let arrStartTime = starttime.split(':')
      let arrEndTime = endtime.split(':')
      let tempWorkTime =
        (parseInt(arrEndTime[0]) * 60 +
          parseInt(arrEndTime[1]) -
          (parseInt(arrStartTime[0]) * 60 + parseInt(arrStartTime[1]))) /
        60
      // số giờ * 60 + số phút để chuyển sang số phút rồi trừ cho nhau rồi /60 để trả về số giờ
      let resultTime = Math.round((tempWorkTime / 8) * 1000) / 1000
      // Chia cho 8 để trả ra số ngày(1 ngày làm 8 tiếng) làm tròn 2 chữ số sau dấu phẩy
      if (isNaN(resultTime) || resultTime <= 0) {
        return 0
      } else {
        return resultTime
      }
    },
    checkPhone (num) {
      let regex = /(09|01[2|6|8|9])+([0-9]{8})\b/g
      if (regex.test(num) === false) {
        return false
      } else return true
    }
  },
  computed: {
    handleShortTime: function () {
      this.time = this.handleTime(this.startTime, this.endTime)
      return this.time
    },
    handleLongTime: function () {
      let handleDate =
        (new Date(this.endDate) - new Date(this.dataDate)) /
        (1000 * 60 * 60 * 24)
      // khởi tạo ngày bắt đầu và ngày kết thúc
      // sau khi trừ cho nhau thì kết quả trả về miliseconds
      // convert sang thành ngày bằng cách chia cho (1000 * 60 * 60 * 24)
      this.restDate = handleDate + this.time
      if (isNaN(this.restDate) || this.restDate < 0) {
        return '0'
      } else {
        return this.restDate
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.ss-title {
  margin-bottom: 30px;
  margin-top: 30px;
  .title {
    text-align: center;
    font-size: 30px;
  }
}
</style>
<style lang="css">
.vue__time-picker,
.vue__time-picker input.display-time {
  width: 100%;
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
