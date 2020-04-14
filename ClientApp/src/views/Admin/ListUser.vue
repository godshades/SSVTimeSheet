<template>
  <div>
    <div class="breadcrumb-holder">
      <div class="container-fluid">
        <ul class="breadcrumb">
          <li class="breadcrumb-item">
            <a href="/admin/index">Home</a>
          </li>
          <li class="breadcrumb-item active">List</li>
        </ul>
      </div>
    </div>
    <section>
      <div class="container-fluid">
        <b-button @click="addUser()" variant="success" class="ml-3 mb-3">Thêm nhân viên</b-button>
        <!-- Page Header-->
        <header>
          <h1 class="h3 col display">Danh sách nhân viên</h1>
        </header>
        <div class="row">
          <div class="col-lg-12">
            <div class="table-responsive">
              <table class="table table-striped table-hover">
                <thead>
                  <tr>
                    <th>STT</th>
                    <th>Id</th>
                    <th>Tên</th>
                    <th>Leader</th>
                    <th>Phân quyền</th>
                    <th>Ngày tham gia</th>
                    <th>Ngày sinh</th>
                    <th>Email</th>
                    <th>SĐT</th>
                    <th>Ngày nghỉ</th>
                    <th>Tuỳ chọn</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item, index) in listUser" :key="index">
                    <th scope="row">{{ index + 1 }}</th>
                    <td>{{ item.userId }}</td>
                    <td>{{ item.fullName }}</td>
                    <td>{{ item.leaderName }}</td>
                    <td>{{ item.userTypeName }}</td>
                    <td>{{ item.joinDt }}</td>
                    <td>{{ item.birthDt }}</td>
                    <td>{{ item.email }}</td>
                    <td>{{ item.telNo }}</td>
                    <td>{{ item.restDay }}</td>
                    <td>
                      <b-button @click="editUser(item.userId)" variant="success" class="mr-1">
                        <font-awesome-icon icon="edit" />
                      </b-button>
                      <b-button variant="danger" @click="deleteUser(item.userId, index)">
                        <font-awesome-icon icon="trash" />
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
    <ListUserModal :showPopup="showPopup" :userInfo="userInfo" v-on:reloadComponent="loadData()" />
  </div>
</template>

<script>
import ListUserModal from './ListUserModal.vue'
export default {
  data () {
    return {
      showPopup: '',
      listUser: [],
      userInfo: null
    }
  },
  components: {
    ListUserModal
  },
  created () {
    this.loadData()
  },
  methods: {
    addUser () {
      this.showPopup = 'addUser'
      // console.log("showModal -> e", e);
      this.$bvModal.show('modal-list-user')
    },
    editUser (userId) {
      this.showPopup = 'editUser'
      this.axios
        .get('api/Admin/GetUserById', {
          params: {
            userId: userId
          }
        })
        .then(res => {
          // console.log(res.data)
          this.userInfo = res.data
          // console.log(this.userInfo.birthDt)
        })

      this.$bvModal.show('modal-list-user')
    },
    deleteUser (userId, index) {
      this.$swal({
        title: 'Xoá nhân viên?',
        text: 'Bạn có chắc chắn muốn xoá nhân viên này?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Xoá ngay'
      }).then(result => {
        if (result.value) {
          this.axios
            .get('api/Admin/DeleteUser', {
              params: {
                userId: userId
              }
            })
            .then(res => {
              if (res.data === true) {
                this.$delete(this.listUser, index)
                this.$swal('Hoàn thành!', 'Nhân viên đã bị xoá', 'success')
              } else this.$swal('Thất bại!', 'Vui lòng kiểm tra lại', 'error')
            })
            .catch(err => {
              console.log(err)
            })
        }
      })
    },
    formatDateTime (dttime) {
      // format : yyyy-MM-dd
      let format = dttime.split('T')
      return format[0]
    },
    loadData () {
      this.listUser = []
      this.axios
        .get('api/Admin/GetAllUser')
        .then(res => {
          for (const key in res.data) {
            if (res.data.hasOwnProperty(key)) {
              this.listUser.push({
                userId: res.data[key].userId,
                fullName: res.data[key].fullName,
                leaderName: res.data[key].leaderName,
                userTypeName: res.data[key].userTypeName,
                joinDt: this.formatDateTime(res.data[key].joinDt),
                birthDt: this.formatDateTime(res.data[key].birthDt),
                email: res.data[key].email,
                telNo: res.data[key].telNo,
                restDay: res.data[key].restDay
              })
            }
          }
        })
        .catch(err => {
          console.log(err)
        })
    }
  }
}
</script>

<style scoped>
</style>
