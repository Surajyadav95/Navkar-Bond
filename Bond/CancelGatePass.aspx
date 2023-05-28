<%@ Page Title="Bond | Cancel Gate Pass" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="CancelGatePass.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
<div class="container" style="background-color: white">

<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Cancel Gate Pass<small class="pull-left" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>
<div class="row ">
 
<div class="col-sm-5 col-xs-5 ">
<div class="form-group text-label">
<b  >Gate Pass No</b>
<asp:TextBox ID="txtgatepassno" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Gate Pass No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-2 col-xs-2 pull-left">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnsearch_Click"  
    OnClientClick="return ValidationSave()"
Text="Show"  />
</div>
                                              
                                      
</div>
</div>
 


    <div class="row">
<div class="col-sm-8 col-xs-8">
<div class="form-group text-label">
<b  >Gate Pass Date</b>
<asp:TextBox ID="txtbondate" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Gate Pass Date"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>


    <div class="row">
<div class="col-sm-8 col-xs-8">
<div class="form-group text-label">
<b  >CHA Name</b>
<asp:TextBox ID="txtcha" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="CHA Name"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
 
     <div class="row">
<div class="col-sm-8 col-xs-8">
<div class="form-group text-label">
<b  >Importer Name</b>
<asp:TextBox ID="txtimporter" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Importer Name"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

        <div class="row">
<div class="col-sm-8 col-xs-8">
<div class="form-group text-label">
<b  >Cancel Remark</b>
<asp:TextBox ID="txtcancelremarks" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Cancel Remark"
runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
</div>
</div>

</div>

            <div class="row">
    <div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btncancel" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btncancel_Click"  
Text="Cancel"  OnClientClick="return ValidationSave()"  />
</div>
                                              
                                      
</div>
                <div class="col-sm-1" style="padding-left:26px;">
<div class="form-group" style="padding-top:18px">                          
<a href="CancelGatePass.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a>                               
</div>                                                                               
</div>
                </div>

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
         
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
                   
<a href="CancelGatePass.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
                
</div>
</div>
     
    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
 
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btncancelyes" data-dismiss="modal" runat="server"  onserverclick="btncancelyes_ServerClick"  aria-hidden="true">
Yes 
</button>
<a href="CancelGatePass.aspx" class="btn btn-danger ">No</a>
</div>
</div>
 
</div>
</div>             

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtgatepassno = document.getElementById('<%= txtgatepassno.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

if (txtgatepassno == "") {
document.getElementById('<%= txtgatepassno.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
 
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
</asp:Content>


