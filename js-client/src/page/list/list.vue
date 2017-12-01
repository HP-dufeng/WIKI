<template>
  <div>
    <!--<top-menu></top-menu>-->
    <section>
      <div class="header">
        <h2 style="margin-left: 80px;">搜索结果</h2>
      </div>
    </section>
    <section class="searchBox">
      <div>
        <Input v-model="keyword" @keyup.enter.native="searchFn">
        <!--<Select v-model="selectKey" slot="prepend" style="width:140px">-->
          <!--<Option value="day">Day</Option>-->
          <!--<Option value="month">Month</Option>-->
        <!--</Select>-->
          <Button slot="append" icon="ios-search" @click="searchFn"> 搜索</Button>
        </Input>
      </div>
    </section>

    <section class="tabPaneBox">
      <Tabs v-model="tabName" :animated="false" @on-click="changeTabs(tabName)">
        <!--<TabPane label="置顶知识" name="topWiki">-->
        <!--<all></all>-->
      <!--</TabPane>-->
      <TabPane label="常见问题" name="question">
        <question></question>
      </TabPane>
      <TabPane label="共享文档" name="docFile">
        <doc-file></doc-file>
      </TabPane>
      </Tabs>
    </section>
  </div>
</template>
<script>
  import all from '../appTabs/all.vue'
  import question from '../appTabs/question.vue'
  import docFile from '../appTabs/docFile.vue'
  import topMenu from '../../components/topMenu/topMenu.vue'
  export default {
    data () {
      return {
        tabName: 'question',
        keyword: null,
        selectKey: null
      }
    },
    computed: {
      getCommon () {
        return this.$store.state.common
      }
    },
    components: {
      'all': all,
      'top-menu': topMenu,
      'question': question,
      'doc-file': docFile
    },
    mounted: function () {
      this.keyword = this.$route.query.keyword
//      this.changeTabs('question')
      this.tabName = this.getCommon.appTabs
//      console.log(this.tabName)
      this.searchFn()
    },
    methods: {
      searchFn () {
        let searchBoolean = this.$store.state.common.searchBoolean
        this.$store.dispatch('setCommon', {searchBoolean: !searchBoolean})
      },
      changeTabs (tab) {
        this.$store.dispatch('setCommon', {appTabs: tab})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" rel="stylesheet/scss" scoped>
  .tabPaneBox{
    width: 50%;
    /* margin: 0px auto; */
    margin-left: 80px;
    margin-top: 20px;
  }
</style>
