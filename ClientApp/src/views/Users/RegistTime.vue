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
        today :'Hôm nay',
        month : 'Tháng',
        week : 'Tuần',
        day : 'Ngày'
        }"
        @dateClick="handleDateClick"
      />
    </b-container>
    <ModalRestTime :data="getDateClick"></ModalRestTime>
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
      getDateClick: ''
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
    }
  },
  prop: {}
}
</script>
<style lang='scss' scoped>
@import "~@fullcalendar/core/main.css";
@import "~@fullcalendar/daygrid/main.css";
</style>
