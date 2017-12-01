/**
 *   这个页面存放林琳碎碎 的一些data
 */
import {
  SET_COMMON
} from './../../mutation-types.js'

const state = {
  appTabs: 'question',
  searchBoolean: true,
  ueBoolean: true,
  ueText: String,
  fileCenter: []
}

const getters = {}

const mutations = {
  [SET_COMMON] (state, data) {
    Object.assign(state, data)
  }
}

const actions = {
  setCommon ({commit, state}, data) {
    commit(SET_COMMON, data)
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
