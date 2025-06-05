<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MessageBoard._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>留言板</title>
    <style>
        html, body {
            margin: 0;
            padding: 0;
            height: 100%;
            background: #f5f6fa url('image/bg.jpg') no-repeat center center fixed;
            background-size: cover;
            font-family: 'Microsoft YaHei', Arial, sans-serif;
        }

        .center-container {
            max-width: 700px;
            margin: 40px auto;
            background: #fff; /* 保留白色背景 */
            border-radius: 12px;
            box-shadow: 0 4px 24px rgba(0,0,0,0.08);
            padding: 40px 32px;
        }

        .title {
            font-size: 2rem;
            font-weight: bold;
            color: #2d3436;
            margin-bottom: 24px;
            text-align: center;
        }

        .label {
            font-size: 1.1rem;
            margin-bottom: 8px;
            display: block;
            color: #636e72;
        }

        .input, .textarea {
            width: 100%;
            font-size: 1.1rem;
            padding: 10px 12px;
            margin-bottom: 20px;
            border: 1px solid #dfe6e9;
            border-radius: 6px;
            box-sizing: border-box;
        }

        .textarea {
            min-height: 120px;
            resize: vertical;
        }

        .btn {
            font-size: 1.1rem;
            padding: 10px 32px;
            background: #0984e3;
            color: #fff;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            margin-top: 10px;
            margin-bottom: 10px;
            transition: background 0.2s;
        }

        .btn:hover {
            background: #74b9ff;
        }

        .message {
            background: #f1f2f6; /* 白灰色背景（不透明） */
            border-radius: 8px;
            padding: 16px;
            margin-bottom: 18px;
            font-size: 1.05rem;
        }

        .message .btn {
            background: #d63031;
            margin-left: 10px;
            font-size: 0.95rem;
            padding: 6px 18px;
        }

        .message .btn:hover {
            background: #ff7675;
        }

        .banner-top,
        .banner-bottom {
            width: 100%;
            height: 220px;
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
        }

        .banner-top {
            background-image: url('image/bg-top.jpg');
        }

        .banner-bottom {
            background-image: url('image/bg-bottom.jpg');
        }
    </style>
</head>
<body>

    <!-- 留言内容 -->
    <form id="form1" runat="server">
        <div class="center-container">
            <div class="title">欢迎访问武汉大学游客留言模块！</div>
            
            <asp:Repeater ID="rep" runat="server">
                <ItemTemplate>
                    <div class="message">
                        <strong><%#Eval("username") %></strong> 留言于 <%#Eval("createdate") %><br />
                        <div style="margin: 8px 0 12px 0;">&nbsp;&nbsp;&nbsp;<%#Eval("body") %></div>
                        <asp:Button CssClass="btn" ID="btnDel" OnClick="btnDel_Click" CommandArgument='<%#Eval("id") %>' runat="server" Text="删除留言" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <label class="label" for="username">用户名：</label>
            <asp:TextBox CssClass="input" ID="username" runat="server"></asp:TextBox>

            <label class="label" for="txtBody">留言内容：</label>
            <asp:TextBox CssClass="textarea" ID="txtBody" runat="server" TextMode="MultiLine"></asp:TextBox>

            <asp:Button CssClass="btn" ID="btnAdd" runat="server" Text="发送留言" OnClick="btnAdd_Click" />
            <asp:Literal ID="litres" runat="server"></asp:Literal>
        </div>
    </form>

</body>
</html>
