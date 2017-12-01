/* eslint-disable */
identitySiteRootSite = 'http://wucc.wdqh.com:6789'

// wiki 接口
wikiSite = 'http://wiki.cefcfco.com:6789/api/odata/'
fileSite = 'http://filecenter.cefcfco.com:6789/api/filemanage/odata'

// 解决url多层级报错
localStorage.setItem('identitySiteRootSite', identitySiteRootSite)
localStorage.setItem('fileSite', fileSite)
localStorage.setItem('wikiSite', wikiSite)

var refresh_token = window.location.href.split('refresh=')[1]
// var refresh_token = 'e90234fdcfdeab72dbd40737f729d26b'
// var refresh_token = false
//  ?refresh=e90234fdcfdeab72dbd40737f729d26b
if (refresh_token) {
  slientRenew(refresh_token)
}
function slientRenew (refresh_token) {
  let _this = this
  let clientId = 'Wpbs'
  let clientSecret = 'secret'
  var auth = 'Basic ' + btoa(`${clientId}:${clientSecret}`)
  var datarefresh = {
    grant_type: 'refresh_token',
    refresh_token: refresh_token
    // refresh_token: JSON.parse(localStorage.getItem('STORAGE_IDENTITY')).refresh_token
  }
  var body = ''
  for (let key in datarefresh) {
    if (body.length) {
      body += '&'
    }
    body += key + '='
    body += encodeURIComponent(datarefresh[key])
  }

  var xhr = new XMLHttpRequest()
  xhr.onload = function (e) {
    let responseData = JSON.parse(xhr.response)
    if (xhr.status === 200 && responseData.access_token) {
      let expiresIn = responseData.expires_in
      let accesstoken = responseData.access_token
      // 获取refresh_token并储存用户名
      let refreshtoken = responseData.refresh_token
      _this.getUser(accesstoken, expiresIn, refreshtoken)
    } else {
      console.log(e)
    }
  }
  xhr.open('POST', identitySiteRootSite + '/identity/connect/token')
  xhr.setRequestHeader('Authorization', auth)
  xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded')
  xhr.send(body)
}
function getUser (accessToken, expiresIn, refreshtoken) {
  let _this = this
  let url = identitySiteRootSite + '/identity/connect/userinfo'
  let xhr = new XMLHttpRequest()
  xhr.onload = function (e) {
    var responseData = JSON.parse(xhr.response)
    let userProfile = {}
    userProfile.profile = responseData
    var timestamp = (new Date()).getTime() / 1000 + expiresIn
    let user = Object.assign({}, userProfile, {access_token: accessToken}, {expires_at: timestamp}, {refresh_token: refreshtoken})
    if (xhr.status === 200) {
      const STORAGE_IDENTITY_KEY = 'STORAGE_IDENTITY'
      localStorage.setItem(STORAGE_IDENTITY_KEY, JSON.stringify(user))
      location.href = 'index.html'
    }
  }
  xhr.open('GET', url)
  xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken)
  xhr.send()
}
