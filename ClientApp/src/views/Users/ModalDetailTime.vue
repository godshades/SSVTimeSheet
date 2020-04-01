<template>
  <div>
    <b-modal id="detail-time" hide-footer size="md">
      <div class="ss-title">
        <h2 class="title">Chi tiết</h2>
      </div>
      <div class="content-details table-responsive">
        <table class="table table-striped table-hover">
          <tbody>
            <tr>
              <th>Kiểu</th>
              <td>{{ registDetail.classifyName }}</td>
            </tr>
            <tr>
              <th>Tên leader</th>
              <td>{{ registDetail.leaderName }}</td>
            </tr>
            <tr>
              <th>Giờ bắt đầu</th>
              <td>{{ registDetail.startTime }}</td>
            </tr>
            <tr>
              <th>Giờ kết thúc</th>
              <td>{{ registDetail.endTime }}</td>
            </tr>
            <tr>
              <th>Tổng thời gian</th>
              <td>{{ registDetail.time }}</td>
            </tr>
            <tr>
              <th>Ngày bắt đầu</th>
              <td>{{ registDetail.startTime }}</td>
            </tr>
            <tr>
              <th>Ngày kết thúc</th>
              <td>{{ registDetail.endTime }}</td>
            </tr>
             <tr>
              <th>Lý do nghỉ</th>
              <td>{{ registDetail.reasonName }}</td>
            </tr>
            <tr>
              <th>Tên liên hệ</th>
              <td>{{ registDetail.nameContact }}</td>
            </tr>
            <tr>
              <th>Sđt liên hệ</th>
              <td>{{ registDetail.phoneContact }}</td>
            </tr>
            <tr>
              <th>Trạng thái</th>
              <td>{{ registDetail.statusName }}</td>
            </tr>
            <tr>
              <th>Ghi chú</th>
              <td>{{ registDetail.note }}</td>
            </tr>

          </tbody>
        </table>
        <div class="d-flex mt-3">
          <b-button variant="success" class="mr-3">Edit</b-button>
          <b-button variant="success">Delete</b-button>
        </div>
      </div>
    </b-modal>
  </div>
</template>

<script>
export default {
  name: 'modal-detailtime',
  props: ['registDetail'],
  methods: {
  }
}
</script>

<style scoped></style>
