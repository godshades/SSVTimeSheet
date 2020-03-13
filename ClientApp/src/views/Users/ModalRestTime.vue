<template>
  <div>
    <b-modal id="register-time" hide-footer size="lg">
      <!-- Modal Header -->

      <div>
        <b-button
          variant="outline-success"
          class="mr-2"
          @click="showRegistTime('work-time')"
        >Đăng ký làm thêm</b-button>
        <b-button variant="outline-success" @click="showRegistTime('rest-time')">Đăng ký ngày nghỉ</b-button>
      </div>

      <!-- Modal body -->

      <div v-if="currentRegistTime == 'work-time'">
        <div class="ss-title">
          <h2 class="title">Đăng ký làm thêm tại công ty</h2>
        </div>
        <b-form-group label="Người gửi(dùng tạm thời)">
          <b-form-input v-model="userSelected"></b-form-input>
        </b-form-group>
        <b-form-group label="Người nhận">
          <b-form-select v-model="leadSelected" :options="LeaderSelect"></b-form-select>
        </b-form-group>
        <b-form-group label="Phân loại thời gian" label-for="classifyTime">
          <b-form-select v-model="classifySelected" :options="classifyTime"></b-form-select>
        </b-form-group>
        <b-form-group label="Thời gian bắt đầu">
          <b-form-timepicker v-model.lazy="startTime" locale="vi" placeholder="Chọn thời gian"></b-form-timepicker>
        </b-form-group>
        <b-form-group label="Thời gian kết thúc">
          <b-form-timepicker v-model.lazy="endTime" locale="vi" placeholder="Chọn thời gian"></b-form-timepicker>
        </b-form-group>
        <b-form-group label="Số giờ làm">
          <b-form-input v-model.lazy="handleWorkTime" disabled></b-form-input>
        </b-form-group>
        <b-form-group label="Ngày đăng ký làm">
          <b-form-input :value="dataDate" disabled></b-form-input>
        </b-form-group>
        <b-form-group label="Ghi chú">
          <b-form-textarea v-model="noteTime" placeholder="Viết vào đây..." rows="3" max-rows="6"></b-form-textarea>
        </b-form-group>
        <b-button @click="sendWorkTime()" pill variant="success" class="ml-auto d-block">Gửi</b-button>
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
          >Nghỉ trong ngày</b-button>
          <b-button variant="outline-success" @click="showRestTime('many-day')">Nghỉ nhiều ngày</b-button>
        </div>
        <div v-if="currentRestTime == 'one-day'">
          <b-form-group label="Người nhận">
            <b-form-select v-model="leadSelected" :options="LeaderSelect" name="LeaderSelect"></b-form-select>
          </b-form-group>
          <b-form-group label="Thời gian bắt đầu">
            <b-form-timepicker v-model.lazy="startTime" locale="vi" placeholder="Chọn thời gian"></b-form-timepicker>
          </b-form-group>
          <b-form-group label="Thời gian kết thúc">
            <b-form-timepicker v-model.lazy="endTime" locale="vi" placeholder="Chọn thời gian"></b-form-timepicker>
          </b-form-group>
          <b-form-group label="Số giờ nghỉ">
            <b-form-input v-model.lazy="handleWorkTime" disabled></b-form-input>
          </b-form-group>
          <b-form-group label="Ngày xin nghỉ">
            <b-form-input :value="dataDate" disabled></b-form-input>
          </b-form-group>
          <b-form-group label="Ghi chú">
            <b-form-textarea placeholder="Viết vào đây..." rows="3" max-rows="6"></b-form-textarea>
          </b-form-group>
          <b-button @click="sendWorkTime()" pill variant="success" class="ml-auto d-block">Gửi</b-button>
        </div>
        <div v-if="currentRestTime == 'many-day'">
          <b-form-group label="Người nhận">
          <b-form-select v-model="leadSelected" :options="LeaderSelect"></b-form-select>
        </b-form-group>
          <b-form-group label="Ngày bắt đầu">
            <b-form-datepicker :value="dataDate" locale="vi" disabled></b-form-datepicker>
          </b-form-group>
          <label>Ngày kết thúc</label>
          <b-form-datepicker
            v-model="endDate"
            today-button
            reset-button
            close-button
            locale="vi"
            placeholder="Chọn ngày"
          ></b-form-datepicker>
          <b-form-group label="Số ngày nghỉ">
            <b-form-input v-model.lazy="handleRestDate" disabled></b-form-input>
          </b-form-group>
          <b-form-group label="Ghi chú">
            <b-form-textarea placeholder="Viết vào đây..." rows="3" max-rows="6"></b-form-textarea>
          </b-form-group>
          <b-button pill variant="success" class="ml-auto d-block">Gửi</b-button>
        </div>
      </div>

      <!-- Modal footer -->
    </b-modal>
  </div>
</template>

<script>
export default {
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
      startTime: '',
      endTime: '',
      workTime: '',
      currentRegistTime: 'work-time',
      currentRestTime: 'one-day',
      endDate: '',
      restDate: '',
      noteTime: '',
      userSelected: '' // dùng tạm thời
    }
  },

  methods: {
    showRegistTime (nameBlock) {
      this.currentRegistTime = nameBlock
    },
    showRestTime (nameBlock) {
      this.currentRestTime = nameBlock
    },
    sendWorkTime () {
      this.axios({
        method: 'post',
        url: '/api/RegistTime/InsertTime',
        data: {
          UserId: this.userSelected,
          LeaderId: this.leadSelected,
          ClassifyTime: this.classifySelected,
          StartWorkTime: new Date(this.dataDate + ',' + this.startTime),
          EndWorkTime: new Date(this.dataDate + ',' + this.endTime),
          // Khởi tạo ngày tháng rồi cộng 2 chuỗi string ngày với giờ lại
          WorkTime: this.workTime,
          StartRestTime: new Date(this.dataDate + ',' + this.startTime),
          EndRestTime: new Date(this.dataDate + ',' + this.endTime),
          RestTime: this.workTime,
          Note: this.noteTime
        }
      })
        .then(res => {
          console.log(res.data)
          if (res.data == false) {
            this.$toastr.error(
              'Thêm không thành công, vui lòng kiểm tra lại',
              'Lỗi rồi!'
            )
          } else this.$toastr.success('Thêm thành công', 'Ngon lành')
        })
        .catch(err => {
          // console.error(err)
        })
    },
    sendRestDate(){
      axios({
        method:'post',
        url: '/api/RegistTime/InsertTime',
        data: {
          UserId: this.userSelected,
          LeaderId: this.leadSelected,
          StartRestTime: new Date(this.dataDate),
          EndRestTime: new Date(this.endDate),
          RestTime: this.restDate,
          Note: this.noteTime
        }
      })
    }
  },
  computed: {
    handleWorkTime: function () {
      let arrStartTime = this.startTime.split(':')
      let arrEndTime = this.endTime.split(':')
      let tempWorkTime =
        (parseInt(arrEndTime[0]) * 60 +
          parseInt(arrEndTime[1]) -
          (parseInt(arrStartTime[0]) * 60 + parseInt(arrStartTime[1]))) /
        60
      // số giờ * 60 + số phút để chuyển sang số phút rồi trừ cho nhau rồi /60 để trả về số giờ
      this.workTime = Math.round(tempWorkTime * 100) / 100
      return this.workTime
    },
    handleRestDate: function () {
      this.restDate =
        (new Date(this.endDate) - new Date(this.dataDate)) /
        (1000 * 60 * 60 * 24)
      // khởi tạo ngày bắt đầu và ngày kết thúc
      // sau khi trừ cho nhau thì kết quả trả về miliseconds
      // convert sang thành ngày bằng cách chia cho (1000 * 60 * 60 * 24)
      return this.restDate
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
