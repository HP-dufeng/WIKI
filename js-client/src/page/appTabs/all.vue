<template>
  <div>
    <section class="question-list-Box">
      <problem-model></problem-model>
    </section>
  </div>
</template>
<script>
  import problemModel from '../../components/problemModel/problemModel.vue'
  export default {
    data () {
      return {
        value: null
      }
    },
    components: {
      'problem-model': problemModel
    },
    methods: {
      routerChange (router) {
        this.$router.push({name: router})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" rel="stylesheet/scss">
  .question-list-Box {
    width: 100%;
    overflow: hidden;
  }

</style>
