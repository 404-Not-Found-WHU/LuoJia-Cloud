<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="OnlineSupermarketTuto.Views.Admin.Categories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        body {
            background-image: url('<%= ResolveUrl("~/Assets/Images/9.jpg") %>');
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
                <h3 class="text-center">商品类别管理</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="mb-3">
                   
                    <label class="form-label" style="color: white; font-weight: bold;">分类名称</label>
                    <input type="text" autocomplete="off" class="form-control" runat="server" id="CatNameTb"/>
                </div>
                <div class="mb-3">
                    <label class="form-label" style="color: white; font-weight: bold;">分类描述</label>
                    <input type="text" autocomplete="off" class="form-control" runat="server" id="DescriptionTb"/>
                </div>
                <div class="row">
                    <asp:Label runat="server" ID="ErrMsg" class="text-danger text-center"></asp:Label>
                    <div class="col-md-4">
                        <asp:Button Text="编辑分类" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" id="EditBtn" class="btn-warning btn-block btn" Width="100px" OnClick="EditBtn_Click"/>
                    </div>
                    <div class="col-md-4">
                        <asp:Button Text="保存分类" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" id="SaveBtn" class="btn-success btn-block btn" Width="100px" OnClick="SaveBtn_Click"/>
                    </div>
                    <div class="col-md-4">
                        <asp:Button Text="删除分类" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" id="DeleteBtn" class="btn-danger btn-block btn" Width="100px" OnClick="DeleteBtn_Click"/>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <asp:GridView ID="CategoryList" runat="server" class="table" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="CategoryList_SelectedIndexChanged">
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
