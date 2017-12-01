/**
 * Created by wisdom on 2017/4/24.
 */

/**
 *  role 为权限设置 * 或者 ture 显示
 * index 为唯一标识,vuex state 存储
 */
// import { baseRole } from './role-map.js'

let item1 = {
  name: 'app',
  text: '首页',
  index: '1',
  icon: 'home',
  display: true,
  dir: 'page/app',
  path: '/app',
  role: '*',
  meta: {
    requiresAuth: true,
    role: '*'
  }
}
//
// let item2 = {
//   name: 'commonProblem',
//   text: '常见问题',
//   index: '1',
//   icon: 'help-circled',
//   display: true,
//   dir: 'page/commonProblem',
//   path: '/commonProblem',
//   role: '*',
//   meta: {
//     requiresAuth: true,
//     role: '*'
//   }
// }
//
// let item3 = {
//   name: 'commonFile',
//   text: '文档共享',
//   index: '1',
//   icon: 'paper-airplane',
//   display: true,
//   dir: 'page/commonFile',
//   path: '/commonFile',
//   role: '*',
//   meta: {
//     requiresAuth: true,
//     role: '*'
//   }
// }

let item2 = {
  name: 'client',
  text: '个人中心',
  index: '3',
  icon: 'android-contact',
  display: true,
  role: '*',
  child: [{
    dir: 'page/myProblemList',
    name: 'myProblemList',
    text: '我的问题',
    index: '3-1',
    icon: 'help-circled',
    path: '/myProblemList',
    display: true,
    role: '*',
    meta: {
      requiresAuth: true,
      role: '*'
    }
  }, {
    dir: 'page/myDocList',
    name: 'myDocList',
    text: '我的文档',
    index: '3-1',
    icon: 'paper-airplane',
    path: '/myDocList',
    display: true,
    role: '*',
    meta: {
      requiresAuth: true,
      role: '*'
    }
  }]
}

// let item5 = {
//   name: 'client',
//   text: '数据字典',
//   index: '3',
//   icon: 'navigate',
//   display: true,
//   role: `${baseRole.baseMsgMenu}`,
//   child: [{
//     dir: 'page/clientMsg',
//     name: 'clientMsg2',
//     text: '部门',
//     index: '3-1',
//     icon: 'person-stalker',
//     path: '/clientPage',
//     display: true,
//     role: `${baseRole.clientMsg}|Get`,
//     meta: {
//       requiresAuth: true,
//       role: '*'
//     }
//   }]
// }
const menus = [item1, item2]
export default menus
