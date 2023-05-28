﻿<%@ Page Title="Bond | Tariff Master" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="TariffMaster.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Tariff Master</title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>  Tariff Master 
</h3>           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="col-md-3 pull-md-left sidebar" style="padding-top:12px;">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title" style="padding-bottom: 0px !important; ">
<i class="fa fa-cube"></i>&nbsp; Tariff Master 
<%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>   
<div class="panel-body">
<asp:Repeater ID="rptnoLIst" runat="server">
<ItemTemplate>
<div  >
                             
                            
<a  href='<%# "TariffMaster.aspx?TariffIDView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "TariffID")).ToString())%>' target="_blank" ><strong><asp:Label runat="server"  Text='<%#Eval("TariffID")%>' ID="lblNONumber" style="text-transform:uppercase;"></asp:Label></strong></a>                                                     
<br /> <asp:Label runat="server" Text='<%#Eval("AddDate")%>' Id="lblDate" style="text-transform:uppercase;"></asp:Label>
<br/>Generated By: &nbsp;<asp:Label runat="server" Text='<%#Eval("Users")%>' Id="lbluser" style="text-transform:uppercase;"></asp:Label>
<br />
<asp:Label runat="server" >----------------------------------</asp:Label>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
</div>
                           
</div>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-9 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Tariff Master 
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Customer Name</b>
<asp:DropDownList ID="ddlCustomer"   Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"     runat="server"    class="form-control text-label">
                                            
</asp:DropDownList> 
</div>
</div>
<div class="col-sm-3 col-xs-12" style="display:none;"  id="divjono" runat="server">
<div class="form-group text-label" style="padding-top:22px; margin-left:14px;">
<b>CustomerID :</b>
<asp:Label ID="lblID"  runat="server" Text=""></asp:Label>

</div>
</div>
</div>
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Tariff ID</b>
<asp:TextBox ID="txttariffId" ReadOnly="true" Style="text-transform:uppercase" AutoPostBack="true" OnTextChanged="txttariffId_TextChanged" class="form-control text-label"  placeholder="Tariff ID"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Description</b>
<asp:TextBox ID="txtDescription" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Description"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
From
                                           
<asp:TextBox ID="txtfrom"  placeholder="yyyy-mm-dd " TextMode="Date"  runat="server"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
 

<div class="col-md-3  col-xs-12" ">                                      
<div class="form-group text-label">
<b >To</b>
<asp:TextBox ID="txtTo"  placeholder="yyyy-mm-dd" TextMode="Date"  runat="server"   Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
 
</div>
    <div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Storage Weeks</b>
<asp:TextBox ID="txtStorageWeeks" Style="text-transform:uppercase" onkeypress="return ValidateQty()" class="form-control text-label"  placeholder="Enter Storage Weeks"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Insurance Weeks</b>
<asp:TextBox ID="txtInsuranceWeeks" Style="text-transform:uppercase" onkeypress="return ValidateQty()" class="form-control text-label"  placeholder="Enter Insurance Weeks"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Discount %</b>
<asp:TextBox ID="txtDiscount"  Style="text-transform:uppercase" class="form-control text-label"  placeholder="Discount %"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label" style="padding-top:18px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline"> Is Active ?</asp:Label>
</div>
</div>
</div>
                             
</asp:Panel>
                         
</div>
</div>


<div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="TariffMaster.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
<div class="col-sm-5 pull-right" style="padding-top:15px;">
<div class="form-group">
<a href="TariffSummary.aspx" target="_blank"><b style="color:blue">Click here to view Tariff Summary</b> </a>
</div>
</div>
                         
</div>
<asp:Label ID="lblcode" Visible="false" runat="server" Text=""></asp:Label>
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
                   
<a href="TariffMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
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
            
var ddlCustomer = document.getElementById('<%= ddlCustomer.ClientID%>').value;
var txttariffId = document.getElementById('<%= txttariffId.ClientID%>').value;
    var txtDescription = document.getElementById('<%= txtDescription.ClientID%>').value;
    var txtStorageWeeks = document.getElementById('<%= txtStorageWeeks.ClientID%>').value;

    var txtInsuranceWeeks = document.getElementById('<%= txtInsuranceWeeks.ClientID%>').value;

                  


var blResult = Boolean;
blResult = true;
              
if (txttariffId == "") {
document.getElementById('<%= txttariffId.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

if (txtDescription == "") {
document.getElementById('<%= txtDescription.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if ((txtStorageWeeks == "") || (txtStorageWeeks == 0)) {
        document.getElementById('<%= txtStorageWeeks.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if ((txtInsuranceWeeks == "") || (txtInsuranceWeeks == 0)) {
        document.getElementById('<%= txtInsuranceWeeks.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
                
              
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});



</script>  

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy2').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});

</script> 
    
  
          

<script type="text/javascript">
function ValidateQty() {
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
