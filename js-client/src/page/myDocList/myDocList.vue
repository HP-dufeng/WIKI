<template>
  <div>
    <!--<top-menu></top-menu>-->
    <div class="header">
      <!--<div style="float: right;" v-if="isDownExl()">-->
      <!--<Tooltip content="筛选后导出为筛选后的结果，未筛选导出全部结果" placement="left">-->
      <!--<Button type="primary" @click="downExl">导出Exl</Button>-->
      <!--</Tooltip>-->
      <!--</div>-->
      <h2>{{options.title}}</h2>
    </div>
    <xheaderBar
      :headerFn='headerFn'
      :options="options"
      :searchShow="true"
      :refreshShow="true"
      :addShow="false"
    >
      <slot>
        <Button type="primary" @click="routerChange('addDoc')">
          <Icon type="plus"></Icon>
        </Button>
      </slot>
    </xheaderBar>
    <xtable
      :tableFn="tableFn"
      :options="options"
    ></xtable>
    <!--<xdetails :options="options">-->
      <!--<slot>-->
        <!--<Button type="primary" @click="setEdit">修改</Button>-->
      <!--</slot>-->
    <!--</xdetails>-->
    <xedit
      :options="options"
      :editFn="editFn"
    >
    </xedit>
    <!--&lt;!&ndash;新增&ndash;&gt;-->
    <!--<xadd :options="options"-->
    <!--:addFn="addFn"-->
    <!--&gt;-->
    <!--</xadd>-->
    <!--分页-->
    <xpagers :options="options"></xpagers>
  </div>
</template>
<script>
  import topMenu from '../../components/topMenu/topMenu.vue'
  import o from 'o.js'
  import clone from 'clone'
  import urlAppend from 'url-append'

  export default {
    name: 'QuestionView',
    data () {
      let _this = this
      let url = this.$baseUrl.wiki + `DocumentView`
//      let url = this.$baseUrl.wiki + `DocumentView?$orderby=CreatedTime desc`
      return {
        url: this.$baseUrl.wiki,
        options: Object.assign({}, this.$xvuex.options, {   // options将存入vuex,基础配置看 cVuex.options
          pager_Opts: [10, 15, 30],  // 每页展示数量
          pager_Size: 10,   //  默认显示每页数量，和opts第一个一样
          timeSearch: true,
          details: null,
          tab: null,
          setQuery: null,  // 模糊搜索默认值
          api: url,  // 原始地址，不会改动
          url: url, // 计算存入的，初始值和api相同
          title: '我的文档',  // 本页面名称
          gridKey: 'myDocList',  // 本页面 Eng名，唯一
          arr: [
            {
              type: 'selection',
              width: 60,
              add_hide: 1,  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              search_hide: 1, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              details_hide: 1,
              align: 'center',
              fixed: 'left'
            },
            {
              key: 'ContentNo',
              title: '文档编号',
              width: 180,
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: ''
            },
            {
              key: 'Title',
              title: '标题',
              width: 450,
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              render: (h, params) => {
                return h('div',
                  {
                    class: 'tableBtn'
                  }, [
                    (function () {
                      return h(
                        'a', {
                          on: {
                            click: () => {
                              _this.routerQuestion(params.row)
                            }
                          }
                        }, params.row.Title)
                    })()
                  ])
              },
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: ''
            },
            {
              key: 'CreatedBy',
              title: '创建者',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: ''
            },
            {
              key: 'CreatedBy_Department',
              title: '部门',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: ''
            },
            {
              key: 'CreatedTime',
              title: '创建日期',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              sortable: 'custom',
              type: 'date'
            },
            {
              key: 'action',
              title: '操作',
              width: 110,
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
                      return h(
                        'i-button', {
                          attrs: {
                            title: '编辑'
                          },
                          props: {
                            type: 'info',
                            size: 'small'
                          },
                          on: {
                            click: () => {
                              _this.updateData(params.row)
                            }
                          }
                        }, [

                          h('Icon', {
                            props: {
                              type: 'edit'
                            }
                          })
                        ]
                      )
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
                            _this.delData(params.row)
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
        })
      }
    },
    components: {
      'top-menu': topMenu
    },
    computed: {
      getOptions () {
        return this.$store.state[this.options.gridKey]
      }
    },
    mounted: function () {
      this.$xvuex.registerModule(this, this.options, this.options.gridKey)
      // 处理table render的select 和 date
      this.$f.tableRender(this, this.getOptions, clone)
      this.$f.fnReset(this, o)
    },
    methods: {
      routerQuestion: function (row) {
        this.$router.push({
          name: 'docItem',
          query: {
            Id: row.Id
          }
        })
      },
      updateData: function (row) {
        this.$router.push({
          name: 'updateDoc',
          query: {
            Id: row.Id
          }
        })
      },
      delData: function (item) {
        let _this = this
        _this.$Modal.confirm({
          title: '删除确认',
          content: '此操作将删除该项, 是否继续?',
          onOk: function () {
            let url = _this.url + `Document(${item.Id})`
            o(url).remove().save().then(function (data) {
              _this.$store.dispatch(_this.options.gridKey + '_set_refresh')
            })
          }
        })
      },
      setEdit: function () {
        return {}
      },
      headerFn: function () {
        return {
          batchDel () { // 批量删除
            let _self = this
            let delObjs = _self.getOptions.delData
            let $length = delObjs.length
            if ($length === 0) {
              this.$Message.warning('请先选中需要删除的项目。')
              return
            }
            let nowNumber = 0
            _self.$Modal.confirm({
              title: '批量删除确认111',
              content: '此操作将删除选中项, 是否继续?',
              onOk: function () {
                delObjs.forEach(function (Item) {
                  let url = _self.$baseUrl.wiki + `Document(${Item.Id})`
                  o(urlAppend(url, {r: Math.random()})).remove().save().then(function (data) {
                    nowNumber += 1
                    if (nowNumber === $length) {
                      _self.$Message.info('删除成功')
                      _self.$store.dispatch(_self.options.gridKey + '_set_refresh')
                    }
                  })
                })
                //            删除最后一页 bug
                let states = _self.$store.state[_self.options.gridKey]
                let pagerCurrentPage = states.pager_CurrentPage
                let pageSize = states.pager_Size
                let pagerTotal = states.pager_Total
//            console.log(pager_Total % pageSize + '---------' + $length)
                if (pagerCurrentPage > 1 && pagerTotal % pageSize === $length) {
                  _self.$store.dispatch(_self.options.gridKey + '_set_state_data', {pager_CurrentPage: pagerCurrentPage - 1})
                }
                _self.$store.dispatch(_self.options.gridKey + '_set_state_data', {delData: []})
              }
            })
          }
        }
      },
      addFn: function () {
        return {}
      },
      editFn: function () {
        return {}
      },
      tableFn: function () {
        return {}
      },
      routerChange (router) {
        this.$router.push({name: router})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" rel="stylesheet/scss" scoped></style>
