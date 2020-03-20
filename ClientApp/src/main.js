import axios from 'axios'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import 'bootstrap/dist/css/bootstrap.css'
import Vue from 'vue'
import VueAxios from 'vue-axios'
import VueCookies from 'vue-cookies'
import VueToastr2 from 'vue-toastr-2'
import 'vue-toastr-2/dist/vue-toastr-2.min.css'
import Vuex from 'vuex'
import App from './App.vue'
import router from './router'

Vue.use(Vuex)
window.toastr = require('toastr')
Vue.use(VueToastr2)
Vue.use(VueCookies)
Vue.use(VueAxios, axios)
Vue.config.productionTip = false
// Install BootstrapVue
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)
new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
