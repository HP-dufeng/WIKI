<template>
  <div :class="['user', {userselect: messageShow}]" @click="personShow">
    <span class="person-icon"></span>
    <span class="user-name">{{user.profile.FullName}}</span>
    <div class="person-messageShow" v-show="messageShow">
      <div class="person-message">
        <span class="person-image"></span>
        <p class="person-userFullName">
          {{user.profile.FullName}}
        </p>
        <p class="person-SalesDepartment">
          <small>{{user.profile.SalesDepartment}}</small>
        </p>
      </div>
      <div class="person-button">
        <Button class="person-password">
          <a @click="show=true">
            <Icon type="unlocked"></Icon>
            修改密码
          </a>
        </Button>
        <Button @click="logout" class="person-layout">退出</Button>
      </div>
    </div>
    <template>
      <div>
        <Modal
          v-model="show"
          title="修改密码"
          @on-cancel="show=false"
          :mask-closable="false"
          width="600"
        >
          <div class="password">
            <Form ref="formCustom"
                  :model="formCustom"
                  :rules="ruleCustom"
                  :label-width="120">
              <Row>
                <Col span="20">
                <Form-item label="旧密码" prop="OldPassword">
                  <Input type="password" v-model="formCustom.OldPassword"></Input>
                </Form-item>
                <Form-item label="新密码" prop="NewPassword">
                  <Input type="password" v-model="formCustom.NewPassword"></Input>
                </Form-item>
                <Form-item label="确认密码" prop="passwdCheck">
                  <Input type="password" v-model="formCustom.passwdCheck"></Input>
                </Form-item>
                </Col>
              </Row>
            </Form>
          </div>
          <div slot="footer">
            <Button @click="show=false">取消</Button>
            <Button type="primary" @click="handleSubmit('formCustom')">提交修改</Button>
          </div>
        </Modal>
      </div>
    </template>
  </div>
</template>
<script>
  import o from 'o.js'
  import { mapGetters } from 'vuex'

  export default {
    data () {
      const validatePass = (rule, value, callback) => {
        if (value.length < 6) {
          callback(new Error('密码不能少于6位'))
        }
        if (value === '') {
          callback(new Error('请输入密码'))
        } else {
          if (this.formCustom.passwdCheck !== '') {
            // 对第二个密码框单独验证
            this.$refs.formCustom.validateField('passwdCheck')
          }
          callback()
        }
      }
      const validatePassCheck = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'))
        } else if (value !== this.formCustom.NewPassword) {
          callback(new Error('两次输入密码不一致!'))
        } else {
          callback()
        }
      }
      return {
        ruleCustom: {
          NewPassword: [
            {validator: validatePass, trigger: 'blur'}
          ],
          passwdCheck: [
            {validator: validatePassCheck, trigger: 'blur'}
          ]
        },
        formCustom: {
          NewPassword: '',
          OldPassword: '',
          passwdCheck: ''
        },
        show: false,
        messageShow: false
      }
    },
    methods: {
//      ...mapActions({
//        // 登出
//        logout: 'logout'
//      }),
      logout () {
        localStorage.removeItem('STORAGE_IDENTITY')
        window.location.href = 'login.html'
      },
      // 修改密码
      handleSubmit (formName) {
        let _self = this
        this.$refs[formName].validate((valid) => {
          if (valid) {
            const apiUrl = _self.$baseUrl.roleUrl + '/User/WUCC.ChangePassword'
            o(apiUrl).post({
              OldPassword: _self.formCustom.OldPassword,
              NewPassword: _self.formCustom.NewPassword
            }).save().then(function (result) {
              _self.show = false // 关闭弹窗
              _self.$Message.success('修改成功')
              _self.handleReset('formCustom') // 初始化表格
            })
          } else {
            console.log('error submit!!')
            return false
          }
        })
      },
      handleReset (name) {
        this.$refs[name].resetFields()
      },
      personShow () {
        this.messageShow = !this.messageShow
      }
    },
    computed: {
      ...mapGetters({
        user: 'user'
      }),
      currentHref: function () {
        return escape(window.location.href)
      }
    },
    mounted () {
    }
  }
</script>
<style lang="scss" type="text/scss" scoped>
  .person-icon {
    margin-left: 20px;
    border-radius: 50%;
    top: 50%;
    position: absolute;
    margin-top: -13px;
    display: inline-block;
    height: 26px;
    width: 26px;
    background: url("../../assets/image/defaultUser.jpg") no-repeat center center;
    background-size: cover
  }

  .user-name {
    padding-left: 20px;
    padding-right: 25px;
    display: inline-block;
    height: 60px;
    line-height: 60px;
    margin-left: 40px;
  }

  .user {
    display: inline-block;
    position: absolute;
    height: 60px;
    right: 0;
  }

  .user:hover {
    background: #dadde0;
  }

  .userselect {
    background: #dadde0;
  }

  .person-messageShow {
    border-radius: 5px;
    position: absolute;
    width: 300px;
    height: 250px;
    right: 0;
    top: 62px;
    z-index: 800;
    background: #dadde0;
  }

  .person-message {
    border-radius: 5px 5px 0 0;
    background: #657180;
    width: 300px;
    height: 200px;
  }

  .person-image {
    border-radius: 50%;
    margin-left: -45px;
    left: 50%;
    top: 15px;
    position: absolute;
    display: inline-block;
    width: 90px;
    height: 90px;
    background: url("../../assets/image/defaultUser.jpg") no-repeat center center;
    background-size: cover
  }

  .person-userFullName {
    font-size: 16px;
    text-align: center;
    padding-top: 120px;
    color: #ffffff;
  }

  .person-SalesDepartment {
    font-size: 14px;
    text-align: center;
    padding-top: 10px;
    color: #ffffff;
  }

  .person-password {
    margin-top: 10px;
    margin-left: 20px;
  }

  .person-layout {
    margin-top: 10px;
    margin-left: 110px;
  }
</style>
