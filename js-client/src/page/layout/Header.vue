<template>
  <div class="layout-header">
    <span @click="goHome" class="goHome" title="返回WPBS">
      <Icon type="home"></Icon>
    </span>
    <div class="header-menu">
      <top-menu></top-menu>
    </div>
    <i-button type="text" @click="MenuClick" class="navicon-icon-button">
      <Icon type="navicon" size="32" class="navicon-icon"></Icon>
    </i-button>
    <!--<div class="logo"><img src="../../assets/image/logo.png" alt=""></div>-->
    <!--<span class="project-head">华信期货周边知识库</span>-->

    <person></person>
  </div>
</template>
<script>
  import person from '../../components/person/person.vue'
  import topMenu from '../../components/topMenu/topMenu.vue'

  export default {
    data () {
      return {
        message: 1,
        homePage: 'http://wpbs.wdqh.com:6789/login.html?refresh=',
        refresh: null
      }
    },
    mounted: function () {
    },
    computed: {
      homeLink () {
        return 'http://wpbs.wdqh.com:6789/login.html?refresh=' + this.$store.state.identity.user.refresh_token
      }
    },
    methods: {
      goHome () {
        window.location.href = this.homeLink
        localStorage.removeItem('STORAGE_IDENTITY')
      },
      MenuClick () {
        this.$emit('toggleClick')
      }
    },
    components: {
      'person': person,
      'top-menu': topMenu
    }
  }
</script>
<style lang="scss" type="text/scss" scoped>
  .header-menu {
    float: right;
    display: inline-block;
    margin-right: 20px;
    /*margin-top: 24px;*/
  }

  .goHome {
    font-size: 27px;
    float: right;
    margin: 12px 128px 0px 0px;
    color: #666;
    cursor: pointer;
    &:hover {
      color: #2d8cf0
    }
  }

  .layout-header {
    position: relative;
    min-width: 700px;
  }

  .navicon-icon-button {
    margin-top: 8px;
  }

  .project-head {
    display: inline-block;
    text-align: center;
    line-height: 60px;
    font-size: 23px;
    font-weight: bold;
    margin-left: 225px;
    width: 58%;
  }

  .logo {
    margin-left: 40px;
    position: absolute;
    top: 5px;
    display: inline-block;
    height: 50px;
    width: 158px;
    /*background: url("../../assets/image/logo.png") no-repeat center center;*/
    background-size: cover;
  }
</style>
