<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="Search.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

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
<h1>  Search  <small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>
<div class="row"> 
<div class="col-md-3 col-xs-6">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearch"   Style="text-transform: uppercase;"  runat="server" placeholder="Search by NOC No or Assess No"    class="form-control text-label">                                            
</asp:TextBox> 
</div>
</div>
    <div class="col-md-2 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Work Year</b>
<asp:TextBox ID="txtWorkYear"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                                        
</asp:TextBox> 
</div>
</div>
                                                  
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline " runat="server"
  OnClick="btnsearch_Click"
Text="Search"     />
</div>
</div>                                             
</div>
<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">
<div class="row">
<div class="col-md-10 col-xs-12 text-label " style="padding-right: 70px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 12px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6" >
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary btn-sm outline' Text="Select"                                                         
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AssessNo")%>' runat="server" OnClick="lnkselect_Click"
></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>
    <asp:BoundField DataField="AssessNo" HeaderText="Assess No"></asp:BoundField>
    <asp:BoundField DataField="Work Year" HeaderText="Work Year"></asp:BoundField>
    <asp:BoundField DataField="Assess Date" HeaderText="Assess Date"></asp:BoundField>
    <asp:BoundField DataField="NOCNo" HeaderText="NOC No"></asp:BoundField>
    <asp:BoundField DataField="Bond Type" HeaderText="Bond Type"></asp:BoundField>
    <asp:BoundField DataField="Delivery Type" HeaderText="Delivery Type"></asp:BoundField>
    <asp:BoundField DataField="BOENo" HeaderText="BOENo"></asp:BoundField>
<asp:BoundField DataField="IGMNo" HeaderText="IGM No"  ></asp:BoundField>
<asp:BoundField DataField="ItemNo" HeaderText="Item No"></asp:BoundField>

    
    
<%--    <asp:BoundField DataField="Receipt/PD No" HeaderText="Receipt/PD No"></asp:BoundField>
     <asp:BoundField DataField="Receipt Date" HeaderText="Receipt Date"></asp:BoundField>--%>
     
     
     <asp:BoundField DataField="Grand Total" HeaderText="Grand Total"></asp:BoundField>
 

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


