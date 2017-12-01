<template>
  <div>
    <div class="simp-question-wrap">
      <h3 class="simp-question-tit">
        <span class="setTop" v-if="questionItem.Document.Sticky">[置顶]</span>
        <span v-if="questionItem.Document.Important" class="setTop">[重要]</span>
        <label @click="routerChange(questionItem)">{{questionItem.Document.Title}}</label></h3>
      <div class="tag">
        <template v-for="tag in questionItem.Tags">
          <Tag>{{tag}}</Tag>
        </template>
      </div>
      <div class="simp-question-answer" v-html="questionItem.Document.Description"></div>
      <div class="simp-question-tip">
        <!--<template v-if="questionItem.Document.UpdatedTime != null">-->
          <!--<div class="simp-question-tip-right">修改日期：{{questionItem.Document.UpdatedTime}}</div>-->
        <!--</template>-->
        <!--<template v-else>-->
          <div class="simp-question-tip-right">创建日期：{{questionItem.Document.CreatedTime}}</div>
        <!--</template>-->
        <div class="simp-question-tip-left">创建人：{{questionItem.Document.ExpandProperty.CreatedBy}}</div>
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
            Id: questionItem.Document.Id
          }})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" scoped>

</style>
