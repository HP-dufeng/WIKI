<template>
  <div>
    <!--<top-menu></top-menu>-->
    <section>
      <div class="header">
        <h2 style="margin-left: 80px;margin-bottom: 20px;">编辑常见问题</h2>
      </div>
    </section>
    <!--<section>-->
    <!--<div class="header">-->
    <!--<h3>编辑常见问题</h3>-->
    <!--</div>-->
    <!--</section>-->
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
  import urlAppend from 'url-append'
  import $ from 'jquery'

  export default {
    data () {
      return {
        addBtnShow: true,
        questionId: null,
        AnswersId: null,
        url: this.$baseUrl.wiki,
        defaultMsg: '',
        noAnswer: true,
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
    computed: {},
    mounted: function () {
      this.questionId = this.$route.query.Id
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
      updataUedit (text) {
        let ueBoolean = this.$store.state.common.ueBoolean
        this.$store.dispatch('setCommon', {
          ueBoolean: !ueBoolean,
          ueText: text
        })
//        this.$store.dispatch('setCommon', {ueText: text})
      },
      setDate () {
        let _this = this
        let url = this.url + `Question(${this.questionId})`
        o(urlAppend(url, {r: Math.random()})).get(function (data) {
          let Question = data.Question
          let Tags = data.Tags
          let Answers = data.Answers
          if (Answers.length === 0) {
            _this.noAnswer = true
          } else {
            _this.noAnswer = false
            _this.AnswersId = Answers[0].Id
            _this.defaultMsg = Answers[0].Text
            _this.updataUedit(Answers[0].Text)
          }
          _this.tags = Tags
          _this.addQuestion.Title = Question.Title
          _this.addQuestion.Important = Question.Important
          _this.addQuestion.Sticky = Question.Sticky
          _this.addQuestion.Text = Question.Text
        })
      },
      handleSubmit (name) {
        this.$refs[name].validate((valid) => {
          if (valid) {
            this.updataQuestion()
            if (this.noAnswer) {
              this.addAnswer()
            } else {
              this.updataAnswer()
            }
          } else {
            this.$Message.error('Fail!')
          }
        })
      },
      addAnswer () {
        let url = this.url + `Answer`
        let content = this.$refs.ue.getUEContent()
        let addAnswerObj = {
          QuestionId: Number(this.questionId),
          Text: content
        }
        let _this = this
        o(urlAppend(url, {r: Math.random()})).post(addAnswerObj).save().then(function (data) {
//          _this.$Message.success('编辑成功')
          // 更新answer 状态
          _this.setDate()
        })
      },
      updataQuestion () {
        let url = this.url + `Question(${this.questionId})/WIKI.Update`
        let _this = this
        let questionObj = {
          dto: {
            Title: _this.addQuestion.Title,
            Sticky: _this.addQuestion.Sticky,
            Important: _this.addQuestion.Important,
            Text: _this.addQuestion.Text,
            Tags: _this.tags
          }
        }
        o(urlAppend(url, {r: Math.random()})).post(questionObj).save().then(function (data) {
//          _this.$Message.success('编辑成功')
          _this.$router.replace({
            path: '/problemItem',
            query: {
              Id: _this.questionId,
              tip: false
            }
          })
        })
      },
      updataAnswer () {
        let _this = this
        let url = this.url + `Answer(${_this.AnswersId})`
        let content = this.$refs.ue.getUEContent()
        if (content !== '') {
          this.addQuestion.Answers.push(content)
        }
        let answerObj = {
          QuestionId: Number(this.questionId),
          Text: content
        }
        o(urlAppend(url, {r: Math.random()})).patch(answerObj).save(function (data) {
          _this.$Message.success('编辑成功')
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
