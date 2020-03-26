<template>
  <div>
    <div class="breadcrumb-holder">
      <div class="container-fluid">
        <ul class="breadcrumb">
          <li class="breadcrumb-item"><a href="/admin/index">Home</a></li>
          <li class="breadcrumb-item active">Tất cả yêu cầu</li>
        </ul>
      </div>
    </div>
    <section>
      <div class="container-fluid">
        <!-- Page Header-->
        <header>
          <h1 class="h3 display">Yêu cầu chưa phê duyệt</h1>
        </header>
        <div class="row">
          <div class="col-lg-12">
            <div class="table-responsive">
              <table class="table table-striped table-hover">
                <thead>
                  <tr>
                    <th>STT</th>
                    <th>Tên người dùng</th>
                    <th>Kiểu thời gian</th>
                    <th>Thời gian bắt đầu</th>
                    <th>Thời gian kết thúc</th>
                    <th>Tổng thời gian</th>
                    <th>Tên người liên hệ</th>
                    <th>SĐT liên hệ</th>
                    <th>Ghi chú</th>
                    <th>Phê duyệt</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item, index) in requirementTime" :key="index">
                    <th scope="row">{{ index + 1 }}</th>
                    <td>{{ item.userName }}</td>
                    <td>{{ item.classifyTime }}</td>
                    <td>{{ item.startTime }}</td>
                    <td>{{ item.endTime }}</td>
                    <td>{{ item.time }} ngày</td>
                    <td>{{ item.nameContact }}</td>
                    <td>{{ item.phoneContact }}</td>
                    <td>{{ item.note }}</td>
                    <td>
                      <b-button
                        variant="success"
                        @click="removeRequire(item.Id, 2, index)"
                        class="mr-1"
                        ><font-awesome-icon icon="check"
                      /></b-button>
                      <b-button
                        variant="danger"
                        @click="removeRequire(item.Id, 1, index)"
                        ><font-awesome-icon icon="times"
                      /></b-button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: 'manager-requirement',
  data () {
    return {
      requirementTime: []
    }
  },
  methods: {
    getRequirementTime () {
      let leaderid = this.$cookies.get('userData').userId
      this.axios
        .get('/api/RegistTime/GetRequirementTime', {
          params: {
            leaderId: leaderid
          }
        })
        .then(res => {
          //   console.log('getRequirementTime -> res', res)
          for (let key in res.data) {
            if (res.data.hasOwnProperty(key)) {
              let startTime = this.formatDateTime(res.data[key].startTime)
              let endTime = this.formatDateTime(res.data[key].endTime)
              this.requirementTime.push({
                Id: parseInt(res.data[key].id),
                userName: res.data[key].userName,
                classifyTime:
                  res.data[key].classifyTime === 1 ? 'Làm thêm' : 'Xin nghỉ',
                startTime: startTime,
                endTime: endTime,
                nameContact: res.data[key].nameContact,
                phoneContact: res.data[key].phoneContact,
                time: res.data[key].time,
                note: res.data[key].note
              })
            }
          }          
        })
        .catch(err => {
          console.error(err)
        })
    },
    formatDateTime (datetime) {
      let format = datetime.replace('T', ' ').slice(0, 16)
      return format
    },
    removeRequire (id, status, index) {
      console.log('removeRequire -> status', status)
      console.log('removeRequire -> id', id)
      this.axios
        .get('/api/RegistTime/ApproveTime', {
          params: {
            id: id,
            status: status
          }
        })
        .then(res => {
          if (res.data === true) {
            this.$delete(this.requirementTime, index)
            this.$emit('refreshComponent')
            this.$toastr.success('Xác nhận hành công', 'OK!')
          } else this.$toastr.error('', 'Lỗi rồi!')
        })
        .catch(err => {
          console.error(err)
        })
    }
  },
  created () {
    this.getRequirementTime()    
  },
  mounted () {
    // console.log('mounted -> requirementTime', this.requirementTime)
  }
}
</script>

<style lang="css" scoped>
a {
  color: #33b35a;
  text-decoration: none;
}
.breadcrumb-holder {
  background: #eceeef;
}
.container-fluid {
  padding: 0 3rem;
}
</style>
