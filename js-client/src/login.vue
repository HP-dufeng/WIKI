<template>
  <div class="loginPage"
       :style="'background: url('+ bg +') no-repeat center top;height:100%;'">
    <!--<canvas id="canvas"></canvas>-->
    <app-content></app-content>
  </div>
</template>

<script>
  import Content from './page/loginPage/Content.vue'
  import bg from '../src/assets/image/bg4.jpg'
//  import { canvas1 } from '../src/assets/common/canvasBg'
  export default {
    name: 'login',
    components: {
      'app-content': Content
    },
    data () {
      return {
        bg: bg
      }
    },
    mounted: function () {
//      canvas1()
    },
    methods: {}
  }
</script>
<style lang="scss" type="text/scss">
  #canvas {
    position: absolute;
    top: 0px;
    left: 0px;
    width: 100%;
    height: 100%;
  }

  html, body {
    height: 100% !important;
  }

  .loginLogo {
    position: absolute;
    top: 104px;
    width: 80px;
    height: 80px;
    left: 50%;
    margin-left: -40px;
  }

  .loginPage {
    width: 100%;
    height: 100%;
  }
</style>
