import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'

Vue.use(Router)

let router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/dang-nhap',
      name: 'userlogin',
      component: () => import('./views/Users/Login.vue')
    }
  ]
})

router.beforeEach((to, from, next) => {
  const publicPages = ['/dang-nhap']
  const authRequired = !publicPages.includes(to.path)
  console.log('authRequired', authRequired)
  const loggedIn = Vue.$cookies.get('token')
  if (authRequired && loggedIn === null) {
    return next('/dang-nhap')
  }

  next()
})
export default router
