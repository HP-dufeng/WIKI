<template>
  <div>
    <top-menu></top-menu>
    <div class="header">
      <!--<div style="float: right;" v-if="isDownExl()">-->
      <!--<Tooltip content="筛选后导出为筛选后的结果，未筛选导出全部结果" placement="left">-->
      <!--<Button type="primary" @click="downExl">导出Exl</Button>-->
      <!--</Tooltip>-->
      <!--</div>-->
      <h3>{{options.title}}</h3>
    </div>
    <xheaderBar
      :headerFn='headerFn'
      :options="options"
      :searchShow="true"
      :refreshShow="true"
      :addShow="false"
      :delShow="false"
    >
    </xheaderBar>
    <xtable
      :tableFn="tableFn"
      :options="options"
    ></xtable>
    <xdetails :options="options">
      <slot>
        <Button type="primary" @click="setEdit">修改</Button>
      </slot>
    </xdetails>
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
  //  import urlAppend from 'url-append'
  export default {
    name: 'QuestionView',
    data () {
      let _this = this
      let url = this.$baseUrl.wiki + `QuestionView`
      return {
        options: Object.assign({}, this.$xvuex.options, {   // options将存入vuex,基础配置看 cVuex.options
          details: null,
          tab: null,
          setQuery: null,  // 模糊搜索默认值
          api: url,  // 原始地址，不会改动
          url: url, // 计算存入的，初始值和api相同
          title: '问题日志',  // 本页面名称
          gridKey: 'problemLog',  // 本页面 Eng名，唯一
          arr: [
            {
              key: 'ContentNo',
              title: '问题编号',
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
//            {
//              key: 'CreatedBy_FullName',
//              title: '提问者',
//              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
//              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
//              details_hide: 12,
//              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
//              table_hide: 12,
//              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
//              type: ''
//            },
//            {
//              key: 'CreatedTime',
//              title: '提问日期',
//              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
//              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
//              details_hide: 12,
//              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
//              table_hide: 12,
//              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
//              type: 'date'
//            },
            {
              key: 'Answer_CreatedBy_FullName',
              title: '回复者',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: ''
            },
            {
              key: 'Answer_CreatedBy_Department',
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
              key: 'UpdatedTime',
              title: '回复日期',
              add_hide: 'relyOn',  // 新增页面 是否显示：不显示写，显示可不写或其他值
              edit_hide: 1, // 编辑页面 是否显示：不显示写，显示可不写或其他值
              details_hide: 12,
              search_hide: 12, // 搜索下拉 是否显示：不显示写，显示可不写或其他值
              table_hide: 12,
              value: '123',  // 当 add_hide 值为relyOn 时，add时这个为依赖，且有value属性
              type: 'date'
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
          name: 'problemItem',
          query: {
            Id: row.Id
          }
        })
      },
      updateData: function (row) {
        this.$router.push({name: 'updateProblem',
          query: {
            Id: row.Id
          }})
        localStorage.setItem(`QuestionView`, JSON.stringify(row))
      },
      delData: function () {
        return {}
      },
      setEdit: function () {
        return {}
      },
      headerFn: function () {
        return {}
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
