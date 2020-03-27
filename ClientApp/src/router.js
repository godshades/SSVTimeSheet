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
      component: Home,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/dang-nhap',
      name: 'userlogin',
      component: () => import('./views/Users/Login.vue')
    },
    {
      path: '/admin',
      name: 'admin',
      component: () => import('./views/Admin/Index.vue'),
      meta: {
        requiresAuth: true,
        is_admin: true
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  const loggedIn = Vue.$cookies.get('token')
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (loggedIn === null) {
      next({
        path: '/dang-nhap',
        params: { nextUrl: to.fullPath }
      })
    } else {
      let permission = Vue.$cookies.get('userData').typeId
      if (to.matched.some(record => record.meta.is_admin)) {
        if (permission === 2 || permission === 3) {
          next()
        } else {
          next({ name: 'home' })
        }
      } else {
        next()
      }
    }
  } else {
    next()
  }
})
export default router
