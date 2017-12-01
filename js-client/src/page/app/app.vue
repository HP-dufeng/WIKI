<template>
  <div>
    <!--<top-menu></top-menu>-->
    <section class="wikiTit">
      <img class="logo" src="../../assets/image/logo.png" alt="">
      <h2>华信期货共享知识库</h2>
    </section>
    <section class="wikiSearchInput">
      <Input v-model="keyword" @keyup.enter.native="routerChange" style="width: 480px"></Input>
      <Button type="primary" icon="ios-search" @click="routerChange">搜索</Button>
    </section>
    <section class="about">
      <a href="http://cefcfco.com/" target="_blank">关于华信期货</a>
      <p>注：“本系统仅供内部交流使用，未经华信期货书面许可，其他任何机构或个人不得擅自传阅、引用或复制”</p>
    </section>
  </div>
</template>
<script>
  import topMenu from '../../components/topMenu/topMenu.vue'
  export default {
    data () {
      return {
        keyword: null
      }
    },
    components: {
      'top-menu': topMenu
    },
    mounted: function () {
    },
    methods: {
      routerChange () {
        if (this.keyword === null || this.keyword === '') {
          this.$Message.warning('请输入关键词')
          return false
        }
        this.$router.push({name: 'list',
          query: {
            keyword: this.keyword
          }})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" rel="stylesheet/scss" scoped>
  .about{
    position: absolute;
    bottom:0px;
    text-align: center;
    font-size:14px;
    width:100%;
    /*width:600px;*/
    /*margin:0px auto;*/
    /*margin-top:4px;*/
    p{
      margin-top:8px;
      /*color:red;*/
    }
  }
  .tabPaneBox{
    width:94%;
    margin:0px auto;
    margin-top:40px;
  }
  .wikiSearchInput {
    /*width:600px;*/
    /*margin:0px auto;*/
    width:100%;
    text-align: center;
    margin-top: 40px;
  }

  .wikiTit {
    text-align: center;
    width: 400px;
    margin: 0px auto;
    margin-top: 120px;
    overflow: hidden;
    .logo {
      width: 70px;
      float: left;
    }
    h2 {
      float: left;
      font-size: 34px;
      line-height: 70px;
      margin-left: 12px;
    }
  }
</style>
