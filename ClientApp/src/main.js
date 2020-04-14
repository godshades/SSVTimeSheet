import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import axios from 'axios'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import 'bootstrap/dist/css/bootstrap.css'
import 'sweetalert2/dist/sweetalert2.min.css'
import VeeValidate from 'vee-validate'
import Vue from 'vue'
import VueAxios from 'vue-axios'
import VueCookies from 'vue-cookies'
import VueSweetalert2 from 'vue-sweetalert2'
import VueToastr2 from 'vue-toastr-2'
import 'vue-toastr-2/dist/vue-toastr-2.min.css'
import Vuex from 'vuex'
import App from './App.vue'
import router from './router'

Vue.use(VeeValidate)
library.add(fas)
const optionsSwalAlert = {
  confirmButtonColor: '#28a745',
  cancelButtonColor: '#bd2130'
}
Vue.component('font-awesome-icon', FontAwesomeIcon)
Vue.use(VueSweetalert2, optionsSwalAlert)
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
