<template>
  <div>
    <script ref="editor" id="editor" type="text/plain"></script>
  </div>
</template>
<script>
  export default {
    name: 'UE',
    data () {
      return {
        editor: null,
        msg: null
      }
    },
    props: {
      defaultMsg: {
        type: String
      },
      config: {
        type: Object
      }
    },
    computed: {
      getStore () {
        return this.$store.state.common
      }
    },
    watch: {
      'getStore.ueBoolean': {
        handler: function (val, oldVal) {
          const _this = this
          let text = this.$store.state.common.ueText
          if (text !== '' && text !== null && val !== oldVal) {
//            _this.msg = text
            this.editor.addListener('ready', function () {
              _this.editor.setContent(text) // 确保UE加载完成后，放入内容。
            })
            // 加载为初始化后执行
            try {
              _this.editor.setContent(text)
            } catch (e) { }
          }
        },
        deep: true
      }
    },
    mounted () {
      this.editor = window.UE.getEditor('editor', this.config) // 初始化UE
//      const _this = this
//      this.editor.addListener('ready', function () {
//        console.log(_this.msg)
//        _this.editor.setContent(_this.msg) // 确保UE加载完成后，放入内容。
//      })
    },
    methods: {
      getUEContent () { // 获取内容方法
        return this.editor.getContent()
      }
    },
    destroyed () {
      this.editor.destroy()
    }
  }
</script>
