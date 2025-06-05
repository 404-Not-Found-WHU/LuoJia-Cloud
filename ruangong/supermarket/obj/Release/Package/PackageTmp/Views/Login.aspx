<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineSupermarketTuto.Views.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录</title>
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
    </style>
</head>
<body>
    <h1 class="system-title">武汉大学文创商品购买网站</h1>

    <form id="form1" runat="server">
        <div class="container d-flex justify-content-center align-items-center" style="min-height: 80vh;">
            <div class="col-md-4 login-card">
                <img src="../Assets/Images/whu.png" class="login-logo" />

                <div class="mt-3">
                    <label for="UnameTb" class="form-label">电子邮箱</label>
                    <input type="email" placeholder="" autocomplete="off" class="form-control" runat="server" id="UnameTb" />
                </div>

                <div class="mt-3">
                    <label for="PasswordTb" class="form-label">密码</label>
                    <input type="password" placeholder="" autocomplete="off" class="form-control" runat="server" id="PasswordTb" />
                </div>

                <div class="mt-3 d-grid">
                    <asp:Label runat="server" ID="ErrMsg" CssClass="text-danger text-center"></asp:Label><br />
                    <asp:Button Text="登录" runat="server" CssClass="btn btn-success w-100" ID="LoginBtn" OnClick="LoginBtn_Click"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
