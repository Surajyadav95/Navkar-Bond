<%@ Page Title="Bond | Annexure 4 Form A Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="Annexure4Form-ASummary.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond | Annexure 4 Form A Summary</title>
    </head>
    <style>
        .center-header{
            text-align:center
        }
    </style>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i> Annexure 4 Form A Summary
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
                            <div class="col-sm-5 col-xs-12" style="width:450px">
                                <div class="form-group date text-label">
                                    Date
                                           
                                    <div class="input-group" id="datePicker">
                                        <asp:TextBox ID="txtfromDate" Width="200px" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                        <div class="input-group-addon text-label" style="width: 40px;">To</div>
                                        <asp:TextBox ID="txttoDate" Width="200px" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                    </div>

                                </div>


                            </div>

                            
                            <div class="col-sm-1 col-xs-12">
                                <div class="form-group pull-right" style="padding-top: 20px">
                                    <asp:Button ID="btnsearch" runat="server"
                                        class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click"></asp:Button>
                                </div>
                            </div>


                              <div class="col-sm-2 col-xs- 12">
                                <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>

                        </div>


                        <div class="row">
                            <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-sm-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="No records found!" HeaderStyle-CssClass="center-header" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />
                                                    
                                                    <Columns>
                                                        <asp:BoundField DataField="Sr.No" ItemStyle-HorizontalAlign="Center" HeaderText="Sr.No"></asp:BoundField>
                                                        <asp:BoundField DataField="Bill of Entry No" ItemStyle-HorizontalAlign="Center" HeaderText="Bill of Entry No"></asp:BoundField>
                                                        <asp:BoundField DataField="Bill of Entry Date" ItemStyle-HorizontalAlign="Center" HeaderText="Bill of Entry Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Custom Stations of Import" ItemStyle-HorizontalAlign="Center" HeaderText="Custom Stations of Import"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond No" ItemStyle-HorizontalAlign="Center" HeaderText="Bond No"></asp:BoundField>
                                                        <asp:BoundField DataField="Bond Date" ItemStyle-HorizontalAlign="Center" HeaderText="Bond Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Description of Goods" ItemStyle-HorizontalAlign="Left" HeaderText="Description of Goods"></asp:BoundField>
                                                        <asp:BoundField DataField="Description and No of Packages" ItemStyle-HorizontalAlign="Left" HeaderText="Description and No of Packages"></asp:BoundField>
                                                        <asp:BoundField DataField="Marks and Number on Packages" ItemStyle-HorizontalAlign="Center" HeaderText="Marks and Number on Packages"></asp:BoundField>
                                                        <asp:BoundField DataField="Unit" ItemStyle-HorizontalAlign="Center" HeaderText="Unit"></asp:BoundField>
                                                        <asp:BoundField DataField="Weight" ItemStyle-HorizontalAlign="Center" HeaderText="Weight"></asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" ItemStyle-HorizontalAlign="Center" HeaderText="Quantity"></asp:BoundField>
                                                        <asp:BoundField DataField="Value" ItemStyle-HorizontalAlign="Center" HeaderText="Value"></asp:BoundField>
                                                        <asp:BoundField DataField="Duty Assessed" ItemStyle-HorizontalAlign="Center" HeaderText="Duty Assessed"></asp:BoundField>
                                                        <asp:BoundField DataField="Date of Order under Section 60(1)" ItemStyle-HorizontalAlign="Center" HeaderText="Date of Order under Section 60(1)"></asp:BoundField>
                                                        <asp:BoundField DataField="Warehouse code and address (in case of bond to bond transfer)"  HeaderText="Warehouse code and address (in case of bond to bond transfer)"></asp:BoundField>
                                                        <asp:BoundField DataField="Registration No. of means of transport" HeaderText="Registration No. of means of transport"></asp:BoundField>
                                                        <asp:BoundField DataField="OTL No." HeaderText="OTL No."></asp:BoundField>                                                        
                                                        <asp:BoundField DataField="Quantity Adviced" ItemStyle-HorizontalAlign="Center" HeaderText="Quantity Adviced"></asp:BoundField>
                                                        <asp:BoundField DataField="Quantity received" ItemStyle-HorizontalAlign="Center" HeaderText="Quantity received"></asp:BoundField>
                                                        <asp:BoundField DataField="Breakage/damage" ItemStyle-HorizontalAlign="Center" HeaderText="Breakage/damage"></asp:BoundField>
                                                        <asp:BoundField DataField="Shortage" ItemStyle-HorizontalAlign="Center" HeaderText="Shortage"></asp:BoundField>
                                                        <asp:BoundField DataField="Sample drawn by government agencies" HeaderText="Sample drawn by government agencies"></asp:BoundField>
                                                        <asp:BoundField DataField="Activities Undertaken under section 64" HeaderText="Activities Undertaken under section 64"></asp:BoundField>
                                                        <asp:BoundField DataField="Date of expiry of initial Bonding Period" HeaderText="Date of expiry of initial Bonding Period"></asp:BoundField>
                                                        <asp:BoundField DataField="Period extended upto" HeaderText="Period extended upto"></asp:BoundField>
                                                        <asp:BoundField DataField="Details of Bank Guarantee" HeaderText="Details of Bank Guarantee"></asp:BoundField>
                                                        <asp:BoundField DataField="Relinquishment" HeaderText="Relinquishment"></asp:BoundField>
                                                        <asp:BoundField DataField="Date and time of removal" ItemStyle-HorizontalAlign="Center" HeaderText="Date and time of removal"></asp:BoundField>
                                                        <asp:BoundField DataField="Purpose of removal(home consumption/deposit in another warehouse/export/sold under Sec.72(2)/destruction etc).Give details" 
                                                                        HeaderText="Purpose of removal(home consumption/deposit in another warehouse/export/sold under Sec.72(2)/destruction etc).Give details"></asp:BoundField>
                                                        <asp:BoundField DataField="Quantity cleared" ItemStyle-HorizontalAlign="Center" HeaderText="Quantity cleared"></asp:BoundField>
                                                        <asp:BoundField DataField="Value" ItemStyle-HorizontalAlign="Center" HeaderText="Value"></asp:BoundField>
                                                        <asp:BoundField DataField="Duty" ItemStyle-HorizontalAlign="Center" HeaderText="Duty"></asp:BoundField>
                                                        <asp:BoundField DataField="Interest" ItemStyle-HorizontalAlign="Center" HeaderText="Interest"></asp:BoundField>
                                                        <asp:BoundField DataField="Balance Quantity" ItemStyle-HorizontalAlign="Center" HeaderText="Balance Quantity"></asp:BoundField>
                                                        <asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderText="Remarks"></asp:BoundField>

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

</asp:Content>
