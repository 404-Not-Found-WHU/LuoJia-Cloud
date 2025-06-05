<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="OnlineSupermarketTuto.Views.Admin.Customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       
        body {
            background-image: url('<%= ResolveUrl("~/Assets/Images/6.jpg") %>');
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
        }
       
        .container-fluid {
            font-family: '思源黑体', sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <h3 class="text-center">用户管理</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="mb-3">
                    <label for="" class="form-label" style="color: white; font-weight: bold;">用户名</label>
                       <input type="text" placeholder="" autocomplete="off" runat="server" id="CNameTb" class ="form-control" />
                </div>
                <div class="mb-3">
                    <label for="" class="form-label" style="color: white; font-weight: bold;">电子邮箱</label>
                    <input type="email" placeholder="" autocomplete="off" runat="server" id="EmailTb" class ="form-control" />
                </div>
                <div class="mb-3">
                    <label for="" class="form-labels" style="color: white; font-weight: bold;">电话号码</label>
                    <input type="text" placeholder="" autocomplete="off" runat="server" id="PhoneTb" class ="form-control" />
                    
                </div>
                <div class="mb-3">
                    <label for="" class="form-label" style="color: white; font-weight: bold;">密码</label>
                       <input type="text" placeholder="" autocomplete="off" runat="server" id="PassTb" class ="form-control" />
                </div>
                <div class="row">
                    <asp:Label runat="server" ID="ErrMsg" class="text-danger text-center"></asp:Label>
                    <div class="col-md-4"><asp:Button Text="编辑" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" id="EditBtn" runat="server" class="btn-warning btn-block btn" Width="100px" OnClick="EditBtn_Click"/></div>
                    <div class="col-md-4"><asp:Button Text="保存" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" id="SaveBtn" runat="server" class="btn-success btn-block btn" Width="100px" OnClick="SaveBtn_Click"/></div>
                    <div class="col-md-4"><asp:Button Text="删除" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" id="DeleteBtn" runat="server" class="btn-danger btn-block btn" Width="100px" OnClick="DeleteBtn_Click"/></div>
                </div>
            </div>
            <div class="col-md-8">
                <asp:GridView ID="CustomerList" runat="server" class="table" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="CustomerList_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="teal" Font-Bold="false" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
