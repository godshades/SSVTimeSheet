<template>
  <div class="about">
    <p>Chào mừng {{ this.$route.params.username }}</p>
    <table class="from-group">
      <tr v-for='data in datas'>
        <td>{{ data.id }}</td>
        <td>{{ data.name }}</td>
        <td>{{ data.age }}</td>
      </tr>
    </table>

    <button type="button" @click="TestVinhNC">This is an about page</button>
  </div>
</template>

<script>
  import axios from 'axios'
  export default {
    name: 'about',
    data() {
      return {
        datas: []
      }
    },
    methods: {
      TestVinhNC: function () {
        //axios.get('/loginUser/getAll',).then(response => {
        //  console.log(response.data)
        //})
        //  .catch(e => {
        //    console.log(e)
        //  });
        let dt = this;
        axios({
          method: 'get',
          url: '/loginUser/getAll',
          headers: {
            'Content-Type': 'application/json'
          }
        })
          .then(function (reponse) {
            dt.datas = reponse.data;
          })
          .catch(e => {
            console.log(e)
          });
      }
    }
  }
</script>
