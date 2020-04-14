import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

let router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: () => import('./views/Home.vue'),
      meta: {
        requiresAuth: true
      },
      children: [
        {
          path: '/',
          name: 'registTime',
          component: () => import('./views/Users/RegistTime.vue')
        },
        {
          path: 'thong-tin-user',
          name: 'userInfo',
          component: () => import('./views/Users/UserInfo.vue')
        }
      ]
    },
    {
      path: '/dang-nhap',
      name: 'userlogin',
      component: () => import('./views/Users/Login.vue')
    },
    {
      path: '/admin',
      component: () => import('./views/Admin/Index.vue'),
      meta: {
        requiresAuth: true,
        is_admin: true
      },
      children: [
        {
          path: '/',
          name: 'managerRequirement',
          component: () => import('./views/Admin/ManagerRequirement.vue')
        },
        {
          path: 'danh-sach-user',
          name: 'listUser',
          component: () => import('./views/Admin/ListUser.vue')
        }
      ]
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
      let permission = Vue.$cookies.get('userData').userTypeId
      if (to.matched.some(record => record.meta.is_admin)) {
        if (permission === 4 || permission === 5 || permission === 6) {
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
