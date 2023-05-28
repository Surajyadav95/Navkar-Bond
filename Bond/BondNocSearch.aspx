<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="BondNocSearch.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

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
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>Noc Search  <small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>
<div class="row"> 
    <div class="col-md-4 col-xs-4">
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="1">NOC No</asp:ListItem>
<asp:ListItem Value="2">BOE No</asp:ListItem>
<asp:ListItem Value="3">Ex-BOE No</asp:ListItem> 
<asp:ListItem Value="4">IGM-Item No</asp:ListItem> 

</asp:DropDownList>
                                               
</div>

</div>
<div class="col-md-6 col-xs-6" runat="server" id="divSearchText" style="display:none">
<div class="form-group text-label">
<b  >Search Text</b>
<asp:TextBox ID="txtsearc"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">                                            
</asp:TextBox> 
</div>
</div>
<div class="col-md-4 col-xs-4" runat="server" id="divIGMNo" style="display:none">
<div class="form-group text-label">
<b  >IGM No</b>
<asp:TextBox ID="txtIGMNo"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">                                            
</asp:TextBox> 
</div>
</div>
    <div class="col-md-2 col-xs-2" runat="server" id="divItemNo" style="display:none">
<div class="form-group text-label">
<b  >Item No</b>
<asp:TextBox ID="txtItemNo"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">                                            
</asp:TextBox> 
</div>
</div>                                                
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
</div>                                             
</div>
<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">
<div class="row">
<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container" style="margin-left: 12px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary' Text="Select"                                                         
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NOCno")%>' runat="server" OnClick="lnkselect_Click"
></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="80px" />
</asp:TemplateField>
<asp:BoundField DataField="NOCNo" HeaderText="Noc No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="NOCDate" HeaderText="NOC Date"></asp:BoundField>
<asp:BoundField DataField="BOENo" HeaderText="BOE No"></asp:BoundField>
<asp:BoundField DataField="ExBoeNo" HeaderText="Ex-BOE No"></asp:BoundField>
<asp:BoundField DataField="CHAName" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="ImporterName" HeaderText="Importer Name"></asp:BoundField>
<asp:BoundField DataField="BondType" HeaderText="Bond Type"></asp:BoundField>
<asp:BoundField DataField="IGMNo" HeaderText="IGM No"></asp:BoundField>
<asp:BoundField DataField="ItemNo" HeaderText="Item No"></asp:BoundField>


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
<%-- <a href="BondSearch.aspx" class="btn btn-info btn-block">OK</a>--%>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
<script type="text/javascript" >
function callparentfunction() {
window.opener.document.getElementById("ContentPlaceHolder1_btnIndentItem").click();
self.close();
}
</script>    
</asp:Content>


