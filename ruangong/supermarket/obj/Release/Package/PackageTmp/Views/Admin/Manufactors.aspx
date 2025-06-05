<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Manufactors.aspx.cs" Inherits="OnlineSupermarketTuto.Views.Admin.Suppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       
        body {
            background-image: url('<%= ResolveUrl("~/Assets/Images/5.jpg") %>');
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
                <h3 class="text-center">生产商</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="mb-3">
                    <label for="" class="form-label" style="color: white; font-weight: bold;">生产商名称</label>
                       <input type="text" placeholder="" autocomplete="off" class ="form-control" runat="server" id="MNameTb"/>
                </div>
                <div class="mb-3">
                    <label for="" class="form-label" style="color: white; font-weight: bold;">生产许可证号</label>
                    <input type="text" placeholder="" autocomplete="off" class ="form-control" runat="server" id="PermNumTb"/>
                </div>
                <div class="mb-3">
                    <label for="" class="form-label" style="color: white; font-weight: bold;">产地</label>
                    <asp:DropDownList ID="PlaceCb" runat="server" class="form-control">
                        <asp:ListItem>东北</asp:ListItem>
                        <asp:ListItem>华北</asp:ListItem>
                        <asp:ListItem>华中</asp:ListItem>
                        <asp:ListItem>华南</asp:ListItem>
                        <asp:ListItem>西北</asp:ListItem>
                        <asp:ListItem>西南</asp:ListItem>
                        <asp:ListItem>海外</asp:ListItem>
                    </asp:DropDownList>
                    
                </div>
                
                <div class="row">
                    <asp:Label runat="server" ID="ErrMsg" class="text-danger text-center"></asp:Label>
                    <div class="col-md-4"><asp:Button Text="编辑" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" id="EditBtn" class="btn-warning btn-block btn" Width="100px" OnClick="EditBtn_Click"/></div>
                    <div class="col-md-4"><asp:Button Text="保存" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" id="SaveBtn" class="btn-success btn-block btn" Width="100px" OnClick="SaveBtn_Click"/></div>
                    <div class="col-md-4"><asp:Button Text="删除" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" id="DeleteBtn" class="btn-danger btn-block btn" Width="100px" OnClick="DeleteBtn_Click"/></div>
                </div>
            </div>
            <div class="col-md-8">
                <asp:GridView ID="ManufactList" runat="server" class="table" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="ManufactList_SelectedIndexChanged">
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
