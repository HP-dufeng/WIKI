<template>
  <div style="margin-top: 20px;">
    <Upload
      ref="upload"
      multiple
      :headers="header"
      type="drag"
      :max-size='fileSize'
      :on-exceeded-size="handleMaxSize"
      :on-success="uploadOk"
      :on-error="uploadError"
      :action="upUrl"
      :data='upData'>
      <div style="padding: 20px 0">
        <Icon type="ios-cloud-upload" size="52" style="color: #3399ff"></Icon>
        <p>点击或将文件拖拽到这里上传</p>
      </div>
    </Upload>
  </div>
</template>
<script>
  import store from './../../vuex/store.js'
  import clone from 'clone'
  export default {
    data () {
      let url = this.$baseUrl.file + '/Upload'
      return {
        files: [],
        fileSize: this.config.fileSize,
        header: {
          Authorization: 'Bearer ' + store.state.identity.user.access_token
        },
        upUrl: url
//        upData: {
//          system: 'WIKI',
//          Data: {
//            Id: '10',
//            EventType: 'Document'
//          }
//        }
      }
    },
    props: {
      upData: {
        type: Object,
        default: null
      }
    },
    mounted: function () {
      this.$store.dispatch('setCommon', {fileCenter: []}) // 初始化filecenter
    },
    components: {},
    methods: {
      handleMaxSize (file) {
        this.$Notice.warning({
          title: '超出文件大小限制',
          desc: `文件${file.name}太大，不能超过 ${this.config.fileSize}K。`
        })
      },
      uploadError (errors, file, fileList) {
        const showList = this.$refs.upload.fileList
        showList.push({
          name: `${fileList.name}  --- 失败!`,
          showProgress: false,
          status: 'normal'
        })
        this.$Notice.error({
          title: '上传错误信息',
          desc: file
        })
      },
      uploadOk (data) {
        let _this = this
        data.forEach(function (item) {
          _this.files.push(item)
        })
        let files = clone(this.files)
        this.$store.dispatch('setCommon', {fileCenter: files})
        this.$Message.success('导入成功')
      },
      routerChange (router) {
        this.$router.push({name: router})
      }
    }
  }
</script>
<style lang="scss" type="text/scss" scoped>

  .topMenu {
    width: 100%;
    text-align: right;
    li {
      /*float: left;*/
      display: inline-block;
      margin-right: 18px;
      text-decoration: underline;
      font-size: 15px;
      position: relative;
      cursor: pointer;
      color: #2e82ff;
      font-weight: 700;
      .top-menu-dl {
        display: none;
        position: absolute;
        top: 22px;
        left: 0px;
        font-size: 14px;
        z-index: 999;
        background: red;
        dd {
          line-height: 24px;
          span {
            padding:2px 14px;
            display: block;
            text-align: center;
            /*text-decoration: underline;*/
          }
        }
      }
      span:hover {
        color: #00a0e9;
      }
    }
  }
</style>
