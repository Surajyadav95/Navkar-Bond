<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="PendingUnloadingSheet.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btnIndentItem").click();
    self.close();
}
</script>
<div class="container" style="background-color: white">
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Pending Unloading Sheets <small class="pull-right" style="margin-right:20px"></small></h1>
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
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b>Search</b>
<asp:TextBox ID="txtSearch" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>

</div>
</div>

            
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnShow" class="btn btn-primary btn-sm" runat="server" OnClick="btnShow_Click"  
Text="Show"  />
</div>
                                              
                                      
</div>

</div>

             

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">                  

<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive" style="margin-right: 0px;">
<asp:GridView ID="grdpendinggrnlist" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">

<Columns>
<asp:TemplateField>
<ItemTemplate>
    <asp:LinkButton ID="lnkselect" ControlStyle-CssClass='btn btn-primary btn-sm outline' OnClick="btnSave_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DepositNo")%>' runat="server" 
                                                            ><i class="fa fa-check" aria-hidden="true"></i></asp:LinkButton>
<%--<asp:Label runat="server" ID="lblcode" Text='<%#Eval("LOT_NO")%>' Visible="false"></asp:Label>--%>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="60px" />
</asp:TemplateField>                                  
<asp:BoundField DataField="DepositNo" HeaderText="Unloading Sheet No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="EntryDate" HeaderText="Unloading Date" ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="NOC_No" HeaderText="NOC No" ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="text-center"></asp:BoundField>

<asp:BoundField DataField="BondNo" HeaderText="Bond No"></asp:BoundField>
<asp:BoundField DataField="IGM_NO" HeaderText="IGM No"></asp:BoundField>
<asp:BoundField DataField="ITEM_NO" HeaderText="Item No"></asp:BoundField>
<asp:BoundField DataField="BE_NO" HeaderText="BOE No"></asp:BoundField>

<asp:BoundField DataField="AGENTNAME" HeaderText="Customer"></asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Qty"></asp:BoundField>



</Columns>

</asp:GridView>
</div>
</div>
</div>                        
             
</div>
</div>
</div>

</asp:Content>


