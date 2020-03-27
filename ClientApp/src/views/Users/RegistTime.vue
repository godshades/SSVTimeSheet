<template>
  <div>
    <b-container>
      <FullCalendar
        locale="vi"
        :plugins="calendarPlugins"
        :header="{
          left: 'title',
          center: 'dayGridMonth,timeGridWeek,timeGridDay,List',
          right: 'prev today next'
        }"
        :buttonText="{
          today: 'Hôm nay',
          month: 'Tháng',
          week: 'Tuần',
          day: 'Ngày'
        }"
        :events="event"
        @dateClick="handleDateClick"
      />
    </b-container>
    <ModalRestTime
      :dataDate="getDateClick"
      v-on:changComponent="refreshComponent"
    ></ModalRestTime>
  </div>
</template>

<script>
import FullCalendar from '@fullcalendar/vue'
import dayGridPlugin from '@fullcalendar/daygrid'
import TimeGridPlugin from '@fullcalendar/timegrid'
import InteractionPlugin from '@fullcalendar/interaction'
import ListPlugin from '@fullcalendar/list'
import ModalRestTime from './ModalRestTime'
export default {
  name: 'regist-time',
  data () {
    return {
      calendarPlugins: [
        dayGridPlugin,
        TimeGridPlugin,
        InteractionPlugin,
        ListPlugin
      ],
      getDateClick: '',
      event: []
    }
  },
  components: {
    FullCalendar, // make the <FullCalendar> tag available
    ModalRestTime
  },
  methods: {
    handleDateClick (e) {
      this.$bvModal.show('register-time')
      // console.log("handleDateClick -> e.dateStr", e.dateStr)
      this.getDateClick = e.dateStr
      // console.log(this.getDateClick)
    },
    refreshComponent () {
      // console.log('ham 1 chay')
      this.$emit('changeComponentEvent')
      // sự kiện thay đổi component để refresh lại trang mà không cần reload
    },
    getDataRegist () {
      let userId = this.$cookies.get('userData').userId
      // console.log('created -> userId', userId)

      this.axios({
        method: 'get',
        url: '/api/RegistTime/GetTimeUser',
        params: {
          userId: userId
        }
      }).then(res => {
        // console.log('getDataRegist -> res.data', res.data)
        for (let key in res.data) {
          if (res.data.hasOwnProperty(key)) {
            let color = ''
            if (res.data[key].status === 2) {
              color = 'red'
            } else if (res.data[key].status === 4) {
              color = '#28a745'
            } else color = '#ffc107'
            this.event.push({
              title: res.data[key].classifyTime === 1 ? 'Làm thêm' : 'Xin nghỉ',
              start: res.data[key].startTime,
              end: res.data[key].endTime,
              color: color
            })
          }
        }
        // console.log('created -> this.event', this.event)
      })
    }
  },
  created () {
    this.getDataRegist()
  }
}
</script>
<style lang="scss" scoped>
@import "~@fullcalendar/core/main.css";
@import "~@fullcalendar/daygrid/main.css";
</style>
