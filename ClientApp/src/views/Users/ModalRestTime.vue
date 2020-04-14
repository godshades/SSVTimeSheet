<template>
  <div>
    <b-modal id="popup-time" hide-footer size="lg">
      <div class="ss-title">
        <h2 class="title">{{ showTitle }}</h2>
      </div>
      <div v-if="showPopup === 'regist' || showPopup === 'edit'">
        <div>
          <div class="row">
            <b-form-group class="col-md-6" label="Phân loại thời gian" label-for="jobType">
              <b-form-select
                v-model="jobTypeSelected"
                :options="jobType"
                :class="errors.has('jobTypeSelected') || jobTypeSelected === null ? 'is-invalid' : 'is-valid'"
                name="jobTypeSelected"
                data-vv-as="Phân loại"
                v-validate="'required'"
              ></b-form-select>
              <span
                class="invalid-feedback d-block"
                v-if="errors.has('jobTypeSelected') == true"
              >{{errors.first('jobTypeSelected')}}</span>
            </b-form-group>
            <b-form-group class="col-md-6" label="Người nhận">
              <b-form-select
                v-model="leadSelected"
                :options="LeaderSelect"
                name="leadSelected"
                data-vv-as="Người nhận"
                v-validate="'required'"
                :class="errors.has('leadSelected') || leadSelected === null ? 'is-invalid' : 'is-valid'"
              ></b-form-select>
              <span
                class="invalid-feedback d-block"
                v-if="errors.has('leadSelected') == true"
              >{{errors.first('leadSelected')}}</span>
            </b-form-group>
          </div>
          <div>
            <div class="row">
              <b-form-group class="col-md-6" label="Giờ bắt đầu">
                <vue-timepicker
                  v-model.lazy="startTime"
                  :input-class="{
                    'custom-select': true,
                    'is-invalid':
                      errors.has('startTime') || startTime === ''|| handleShortTime == 0,
                    'is-valid':
                      !errors.has('startTime') && startTime !== '' && handleShortTime != 0
                  }"
                  hide-clear-button
                  placeholder="Chọn giờ"
                  name="startTime"
                  data-vv-as="Thời gian bắt đầu"
                  v-validate="'required'"
                ></vue-timepicker>
                <span
                  class="invalid-feedback d-block"
                  v-if="errors.has('startTime') == true"
                >{{errors.first('startTime')}}</span>
              </b-form-group>
              <b-form-group class="col-md-6" label="Giờ kết thúc">
                <vue-timepicker
                  v-model.lazy="endTime"
                  :input-class="{
                    'custom-select': true,
                    'is-invalid':
                      errors.has('endTime') || endTime === ''|| handleShortTime == 0,
                    'is-valid':
                      !errors.has('endTime') && endTime !== '' && handleShortTime != 0
                  }"
                  hide-clear-button
                  placeholder="Chọn giờ"
                  name="endTime"
                  data-vv-as="Thời gian kết thúc"
                  v-validate="'required'"
                ></vue-timepicker>
                <span
                  class="invalid-feedback d-block"
                  v-if="errors.has('endTime') == true"
                >{{errors.first('endTime')}}</span>
              </b-form-group>
            </div>
            <div class="row" v-if="jobTypeSelected === 1">
              <b-form-group class="col-md-6" label="Số ngày làm">
                <b-form-input v-model.lazy="handleShortTime" disabled></b-form-input>
              </b-form-group>

              <b-form-group class="col-md-6" label="Ngày đăng ký làm">
                <b-form-input :value="startDate" disabled></b-form-input>
              </b-form-group>
            </div>
            <div v-if="jobTypeSelected == 2">
              <div class="row">
                <b-form-group class="col-md-6" label="Ngày bắt đầu">
                  <b-form-datepicker :value="startDate" locale="vi" disabled></b-form-datepicker>
                </b-form-group>
                <b-form-group class="col-md-6" label="Ngày kết thúc">
                  <b-form-datepicker
                    v-model="endDate"
                    today-button
                    reset-button
                    close-button
                    locale="vi"
                    placeholder="Chọn ngày"
                    name="endDate"
                    data-vv-as="Ngày kết thúc"
                    v-validate="'required'"
                    :class="errors.has('endDate') || endDate === '' || handleLongTime === '0' ? 'is-invalid' : 'is-valid'"
                  ></b-form-datepicker>
                  <span
                    class="invalid-feedback d-block"
                    v-if="errors.has('endDate') == true"
                  >{{errors.first('endDate')}}</span>
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
                    name="reasonSelected"
                    data-vv-as="Lý do nghỉ"
                    v-validate="'required'"
                    :class="errors.has('reasonSelected') || reasonSelected === '' ? 'is-invalid' : 'is-valid'"
                  ></b-form-select>
                  <span
                    class="invalid-feedback d-block"
                    v-if="errors.has('reasonSelected') == true"
                  >{{errors.first('reasonSelected')}}</span>
                </b-form-group>
              </div>
              <div class="row">
                <b-form-group class="col-md-6" label="Họ tên người liên hệ">
                  <b-form-input
                    v-model="nameContact"
                    :class="errors.has('nameContact') || nameContact === '' ? 'is-invalid' : 'is-valid'"
                    v-validate="'required'"
                    name="nameContact"
                    data-vv-as="Họ tên người liên hệ"
                  ></b-form-input>
                  <span
                    class="invalid-feedback d-block"
                    v-if="errors.has('nameContact') == true"
                  >{{errors.first('nameContact')}}</span>
                </b-form-group>
                <b-form-group class="col-md-6" label="SĐT người liên hệ">
                  <b-form-input
                    v-model.lazy="phoneContact"
                    name="phoneContact"
                    data-vv-as="SĐT"
                    v-validate="{ required: true,max : 10, regex: /(09|01[2|6|8|9])+([0-9]{8})\b/g }"
                    :class="errors.has('phoneContact') || phoneContact === '' ? 'is-invalid' : 'is-valid'"
                  ></b-form-input>
                  <span
                    class="invalid-feedback d-block"
                    v-if="errors.has('phoneContact') == true"
                  >{{errors.first('phoneContact')}}</span>
                </b-form-group>
              </div>
            </div>
            <b-form-group label="Ghi chú">
              <b-form-textarea
                v-model="noteTime"
                placeholder="Viết vào đây..."
                rows="3"
                max-rows="6"
              ></b-form-textarea>
            </b-form-group>
            <b-button @click="sendWorkTime()" pill variant="success" class="ml-auto d-block">Gửi</b-button>
          </div>
        </div>
      </div>
      <div v-if="showPopup === 'detail'">
        <div class="content-details table-responsive">
          <table class="table table-striped table-hover">
            <tbody>
              <tr>
                <th>Kiểu</th>
                <td>{{ registDetail.jobTypeName }}</td>
              </tr>
              <tr>
                <th>Tên leader</th>
                <td>{{ registDetail.leaderName }}</td>
              </tr>
              <tr>
                <th>Giờ bắt đầu</th>
                <td>{{ registDetail.startDtTm }}</td>
              </tr>
              <tr>
                <th>Giờ kết thúc</th>
                <td>{{ registDetail.endDtTm }}</td>
              </tr>
              <tr>
                <th>Tổng thời gian</th>
                <td>{{ registDetail.totalTime }}</td>
              </tr>
              <tr>
                <th>Ngày bắt đầu</th>
                <td>{{ registDetail.startDtTm }}</td>
              </tr>
              <tr>
                <th>Ngày kết thúc</th>
                <td>{{ registDetail.endDtTm }}</td>
              </tr>
              <tr v-if="registDetail.jobType === 2">
                <th>Lý do nghỉ</th>
                <td>{{ registDetail.restReasonName }}</td>
              </tr>
              <tr v-if="registDetail.jobType === 2">
                <th>Tên liên hệ</th>
                <td>{{ registDetail.restNameContact }}</td>
              </tr>
              <tr v-if="registDetail.jobType === 2">
                <th>Sđt liên hệ</th>
                <td>{{ registDetail.restPhoneContact }}</td>
              </tr>
              <tr>
                <th>Trạng thái</th>
                <td>{{ registDetail.statusName }}</td>
              </tr>
              <tr>
                <th>Ghi chú</th>
                <td>{{ registDetail.note }}</td>
              </tr>
            </tbody>
          </table>
          <div class="d-flex mt-3 justify-content-center">
            <b-button
              v-if="calculateTime(registDetail.insertDt) <= 3 && registDetail.status !== 4 && registDetail.status !== 2"
              @click="editTime()"
              variant="success"
              class="mr-3"
            >Edit</b-button>
            <b-button
              v-if="calculateTime(registDetail.insertDt) <= 1 && registDetail.status !== 4 && registDetail.status !== 2"
              @click="deleteTime(registDetail.id)"
              variant="success"
            >Delete</b-button>
          </div>
        </div>
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
  props: ['registDetail', 'dataDate', 'showPopup'],
  data () {
    return {
      leadSelected: null,
      LeaderSelect: [],
      jobTypeSelected: null,
      jobType: [
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
      startDate: '',
      endDate: '',
      noteTime: ''
    }
  },
  created () {
    this.loadData()
  },
  watch: {
    dataDate: function () {
      this.startDate = this.dataDate
      this.endDate = this.dataDate
    }
  },
  methods: {
    calculateTime (time) {
      return (Date.now() - new Date(time)) / (1000 * 60 * 60 * 24)
    },
    deleteTime (id) {
      this.$swal({
        title: 'Bạn có chắc muốn xoá?',
        text: 'Bạn sẽ không thể hoàn nguyên điều này!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Xoá!'
      }).then(result => {
        if (result.value) {
          this.axios
            .delete('/api/RegistTime/DeleteTime', {
              params: {
                id: id
              }
            })
            .then(res => {
              if (res.data === true) {
                this.$swal('Hoàn thành!', 'Yêu cầu đã bị xoá', 'success')
              } else {
                this.$toastr.error(
                  'Xoá thất bại, vui lòng kiểm tra lại',
                  'Lỗi rồi'
                )
              }
              this.$emit('changComponent')
            })
            .catch(err => {
              console.error(err)
            })
        }
      })
    },
    editTime () {
      this.$emit('showEdit')
      this.handleEditTime(
        this.registDetail.startDtTm,
        this.registDetail.endDtTm
      )
      this.jobTypeSelected = this.registDetail.jobType
      this.leadSelected = this.registDetail.leaderId
      this.reasonSelected = this.registDetail.restReasonId
      this.nameContact =
        this.registDetail.restNameContact != null
          ? this.registDetail.restNameContact
          : this.$cookies.get('userData').fullName
      this.phoneContact =
        this.registDetail.restPhoneContact != null
          ? this.registDetail.restPhoneContact
          : this.$cookies.get('userData').telNo
      this.noteTime = this.registDetail.note
    },
    handleEditTime (startTime, endTime) {
      let arrStart = startTime.split('T')
      this.startTime = arrStart[1]
      this.startDate = arrStart[0]
      let arrEnd = endTime.split('T')
      this.endTime = arrEnd[1]
      this.endDate = arrEnd[0]
    },
    loadData () {
      this.nameContact = this.$cookies.get('userData').fullName
      this.phoneContact = this.$cookies.get('userData').telNo
      let userTypeId = this.$cookies.get('userData').userTypeId
      let leaderId = this.$cookies.get('userData').leaderId
      this.axios
        .get('/api/RegistTime/GetLeaderByUser', {
          params: {
            userTypeID: userTypeId
          }
        })
        .then(res => {
          for (const key in res.data) {
            if (res.data.hasOwnProperty(key)) {
              if (res.data[key].userId === leaderId) {
                this.leadSelected = leaderId
              }
              this.LeaderSelect.push({
                value: res.data[key].userId,
                text: res.data[key].fullName
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
              value: res.data[key].typeValue,
              text: res.data[key].typeValueName
            })
          }
        }
      })
    },
    sendAxiosTime (requestUrl, data) {
      this.$validator.validateAll().then(result => {
        if (result) {
          this.axios({
            method: 'post',
            url: requestUrl,
            data: data
          })
            .then(res => {
              if (res.data === false) {
                this.$swal('Thất bại', 'Vui lòng kiểm tra lại', 'error')
              } else {
                this.$emit('changComponent') // dùng sự kiện để gọi hàm refresh ở component cha
                this.$swal('Ok!', 'Thành công', 'success')
              }
            })
            .catch(err => {
              console.log('sendAxiosTime -> err', err)
            })
        }
      })
    },

    sendWorkTime () {
      let data = {}
      if (this.jobTypeSelected === 1) {
        data = {
          Id: this.registDetail !== null ? this.registDetail.id : 0,
          UserId: this.$cookies.get('userData').userId,
          LeaderId: this.leadSelected,
          JobType: this.jobTypeSelected,
          StartDtTm: new Date(this.startDate + ',' + this.startTime + ' UTC'),
          EndDtTm: new Date(this.endDate + ',' + this.endTime + ' UTC'),
          TotalTime: this.handleShortTime,
          Note: this.noteTime
        }
      }
      if (this.jobTypeSelected === 2) {
        data = {
          Id: this.registDetail !== null ? this.registDetail.id : 0,
          UserId: this.$cookies.get('userData').userId,
          LeaderId: this.leadSelected,
          JobType: this.jobTypeSelected,
          StartDtTm: new Date(this.startDate + ',' + this.startTime + ' UTC'),
          EndDtTm: new Date(this.endDate + ',' + this.endTime + ' UTC'),
          // Khởi tạo ngày tháng rồi cộng 2 chuỗi string ngày với giờ lại
          RestReasonId: this.reasonSelected,
          RestNameContact: this.nameContact,
          RestPhoneContact: this.phoneContact,
          TotalTime: this.handleLongTime,
          Note: this.noteTime
        }
      }

      let requestUrl = ''
      if (this.showPopup === 'regist') {
        requestUrl = '/api/RegistTime/InsertTime'
      }
      if (this.showPopup === 'edit') {
        requestUrl = '/api/RegistTime/UpdateRegist'
      }

      this.sendAxiosTime(requestUrl, data)
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
    }
  },
  computed: {
    showTitle () {
      let title = ''
      if (this.showPopup === 'regist') {
        title = 'Đăng ký lịch'
      }
      if (this.showPopup === 'detail') {
        title = 'Chi tiết'
      }
      if (this.showPopup === 'edit') {
        title = 'Chỉnh sửa'
      }
      return title
    },
    handleShortTime () {
      let time = this.handleTime(this.startTime, this.endTime)
      return time
    },
    handleLongTime () {
      let handleDate =
        (new Date(this.endDate) - new Date(this.startDate)) /
        (1000 * 60 * 60 * 24)
      // khởi tạo ngày bắt đầu và ngày kết thúc
      // sau khi trừ cho nhau thì kết quả trả về miliseconds
      // convert sang thành ngày bằng cách chia cho (1000 * 60 * 60 * 24)
      let restDate = handleDate + this.handleShortTime
      if (isNaN(restDate) || restDate < 0) {
        return '0'
      } else {
        return restDate
      }
    }
  }
}
</script>
<style lang="css" scope>
.vue__time-picker,
.vue__time-picker input.display-time {
  width: 100%;
}
</style>
