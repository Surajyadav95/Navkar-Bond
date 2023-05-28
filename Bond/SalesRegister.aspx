<%@ Page Title="Bond |Sales Register" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="SalesRegister.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond |Sales Register</title>
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i>Sales Register  
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
                                        <asp:ListItem Value="4">GST Party</asp:ListItem>
                                      
                                        <asp:ListItem Value="6">NOC No</asp:ListItem>
                                        
                                    </asp:DropDownList>

                                </div>

                            </div>
                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCHA" runat="server">
                                <div class="form-group text-label">
                                    CHA Name
                                    <asp:DropDownList ID="ddlcha" runat="server" Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-md-4 col-xs-12" style="display: none;" id="divImpoeter" runat="server">
                                <div class="form-group text-label">
                                    Importer Name
                                    <asp:DropDownList ID="ddlimpor" runat="server" Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCustomer" runat="server">
                                <div class="form-group text-label">
                                    Customer Name
                                    <asp:DropDownList ID="ddlcustomer" runat="server" Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                             <div class="col-md-4 col-xs-12" style="display: none;" id="divGSTParty" runat="server">
                                <div class="form-group text-label">
                                    GST Party Name
                                    <asp:DropDownList ID="ddlGSTParty" runat="server" Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3 col-xs-12" style="display: none;" id="divBond" runat="server">
                                <div class="form-group text-label">
                                    Invoice Type
                                    <asp:DropDownList ID="ddlBondType" runat="server" class="form-control text-label">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                         <asp:ListItem Value="N">NOC</asp:ListItem>
                                         <asp:ListItem Value="E">Bond-Ex</asp:ListItem>
                                         <asp:ListItem Value="O">Other</asp:ListItem>

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
                                    <b>Cargo Descriptions</b>
                                    <asp:TextBox ID="txtCargo" Style="text-transform: uppercase" class="form-control text-label " placeholder="Search Text"
                                        runat="server"></asp:TextBox>
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
                                <div class="form-group pull-left" style="padding-top: 20px">
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
                                                    AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="left" />
                                                    <Columns>                                                         
                                                        <%--<asp:BoundField DataField="Sr.No" HeaderText="Sr.No"></asp:BoundField>
                                                        <asp:BoundField DataField="NOC No" HeaderText="Noc No"></asp:BoundField>
                                                        <asp:BoundField DataField="Invoice Type" HeaderText="Invoice Type"></asp:BoundField>
                                                        <asp:BoundField DataField="Invoice No" HeaderText="Invoice No"></asp:BoundField>
                                                        <asp:BoundField DataField="Invoice Date" HeaderText="Invoice Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Receipt No" HeaderText="Receipt No"></asp:BoundField>
                                                        <asp:BoundField DataField="Receipt Date" HeaderText="Receipt Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name"></asp:BoundField>
                                                        <asp:BoundField DataField="Importer Name" HeaderText="Importer Name"></asp:BoundField>
                                                        <asp:BoundField DataField="CHA Name" HeaderText="CHA Name"></asp:BoundField>
                                                        <asp:BoundField DataField="GST No" HeaderText="GST No"></asp:BoundField>
                                                        <asp:BoundField DataField="GST Party Name" HeaderText="GST Party Name"></asp:BoundField>                                                        
                                                        <asp:BoundField DataField="Net Amount" HeaderText="Net Amount"></asp:BoundField>                                                       
                                                        <asp:BoundField DataField="Credit Amt" HeaderText="Credit Amt"></asp:BoundField>
                                                        <asp:BoundField DataField="Total Amt" HeaderText="Total Amt"></asp:BoundField>
                                                        <asp:BoundField DataField="SGST" HeaderText="SGST"></asp:BoundField>
                                                        <asp:BoundField DataField="CGST" HeaderText="CGST"></asp:BoundField>
                                                        <asp:BoundField DataField="IGST" HeaderText="IGST"></asp:BoundField>
                                                        <asp:BoundField DataField="Grand Total" HeaderText="Grand Total"></asp:BoundField> --%>                                                       
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
