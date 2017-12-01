import {
  LOGIN,
  LOGINLATER,
  LOGIN_CALLBACK,
  LOGOUT,
  LOGIN_FAILED
}
  from './../../mutation-types.js'

import * as actions from './identityActions.js'
import * as getters from './identityGetters.js'

export const STORAGE_IDENTITY_KEY = 'STORAGE_IDENTITY'

const state = {
  user: JSON.parse(localStorage.getItem(STORAGE_IDENTITY_KEY)) || {},
  error: null
}

const mutations = {
  [LOGINLATER] (state, user) {
    state.user = user
    localStorage.setItem(STORAGE_IDENTITY_KEY, JSON.stringify(user))
  },
  [LOGIN] (state) {
  },
  [LOGIN_CALLBACK] (state, user) {
    state.user = user
    localStorage.setItem(STORAGE_IDENTITY_KEY, JSON.stringify(user))
  },
  [LOGOUT] (state) {
    state.user = {}
    localStorage.removeItem(STORAGE_IDENTITY_KEY)
  },
  [LOGIN_FAILED] (state, error) {
    state.error = error
  }
}

export default {
  state,
  mutations,
  actions,
  getters
}
