﻿<%@ Page Title="Bond | Package Master" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="PackageMaster.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

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
<h1>Package Master  <small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>
               
                    
<div class="row">

<div class="col-sm-6 col-xs-8">
<div class="form-group text-label">
<b  >Package ID</b>
<asp:TextBox ID="txtpackageId" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder="NEW"
runat="server"   ></asp:TextBox>

</div>
</div>

<div class="col-sm-12 col-xs-12">
<div class="form-group text-label">
<b  >Package Name</b>
<asp:TextBox ID="txtpackagename" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Package Name"
runat="server"   ></asp:TextBox>
</div>
</div>

             
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"   
Text="Save"  OnClientClick="return ValidationSave()"  />
</div>
                                              
                                      
</div>

<div class="col-sm-1 col-xs-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:18px">
<a href="PackageMaster.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
</div>
                                              
                                      
</div>
</div>

             

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">
                   

<div class="row">
<div class="col-md-6 col-xs-8 text-label " style="padding-right: 60px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="4">
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
                                                 
<asp:BoundField DataField="CodeID"   HeaderText="Package ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Package"   HeaderText="Package Name"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
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
                   
<a href="PackageMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
             
</div>
</div>
                 

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
var txtpackagename = document.getElementById('<%= txtpackagename.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

if (txtpackagename == "") {
document.getElementById('<%= txtpackagename.ClientID%>').style.borderColor = "red";
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


