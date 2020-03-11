<template>
  <div>
    <b-modal id="register-time" hide-footer size="lg">
      <!-- Modal Header -->

      <div>
        <b-button class="mr-2" data-id="regist-work">Đăng ký làm thêm</b-button>
        <b-button class data-id="regist-rest">Đăng ký ngày nghỉ</b-button>
      </div>

      <!-- Modal body -->

      <div>
        <div class="ss-title">
          <h2 class="title">Đăng ký làm thêm tại công ty</h2>
        </div>

        <b-form-group label="Người nhận">
          <b-form-select v-model="leadSelected" :options="LeaderSelect" name="LeaderSelect"></b-form-select>
        </b-form-group>
        <b-form-group label="Phân loại thời gian" label-for="classifyTime">
          <b-form-select v-model="classifySelected" :options="classifyTime" name="classifyTime"></b-form-select>
        </b-form-group>
        <b-form-group label="Thời gian bắt đầu">
          <b-form-timepicker v-model="startTime" locale="vi" placeholder="Chọn thời gian"></b-form-timepicker>
          <div>{{workTime}}</div>
        </b-form-group>
        <b-form-group v-model="endTime" label="Thời gian kết thúc">
          <b-form-timepicker locale="vi" placeholder="Chọn thời gian"></b-form-timepicker>
        </b-form-group>
        <b-form-group label="Số giờ làm">
          <b-form-input placeholder="0" disabled></b-form-input>
        </b-form-group>
        <b-form-group label="Ngày đăng ký làm">
          <b-form-input :value="data" disabled></b-form-input>
        </b-form-group>
        <b-form-group label="Ghi chú">
          <b-form-textarea placeholder="Viết vào đây..." rows="3" max-rows="6"></b-form-textarea>
        </b-form-group>
      </div>

      <div id="regist-rest" class="d-none js-none">
        <div class="ss-title">
          <h2 class="title">Xin nghỉ phép</h2>
        </div>
        <div class="mb-3">
          <button class="btn js--regist" data-id="rest-in-day">Nghỉ trong ngày</button>
          <button class="btn js--regist" data-id="rest-many-date">Nghỉ nhiều ngày</button>
        </div>
        <div id="rest-in-day" class="js--none">
          <div class="input-group mb-3">
            <div class="col-md-3">
              <label class for>Người nhận</label>
            </div>
            <select class="custom-select rounded-left" id>
              <option selected>Chọn</option>
              <option value="1">Leader 1</option>
              <option value="2">Leader 2</option>
              <option value="3">Leader 3</option>
            </select>
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <label class for>Thời gian bắt đầu</label>
            </div>
            <select class="custom-select rounded-left" id>
              <option value="00" selected>00</option>
              <option value="01">01</option>
              <option value="02">02</option>
              <option value="03">03</option>
              <option value="04">04</option>
              <option value="05">05</option>
              <option value="06">06</option>
              <!-- dùng vòng for render ra 24 tiếng -->
            </select>
            <span class="input-group-text rounded-0">giờ</span>
            <select class="custom-select" id>
              <option value="00" selected>00</option>
              <option value="15">15</option>
              <option value="30">30</option>
              <option value="45">45</option>
              <option value="60">60</option>
            </select>
            <span class="input-group-text rounded-0">phút</span>
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <label class for>Thời gian kết thúc</label>
            </div>
            <select class="custom-select rounded-left" id>
              <option value="00" selected>00</option>
              <option value="01">01</option>
              <option value="02">02</option>
              <option value="03">03</option>
              <option value="04">04</option>
              <option value="05">05</option>
              <option value="06">06</option>
              <!-- dùng vòng for render ra 24 tiếng -->
            </select>
            <span class="input-group-text rounded-0">giờ</span>
            <select class="custom-select" id>
              <option value="00" selected>00</option>
              <option value="15">15</option>
              <option value="30">30</option>
              <option value="45">45</option>
              <option value="60">60</option>
            </select>
            <span class="input-group-text rounded-0">phút</span>
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <span class id>Số tiếng</span>
            </div>
            <input type="number" disabled class="form-control rounded-left" value="0" />
            <!-- Người dùng nhập giờ xong tự trừ và thêm value vào -->
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <span class id>Ngày xin nghỉ</span>
            </div>
            <input type="date" class="form-control rounded-left" />
            <!-- dùng date picker -->
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <span class id>Ghi chú</span>
            </div>
            <textarea class="form-control rounded-left"></textarea>
            <!-- dùng date picker -->
          </div>
        </div>
        <div id="rest-many-date" class="d-none js--none">
          <div class="input-group mb-3">
            <div class="col-md-3">
              <span class id>Ngày bắt đầu</span>
            </div>
            <input type="date" class="form-control rounded-left" />
            <!-- dùng date picker -->
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <span class id>Ngày kết thúc</span>
            </div>
            <input type="date" class="form-control rounded-left" />
            <!-- dùng date picker -->
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <span class id>Số ngày</span>
            </div>
            <input type="number" disabled class="form-control rounded-left" value="0" />
            <!-- Người dùng nhập giờ xong tự trừ và thêm value vào -->
          </div>
          <div class="input-group mb-3">
            <div class="col-md-3">
              <span class id>Ghi chú</span>
            </div>
            <textarea class="form-control rounded-left"></textarea>
            <!-- dùng date picker -->
          </div>
        </div>
      </div>

      <!-- Modal footer -->
      <button class="btn btn-success d-block ml-auto">Gửi</button>
    </b-modal>
  </div>
</template>

<script>
export default {
  data() {
    return {
      leadSelected: null,

      LeaderSelect: [
        { value: null, text: "Chọn", disabled: true },
        { value: "Leader1", text: "Leader1" },
        { value: "Leader2", text: "Leader2" },
        { value: "Leader3", text: "Leader3" },
        { value: "Leader4", text: "Leader4" }
      ],
      classifySelected: null,
      classifyTime: [
        { value: null, text: "Chọn", disabled: true },
        { value: "mainTime", text: "Làm chính" },
        { value: "overTime", text: "Tăng ca" },
        { value: "holidayTime", text: "Ngày lễ" }
      ],
      startTime: "",
      endTime: "",
      workTime: ""
    };
  },
  props: ["data"],
  methods: {},
  computed: {
    handleWorkTime: function() {
      let arrStartTime = this.startTime.split(":");
      let arrEndTime = this.endTime.split(":");
      let hours = parseInt(arrEndTime[0]) - parseInt(arrStartTime[0]);
      let minute = parseInt(arrEndTime[0]) - parseInt(arrStartTime[0]);
      let workTime = (hours+ ':' + minute).toString();
      return workTime;
    }
  }
};
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
