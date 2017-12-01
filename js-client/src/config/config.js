/**
 * Created by wisdom on 2017/4/24.
 */

/**
 *  自定义的一些全局变量
 */

let qSearchSelect = [{
  title: '全部',
  key: 'all'
}, {
  title: '文档编号',
  key: 'all1'
}, {
  title: '标题',
  key: 'all2'
}, {
  title: '上传人',
  key: 'all3'
}, {
  title: '部门',
  key: 'all4'
}, {
  title: '标签',
  key: 'all5'
}, {
  title: '说明',
  key: 'all6'
}, {
  title: '上传日期',
  key: 'all7'
}]

let routers = ['updateDoc', 'updateProblem', 'addDoc', 'addProblem']
const config = {
  fileSize: 20480,   // 上传文件
  qSearchSelect: qSearchSelect,
  routers: routers,  // 离开页面提示
  tagNumberLength: 5,  // 标签个数显示
  tagStringLength: 16   // 标签字数长度限制（字节）
}

export default config
