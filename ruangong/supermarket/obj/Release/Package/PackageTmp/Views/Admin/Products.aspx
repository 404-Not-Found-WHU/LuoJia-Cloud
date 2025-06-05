<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="OnlineSupermarketTuto.Views.Admin.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: url('<%= ResolveUrl("~/Assets/Images/4.jpg") %>');
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
                <h3 class="text-center">商品管理</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="mb-3">
                    <label class="form-label" style="color: white; font-weight: bold;">商品名称</label>
                    <input type="text" runat="server" id="PNameTb" class="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label" style="color: white; font-weight: bold;">生产厂家</label>
                    <asp:DropDownList ID="PManufactCb" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label class="form-label" style="color: white; font-weight: bold;">商品类型</label>
                    <asp:DropDownList ID="PCatCb" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label class="form-label" style="color: white; font-weight: bold;">价格</label>
                    <input type="text" runat="server" id="PriceTb" class="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label" style="color: white; font-weight: bold;">库存</label>
                    <input type="text" runat="server" id="QtyTb" class="form-control" />
                </div>
                <div class="row">
                    <asp:Label runat="server" ID="ErrMsg" CssClass="text-danger text-center" />
                    <div class="col-md-4">
                        <asp:Button Text="编辑" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" ID="EditBtn" CssClass="btn-warning btn-block btn" Width="100px" OnClick="EditBtn_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button Text="保存" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" ID="SaveBtn" CssClass="btn-success btn-block btn" Width="100px" OnClick="SaveBtn_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button Text="删除" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" ID="DeleteBtn" CssClass="btn-danger btn-block btn" Width="100px" OnClick="DeleteBtn_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <asp:GridView ID="ProductList" runat="server" CssClass="table"
                    AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnSelectedIndexChanged="ProductList_SelectedIndexChanged"
                    DataKeyNames="PId,PManufact,PCategory">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="选择" />
                        <asp:BoundField DataField="PId" HeaderText="商品编号" />
                        <asp:BoundField DataField="PName" HeaderText="商品名称" />
                        <asp:BoundField DataField="ManufactName" HeaderText="生产商" />
                        <asp:BoundField DataField="CatName" HeaderText="商品类型" />
                        <asp:BoundField DataField="PQty" HeaderText="库存" />
                        <asp:BoundField DataField="PPrice" HeaderText="价格" />
                    </Columns>
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
