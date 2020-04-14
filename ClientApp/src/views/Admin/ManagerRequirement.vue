<template>
  <div>
    <div class="breadcrumb-holder">
      <div class="container-fluid">
        <ul class="breadcrumb">
          <li class="breadcrumb-item">
            <a href="/admin/index">Home</a>
          </li>
          <li class="breadcrumb-item active">Yêu cầu</li>
        </ul>
      </div>
    </div>
    <section>
      <div class="container-fluid">
        <!-- Page Header-->
        <header>
          <h1 class="h3 col display">Yêu cầu chưa phê duyệt</h1>
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
                    <td>{{ item.fullName }}</td>
                    <td>{{ item.jobTypeName }}</td>
                    <td>{{ item.startDtTm }}</td>
                    <td>{{ item.endDtTm }}</td>
                    <td>{{ item.totalTime }} ngày</td>
                    <td>{{ item.restReasonName }}</td>
                    <td>{{ item.restNameContact }}</td>
                    <td>{{ item.restPhoneContact }}</td>
                    <td>{{ item.note }}</td>
                    <td>
                      <b-button
                        variant="success"
                        @click="approveRequire(item.id, approYes, index)"
                        class="mr-1"
                      >
                        <font-awesome-icon icon="check" />
                      </b-button>
                      <b-button variant="danger" @click="approveRequire(item.id, 2, index)">
                        <font-awesome-icon icon="times" />
                      </b-button>
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
      approYes: this.$cookies.get('userData').userTypeId === 4 ? 3 : 4
      // Nếu là leader thì trạng thái đồng ý sẽ là 3|| còn manager thì sẽ là 4
    }
  },
  methods: {
    getRequirementTime () {
      let leaderid = this.$cookies.get('userData').userId
      let status = this.$cookies.get('userData').userTypeId === 4 ? 1 : 3
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
              let startDtTm = this.formatDateTime(res.data[key].startDtTm)
              let endDtTm = this.formatDateTime(res.data[key].endDtTm)
              this.requirementTime.push({
                id: parseInt(res.data[key].id),
                fullName: res.data[key].fullName,
                jobTypeName: res.data[key].jobTypeName,
                startDtTm: startDtTm,
                endDtTm: endDtTm,
                restReasonName: res.data[key].restReasonName,
                restNameContact: res.data[key].restNameContact,
                restPhoneContact: res.data[key].restPhoneContact,
                totalTime: res.data[key].totalTime,
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
      // console.log('removeRequire -> status', status)
      // console.log('removeRequire -> id', id)
      let title = ''
      let text = ''
      let confirmButtonText = ''
      if (status === 2) {
        title = 'Huỷ yêu cầu?'
        text = 'Bạn có chắc chắn muốn huỷ yêu cầu này?'
        confirmButtonText = 'Xoá!'
      }
      if (status === 3 || status === 4) {
        title = 'Đồng ý yêu cầu?'
        text = 'Bạn có chắc chắn muốn đồng ý yêu cầu này?'
        confirmButtonText = 'Đồng ý!'
      }

      this.$swal({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: confirmButtonText
      }).then(result => {
        if (result.value) {
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
                this.$swal('Hoàn thành!', 'Xác nhận thành công', 'success')
              } else this.$swal('Sorry', 'Bị lỗi gì đó!!!', 'error')
            })
            .catch(err => {
              console.error(err)
            })
        }
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

<style lang="css" scope>

</style>
