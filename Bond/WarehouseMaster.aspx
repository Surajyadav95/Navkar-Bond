﻿<%@ Page Title="Bond | Warehouse Master" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="WarehouseMaster.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

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
<h1>Warehouse Master  <small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>

<div class="row">
<div class="col-sm-10 col-xs-8">
<div class="form-group text-label">
<b  >Warehouse Name</b>
<asp:TextBox ID="txtWarehousename" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Warehouse Name"
runat="server"   ></asp:TextBox>
</div>
</div>

    <div class="col-sm-4 col-xs-8">
<div class="form-group text-label">
<b  >Warehouse Code</b>
<asp:TextBox ID="txtWarehouseCode" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Warehouse Code"
runat="server"   ></asp:TextBox>
</div>
</div>

    
    <div class="col-sm-4 col-xs-8">
<div class="form-group text-label">
<b  >Series Code</b>
<asp:TextBox ID="txtseriesCode" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Series Code"
runat="server"   ></asp:TextBox>
</div>
</div>
             
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"   
Text="Save"  OnClientClick="return ValidationSave()"  />
</div>
                                              
                                      
</div>
</div>

<div class="row">
<div class="col-sm-4 col-xs-4 pull-right">
<div class="form-group text-label" style="padding-top:10px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline"> Is Active?</asp:Label>
</div>
</div>
</div>

<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblwhCode" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblwhSeries" Visible="false" runat="server" Text=""></asp:Label>
    <div class="row ">
<div class="col-sm-5 col-xs-5 ">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-2 col-xs-2">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary " runat="server" OnClick="btnsearch_Click"  
Text="Search"  />
</div>
                                              
                                      
</div>
</div>
<div class="row">
                    

<div class="row">
<div class="col-lg-6 text-label " style="padding-right: 50px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="4">
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>

<asp:TemplateField>
<ItemTemplate>
                                                          
<a  href='<%# "WarehouseMaster.aspx?WHIDView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "WHID")).ToString())%>' 
Class='btn btn-success btn-xs outline' 
>View</a>

<a  href='<%# "WarehouseMaster.aspx?WHIDEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "WHID")).ToString())%>' 
Class='btn btn-info btn-xs outline' 
>Edit</a>
   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="110px" />
</asp:TemplateField>
 
                                                 
                                                 
<asp:BoundField DataField="WHID"   HeaderText="Warehouse ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="WHName"   HeaderText="Warehouse Name"></asp:BoundField>
    <asp:BoundField DataField="Warehouse_Code"   HeaderText="Warehouse Code"></asp:BoundField>
    <asp:BoundField DataField="Series_Code"   HeaderText="Series Code"></asp:BoundField>
<asp:BoundField DataField="IsActive"   HeaderText="Is Active"></asp:BoundField>

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
                   
<a href="WarehouseMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
                
</div>
</div>
                 

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
var txtWarehousename = document.getElementById('<%= txtWarehousename.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

if (txtWarehousename == "") {
document.getElementById('<%= txtWarehousename.ClientID%>').style.borderColor = "red";
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


