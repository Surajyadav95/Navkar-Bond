﻿<%@ Page Title="Bond |Vendor Summary" Language="VB" MasterPageFile="../Bond/User.master" AutoEventWireup="false"
CodeFile="VendorSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond |  Vendor Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Vendor Summary  
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
<ContentTemplate>
                                    
                                                
<div class="row">
<div class="col-md-4 col-xs-12" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearchs" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 

<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
                                              
                                      
</div>
                                               
</div>
</ContentTemplate>
</asp:UpdatePanel>

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

 

<asp:BoundField DataField="EntryID" HeaderText="Entry ID"></asp:BoundField>
 <asp:BoundField DataField="Entry Date" HeaderText="Entry Date"></asp:BoundField>                                                 
<asp:BoundField DataField="TariffID" HeaderText="Tariff ID"></asp:BoundField>
<asp:BoundField DataField="Name" HeaderText="Vendor Invoice Type"></asp:BoundField>
<asp:BoundField DataField="AccountName" HeaderText="Account Head"></asp:BoundField>
<asp:BoundField DataField="Equipment_Name" HeaderText="Equipment Name"></asp:BoundField>
    <asp:BoundField DataField="Material_Name" HeaderText="Material Type"></asp:BoundField>
    <asp:BoundField DataField="BasedOn" HeaderText="Based On"></asp:BoundField>


</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>


</div>
</div>   
                                 
                               
</div>
</div>
                          
                     
                       
                       
</div>
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
         
</div>

</asp:Content>
