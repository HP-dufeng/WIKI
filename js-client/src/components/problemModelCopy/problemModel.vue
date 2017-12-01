<template>
  <div>
    <div class="simp-question-wrap">
      {{questionItem}}
      <h3 class="simp-question-tit">
        <span class="setTop" v-if="questionItem.Sticky">[置顶]</span>
        <span v-if="questionItem.Important" class="setTop">[重要]</span>
        <label @click="routerChange(questionItem)">{{questionItem.Title}}</label></h3>
      <div class="tag">
        <!--<template v-for="tag in tags">-->
          <!--<Tag>{{tag}}</Tag>-->
        <!--</template>-->
      </div>
      <div class="simp-question-answer" v-html="questionItem.Answer_Text"></div>
      <div class="simp-question-tip">
        <template v-if="questionItem.Answer_UpdatedTime != null">
          <div class="simp-question-tip-right">修改日期：{{questionItem.Answer_UpdatedTime}}</div>
        </template>
        <template v-else>
          <div class="simp-question-tip-right">修改日期：{{questionItem.Answer_CreatedTime}}</div>
        </template>
          <template v-if="questionItem.Answer_UpdatedBy != null">
            <div class="simp-question-tip-left">修改人：{{questionItem.Answer_UpdatedBy}}</div>
          </template>
          <template v-else>
            <div class="simp-question-tip-left">修改人：{{questionItem.Answer_CreatedBy_FullName}}</div>
          </template>
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
            Id: questionItem.Id
          }})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" scoped>

</style>
