<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnlineSupermarketTuto.Views.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>注册</title>
    <link rel="stylesheet" href="../Assets/Lib/css/bootstrap.min.css" />
    <style>
        body {
            background-image: url('../Assets/Images/1.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            min-height: 100vh;
        }
        .login-card {
            background-color: rgba(255, 255, 255, 0.9);
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
        }
        .login-logo {
            display: block;
            margin: 0 auto 20px auto;
            height: 160px;
            width: 136px;
        }
        .system-title {
            font-family: 'STXingkai', 'KaiTi', '楷体', 'FangSong', '仿宋', serif;
            font-size: 36px;
            font-weight: bold;
            text-align: center;
            color: pink;
            text-shadow: 2px 2px 5px black;
            margin-top: 60px;
            margin-bottom: 30px;
        }
        /* 新增：输入框错误提示样式 */
        .invalid-feedback {
            display: block;
        }
    </style>
</head>
<body>
    <h1 class="system-title">武汉大学文创商品购买网站</h1>
    <form id="form1" runat="server">
        <div class="container d-flex justify-content-center align-items-center" style="min-height: 80vh;">
            <div class="col-md-5 login-card">
                <img src="../Assets/Images/whu.png" class="login-logo" />

                <!-- 用户名输入框 -->
                <div class="mt-3 position-relative">
                    <label for="NameTb" class="form-label">用户名</label>
                    <input type="text" 
                           placeholder="请输入用户名（2-12位汉字/字母）" 
                           autocomplete="off" 
                           class="form-control" 
                           runat="server" 
                           id="NameTb" 
                           required 
                           minlength="2" 
                           maxlength="12" />
                   
                </div>

                <!-- 电子邮箱输入框 -->
                <div class="mt-3 position-relative">
                    <label for="UnameTb" class="form-label">电子邮箱</label>
                    <input type="email" 
                           placeholder="请输入有效邮箱（如user@whu.edu.cn）" 
                           autocomplete="off" 
                           class="form-control" 
                           runat="server" 
                           id="UnameTb" 
                           required />
                  
                </div>

                <!-- 密码输入框 -->
                <div class="mt-3 position-relative">
                    <label for="PasswordTb" class="form-label">密码</label>
                    <input type="password" 
                           placeholder="请输入密码（6-16位，含字母+数字）" 
                           autocomplete="off" 
                           class="form-control" 
                           runat="server" 
                           id="PasswordTb" 
                           required 
                           minlength="6" 
                           maxlength="16" />
                  
                </div>

                <!-- 电话号码输入框 -->
                <div class="mt-3 position-relative">
                    <label for="PhoneTb" class="form-label">电话号码</label>
                    <input type="tel" 
                           placeholder="请输入手机号" 
                           autocomplete="off" 
                           class="form-control" 
                           runat="server" 
                           id="PhoneTb" 
                           required 
                           pattern="^1[3-9]\d{9}$" />
                    
                </div>

                <!-- 错误提示与注册按钮 -->
                <div class="mt-3 d-grid">
                    <asp:Label runat="server" ID="ErrMsg" CssClass="text-danger text-center mb-2"></asp:Label>
                    <asp:Button Text="注册" 
                                runat="server" 
                                CssClass="btn btn-primary w-100 py-2" 
                                ID="RegisterBtn" 
                                OnClick="RegisterBtn_Click" />
                </div>

                <!-- 登录跳转链接 -->
                <div class="mt-3 text-center small">
                    已有账号？<a href="Login.aspx" class="text-primary">立即登录</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>