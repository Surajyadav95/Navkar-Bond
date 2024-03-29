﻿<%@ Page Title="Bond |  CHA Master" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ChaMaster.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond |  CHA Master</title>

</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>CHA Master 
</h3>

</div>

<div id="page-content">



<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="col-md-3 pull-md-left sidebar" style="padding-top: 12px;">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title" style="padding-bottom: 0px !important;">
<i class="fa fa-cube"></i>&nbsp;   CHA Master
<%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
<div class="panel-body">
<asp:Repeater ID="rptnoLIst" runat="server">
<ItemTemplate>
<div>
<a href='<%# "CHAmaster.aspx?CHANoView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "ChaNo")).ToString())%>' target="_blank"><strong>
<asp:Label runat="server" Text='<%#Eval("ChaName")%>' ID="lblNONumber" Style="text-transform: uppercase;"></asp:Label></strong></a>
<br />
<asp:Label runat="server" Text='<%#Eval("AddDate")%>' ID="lblDate" Style="text-transform: uppercase;"></asp:Label>
<br />
Generated By: &nbsp;<asp:Label runat="server" Text='<%#Eval("Users")%>' ID="lbluser" Style="text-transform: uppercase;"></asp:Label>

<br />
<asp:Label runat="server">----------------------------------</asp:Label>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
</div>

</div>
<div class="panel-body">

<div class="row">

<div class="col-md-9 pull-md-right main-content">
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">CHA Details
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>

<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true">

<div class="row">

<div class="col-sm-2 col-xs-12">
<div class="form-group text-label" style="text-decoration-color: black">
<b>CHA No</b>
<asp:TextBox ID="txtchano" Style="text-transform: uppercase; background-color: #e9e9e9" ReadOnly class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label" style="text-decoration-color: black">
<b>CHA Code</b>
<asp:TextBox ID="txtchac" Style="text-transform: uppercase" class="form-control text-label" placeholder="CHA Code"
runat="server"></asp:TextBox>
</div>
</div>



</div>


<div class="row">
<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b>CHA Name</b>
<asp:TextBox ID="txtchaname" Style="text-transform: uppercase" class="form-control text-label" placeholder="CHA Name"
runat="server"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b>Address</b>
<asp:TextBox ID="txtaddress" Style="text-transform: uppercase" class="form-control text-label" placeholder="Address"
TextMode="MultiLine" runat="server"></asp:TextBox>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>City</b>
<asp:TextBox ID="txtcity" Style="text-transform: uppercase" class="form-control text-label" placeholder="City"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>State</b>
<asp:DropDownList ID="ddlstste" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
</div>


</asp:Panel>




</div>
</div>

<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">CHA Contact Details
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>

<div class="panel-body">
<asp:Panel ID="Panel1" runat="server" Enabled="true">
<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Contact Person</b>
<asp:TextBox ID="txtcontactper" Style="text-transform: uppercase" class="form-control text-label" placeholder="Contact Person"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Person Designation</b>
<asp:TextBox ID="txtperdestinston" Style="text-transform: uppercase" class="form-control text-label" placeholder="Person Designation"
runat="server"></asp:TextBox>
</div>
</div>
<asp:Label ID="lblCHano" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblChaname" Visible="false" runat="server" Text=""></asp:Label>
</div>

<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Contact Number 1</b>
<asp:TextBox ID="txtnumber1" Style="text-transform: uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()" placeholder="Contact Number 1"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Contact Number 2</b>
<asp:TextBox ID="txtnumber2" Style="text-transform: uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()" placeholder="Contact Number 2"
runat="server"></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Fax Number</b>
<asp:TextBox ID="txtfax" Style="text-transform: uppercase" class="form-control text-label" placeholder="Fax Number"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Mobile Number</b>
<asp:TextBox ID="txtmobile" Style="text-transform: uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()" placeholder="Mobile Number"
runat="server"></asp:TextBox>
</div>
</div>

</div>
<div class="row">

<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b>Email ID</b>
<asp:TextBox ID="txtemail" Style="text-transform: uppercase" class="form-control text-label" placeholder="Email ID"
runat="server"></asp:TextBox>
</div>
</div>
</div>


</asp:Panel>

</div>

</div>



<div class="row">
<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b>Remarks</b>
<asp:TextBox ID="txtremarks" Style="text-transform: uppercase" class="form-control text-label" placeholder="Remarks"
TextMode="MultiLine" runat="server"></asp:TextBox>
</div>
</div>

</div>

<div class="row">


<div class="col-md-6 col-xs-12">
<div class="form-group text-label" style="padding-top: 22px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:HiddenField ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline">Is Active?</asp:Label>
</div>
</div>
</div>

<div class="row">
<div class="col-sm-1" style="padding-left: 16px;">
<div class="form-group" style="padding-top: 15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"
Text="Save" OnClientClick="return ValidationSave()" />
</div>


</div>

<div class="col-sm-1" style="padding-left: 20px;">
<div class="form-group" style="padding-top: 15px">
<a href="CHAmaster.aspx" id="btnclear" runat="server" class="btn btn-primary ">Clear
</a>
</div>


</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="ChaSummary.aspx" target="_blank"><b style="color:blue">Click here to view CHA summary</b> </a>
</div>
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

<a href="CHAmaster.aspx" class="btn btn-info btn-block">OK</a>

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

</ContentTemplate>
</asp:UpdatePanel>
</div>


</div>

<script type="text/javascript">
function ValidationSave() {
var txtchac = document.getElementById('<%= txtchac.ClientID%>').value;
var txtchaname = document.getElementById('<%= txtchaname.ClientID%>').value;


var blResult = Boolean;
blResult = true;

if (txtchac == "") {
document.getElementById('<%= txtchac.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}

if (txtchaname == "") {
document.getElementById('<%= txtchaname.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>

<script type="text/javascript">
function ValidatePhoneNo() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 43 || event.keyCode == 32 || event.keyCode == 40 || event.keyCode == 41)
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
