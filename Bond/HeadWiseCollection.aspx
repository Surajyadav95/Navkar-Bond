<%@ Page Title="Bond | Head Wise Collection Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="HeadWiseCollection.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond | Head Wise Collection Summary</title>
    </head>
    <style>
        .center-head{
            text-align:center
        }
    </style>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i>Head Wise Collection Summary 
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
                            <div class="col-md-4 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate"  style="width: 150px;" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>

</div>

                                          
</div>

                            <div class="col-md-1 col-xs- 12">
                                <div class="form-group pull-right" style="padding-top: 20px">
                                    <asp:Button ID="btnsearch" runat="server"
                                        class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click"></asp:Button>
                                </div>
                            </div>


                              <div class="col-md-2 col-xs- 12" style="display:none">
                                <div class="form-group pull-Left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>

                        </div>


                            

                                    <div class="row">
                                        <div class="col-lg-8 col-xs-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="left" />

                                                    <Columns>
                                                        
                                                          <asp:BoundField DataField="Sr No" HeaderStyle-CssClass="center-head" ItemStyle-HorizontalAlign="Center" HeaderText="Sr No"></asp:BoundField>
                                                          <asp:BoundField DataField="Account Name" HeaderStyle-CssClass="center-head" ItemStyle-HorizontalAlign="Left" HeaderText="Account Name"></asp:BoundField>
                                                        <asp:BoundField DataField="Net Amount" HeaderStyle-CssClass="center-head" ItemStyle-HorizontalAlign="Right" HeaderText="Net Amount"></asp:BoundField>
                                                                                                                 
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
