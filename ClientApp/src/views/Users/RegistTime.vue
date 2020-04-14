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
        :eventLimit="true"
        :views="{
      timeGrid: {
      eventLimit: 6,
      }
      }"
        eventLimitText="Xem thêm"
        :events="event"
        @dateClick="handleDateClick"
        @eventClick="handleEventClick"
        @eventMouseEnter="handleHover"
        @eventRender="onEventRender"
      />
    </b-container>
    <ModalRestTime
      :dataDate="getDateClick"
      :registDetail="registDetail"
      :showPopup="showPopup"
      v-on:changComponent="getDataRegist()"
      v-on:showEdit="changeEdit"
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
import { BPopover } from 'bootstrap-vue'
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
      registDetail: null,
      showPopup: 'regist'
    }
  },
  components: {
    FullCalendar, // make the <FullCalendar> tag available
    ModalRestTime
  },
  methods: {
    onEventRender (e) {
      let startDtTm = this.formatDateTime(e.event.start.toString())
      let endDtTm = this.formatDateTime(e.event.end.toString())
      // // console.log('onEventRender -> e.event.color', e.event.backgroundColor)

      let variant = ''
      if (e.event.backgroundColor === '#28a745') {
        variant = 'success'
      } else if (e.event.backgroundColor === '#ffc107') {
        variant = 'warning'
      } else variant = 'danger  '
      new BPopover({
        propsData: {
          title: e.event.title,
          content: 'Bắt đầu: ' + startDtTm + ' -------- Kết thúc: ' + endDtTm,
          placement: 'auto',
          boundary: 'scrollParent',
          boundaryPadding: 5,
          offset: 0,
          triggers: 'hover',
          target: e.el,
          variant: variant
        }
      }).$mount()
    },
    handleHover (e) {},
    changeEdit () {
      this.showPopup = 'edit'
    },
    handleEventClick (e) {
      let registId = parseInt(e.event.id)
      this.axios
        .get('/api/RegistTime/GetRegistDetail', {
          params: {
            registId: registId
          }
        })
        .then(res => {
          this.registDetail = res.data
          // console.log('handleEventClick -> this.registDetail', this.registDetail)
          this.showPopup = 'detail'
          this.$bvModal.show('popup-time')
        })
    },
    handleDateClick (e) {
      this.registDetail = null
      this.showPopup = 'regist'
      this.$bvModal.show('popup-time')
      // // console.log("handleDateClick -> e.dateStr", e.dateStr)
      this.getDateClick = e.dateStr
      // console.log(this.getDateClick)
    },    
    formatDateTime (dttime) {
      let format = dttime.replace('GMT', ' ').slice(0, 21)
      return format
    },
    getDataRegist () {
      let userId = this.$cookies.get('userData').userId
      // // console.log('created -> userId', userId)
      this.event = []
      this.axios({
        method: 'get',
        url: '/api/RegistTime/GetTimeUser',
        params: {
          userId: userId
        }
      }).then(res => {
        // // console.log('getDataRegist -> res.data', res.data)
        for (let key in res.data) {
          if (res.data.hasOwnProperty(key)) {
            let color = ''
            if (res.data[key].status === 2) {
              color = '#dc3545'
            } else if (res.data[key].status === 4) {
              color = '#28a745'
            } else color = '#ffc107'
            this.event.push({
              id: res.data[key].id,
              title: res.data[key].jobType === 1 ? 'Làm thêm' : 'Xin nghỉ',
              start: res.data[key].startDtTm,
              end: res.data[key].endDtTm,
              color: color,
              textColor: 'white'
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
<style lang="css" scope>
a:not([href]):hover {
  cursor: pointer;
}
</style>
