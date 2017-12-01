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
      <div class="problem-itme-init-text">{{Question.Text}}</div>
      <div class="proble-item-init-msg">
        <span>创建者：{{Question.ExpandProperty.CreatedBy}}</span> <i>|</i>
        <span>所属部门：{{Question.ExpandProperty.CreatedByDepartment}}</span> <i>|</i>
        <span>创建日期：{{Question.CreatedTime}}</span>
      </div>
    </section>
    <section class="problem-itme-answer">
      <template v-for="item in Answers">
        <div>
          <div class="problem-itme-init-text" v-html="item.Text"></div>
          <!--<div class="proble-item-init-msg">-->
            <!--<span>回复者：{{item.ExpandProperty.CreatedBy}}</span> <i>|</i>-->
            <!--<span>所属部门：{{item.ExpandProperty.CreatedByDepartment}}</span> <i>|</i>-->
            <!--<span>回复日期：{{item.CreatedTime}}</span>-->
          <!--</div>-->
        </div>
      </template>

    </section>
  </div>
</template>
<script>
  import topMenu from '../../components/topMenu/topMenu.vue'
  import o from 'o.js'
  import urlAppend from 'url-append'
  // todo  answer 更新日期无效。question里是时时的
  export default {
    data () {
      return {
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
        ViewCount: null,
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
      goBack () {
        window.history.go(-1)
      },
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
//      searchFn () {
//        let searchBoolean = this.$store.state.common.searchBoolean
//        this.$store.dispatch('setCommon', {searchBoolean: !searchBoolean})
//      },
      upDataQuestion () {
        this.$router.push({
          name: 'updateProblem',
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
            let url = _this.$baseUrl.wiki + `Question(${_this.questionId})`
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
        let url = this.$baseUrl.wiki + `Question(${_this.questionId})`
        o(urlAppend(url, {r: Math.random()})).get(function (data) {
          let Question = data.Question
          let Tags = data.Tags
          let Answers = data.Answers
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
