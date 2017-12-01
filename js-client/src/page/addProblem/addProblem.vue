<template>
  <div>
    <!--<top-menu></top-menu>-->
    <section>
      <div class="header">
        <h3>新增常见问题</h3>
      </div>
    </section>
    <section>
      <Form ref="formCustom" :model="addQuestion" :rules="ruleValidate" :label-width="80">
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
        <FormItem label="描述" prop="Text">
          <Input style="width:60%" type="textarea" :rows="3" v-model="addQuestion.Text"></Input>
        </FormItem>
        <div class="line"></div>
        <FormItem label="回答" prop="age" class="edit-wrap">
          <uedit :defaultMsg=defaultMsg :config=config ref="ue"></uedit>
        </FormItem>
        <FormItem>
          <Button type="primary" @click="handleSubmit('formCustom')">提交</Button>
          <Button @click="goBack">取消</Button>
        </FormItem>
      </Form>
    </section>
  </div>
</template>
<script>
  import uedit from '../../components/uEdit/uEdit.vue'
  import topMenu from '../../components/topMenu/topMenu.vue'
  import o from 'o.js'
  import $ from 'jquery'
  //  import urlAppend from 'url-append'
  export default {
    data () {
      return {
        addBtnShow: true,
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
        tags: [],
        lengthIsOk: false, // tag 长度是否达标
        filterTagsArr: [], // 模糊筛选出来的tag
        addQuestion: {
          Title: null,
          Tags: null,
          Sticky: false,
          Important: false,
          Text: null,
          Answers: []
        }
      }
    },
    components: {
      'uedit': uedit,
      'top-menu': topMenu
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
      handleSubmit (name) {
        let url = this.url + 'Question/WIKI.Create'
        let content = this.$refs.ue.getUEContent()
        if (content !== '') {
          this.addQuestion.Answers.push(content)
        }
        this.addQuestion.Tags = this.tags
        let _this = this
        this.$refs[name].validate((valid) => {
          if (valid) {
            o(url).post({dto: this.addQuestion}).save().then(function (data) {
              _this.$Message.success('新增成功')
              _this.$router.replace({
                path: '/myProblemList',
                query: {
                  tip: false
                }
              })
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
