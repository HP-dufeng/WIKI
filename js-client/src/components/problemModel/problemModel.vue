<template>
  <div>
    <div class="simp-question-wrap">
      <h3 class="simp-question-tit">
        <span class="setTop" v-if="questionItem.Question.Sticky">[置顶]</span>
        <span v-if="questionItem.Question.Important" class="setTop">[重要]</span>
        <label @click="routerChange(questionItem)">{{questionItem.Question.Title}}</label></h3>
      <div class="tag">
        <template v-for="tag in questionItem.Tags">
          <Tag>{{tag}}</Tag>
        </template>
      </div>
      <div class="simp-question-answer" v-html="questionItem.Question.Text"></div>
      <div class="simp-question-tip">
        <!--<template v-if="questionItem.Question.UpdatedTime != null">-->
          <!--<div class="simp-question-tip-right">创建日期：{{questionItem.Question.UpdatedTime}}</div>-->
        <!--</template>-->
        <!--<template v-else>-->
          <div class="simp-question-tip-right">创建日期：{{questionItem.Question.CreatedTime}}</div>
        <!--</template>-->
        <div class="simp-question-tip-left">创建人：{{questionItem.Question.ExpandProperty.CreatedBy}}</div>

        <!--<template v-if="questionItem.Question.Answer_UpdatedBy != null">-->
          <!--</template>-->
          <!--<template v-else>-->
            <!--<div class="simp-question-tip-left">修改人：{{questionItem.Question.Answer_CreatedBy_FullName}}</div>-->
          <!--</template>-->
        <!--<div class="simp-question-tip-left">-->
          <!--修改人：<a href="#">张三</a>-->
        <!--</div>-->
      </div>
    </div>
  </div>
</template>
<script>
  import $ from 'jquery'
  export default {
    data () {
      return {}
    },
    props: {
      questionItem: {
        type: Object,
        default: {}
      },
      routerName: {
        type: String,
        default: 'problemItem'
      }
    },
    mounted: function () {
      $(function () {
        $('.topMenu li').hover(function () {
          $(this).find('.top-menu-dl').show()
        }, function () {
          $('.top-menu-dl').hide()
        })
      })
    },
    computed: {
    },
    components: {},
    methods: {
      routerChange (questionItem) {
        this.$router.push({name: this.routerName,
          query: {
            Id: questionItem.Question.Id
          }})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" scoped>

</style>
