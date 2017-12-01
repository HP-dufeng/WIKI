<template>
  <div>
    <section class="question-list-Box">
      <div class="searchTip">
        <span style="color:#999;font-size: 13px;">为您找到相关搜索结果约 {{total}} 个</span>
        <div class="advancedSearch" @click="toggleAdvancedSearch" v-show="false">
          高级搜索
        </div>
      </div>
      <section class="advancedSearchWrap" v-show="advancedSearchShow">
        <!--<h2>高级搜索</h2>-->
        <Form ref="formCustom" :model="formCustom" :rules="ruleCustom" :label-width="80">
          <FormItem label="部门" prop="passwd">
            <Input type="password" v-model="formCustom.passwd"></Input>
          </FormItem>
          <FormItem label="标签" prop="passwdCheck">
            <Input type="password" v-model="formCustom.passwdCheck"></Input>
          </FormItem>
          <FormItem label="标题" prop="age">
            <Input type="text" v-model="formCustom.age" number></Input>
          </FormItem>
          <FormItem label="上传人" prop="age">
            <Input type="text" v-model="formCustom.age" number></Input>
          </FormItem>
          <FormItem label="开始日期" prop="age">
            <DatePicker type="date" placeholder="Select date" v-model="formCustom.date"></DatePicker>
          </FormItem>
          <FormItem label="结束日期" prop="age">
            <DatePicker type="date" placeholder="Select date" v-model="formCustom.date"></DatePicker>
          </FormItem>
          <FormItem>
            <Button type="primary" @click="handleSubmit('formCustom')">搜索</Button>
            <Button @click="toggleAdvancedSearch">关闭</Button>
          </FormItem>
        </Form>
      </section>
      <template v-for="item in questList">
        <doc-model
          :questionItem="item"
          routerName="docItem"
        ></doc-model>
      </template>
      <div class="pager">
        <Page
          :total="total"
          :page-size="pageSize"
          :current.sync="pageCurrent"
          @on-change="handleCurrentChange"
        ></Page>
      </div>
    </section>
  </div>
</template>
<script>
  import docModel from '../../components/docModel/docModel.vue'
  import o from 'o.js'
  import urlAppend from 'url-append'
  export default {
    data () {
      const validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('Please enter your password'))
        } else {
          if (this.formCustom.passwdCheck !== '') {
            // 对第二个密码框单独验证
            this.$refs.formCustom.validateField('passwdCheck')
          }
          callback()
        }
      }
      const validatePassCheck = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('Please enter your password again'))
        } else if (value !== this.formCustom.passwd) {
          callback(new Error('The two input passwords do not match!'))
        } else {
          callback()
        }
      }
      const validateAge = (rule, value, callback) => {
        if (!value) {
          return callback(new Error('Age cannot be empty'))
        }
        // 模拟异步验证效果
        setTimeout(() => {
          if (!Number.isInteger(value)) {
            callback(new Error('Please enter a numeric value'))
          } else {
            if (value < 18) {
              callback(new Error('Must be over 18 years of age'))
            } else {
              callback()
            }
          }
        }, 1000)
      }
      return {
        url: this.$baseUrl.wiki,
        question: null,
        questList: [],
        total: 0, // 总条数
        pageCurrent: 1, // 当前页码
        pageSize: 10, // 每页条数
        advancedSearchShow: false,
        formCustom: {
          passwd: '',
          passwdCheck: '',
          age: ''
        },
        ruleCustom: {
          passwd: [
            {validator: validatePass, trigger: 'blur'}
          ],
          passwdCheck: [
            {validator: validatePassCheck, trigger: 'blur'}
          ],
          age: [
            {validator: validateAge, trigger: 'blur'}
          ]
        }
      }
    },
    mounted: function () {
    },
    computed: {
      getStore () {
        return this.$store.state.common
      }
    },
    watch: {
      'getStore.appTabs': {
        handler: function (val, oldVal) {
          if (val === 'docFile') {
            this.searchFn()
          }
        },
        deep: true
      },
      'getStore.searchBoolean': {
        handler: function (val, oldVal) {
          if (this.getStore.appTabs === 'docFile') {
            this.pageCurrent = 1
            this.searchFn()
          }
        },
        deep: true
      }
    },
    components: {
      'doc-model': docModel
    },
    methods: {
      handleCurrentChange (val) {
        this.searchFn(val)
      },
      pageChangeFn (val) {
      },
      searchFn (pageSkip = 0) {
        let parent = this.$parent.$parent.$parent
        let keyword = parent.keyword
        let _this = this
        let pageSize = _this.pageSize
        let skip = pageSkip === 0 ? 0 : pageSize * (pageSkip - 1)
//        设置最后条数显示
        let total = this.total
        if (total !== 0) {
          let pageNum = Math.ceil(total / pageSize)
          if (pageSkip === pageNum && total % pageSize !== 0) {
            pageSize = (total - (pageSkip - 1) * pageSize)
          }
        }
        let url = this.url + `Content/WIKI.SearchDocument?$filter=ContentType eq 'Document'
        and (contains(Title,'${keyword}'))&$top=${pageSize}&$skip=${skip}&$count=true`
        // todo 最后一页会重复
        o(urlAppend(url, {r: Math.random()})).post().save().then(function (request) {
          let itesLeng = request.data.Count
          if (itesLeng === 0) {
//            _this.$Message.warning('查无相关数据')
            _this.questList = []
            _this.total = itesLeng
            return false
          }
//          console.log(request.data.Datas)
          _this.total = itesLeng
          let qData = request.data.Datas
          qData.forEach(function (item) {
            if (item.Document.CreatedTime !== null) {
              item.Document.CreatedTime = item.Document.CreatedTime.split('T')[0]
            }
            if (item.Document.UpdatedTime !== null) {
              item.Document.UpdatedTime = item.Document.UpdatedTime.split('T')[0]
            }
          })
          _this.questList = qData
//          f5 刷新 搜索
          _this.$router.push({name: 'list',
            query: {
              keyword: parent.keyword
            }})
        })
//        let parent = this.$parent.$parent.$parent
//        let keyword = parent.keyword
//        let _this = this
//        let url = this.url + `DocumentView?$filter=(contains(Title,'${keyword}'))`
//        let countUrl = this.url + `DocumentView/$count?$filter=(contains(Title,'${keyword}'))`
//        o(urlAppend(countUrl, {r: Math.random()})).get(function (itesLeng) {
//          if (itesLeng === 0) {
//            _this.$Message.warning('查无相关数据')
//            return false
//          }
//          _this.total = itesLeng
//          let pageSize = _this.pageSize
//          // todo 最后一页会重复
//          o(urlAppend(url, {r: Math.random()})).take(pageSize).skip(pageSkip).get(function (data) {
//            let qData = data
//            qData.forEach(function (item) {
//              if (item.CreatedTime !== null) {
//                item.CreatedTime = item.CreatedTime.split('T')[0]
//              }
//              if (item.UpdatedTime !== null) {
//                item.UpdatedTime = item.UpdatedTime.split('T')[0]
//              }
//            })
//            _this.questList = qData
//            _this.$router.push({name: 'list',
//              query: {
//                keyword: parent.keyword
//              }})
//          })
//        })
      },
      toggleAdvancedSearch () {
        this.advancedSearchShow = !this.advancedSearchShow
      },
      routerChange (router) {
        this.$router.push({name: router})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" rel="stylesheet/scss">
  .simp-question-wrap{
    width:100%;
  }
  .question-list-Box {
    width: 100%;
    overflow: hidden;
  }
  .advancedSearchWrap{
    margin-top:12px;
  }
  .searchTip {
    font-size: 14px;
  }

  .advancedSearch {
    cursor: pointer;
    float: right;
    text-decoration: underline;
  }

  .searchBox {
    width: 50%;
    /*margin: 0px auto;*/
    margin-left:80px;
    margin-top: 20px;
  }
</style>
