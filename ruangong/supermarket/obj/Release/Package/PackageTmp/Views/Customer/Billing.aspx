<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="OnlineSupermarketTuto.Views.Customer.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintBill() {
            var PGrid = document.getElementById('<%=ShoppingCartList.ClientID%>');
            PGrid.bordr = 0;
            var PWin = window.open('', 'PrintGrid', 'left=100, top=100, width=1024, height=768, tollbar=0, scrollbars = 1, status=0, resizable=1');
            PWin.document.write(PGrid.outerHTML);
            PWin.document.close();
            PWin.focus();
            PWin.print();
            PWin.close();
        }
    </script>
    <style>
        /* 页面背景图设置，仅应用于页面底层，不影响主要内容区 */
        body {
            background-image: url('<%= ResolveUrl("~/Assets/Images/7.jpg") %>');
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
        }
        /* 设置中文字体为思源黑体 */
        .container-fluid {
            font-family: '思源黑体', sans-serif;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MyContent" runat="server">
    <div class="container-fluid">
        <div class="row"></div>
        <div class="row">
            <div class="col-md-5">
                <h3 class="text-center" style="color: white; font-weight: bold;">文创购买</h3>
                <div class="row">
                    <div class="col">
                        <div class="mt-3">
                            <label class="form-label" style="color: white; font-weight: bold;">商品名称</label>
                            <input type="text" autocomplete="off" class="form-control" runat="server" id="PnameTb" />
                        </div>
                    </div>
                    <div class="col">
                        <div class="mt-3">
                            <label class="form-label" style="color: white; font-weight: bold;">价格</label>
                            <input type="text" autocomplete="off" class="form-control" runat="server" id="PriceTb" />
                        </div>
                    </div>
                    <div class="col">
                        <div class="mt-3">
                            <label class="form-label" style="color: white; font-weight: bold;">数量</label>
                            <input type="text" autocomplete="off" class="form-control" runat="server" id="QtyTb" />
                        </div>
                    </div>
                    <div class="row mt-3 mb-3">
                        <div class="col d-grid">
                            <asp:Button Text="添加商品" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" ID="AddToBillBtn" CssClass="btn-warning btn-block btn" OnClick="AddToBillBtn_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="SearchTb" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" placeholder="输入景点名称"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="SearchBtn" style="background-color: transparent; border: 2px solid white; color: white; font-weight: bold;" runat="server" Text="搜索" OnClick="SearchBtn_Click" />
                        </div>
                    </div>
                </div>
                <div class="row mt-5">
                    <h4 class="text-center" style="color:black; font-weight:bold">商品列表</h4>
                    <div class="col">
                        <asp:GridView ID="ProductList" runat="server" CssClass="table" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanged="ProductList_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="teal" ForeColor="White" />
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

            <div class="col-md-7">
                <h4 class="text-center" style="color: black; font-weight: bold">已选商品</h4>
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                <div class="col">
                    <asp:GridView ID="ShoppingCartList" runat="server" CssClass="table" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="ProductList_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="SlateBlue" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>

                    <div class="row">
                        <div class="row">
                            <div class="col-md-5"></div>
                            <div class="col-md-1">
                                <asp:Label runat="server" ID="RMBLabel" CssClass="text-danger text-center"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:Label runat="server" ID="GrdTotalTb" CssClass="text-danger text-center"></asp:Label><br />
                            </div>
                        </div>
                        <div class="row">
                            <asp:Button Text="提交订单" runat="server" ID="PrintBtn" CssClass="btn-warning btn-block btn" OnClick="PrintBtn_Click" />
                            <div class="row">
                                <asp:Label ID="StatusLabel" runat="server" ForeColor="Green" Font-Bold="true" />
                            </div>

                            <asp:Panel ID="SeatSelectionPanel" runat="server" Visible="false">
                                <asp:Table ID="SeatTable" runat="server"></asp:Table>
                            </asp:Panel>

                            <asp:Label ID="SelectedSeatLabel" runat="server" Text="" CssClass="seat-info"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
