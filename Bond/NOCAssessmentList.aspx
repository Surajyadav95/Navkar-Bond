﻿<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="NOCAssessmentList.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btnNOCList").click();
self.close();
}
</script>
   <style>
       .text-center{
           text-align:center
       }

   </style>
<div class="container" style="background-color: white">   
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>NOC List<small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>
    <div class="row">
        <div class="col-md-4 col-xs-4" >
<div class="form-group text-label">
<b >Search</b>
<asp:DropDownList runat="server" ID="ddlSearchCriteria" CssClass="form-control">
    <asp:ListItem Value="0">All</asp:ListItem>
    <asp:ListItem Value="1">NOC No</asp:ListItem>
    <asp:ListItem Value="2">BOE NO</asp:ListItem>
    <asp:ListItem Value="3">IGM No</asp:ListItem>
    <asp:ListItem Value="4">Item No</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-4 col-xs-6" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 
<div class="col-xs-1" >
<div class="form-group" style="padding-top: 18px;" >
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
</div>                                             
</div>
<div class="row">
<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container" >
<asp:GridView ID="grdNOCList" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary btn-sm'                                                       
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NOCNo")%>' runat="server" OnClick="lnkselect_Click"
><i class="fa fa-check"></i></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="40px" />
</asp:TemplateField>
<asp:BoundField DataField="NOCNo" HeaderText="Noc No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="BOENo" HeaderText="BOE No"></asp:BoundField>
<asp:BoundField DataField="NOCDate" HeaderText="NOC Date"></asp:BoundField>
    <asp:BoundField DataField="IGMNO" HeaderText="IGM No"></asp:BoundField>
<asp:BoundField DataField="ITEMNO" HeaderText="Item No"></asp:BoundField>
<asp:BoundField DataField="Customer" HeaderText="Customer"></asp:BoundField>
<asp:BoundField DataField="CHAName" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="ImporterName" HeaderText="Importer Name"></asp:BoundField>
<asp:BoundField DataField="BondType" HeaderText="Bond Type"></asp:BoundField>
</Columns>
</asp:GridView>
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
<a href="BondSearch.aspx" class="btn btn-info btn-block">OK</a>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
<%--<script type="text/javascript" >
function callparentfunction() {
window.opener.document.getElementById("ContentPlaceHolder1_btnIndentItem").click();
self.close();
}
</script> --%>   
</asp:Content>


