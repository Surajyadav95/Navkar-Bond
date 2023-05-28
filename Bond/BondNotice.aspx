<%@ Page Title="Bond | Notice" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="BondNotice.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond | Notice</title>
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i>Bond Notice
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
                            <div class="col-sm-3 col-xs-12">
                            <div class="form-group text-label">
                            <b  >Uncleared Bond As On Date</b>
                            <asp:TextBox ID="txtAsOnDate" onkeypress="return ValidateNumber()" Style="text-transform:uppercase" class="form-control text-label" TextMode="Date"
                            runat="server" ></asp:TextBox>
                            </div>
                            </div>

                            <div class="col-sm-1 col-xs-12" >
                            <div class="form-group text-label" style="text-decoration-color:black">
                            <b >For Month</b>
                            <asp:TextBox ID="txtForMonth" onkeypress="return ValidateNumber()" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
                            runat="server" Text="" placeholder="For Month"></asp:TextBox>     
                            </div>
                            </div>

                            <div class="col-sm-1 col-xs- 12">
                                <div class="form-group pull-right" style="padding-top: 20px">
                                    <asp:Button ID="btnsearch" runat="server"
                                        class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click"></asp:Button>
                                </div>
                            </div>


                              <div class="col-sm-2 col-xs- 12">
                                <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export TO Excel" ></asp:Button>
                                </div>
                            </div>
                           
                            <asp:UpdatePanel runat="server" ID="updatepanelnew">
                                <ContentTemplate>
                            <div class="col-sm-2 col-xs- 12">
                                <div class="form-group pull-left" style="padding-top: 22px">
                               <asp:LinkButton runat="server" ID="lnkaqua" class="btn" Width="180px" style="cursor:default" Backcolor="aqua">First Notice Sent</asp:LinkButton>
                                    </div>
                            </div>
                            <div class="col-sm-2 col-xs- 12">
                                <div class="form-group pull-left" style="padding-top: 20px">
                                <asp:LinkButton runat="server" ID="lnkpink" class="btn" Width="180px" style="cursor:default" Backcolor="pink">Final Notice Sent</asp:LinkButton>
                                    </div>
                            </div>                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <div class="row">
                           

                                    <div class="row">
                                        <div class="col-sm-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-bordered" OnRowDataBound="grdcontainer_RowDataBound"
                                                    AutoGenerateColumns="False" EmptyDataText="No records found!" HeaderStyle-BackColor="#f9f9f9" DataKeyNames="BOE NO" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />
                                                    <Columns>
                                                         <asp:TemplateField>
                                                             <ItemTemplate>
                                                            <asp:LinkButton ID="lnkNotice1" ControlStyle-CssClass='btn btn-success btn-xs outline' Text="Generate Notice 1" OnClick="lnkNotice1_Click"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Bond No")%>' OnClientClick="return confirm('Are you sure to generate notice 1?')" runat="server"></asp:LinkButton>                                                         
                                                            <br />
                                                             <asp:LinkButton ID="lnkNotice2" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Generate Notice 2" OnClick="lnkNotice2_Click" 
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Bond No")%>' OnClientClick="return confirm('Are you sure to generate notice 2?')" runat="server"></asp:LinkButton>                                                         
                                                            </ItemTemplate>                                                             
                                                         </asp:TemplateField>
                                                         <asp:BoundField DataField="Sr.No" HeaderText="Sr.No"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="Bond No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBondNo" Text='<%# Eval("Bond No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bond IN Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBondInDate" Text='<%# Eval("Bond IN Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bond Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBondDate" Text='<%# Eval("Bond Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bond Expiry Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBondExpiryDate" Text='<%# Eval("Bond Expiry Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                        <asp:TemplateField HeaderText="BOE NO">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBOENo" Text='<%# Eval("BOE NO")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="BOE Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBOEDate" Text='<%# Eval("BOE Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGM No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblIGMNo" Text='<%# Eval("IGM No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                        
                                                        <asp:TemplateField HeaderText="CHA Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCHAName" Text='<%# Eval("CHA Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Importer Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblImporterName" Text='<%# Eval("Importer Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Importer Address">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblImporterAddress" Text='<%# Eval("Importer Address")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBalanceQty" Text='<%# Eval("Balance Qty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                        <asp:TemplateField HeaderText="Ex Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblExQty" Text='<%# Eval("Ex Qty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Manifest Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblManifestQty" Text='<%# Eval("Manifest Qty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance Value">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBalanceValue" Text='<%# Eval("Balance Value")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Balance Duty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBalanceDuty" Text='<%# Eval("Balance Duty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Value Duty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTotalValueDuty" Text='<%# Eval("Total Value Duty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                                                                                                                                                                                               
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                            
                        </div>


                    </div>



                </div>
            </div>
            <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnYesNotice1" />
    </Triggers>
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblsession"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnYesNotice1" data-dismiss="modal" runat="server" onserverclick="btnYesNotice1_ServerClick"  aria-hidden="true">
Yes 
</button>
<button id="Button2" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div> 

<div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnYesNotice1" />
    </Triggers>
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblsession2"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnyesnotice2" data-dismiss="modal" runat="server" onserverclick="btnyesnotice2_ServerClick"  aria-hidden="true">
Yes 
</button>
<button id="Button3" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div> 


        </div>

    </div>
 <script type="text/javascript">
     function ValidateYear() {
         //alert('hii')
         if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 45)
             return event.returnValue;
         return event.returnValue = '';
     }
     function ValidateNumber() {
         //alert('hii')
         if ((event.keyCode > 47 && event.keyCode < 58))
             return event.returnValue;
         return event.returnValue = '';
     }
     function ValidateAmount() {
         //alert('hii')
         if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
             return event.returnValue;
         return event.returnValue = '';
     }
     function checkEmail(str) {
         var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

         if (reg.test(emailField.value) == false) {
             alert('Invalid Email Address');
             return false;
         }

         return true;
     }

     function CheckTelephone(tel) {

         if (tel.length < 7)
             alert("Invalid Telephone number.")
     }

     function CheckMobile(mob) {
         if (mob.length < 10)
             alert("Invalid Mobile number.");

     }
</script>
    <script type="text/javascript">
        var popup;
        function OpenNotice1Print() {
            
            var url = "../Report_Bond/BondNotice1Print.aspx"
            window.open(url);


}

</script>
    <script type="text/javascript">
        var popup;
        function OpenNotice2Print() {

            var url = "../Report_Bond/BondNotice2Print.aspx"
            window.open(url);


        }

</script>
    
</asp:Content>
