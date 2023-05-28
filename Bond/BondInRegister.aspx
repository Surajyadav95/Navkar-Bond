<%@ Page Title="Bond |Bond In Register" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="BondInRegister.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond |Bond In Register</title>
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i>Bond In Register
            </h3>

        </div>
        <div id="page-content">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="page-container">
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

                <div class="panel">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-5  col-xs-12" style="width: 400px;">
                                <div class="form-group date text-label">
                                    Date
                                           
                                    <div class="input-group input-append date input-daterange" id="datePicker">
                                        <asp:TextBox ID="txtfromDate" Style="width: 150px;" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                        <div class="input-group-addon text-label" style="width: 40px;">To</div>
                                        <asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                    </div>

                                </div>


                            </div>



                            <div class="col-md-2 col-xs-12">
                                <div class="form-group text-label">
                                    Category
                                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" class="form-control text-label">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">CHA</asp:ListItem>
                                        <asp:ListItem Value="2">Importer</asp:ListItem>
                                        <asp:ListItem Value="3">Customer</asp:ListItem>
                                        <asp:ListItem Value="4">Cargo Type</asp:ListItem>
                                        <asp:ListItem Value="5">NOC No</asp:ListItem>
                                        <asp:ListItem Value="6">Cargo Description</asp:ListItem>
                                        <asp:ListItem Value="7">Warehouse</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>
                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCHA" runat="server">
                                <div class="form-group text-label">
                                    CHA Name
                                    <asp:DropDownList ID="ddlcha" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-md-4 col-xs-12" style="display: none;" id="divImpoeter" runat="server">
                                <div class="form-group text-label">
                                    Importer Name
                                    <asp:DropDownList ID="ddlimpor" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCustomer" runat="server">
                                <div class="form-group text-label">
                                    Customer Name
                                    <asp:DropDownList ID="ddlcustomer" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3 col-xs-12" style="display: none;" id="divBond" runat="server">
                                <div class="form-group text-label">
                                    Bond Type
                                    <asp:DropDownList ID="ddlBondType" runat="server" class="form-control text-label">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">HAZ</asp:ListItem>
                                        <asp:ListItem Value="2">gen</asp:ListItem>
                                        <asp:ListItem Value="3">ODC</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-md-2 col-xs-12" style="display: none;" id="divNoc" runat="server">
                                <div class="form-group text-label">
                                    <b>Search Text</b>
                                    <asp:TextBox ID="txtNoc" Style="text-transform: uppercase" class="form-control text-label " placeholder="Search Text"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>


                             <div class="col-md-2 col-xs-12" style="display: none;" id="divcargo" runat="server">
                                <div class="form-group text-label">
                                    <b>Cargo Description</b>
                                    <asp:TextBox ID="txtCargo" Style="text-transform: uppercase" class="form-control text-label " placeholder="Search Text"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2 col-xs-12" style="display: none;" id="divWarehouse" runat="server">
                                <div class="form-group text-label">
                                    Warehouses
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <asp:Label ID="lblPDCode" Visible="false" runat="server" Text=""></asp:Label>

                            <div class="col-md-1 col-xs- 12">
                                <div class="form-group pull-right" style="padding-top: 20px">
                                    <asp:Button ID="btnsearch" runat="server"
                                        class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click"></asp:Button>
                                </div>
                            </div>


                              <div class="col-md-2 col-xs- 12">
                                <div class="form-group pull-Left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export TO Excel" ></asp:Button>
                                </div>
                            </div>

                        </div>


                        <div class="row">
                            

                                    <div class="row">
                                        <div class="col-lg-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />

                                                    <Columns>
                                                         
                                                        <asp:BoundField DataField="NOC No" HeaderText="NOC No"></asp:BoundField>
                                                        <asp:BoundField DataField="NOC Date" HeaderText="NOC Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond In Date" HeaderText="Bond IN Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond Gate In Date" HeaderText="Bond Gate In Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond No" HeaderText="Bond No"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond Date" HeaderText="Bond Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond Expiry Date" HeaderText="Bond Expiry Date"></asp:BoundField>
                                                        <asp:BoundField DataField="BE No" HeaderText="BE No"></asp:BoundField>
                                                        <asp:BoundField DataField="BE Date" HeaderText="BE Date"></asp:BoundField>
                                                        <asp:BoundField DataField="IGM No" HeaderText="IGM No"></asp:BoundField>
                                                        <asp:BoundField DataField="Item No" HeaderText="Item No"></asp:BoundField>
                                                        <asp:BoundField DataField="Warehouse" HeaderText="Warehouse"></asp:BoundField>
                                                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name"></asp:BoundField>
                                                        <asp:BoundField DataField="CHA Name" HeaderText="CHA Name"></asp:BoundField>
                                                        <asp:BoundField DataField="Importer Name" HeaderText="Importer Name"></asp:BoundField>
                                                        <asp:BoundField DataField="Description" HeaderText="Description"></asp:BoundField>
                                                        <asp:BoundField DataField="Qty" HeaderText="Qty"></asp:BoundField>
                                                        <asp:BoundField DataField="Unit" HeaderText="UNIT"></asp:BoundField>
                                                        <asp:BoundField DataField="Weight(KGS)" HeaderText="Weight(KGS)"></asp:BoundField>
                                                        <asp:BoundField DataField="Area" HeaderText="Area"></asp:BoundField>
                                                        <asp:BoundField DataField="Value Of Goods" HeaderText="Value Of Goods"></asp:BoundField>
                                                        <asp:BoundField DataField="Value Of Duty" HeaderText="Value Of Duty"></asp:BoundField>
                                                        <asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
                                                        <asp:BoundField DataField="Serial No" HeaderText="Serial No"></asp:BoundField>
                                                        <asp:BoundField DataField="Register No" HeaderText="Register No"></asp:BoundField>
                                                        <asp:BoundField DataField="Container" HeaderText="Containers"></asp:BoundField>
                                                        <asp:BoundField DataField="20" HeaderText="20"></asp:BoundField>
                                                        <asp:BoundField DataField="40" HeaderText="40"></asp:BoundField>
                                                        <asp:BoundField DataField="45" HeaderText="45"></asp:BoundField>
                                                        <asp:BoundField DataField="Used Equipments" HeaderText="Used Equipments"></asp:BoundField>
                                                        <asp:BoundField DataField="Surveyor Name" HeaderText="Executive"></asp:BoundField>
                                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="Prepared By" HeaderText="Prepared By"></asp:BoundField>
                                                         
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                            
                        </div>


                    </div>



                </div>
            </div>




        </div>

    </div>
 


    <script>
        function pop(url, w, h, T, L) {
            n = window.open(url, null, 'top=' + T + ',left=' + L + ',toolbar=0,location=0,directories=0,status=1,menubar=0,titlebar=0,scrollbars=1,resizable=0,width=' + w + ',height=' + h);
            if (n == null) {
                return true;
            }
            return false;
        }
    </script>
    </div>
</asp:Content>
