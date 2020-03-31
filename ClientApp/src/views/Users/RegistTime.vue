<template>
  <div>
    <b-container>
      <FullCalendar
        locale="vi"
        :plugins="calendarPlugins"
        :header="{
          left: 'title',
          right: 'prev today next'
        }"
        :buttonText="{
          today: 'Hôm nay'
        }"
        :events="event"
        @dateClick="handleDateClick"
        @eventClick="handleEventClick"
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
      event: [],
      registDetail: null
    }
  },
  components: {
    FullCalendar, // make the <FullCalendar> tag available
    ModalRestTime
  },
  methods: {
    handleEventClick (e) {      
      e.jsEvent.preventDefault()
      let registId = parseInt(e.event.id)
      console.log('handleEventClick -> registId', registId)
      this.axios.get('/api/RegistTime/GetRegistDetail', {
        params: {
          registId: registId
        }
      })
        .then(res => {
          console.log(res.data)
          this.registDetail = res.data
          console.log('handleEventClick -> registDetail', this.registDetail.leaderId)
          // truyền props data trên vào component con rồi hiển thị lên trên popup        
        })
    },
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
              id: res.data[key].id,
              title: res.data[key].classifyTime === 1 ? 'Làm thêm' : 'Xin nghỉ',
              start: res.data[key].startTime,
              end: res.data[key].endTime,
              color: color
            })
          }
        }
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
<style lang="css">
a:not([href]):hover {
  cursor: pointer;
}
</style>
