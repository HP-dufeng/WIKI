/**
 *   这个页面存放林琳碎碎 的一些data
 */
import {
  SET_DIC,
  INIT_FN
} from './../../mutation-types.js'
import o from 'o.js'
import store from '../../store.js'
// import {dicBook} from '../../../config/dic-map.js'
import urlAppend from 'url-append'
function getDicData (url, key) {
  o(urlAppend(url + key, {r: Math.random()})).get().then(function (data) {
    let datas = data.data
    if (datas.length === 0) {
      return
    }
    let o = {}
    o[key] = datas
    store.dispatch('setDic', o)
  })
}
let url = localStorage.getItem('rbsSite') + '/'

const state = {
  dic: {
    VarietyDict: [], //    品种
    BusinessTypeDict: [], //    业务类型
    FuturesDealDirectionDict: [], //    期货交易方向
    PromptGoodsDealDirectionDict: [], //    现货交易方向
    ContractPerformStatusDict: [], //    合同履行状态
    ContractPostponeDict: [], //    合同是否延期
    UndertakeDepartmentDict: [], //    承办部门
    DepartmentDict: [], //    总部协同
    StdWRDutyPaidStatusDict: [],  // 完税状态
    // WarehousingCompanyDict: [],
    StdWRStatusDict: [],
    WRHoldStatusDict: [],
    WRSourceDict: [],
    CargoStatusDict: [],   // 货物状态
    // WarehouseDict: [],  // 实际存放库
    SystemBusinessDetailDict: [],
    BillFormatDict: [],   // 账单类型
    SettlementWarehouseAttributeDict: [],  // 交割库属性
    StandardWarehouseTypeDict: [],  // 基准库类型
    WarehousingCompanyDetails: [],  // 仓储公司信息
    WarehouseDetails: [],  // 仓储公司信息  --> 实际存放库
    FeeTypeDict: [],  // 费用类型
    AdditionalInsuranceDict: [],  // 是否追保
    InOutFundDirectionDict: [],  // 出入金方向
    FundTypeDict: [],  // 资金类型
    FundAccountTypeDic: [],  // 资金账户类型
    WarehouseReceiptTypeDict: [] //    仓单类型
  }
}

const getters = {}

const mutations = {
  [SET_DIC] (state, data) {
    Object.assign(state.dic, data)
  },
  [INIT_FN] (state, dicName) {
    getDicData(url, dicName)
  }
}

const actions = {
  setDic ({commit, state}, data) {
    commit(SET_DIC, data)
  },
  initFn ({commit, state}, dicName) {
    commit(INIT_FN, dicName)
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
