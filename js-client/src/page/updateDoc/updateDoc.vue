<template>
  <div>
    <!--<top-menu></top-menu>-->
    <section>
      <div class="header">
        <h2 style="margin-left: 80px;margin-bottom: 20px;">编辑共享文档</h2>
      </div>
    </section>
    <section>
      <Form ref="submit" :model="addQuestion" :rules="ruleValidate" :label-width="80">
        <Row>
          <Col span="8">
          <FormItem label="标题" prop="Title">
            <Input style="width:90%" type="text" v-model="addQuestion.Title"></Input>
          </FormItem>
          </Col>
          <Col span="2">
          <FormItem label="置顶" prop="Sticky">
            <i-switch v-model="addQuestion.Sticky"></i-switch>
          </FormItem>
          </Col>
          <Col span="2">
          <FormItem label="重要" prop="Important">
            <i-switch v-model="addQuestion.Important"></i-switch>
          </FormItem>
          </Col>
        </Row>
        <FormItem label="标签" prop="Tags">
          <template v-for="(item,index) in tags">
            <Tag closable @on-close="handleClose(index)" color="green">{{item}}</Tag>
            <!--<span class="tag-style"></span>-->
          </template>
          <div class="tagpositon">
            <Input v-show="addBtnShow" @on-change="tagWatchFn" style="width:180px" type="text"
                   v-model="formCustom.tag"></Input>
            <ul class="filter-tags">
              <template v-for="item in filterTagsArr">
                <li @click="addTag(item)">{{item}}</li>
              </template>
            </ul>
            <Button v-show="addBtnShow" type="primary" @click="addTag">添加</Button>
          </div>
        </FormItem>
        <!--<FormItem label="置顶" prop="Sticky">-->
        <!--<i-switch v-model="addQuestion.Sticky"></i-switch>-->
        <!--</FormItem>-->
        <!--<FormItem label="重要" prop="Important">-->
        <!--<i-switch v-model="addQuestion.Important"></i-switch>-->
        <!--</FormItem>-->
        <FormItem label="描述" prop="Description">
          <Input style="width:60%" type="textarea" :rows="3" v-model="addQuestion.Description"></Input>
        </FormItem>
        <FormItem label="附件列表" prop="Description">
          <!--<xtable-->
          <!--:options="options"-->
          <!--&gt;</xtable>-->
          <Table :columns="options.arr" :data="FileData"></Table>
        </FormItem>
        <div class="line"></div>
        <!--<FormItem label="回答" prop="age" class="edit-wrap">-->
        <!--<uedit :defaultMsg=defaultMsg :config=config ref="ue"></uedit>-->
        <!--</FormItem>-->
        <div style="width:60%;margin-left: 80px;">
          <upload-model
            :upData="upData"
          ></upload-model>
        </div>
        <FormItem>
          <Button class="mt20" type="primary" @click="handleSubmit('submit')">提交</Button>
          <Button class="mt20" @click="goBack">取消</Button>
        </FormItem>
      </Form>
    </section>
  </div>
</template>
<script>
  import uedit from '../../components/uEdit/uEdit.vue'
  import topMenu from '../../components/topMenu/topMenu.vue'
  import uploadModel from '../../components/uploadModel/uploadModel.vue'

  import o from 'o.js'
  import urlAppend from 'url-append'
  import store from './../../vuex/store.js'
  import $ from 'jquery'
  //  import clone from 'clone'
  export default {
    data () {
      let _this = this
      return {
        addBtnShow: true,
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
              fixed: 'right',
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              width: 80,
              type: ''
            },
            {
              key: 'action',
              title: '操作',
              width: 220,
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
                    })(),
                    (function () {
                      return h('i-button', {
                        attrs: {
                          title: '删除'
                        },
                        props: {
                          type: 'error',
                          size: 'small'
                        },
                        on: {
                          click: () => {
                            _this.deleteFile(params.row)
                          }
                        }
                      }, [
                        h('Icon', {
                          props: {
                            type: 'trash-a'
                          }
                        })
                      ])
                    })()
                  ])
              }
            }
          ]
        },
        upData: {
          system: 'WIKI',
          Data: null
        },
        questionId: null,
        url: this.$baseUrl.wiki,
        defaultMsg: '',
        formCustom: {
          tag: null
        },
        ruleValidate: {
          Title: [
            {required: true, message: '标题必填', trigger: 'blur'}
          ]
        },
        lengthIsOk: false, // tag 长度是否达标
        filterTagsArr: [], // 模糊筛选出来的tag
        tags: [],
        DocumentAttachments: [],
        addQuestion: {
          Title: null,
          Tags: null,
          Sticky: false,
          Important: false,
          Description: null,
          DocumentAttachments: []
        }
      }
    },
    components: {
      'uedit': uedit,
      'upload-model': uploadModel,
      'top-menu': topMenu
    },
    watch: {
      'getCommon.fileCenter': {
        handler: function (val, oldVal) {
          if (val.length !== 0) {
            this.handleSubmit()
          }
        },
        deep: true
      }
    },
    computed: {
      getOptions () {
        return this.$store.state[this.options.gridKey]
      },
      getCommon () {
        return this.$store.state.common
      }
    },
    mounted: function () {
      this.questionId = this.$route.query.Id
      this.upData.Data = `{'Id':'null','EventType':'Document'}`
      this.setDate()
    },
    methods: {
      filterTags (val) {
        let _this = this
        let url = this.url + `Tag/WIKI.Search?$filter=contains(Value,'${val}')`
        o(url).post().save().then(function (data) {
          _this.filterTagsArr = data.data
          if (_this.filterTagsArr.length !== 0) {
            $('.filter-tags').show()
          }
        })
      },
      tagWatchFn (val) {
        let value = this.formCustom.tag
        this.filterTags(value)
        let length = this.publishFn.strLeng(value)
        if (length > this.config.tagStringLength) {
          this.$Message.info(`标签最长不能超过${this.config.tagStringLength}个字节`)
          this.lengthIsOk = false
        } else {
          this.lengthIsOk = true
        }
      },
//      getFileList () {
//        let _this = this
//        let url = this.url + `DocumentAttachment?$filter=DocumentId eq ${this.questionId}`
//        o(urlAppend(url, {r: Math.random()})).get(function (data) {
//          _this.FileData = data
//        })
//      },
      goBack () {
        window.history.go(-1)
      },
      addTag (val) {
        $('.filter-tags').hide()
        let tag = this.formCustom.tag
        if (typeof val === 'string') {
          tag = val
        }
        let lengthIsOk = this.lengthIsOk
        if (tag !== null && lengthIsOk) {
          this.tags.push(tag)
          this.formCustom.tag = null
          if (this.tags.length === this.config.tagNumberLength) {
            this.addBtnShow = false
          }
        }
      },
      handleClose (index) {
        this.tags.splice(index, 1)
        if (this.tags.length < this.config.tagNumberLength) {
          this.addBtnShow = true
        }
      },
      toggleAdvancedSearch () {
        this.advancedSearchShow = !this.advancedSearchShow
      },
      deleteFile (row) {
        let _self = this
        let url = this.$baseUrl.wiki + `/DocumentAttachment`
        _self.$Modal.confirm({
          title: '删除确认',
          content: '此操作将删除该文件, 是否继续?',
          onOk: function () {
            o(url).find(row['Id']).remove().save().then(function (data) {
              let msg = row.Name ? row.Name + '删除成功' : '删除成功'
              _self.$Message.info(msg)
              _self.setDate()
            })
          }
        })
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

        function handler () {
          if (this.readyState === this.DONE) {
            if (this.status === 200) {
              var link = document.createElement('a')
              link.href = window.URL.createObjectURL(this.response)
              link.download = displayName
              link.click()
            } else {
              this.$Message.warning('下载失败')
            }
          }
        }
      },
      setDate () {
        let _this = this
        let url = this.url + `Document(${this.questionId})`
        o(urlAppend(url, {r: Math.random()})).get(function (data) {
          let item = data.Document
          let tags = data.Tags
          _this.tags = tags
          _this.addQuestion.Title = item.Title
          _this.addQuestion.Important = item.Important
          _this.addQuestion.Sticky = item.Sticky
          _this.addQuestion.Description = item.Description
          _this.FileData = data.Attachments
          if (data.Attachments.length !== 0) {
            _this.DocumentAttachments = []
            data.Attachments.forEach(function (item) {
              let file = {
                FileName: item.FileName,
                DisplayName: item.DisplayName
              }
              _this.DocumentAttachments.push(file)
            })
          }
        })
      },
      handleSubmit (type) {
        let url = this.url + `Document(${this.questionId})/WIKI.Update`
        this.addQuestion.Tags = this.tags
        this.addQuestion.DocumentAttachments = [...this.DocumentAttachments, ...this.getCommon.fileCenter]
        let _this = this
        this.$refs.submit.validate((valid) => {
          if (valid) {
            o(urlAppend(url, {r: Math.random()})).post({dto: this.addQuestion}).save().then(function (data) {
              if (type === 'submit') {
                _this.$Message.success('修改成功')
                _this.$router.replace({
                  path: '/docItem',
                  query: {
                    Id: _this.questionId,
                    tip: false
                  }
                })
              } else {
                _this.$store.dispatch('setCommon', {fileCenter: []}) // 初始化filecenter
                _this.setDate()
              }
            })
          } else {
            this.$Message.error('Fail!')
          }
        })
      }
    }
  }
</script>
<style lang="scss" type="text/scss" rel="stylesheet/scss">
  .edit-wrap {
    width: 62%;
    margin-top: 20px;
    margin-bottom: 20px;
    .edui-default {
      line-height: 1em;
    }
  }

  .tag-style {
    display: inline-block;
    padding: 2px 6px;
    margin: 2px 4px 2px 0px;
    border: 1px solid #CFD7DC;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border-radius: 3px;
    font-size: 12px;
    line-height: 1.5;
  }
</style>
