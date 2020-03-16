<template>
  <div>
    <b-modal id="register-time" hide-footer size="lg">
      <!-- Modal Header -->

      <div>
        <b-button
          variant="outline-success"
          class="mr-2"
          @click="showRegistTime('work-time')"
          >Đăng ký làm thêm</b-button
        >
        <b-button variant="outline-success" @click="showRegistTime('rest-time')"
          >Đăng ký ngày nghỉ</b-button
        >
      </div>

      <!-- Modal body -->

      <div v-if="currentRegistTime == 'work-time'">
        <div class="ss-title">
          <h2 class="title">Đăng ký làm thêm tại công ty</h2>
        </div>
        <b-form-group label="Người gửi(dùng tạm thời)">
          <b-form-input
            v-model="userSelected"
            :state="validateForm(userSelected)"
          ></b-form-input>
        </b-form-group>
        <div class="row">
          <b-form-group class="col-md-6" label="Người nhận">
            <b-form-select
              v-model="leadSelected"
              :options="LeaderSelect"
              :state="validateForm(leadSelected)"
            ></b-form-select>
          </b-form-group>
          <b-form-group
            class="col-md-6"
            label="Phân loại thời gian"
            label-for="classifyTime"
          >
            <b-form-select
              v-model="classifySelected"
              :options="classifyTime"
              :state="validateForm(classifySelected)"
            ></b-form-select>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Thời gian bắt đầu">
            <vue-timepicker
              v-model.lazy="startWorkTime"
              :input-class="{
                'custom-select': true,
                'is-invalid': !validateForm(startWorkTime),
                'is-valid': validateForm(startWorkTime)
              }"
              hide-clear-button
              placeholder="Chọn giờ"
            >
            </vue-timepicker>
          </b-form-group>

          <b-form-group class="col-md-6" label="Thời gian kết thúc">
            <vue-timepicker
              v-model.lazy="endWorkTime"
              :input-class="{
                'custom-select': true,
                'is-invalid': !validateForm(endWorkTime),
                'is-valid': validateForm(endWorkTime)
              }"
              hide-clear-button
              placeholder="Chọn giờ"
            ></vue-timepicker>
          </b-form-group>
        </div>
        <div class="row">
          <b-form-group class="col-md-6" label="Số giờ làm">
            <b-form-input v-model.lazy="handleWorkTime" disabled></b-form-input>
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

      <div v-if="currentRegistTime == 'rest-time'">
        <div class="ss-title">
          <h2 class="title">Xin nghỉ phép</h2>
        </div>
        <div class="mb-3">
          <b-button
            variant="outline-success"
            class="mr-3"
            @click="showRestTime('one-day')"
            >Nghỉ trong ngày</b-button
          >
          <b-button variant="outline-success" @click="showRestTime('many-day')"
            >Nghỉ nhiều ngày</b-button
          >
        </div>
        <div v-if="currentRestTime == 'one-day'">
          <div class="row">
            <b-form-group class="col-md-4" label="Người nhận">
              <b-form-select
                v-model="leadSelected"
                :options="LeaderSelect"
                :state="validateForm(leadSelected)"                
              ></b-form-select>
            </b-form-group>
            <b-form-group class="col-md-4" label="Thời gian bắt đầu">
              <vue-timepicker
                v-model.lazy="startRestTime"
                :input-class="{
                  'custom-select': true,
                  'is-invalid': !validateForm(startRestTime),
                  'is-valid': validateForm(startRestTime)
                }"
                hide-clear-button
                placeholder="Chọn giờ"
              ></vue-timepicker>
            </b-form-group>
            <b-form-group class="col-md-4" label="Thời gian kết thúc">
              <vue-timepicker
                v-model.lazy="endRestTime"
                :input-class="{
                  'custom-select': true,
                  'is-invalid': !validateForm(endRestTime),
                  'is-valid': validateForm(endRestTime)
                }"
                hide-clear-button
                placeholder="Chọn giờ"
              ></vue-timepicker>
            </b-form-group>
          </div>
          <div class="row">
            <b-form-group class="col-md-6" label="Số giờ nghỉ">
              <b-form-input
                v-model.lazy="handleRestTime"
                disabled
              ></b-form-input>
            </b-form-group>
            <b-form-group class="col-md-6" label="Ngày xin nghỉ">
              <b-form-input :value="dataDate" disabled></b-form-input>
            </b-form-group>
          </div>
          <b-form-group label="Ghi chú">
            <b-form-textarea
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
        <div v-if="currentRestTime == 'many-day'">
          <b-form-group label="Người nhận">
            <b-form-select
              v-model="leadSelected"
              :options="LeaderSelect"
              :state="validateForm(leadSelected)"
            ></b-form-select>
          </b-form-group>
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
                :state="validateForm(endDate)"
                today-button
                reset-button
                close-button
                locale="vi"
                placeholder="Chọn ngày"
              ></b-form-datepicker>
            </b-form-group>
          </div>
          <b-form-group label="Số ngày nghỉ">
            <b-form-input v-model.lazy="handleRestDate" disabled></b-form-input>
          </b-form-group>
          <b-form-group label="Ghi chú">
            <b-form-textarea
              placeholder="Viết vào đây..."
              rows="3"
              max-rows="6"
            ></b-form-textarea>
          </b-form-group>
          <b-button
            @click="sendRestDate()"
            pill
            variant="success"
            class="ml-auto d-block"
            >Gửi</b-button
          >
        </div>
      </div>

      <!-- Modal footer -->
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
      LeaderSelect: [
        { value: null, text: 'Chọn', disabled: true },
        { value: 'Lead1', text: 'Leader1' },
        { value: 'Lead2', text: 'Leader2' },
        { value: 'Lead3', text: 'Leader3' },
        { value: 'Lead4', text: 'Leader4' }
      ],
      classifySelected: null,
      classifyTime: [
        { value: null, text: 'Chọn', disabled: true },
        { value: 'mainTime', text: 'Làm chính' },
        { value: 'overTime', text: 'Tăng ca' },
        { value: 'holidayTime', text: 'Ngày lễ' }
      ],
      startWorkTime: '',
      endWorkTime: '',
      workTime: '',
      startRestTime: '',
      endRestTime: '',
      restTime: '',
      currentRegistTime: 'work-time',
      currentRestTime: 'one-day',
      endDate: '',
      restDate: '',
      noteTime: '',
      userSelected: '' // dùng tạm thời
    }
  },
  methods: {
    validateForm (name) {
      if (name != null && name != '') {
        return true
      } else return false
    },
    showRegistTime (nameBlock) {
      this.currentRegistTime = nameBlock
      this.noteTime = ''
    },
    showRestTime (nameBlock) {
      this.currentRestTime = nameBlock
      this.noteTime = ''
    },
    sendAxiosTime (data) {
      this.axios({
        method: 'post',
        url: '/api/RegistTime/InsertTime',
        data: data
      })
        .then(res => {
          console.log(res.data)
          if (res.data === false) {
            this.$toastr.error(
              'Thêm không thành công, vui lòng kiểm tra lại',
              'Lỗi rồi!'
            )
          } else this.$toastr.success('Thêm thành công', 'Ngon lành')
        })
        .catch(err => {
          console.log(err.response)
        })
    },
    sendWorkTime () {
      let data = {
        UserId: this.userSelected,
        LeaderId: this.leadSelected,
        ClassifyTime: this.classifySelected,
        StartWorkTime: new Date(this.dataDate + ',' + this.startWorkTime),
        EndWorkTime: new Date(this.dataDate + ',' + this.endWorkTime),
        // Khởi tạo ngày tháng rồi cộng 2 chuỗi string ngày với giờ lại
        WorkTime: this.workTime,
        Note: this.noteTime
      }
      this.sendAxiosTime(data)
    },
    sendRestTime () {
      let data = {
        UserId: this.userSelected,
        LeaderId: this.leadSelected,
        StartRestTime: new Date(this.dataDate + ',' + this.startRestTime),
        EndRestTime: new Date(this.dataDate + ',' + this.endRestTime),
        RestTime: this.restTime,
        Note: this.noteTime
      }
      this.sendAxiosTime(data)
    },
    sendRestDate () {
      let data = {
        UserId: this.userSelected,
        LeaderId: this.leadSelected,
        StartRestTime: new Date(this.dataDate),
        EndRestTime: new Date(this.endDate),
        RestTime: this.restDate,
        Note: this.noteTime
      }
      this.sendAxiosTime(data)
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
      let resultTime = Math.round(tempWorkTime * 100) / 100
      if (isNaN(resultTime)) {
        return 0
      } else {
        return resultTime
      }
    }
  },
  computed: {
    handleWorkTime: function () {
      this.workTime = this.handleTime(this.startWorkTime, this.endWorkTime)
      return this.workTime
    },

    handleRestTime: function () {
      this.restTime = this.handleTime(this.startRestTime, this.endRestTime)
      return this.restTime
    },
    handleRestDate: function () {
      this.restDate =
        (new Date(this.endDate) - new Date(this.dataDate)) /
        (1000 * 60 * 60 * 24)
      // khởi tạo ngày bắt đầu và ngày kết thúc
      // sau khi trừ cho nhau thì kết quả trả về miliseconds
      // convert sang thành ngày bằng cách chia cho (1000 * 60 * 60 * 24)
      if (isNaN(this.restDate)) {
        return 0
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
</style>
