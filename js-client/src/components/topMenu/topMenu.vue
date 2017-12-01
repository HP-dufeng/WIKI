<template>
  <div>
    <!--<ul class="topMenu">-->
      <!--<li @click="routerChange('app')"><span>首页</span></li>-->
      <!--<li>个人中心-->
        <!--<dl class="top-menu-dl">-->
          <!--<dd @click="routerChange('myProblemList')"><span>我的问题</span></dd>-->
          <!--<dd @click="routerChange('myDocList')"><span>我的文档</span></dd>-->
        <!--</dl>-->
      <!--</li>-->
    <!--</ul>-->
    <div class="topMenu">
      <Menu mode="horizontal" active-name="1">
        <MenuItem name="1">
          <div @click="routerChange('app')">首页</div>
        </MenuItem>
        <Submenu name="2">
          <template slot="title">
            个人中心
          </template>
            <MenuItem name="2-1"><div @click="routerChange('myProblemList')">我的问题</div></MenuItem>
            <MenuItem name="2-2"><div @click="routerChange('myDocList')">我的文档</div></MenuItem>
        </Submenu>
      </Menu>
    </div>
  </div>
</template>
<script>
  import $ from 'jquery'
  export default {
    data () {
      return {}
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
    components: {},
    methods: {
      routerChange (router) {
        this.$router.push({name: router})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" scoped>

.topMenu {
  /*width:100%;*/
  /*text-align: right;*/
  /*li{*/
    /*!*float: left;*!*/
    /*display: inline-block;*/
    /*margin-right:18px;*/
    /*!*text-decoration: underline;*!*/
    /*font-size: 16px;*/
    /*position: relative;*/
    /*cursor: pointer;*/
    /*color:#2e82ff;*/
    /*font-weight:700;*/
    /*padding:2px 10px;*/
    /*.top-menu-dl{*/
      /*display: none;*/
      /*position: absolute;*/
      /*top: 28px;*/
      /*left: 9px;*/
      /*font-size: 14px;*/
      /*z-index:999;*/
      /*background: #fff;*/
      /*dd{*/
        /*line-height:24px;*/
        /*span{*/
          /*display: block;*/
          /*padding:2px 4px;*/
          /*text-align: center;*/

          /*!*text-decoration: underline;*!*/
        /*}*/
      /*}*/
    /*}*/
    /*span:hover{*/
      /*color:#00a0e9;*/
    /*}*/
  /*}*/
}
</style>
