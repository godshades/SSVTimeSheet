<template>
  <div>
    <div class="breadcrumb-holder">
      <div class="container-fluid">
        <ul class="breadcrumb">
          <li class="breadcrumb-item"><a href="/admin/index">Home</a></li>
          <li class="breadcrumb-item active">Yêu cầu</li>
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
                    <th>Tên</th>
                    <th>Kiểu thời gian</th>
                    <th>Thời gian bắt đầu</th>
                    <th>Thời gian kết thúc</th>
                    <th>Tổng thời gian</th>
                    <th>Lý do</th>
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
                    <td>{{ item.reasonName }}</td>
                    <td>{{ item.nameContact }}</td>
                    <td>{{ item.phoneContact }}</td>
                    <td>{{ item.note }}</td>
                    <td>
                      <b-button
                        variant="success"
                        @click="approveRequire(item.Id, approYes, index)"
                        class="mr-1"
                        ><font-awesome-icon icon="check"
                      /></b-button>
                      <b-button
                        variant="danger"
                        @click="approveRequire(item.Id, 2, index)"
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
      requirementTime: [],
      approYes: this.$cookies.get('userData').typeId === 2 ? 3 : 4
      // Nếu là leader thì trạng thái đồng ý sẽ là 3|| còn manager thì sẽ là 4
    }
  },
  methods: {
    getRequirementTime () {
      let leaderid = this.$cookies.get('userData').userId
      let status = this.$cookies.get('userData').typeId === 2 ? 1 : 3
      // lấy quyền người dùng
      this.axios
        .get('/api/RegistTime/GetRequirementTime', {
          params: {
            leaderId: leaderid,
            status: status
            // nếu là leader(per == 2) thì nhận yêu cầu là chưa duyệt (1)
            // còn nếu là manager (3) tức là khác 2 => thì nhận yêu cầu đã duyệt
            // của leader (leader đã duyệt thì stt = 3)
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
                reasonName: res.data[key].reasonName,
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
    approveRequire (id, status, index) {
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
