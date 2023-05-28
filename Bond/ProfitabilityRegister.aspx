<%@ Page Title="Bond | Profitabilty Register" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ProfitabilityRegister.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Profitabilty Register</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Profitabilty Register
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
Profitabilty Register 
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-md-5  col-xs-12" style="width: 400px;">
                                <div class="form-group date text-label">
                                    Date
                                           
                                    <div class="input-group input-append date input-daterange" id="datePicker">
                                        <asp:TextBox ID="txtfromDate" Style="width: 165px;" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                        <div class="input-group-addon text-label" style="width: 40px;">To</div>
                                        <asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
    <%--<asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
        <ContentTemplate>
            </ContentTemplate>
    </asp:UpdatePanel>--%>
        
    <div class="col-md-2 col-xs-12">
                                <div class="form-group text-label">
                                    Search On 
                                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" class="form-control text-label">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">NOCNo</asp:ListItem> 
                                        <asp:ListItem Value="2">Customer Name</asp:ListItem>                                                                   
                                    </asp:DropDownList>

                                </div>

                            </div>
    <div class="col-md-2 col-xs-12" style="display: none;" id="divNOC" runat="server">
                                <div class="form-group text-label">
                                    NOC No
                                    <asp:TextBox runat="server" ID="txtNOCNo" CssClass="form-control" onkeypress="return ValidateNumber()" placeholder="NOC No"></asp:TextBox>

                                </div>
                            </div>


                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCustomer" runat="server">
                                <div class="form-group text-label">
                                    Customer Name
                                    <asp:DropDownList ID="ddlcustomer" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>
            

     <div class="col-md-1 col-xs-12">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="Button1" class="btn btn-primary" runat="server" 
Text="Export To Excel"  OnClick="Button1_Click" />
</div>                                     
</div>
</div>


 
                               
</asp:Panel>
                        
</div>
</div>


<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
<a href="CustomerDAR.aspx" class="btn btn-info btn-block">OK</a>                                
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
                 
    var ddlCategory = document.getElementById('<%= ddlCategory.ClientID%>').value;
    var ddlcustomer = document.getElementById('<%= ddlcustomer.ClientID%>').value;
   
    
var blResult = Boolean;
blResult = true;

//alert('hi')
if (blResult == false) {
    alert('Please fill the required fields!');
}
return blResult;
}
</script>

<script type="text/javascript">
function ValidateQty() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
return event.returnValue;
return event.returnValue = '';
}
function ValidateNumber() {
    //alert('hii')
    if ((event.keyCode > 47 && event.keyCode < 58))
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
