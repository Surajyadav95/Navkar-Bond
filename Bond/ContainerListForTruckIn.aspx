<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="ContainerListForTruckIn.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btntruckIn").click();
self.close();
}
</script>
   <style>
       .text-center{
           text-align:center
       }
       .scrolling-table-container{
           max-height:300px;
           overflow:auto
       }
   </style>
<div class="container" style="background-color: white">   
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>Container List<small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>

<div class="row">
<div class="col-md-10 col-xs-12 text-label " style="padding-right: 60px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdTruckSlips" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary btn-sm'                                                       
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ContainerNo")%>' runat="server" OnClick="lnkselect_Click"
><i class="fa fa-check"></i></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="40px" />
</asp:TemplateField>
<asp:BoundField DataField="ContainerNo" HeaderText="Container No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="text-center">
       <ItemTemplate>
           <asp:Label runat="server" ID="lblSize" text='<%#Eval("Size")%>'></asp:Label>
       </ItemTemplate> 
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
<%--<asp:BoundField DataField="Size" HeaderText="Size" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>--%>
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


