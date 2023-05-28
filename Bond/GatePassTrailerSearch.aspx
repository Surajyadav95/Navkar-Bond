<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="GatePassTrailerSearch.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function callparentfunction() {
            //alert("hiii");
            alert('hi')

            //  alert(window.opener.document.getElementById("_btnIndentItem"));
            window.opener.document.getElementById("MainContent_btnTrailers").click();
            self.close();
        }
</script>
<div class="container" style="background-color: white">
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>Trailer List <small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>
<div class="row">
<div class="col-md-4 col-xs-6" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
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
<div class="col-md-10 col-xs-12 text-label " style="padding-right: 60px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary btn-sm' Text="Select"                                                         
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VEHICLE_NO")%>' runat="server" OnClick="lnkselect_Click"
></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="80px" />
</asp:TemplateField>
<asp:BoundField DataField="SLIP_NO" HeaderText="Slip No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="SLIP_DATE" HeaderText="Slip Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>

<asp:BoundField DataField="VEHICLE_NO" HeaderText="Trailer No"></asp:BoundField>
<asp:BoundField DataField="agentName" HeaderText="Customer"></asp:BoundField>

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
<a href="BondSearch.aspx" class="btn btn-info btn-block">OK</a>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
<script type="text/javascript" >
function callparentfunction() {
    window.opener.document.getElementById("ContentPlaceHolder1_btnTrailers").click();
self.close();
}
</script>    
</asp:Content>


