<%@ Page Title="Bond | Time Expired Bond Details" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="TimeExpiredBondDetails.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond | Time Expired Bond Details</title>
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i> Time Expired Bond Details
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
                            <div class="col-md-2 col-xs-12">
                                <div class="form-group date text-label">
                                    Date
                                           
                                    <div class="input-group input-append date input-daterange" id="datePicker">
                                        <asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                        <%--<div class="input-group-addon text-label" style="width: 40px;">To</div>--%>
                                        <%--<asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>--%>
                                    </div>

                                </div>


                            </div>



                            <div class="col-md-2 col-xs-12">
                                <div class="form-group text-label" style="padding-top:20px">
                                    
                                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control text-label">
                                        <asp:ListItem Value="0-3 Months">0-3</asp:ListItem>
                                        <asp:ListItem Value="3-6 Months">3-6</asp:ListItem>
                                        <asp:ListItem Value="6-9 Months">6-9</asp:ListItem>
                                        <asp:ListItem Value="9-12 Months">9-12</asp:ListItem>
                                        <asp:ListItem Value="Over 1 Year">Over 1 Year</asp:ListItem>
                                        </asp:DropDownList>

                                </div>

                            </div>
                            
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
                            <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-lg-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SR No." HeaderText="Sr.No"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond Type" HeaderText="Bond Type"></asp:BoundField>
                                                        <asp:BoundField DataField="NOC No" HeaderText="NOC NO"></asp:BoundField>
                                                        <asp:BoundField DataField="NOC Date" HeaderText="NOC Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond No" HeaderText="Bond No"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond Date" HeaderText="Bond Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond Expiry Date" HeaderText="Bond Expiry Date"></asp:BoundField>
                                                        <asp:BoundField DataField="BOE NO" HeaderText="BOE NO"></asp:BoundField>
                                                        <asp:BoundField DataField="BOE Date" HeaderText="BOE Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name"></asp:BoundField>
                                                        <asp:BoundField DataField="Importer Name" HeaderText="Importer Name"></asp:BoundField>
                                                        <asp:BoundField DataField="CHA Name" HeaderText="CHA Name"></asp:BoundField>
                                                        <asp:BoundField DataField="Commodity" HeaderText="Commodity"></asp:BoundField>
                                                        <asp:BoundField DataField="Cargo Type" HeaderText="Cargo Type"></asp:BoundField>
                                                        <asp:BoundField DataField="Dwell Weeks" HeaderText="Dwell Weeks"></asp:BoundField>
                                                        <asp:BoundField DataField="Balance Area" HeaderText="Balance Area"></asp:BoundField>
                                                        <asp:BoundField DataField="Balance Weight(KGS)" HeaderText="Balance Weight(KGS)"></asp:BoundField>
                                                        <asp:BoundField DataField="Unit" HeaderText="Unit"></asp:BoundField>
                                                        <asp:BoundField DataField="Balance Value" HeaderText="Balance Value"></asp:BoundField>
                                                        <asp:BoundField DataField="Balance Duty" HeaderText="Balance Duty"></asp:BoundField>
                                                        <asp:BoundField DataField="Total value & Duty" HeaderText="Total value & Duty"></asp:BoundField>
                                                   </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
