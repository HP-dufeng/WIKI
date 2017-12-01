<template>
  <div style="width:80%;margin:0px auto;">
    <!--<top-menu></top-menu>-->
    <section class="problem-item-tit">
      <div>
        <div class="problem-itme-icon">
          <span class="pointer" @click="upDataQuestion"><Icon type="edit"></Icon>编辑</span>
          <span class="pointer" @click="delQuestion"><Icon type="trash-b"></Icon>删除</span>
          <span><Icon type="eye"></Icon>{{ViewCount}}</span>
          <span @click="goBack" class="goBack">返回</span>
        </div>
        <h3 class="problem-item-h3">
          <span class="setTop" v-if="Question.Sticky === true">[置顶]</span>
          <span v-if="Question.Important === true" class="setTop">[重要]</span>
          {{Question.Title}}
        </h3>
      </div>
      <div class="tag">
        <template v-for="item in Tags">
          <Tag>{{item}}</Tag>
        </template>
      </div>
      <!--<div class="simp-question-answer" v-html="Question.Text"></div>-->
    </section>
    <!--问题简介-->
    <section class="problem-itme-init">
      <div class="problem-itme-init-text">{{Question.Description}}</div>
      <div class="proble-item-init-msg">
        <span>创建者：{{Question.ExpandProperty.CreatedBy}}</span> <i>|</i>
        <span>所属部门：{{Question.ExpandProperty.CreatedByDepartment}}</span> <i>|</i>
        <span>创建日期：{{Question.CreatedTime}}</span>
      </div>
    </section>

    <section class="problem-itme-answer">
      <Table :columns="options.arr" :data="Answers"></Table>
      <!--<div>-->
      <!--<div class="proble-item-init-msg">-->
      <!--<span>上传者：{{Question.ExpandProperty.CreatedBy}}</span> <i>|</i>-->
      <!--<span>所属部门：{{Question.ExpandProperty.CreatedByDepartment}}</span> <i>|</i>-->
      <!--<span>上传日期：{{Question.CreatedTime}}</span>-->
      <!--</div>-->
      <!--</div>-->
    </section>

  </div>
</template>
<script>
  import topMenu from '../../components/topMenu/topMenu.vue'
  import o from 'o.js'
  import urlAppend from 'url-append'
  import store from './../../vuex/store.js'

  export default {
    data () {
      let _this = this
      return {
        FileData: [],
        options: {   // options将存入vuex,基础配置看 cVuex.options
          arr: [
            {
              key: 'DisplayName',
              title: '附件名称',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: ''
            },
            {
              key: 'CreatedBy',
              title: '上传者',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              width: 180,
              type: ''
            },
            {
              key: 'CreatedTime',
              title: '上传时间',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              width: 180,
              type: 'date',
              render: (h, params) => {
                return h('div',
                  {
                    class: 'tableBtn'
                  }, [
                    (function () {
                      try {
                        let time = params.row.CreatedTime
                        return time !== '' ? time.split('T')[0] : ''
                      } catch (e) {
                      }
                    })()
                  ])
              }
            },
            {
              key: 'DownLoadCount',
              title: '下载量',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: '',
              width: 80,
              fixed: 'right'
            },
            {
              key: 'action',
              title: '操作',
              width: 80,
              add_hide: 1,  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              search_hide: 1, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              type: '',
              fixed: 'right',
              render: (h, params) => {
                return h('div',
                  {
                    class: 'tableBtn'
                  }, [
                    (function () {
                      return h('i-button', {
                        attrs: {
                          title: '下载'
                        },
                        props: {
                          type: 'success',
                          size: 'small'
                        },
                        on: {
                          click: () => {
                            _this.down(params.row)
                          }
                        }
                      }, [
                        h('Icon', {
                          props: {
                            type: 'ios-cloud-download-outline'
                          }
                        })
                      ])
                    })()
                  ])
              }
            }
          ]
        },
        questionItem: {
          Sticky: false,
          Important: false
        },
        Answers: [],
        Question: {
          ExpandProperty: {
            CreatedBy: null,
            CreatedByDepartment: null
          }
        },
        ViewCount: [],
        Tags: [],
        questionId: null
      }
    },
    components: {
      'top-menu': topMenu
    },
    mounted: function () {
      this.getQuestion()
      this.setLookNumber()
    },
    methods: {
      setLookNumber () {
        let _this = this
        let url = _this.$baseUrl.wiki + `ViewLogDaily/WIKI.Create`
        let parameter = {
          dto: {
            ContentId: Number(this.questionId)
          }
        }
        o(urlAppend(url, {r: Math.random()})).post(parameter).save().then(function (data) {
        })
      },
      setDownNumber (AttachmentId) {
        let _this = this
        let url = _this.$baseUrl.wiki + `DownLoadLogDaily/WIKI.Create`
        let parameter = {
          dto: {
            AttachmentId: Number(AttachmentId)
          }
        }
        o(urlAppend(url, {r: Math.random()})).post(parameter).save().then(function (data) {
          _this.getQuestion()
        })
      },
//      getFileList () {
//        let _this = this
//        let url = this.$baseUrl.wiki + `DocumentAttachment?$filter=DocumentId eq ${this.questionId}`
//        o(urlAppend(url, {r: Math.random()})).get(function (data) {
//          _this.FileData = data
//        })
//      },
      goBack () {
        window.history.go(-1)
      },
      down (data) {
        let filename = data.FileName
        let displayName = data.DisplayName
        var xhr = new XMLHttpRequest()
        xhr.open('GET', `${this.$baseUrl.file}/Download?filename=${filename}&scope=`)
        xhr.onreadystatechange = handler
        xhr.responseType = 'blob'
        xhr.setRequestHeader('Authorization', 'Bearer ' + store.state.identity.user.access_token)
        xhr.send()
        let _this = this

        function handler () {
          if (this.readyState === this.DONE) {
            if (this.status === 200) {
              if (window.navigator.msSaveBlob) {
                try {
                  var blobObject = new Blob([this.response])
                  window.navigator.msSaveBlob(blobObject, displayName)
                  _this.setDownNumber(data.Id)
                } catch (e) {
                  console.log(e)
                }
              } else {
                var link = document.createElement('a')
                link.href = window.URL.createObjectURL(this.response)
                link.download = displayName
                link.click()
                _this.setDownNumber(data.Id)
              }
            } else {
              this.$Message.warning('下载失败')
            }
          }
        }
      },
//      searchFn () {
//        let searchBoolean = this.$store.state.common.searchBoolean
//        this.$store.dispatch('setCommon', {searchBoolean: !searchBoolean})
//      },
      upDataQuestion () {
        this.$router.push({
          name: 'updateDoc',
          query: {
            Id: this.questionId
          }
        })
      },
      delQuestion () {
        let _this = this
        _this.$Modal.confirm({
          title: '删除确认',
          content: '此操作将删除该项, 是否继续?',
          onOk: function () {
            let url = _this.$baseUrl.wiki + `Document(${_this.questionId})`
            o(urlAppend(url, {r: Math.random()})).remove().save().then(function (data) {
              _this.$Message.success('删除成功')
              _this.goBack()
            })
          }
        })
      },
      getQuestion () {
        let _this = this
        _this.questionId = this.$route.query.Id
        let url = this.$baseUrl.wiki + `Document(${_this.questionId})`
        o(urlAppend(url, {r: Math.random()})).get(function (data) {
          let Question = data.Document
          let Tags = data.Tags
          let Answers = data.Attachments
          let ViewCount = data.ViewCount
          if (Question.CreatedTime !== null) {
            Question.CreatedTime = Question.CreatedTime.split('T')[0]
          }
          if (Question.UpdatedTime !== null) {
            Question.UpdatedTime = Question.UpdatedTime.split('T')[0]
          }
          Answers.forEach(function (item) {
            if (item.CreatedTime !== null) {
              item.CreatedTime = item.CreatedTime.split('T')[0]
            }
            if (item.UpdatedTime !== null) {
              item.UpdatedTime = item.UpdatedTime.split('T')[0]
            }
          })
          _this.Tags = Tags
          _this.Question = Question
          _this.Question = Question
          _this.Answers = Answers
          _this.ViewCount = ViewCount
        })
      }
    }
  }
</script>
<style lang="scss" type="text/scss" rel="stylesheet/scss" scoped>
  .pointer {
    cursor: pointer;
  }

  .problem-item-tit {
    margin-top: 20px;
  }

  .problem-item-h3 {
    font-size: 20px;
    line-height: 30px;
  }

  .problem-itme-icon {
    float: right;
    font-size: 13px;
    span {
      display: inline-block;
      margin-right: 12px;
      line-height: 30px;
      i {
        margin-right: 3px;
      }
    }
  }

  .tag {
    margin-top: 6px;
  }

  .problem-itme-init {
    margin-top: 12px;
    padding-bottom: 20px;
    font-size: 14px;
    border-bottom: 1px solid #dbdbdb;
    .problem-itme-init-text {
      color: #666 !important;
    }
  }

  .proble-item-init-msg {
    margin-top: 4px;
    text-align: right;
    color: #999;
    font-size: 12px;
    span {
      padding: 0px 6px;
    }
    i {
      position: relative;
      top: -1px;
    }
  }

  .problem-itme-answer {
    margin-top: 20px;
    font-size: 15px;
  }
</style>
