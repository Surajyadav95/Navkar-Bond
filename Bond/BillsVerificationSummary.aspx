<%@ Page Title="Bond | Bills Verification Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="BillsVerificationSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond | Bills Verification Summary .</title>
</head>
    <style>
        .text-center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Bills Verification Summary  
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body" >
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 

                                    
                                                
<div class="row">
    <div class="col-md-4 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" style="width: 150px;" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>
</div>                                       
</div>

<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">Vendor</asp:ListItem>
<asp:ListItem Value="2">Vendor Bill No</asp:ListItem>
<asp:ListItem Value="3">Bill Type</asp:ListItem> 
<asp:ListItem Value="4">Activity</asp:ListItem> 
</asp:DropDownList>                                               
</div>
</div>
    <div class="col-md-3 col-xs-12" style="display:none;"  id="divVendor" runat="server">
<div class="form-group text-label">
Vendor
<asp:DropDownList ID="ddlVendor" runat="server" class="form-control text-label">
</asp:DropDownList>                                               
</div>
</div> 
<div class="col-md-2 col-xs-12" id="divVendorBillNo" runat="server" style="display:none" >
<div class="form-group text-label">
<b >Vendor Bill No</b>
<asp:TextBox ID="txtVendorBillNo" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Vendor Bill No"
runat="server"   ></asp:TextBox>
</div>
</div> 
    <div class="col-md-2 col-xs-12" style="display:none;"  id="divBillType" runat="server">
<div class="form-group text-label">
Bill Type
<asp:DropDownList ID="ddlbilltype" runat="server" class="form-control text-label">
</asp:DropDownList>                                               
</div>
</div> 
<div class="col-md-2 col-xs-12" style="display:none;"  id="divActivity" runat="server">
<div class="form-group text-label">
Activity
<asp:DropDownList ID="ddlActivity" runat="server" class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">Bond In</asp:ListItem>
<asp:ListItem Value="2">Bond Ex</asp:ListItem>
</asp:DropDownList>                                               
</div>
</div> 
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
                                              
                                      
</div>
        <div class="col-md-2 col-xs- 12">
                                <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export TO Excel" ></asp:Button>
                                </div>
                            </div>                                       
</div>

                     
<div class="row">

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="7" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
                                                          
<a  href='<%# "BillsVerification.aspx?VerificationNo=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "Verification No")).ToString())%>' target="_blank"
Class='btn btn-success btn-xs outline'>View</a>

<asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Cancel" OnClick="lnkCancel_Click" 
    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Verification No")%>' runat="server"></asp:LinkButton>                                                         
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="130px" />
</asp:TemplateField>
   
<asp:BoundField DataField="Verification No" HeaderText="Verification No" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Verification Date" HeaderText="Verification Date" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Vendor Bill No" HeaderText="Vendor Bill No"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Vendor Bill Date" HeaderText="Vendor Bill Date"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Vendor" HeaderText="Vendor" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Bill Type" HeaderText="Bill Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Activity" HeaderText="Activity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Period From" HeaderText="From" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Period To" HeaderText="To" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Net Total" HeaderText="Net Total" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="CGST" HeaderText="CGST" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="SGST" HeaderText="SGST" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="IGST" HeaderText="IGST" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Grand Total" HeaderText="GrandTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
</div>
</div>

</div>


</div>
</div>
</div>
</div>
                          
                     
                       
                       
</div>
</div>
         
</div>

</asp:Content>
