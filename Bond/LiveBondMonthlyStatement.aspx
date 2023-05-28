<%@ Page Title="Bond | Monthly Statement" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="LiveBondMonthlyStatement.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Live Bond Monthly Statement</title>
       
</head>
    <style>
        .header-center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>  Live Bond Monthly Statement
</h3>
           
</div>
       
<div id="page-content">      
                  
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Live Bond Monthly Statement
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
     <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <div class="form-group date text-label">
                                    As On Date
                                           <asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                    <%--<div class="input-group input-append date input-daterange" id="datePicker">
                                        
                                        <div class="input-group-addon text-label" style="width: 40px;">To</div>
                                        <asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                    </div>--%>

                                </div>


                            </div>

<div class="col-sm-1">
<div class="form-group" style="padding-top:18px;">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"  
Text="Show"  OnClientClick="return ValidationSave()" />
</div>                                                                                  
</div>
         <div class="col-sm-1">
<div class="form-group" style="padding-top:18px;">
    <a runat="server" class="btn btn-primary" style="cursor:pointer" onclick="OpenPrint()">Print</a>
</div>                                                                                  
</div>
        </div>   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    
    <div class="row">
        <div class="col-sm-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdMonthlyStatement" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered"
                                                    AutoGenerateColumns="False" HeaderStyle-BackColor="Orange" EmptyDataText="No records found!" OnRowDataBound="grdMonthlyStatement_RowDataBound" ShowHeaderWhenEmpty="true">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />

                                                    <Columns>
                                                         
                                                        <asp:BoundField DataField="OpeningBonds" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="No of Bond"></asp:BoundField>
                                                        <asp:BoundField DataField="OpeningUnits" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="No of Units"></asp:BoundField>
                                                        <asp:BoundField DataField="OpeningValue" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Assessed Value"></asp:BoundField>
                                                        <asp:BoundField DataField="OpeningDuty" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Duty Amount"></asp:BoundField>
                                                        <asp:BoundField DataField="InBonds" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="No of Bond"></asp:BoundField>
                                                        <asp:BoundField DataField="InUnits" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="No of Units"></asp:BoundField>
                                                        <asp:BoundField DataField="InValue" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Assessed Value"></asp:BoundField>
                                                        <asp:BoundField DataField="InDuty" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Duty Amount"></asp:BoundField>
                                                        <asp:BoundField DataField="ExBondPart" HeaderStyle-BackColor="Orange" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Part"></asp:BoundField>
                                                        <asp:BoundField DataField="ExBondFull" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="Full"></asp:BoundField>                                                     
                                                        <asp:BoundField DataField="ExUnits" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="No of Units"></asp:BoundField>
                                                        <asp:BoundField DataField="ExValue" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="Assessed Value"></asp:BoundField>
                                                        <asp:BoundField DataField="ExDuty" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="Duty Amount"></asp:BoundField>
                                                        <asp:BoundField DataField="ClosingBonds" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="No of Bond"></asp:BoundField>
                                                        <asp:BoundField DataField="ClosingUnits" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="No of Units"></asp:BoundField>
                                                        <asp:BoundField DataField="ClosingValue" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="Assessed Value"></asp:BoundField>
                                                        <asp:BoundField DataField="ClosingDuty" HeaderStyle-CssClass="header-center" HeaderStyle-BackColor="Orange" ItemStyle-HorizontalAlign="Center" HeaderText="Duty Amount"></asp:BoundField>                                                                                                                                                                     
                                                         
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>
    </div>  
    </ContentTemplate>
</asp:UpdatePanel>                  
</asp:Panel>                        
</div>
</div>                                                                       
                         
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I3" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">
<asp:Label ID="lblSession" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">                   
<a href="AccountMaster.aspx" class="btn btn-info btn-block">OK</a>                                
</div>
</div>                    
</ContentTemplate>             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 

</div>
       
         
</div>
   <script type="text/javascript">
       function ValidationSave() {
           
           var AsOnDate = document.getElementById('<%=txtfromDate.ClientID%>').value;
           
           var blresult = Boolean;
           blresult = true;
           if (AsOnDate = "") {
               document.getElementById('<%=txtfromDate.ClientID%>').style.borderColor = "red";
               blresult = blresult && false;
           }
           if (blresult == false) {
               alert("Please fill the required fields!");
           }
           return blresult;
       }
   </script>
    <script type="text/javascript">
        function OpenPrint() {
            
            var AsOnDate = document.getElementById('<%=txtfromDate.ClientID%>').value;
            
           var blresult = Boolean;
           blresult = true;
           if (AsOnDate == "") {
               document.getElementById('<%=txtfromDate.ClientID%>').style.borderColor = "red";
               blresult = blresult && false;
           }
           if (blresult == false) {
               alert("Please fill the required fields!");
           }
           else {
               
               var url = "../Report_Bond/LiveBondMonthlyStatementPrint.aspx?AsOnDate=" + AsOnDate
               window.open(url)               
           }
           return blresult;
       }
   </script>
<script type="text/javascript">
function ValidateQty() {
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
</asp:Content>
