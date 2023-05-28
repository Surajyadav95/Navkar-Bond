<%@ Page Title="" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="UserRights.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
        
<div class="container" style="background-color: white;margin-top:-50px">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
      
<div class="panel-body">
                
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>Assign User Rights  <small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="Label1" runat="server" Text="" ForeColor="red"></asp:Label>

</div>

</div>


<div class="row">
<div class="col-sm-5  col-xs-12">
<div class="form-group text-label">
                              
<b >User Name</b>
<asp:DropDownList ID="ddlUser" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>


</div>
</div>
<div class="col-sm-5  col-xs-12">
<div class="form-group text-label">
                             
<b>Menu From Department</b>
<asp:DropDownList ID="ddlMenu" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>


</div>
</div>
                       
</div>

<div class="row">
                         
<div class="col-md-5 col-xs-5">
<div class="form-group text-label">
<b>Search</b>
<asp:TextBox ID="txtseach" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-2  col-xs-2">

<div class="form-group pull-left" style="padding-top: 20px">
<asp:Button ID="btnSearch" class="btn btn-primary btn-sm outline"
runat="server" Text="Search" OnClick="btnShow_Click" />
<asp:Label ID="lblAssociatesID" Visible="false" runat="server" Text="0"></asp:Label>

</div>
</div>

</div>
<br />
<div class="row">

                       
<div class="row text-label">
<div class="col-lg-12 " style="padding-right: 50px;">
<div class="table-responsive " style="margin-left: 12px; margin-right: 0px; height: 300px;">
<asp:GridView ID="grduserdata" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" Style="align-content: center">

<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />

<Columns>



<asp:TemplateField HeaderText="Menu">
<ItemTemplate>
<asp:HiddenField ID="hfEntryid" runat="server" Value='<%#Eval("Menuid")%>' />
<asp:Label ID="lblmenu" runat="server" Text='<%# Eval("MenuDesc")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Can Access?">
<ItemTemplate>

<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("IsAccess")%>' runat="server" />
</ItemTemplate>
<ItemStyle HorizontalAlign="center" Width="25%" />
</asp:TemplateField>

</Columns>

</asp:GridView>
</div>
</div>
</div>
                           
</div>
<div class="row">

<div class="col-sm-2  col-xs-2">
<div class="form-group pull-left" style="padding-top: 20px;">
<asp:Button ID="btnupdate" class="btn btn-primary btn-sm outline"
runat="server" Text="Update" Style="display: none" OnClick="btnupdate_Click" />
</div>
</div>



</div>



</div>
</div>

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
>
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
<asp:Button ID="btntest" runat="server"
class="btn btn-info btn-block" Text="OK" data-dismiss="modal" aria-hidden="true" OnClientClick="populateCalendarTextbox()"></asp:Button>
</div>
</div>

                      
</div>
</div>
</div>
<script type="text/javascript">
<script type="text/javascript">
function populateCalendarTextbox() {
//alert("Hi")

this.parent.method_that_updates();

}


</script>
</asp:Content>


