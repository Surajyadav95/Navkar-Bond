<%@ Page Title="Bond |Gate Pass Summary" Language="VB" MasterPageFile="../Bond/User.master" AutoEventWireup="false"
CodeFile="GatePassSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond |Gate Pass Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Gate Pass Summary  
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
    <div class="col-md-4 col-xs-4" >
<div class="form-group text-label">
<b >Search</b>
<asp:DropDownList runat="server" ID="ddlSearchCriteria" CssClass="form-control">
    <asp:ListItem Value="0">All</asp:ListItem>
    <asp:ListItem Value="1">NOC No</asp:ListItem>
    <asp:ListItem Value="2">EXB-BOE No</asp:ListItem>
    <asp:ListItem Value="3">IGM No</asp:ListItem>
    <asp:ListItem Value="4">Item No</asp:ListItem>
    <asp:ListItem Value="5">Gate Pass No</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-4 col-xs-12" >
<div class="form-group text-label">
<b >Gate Pass Number</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Gate Pass Number"
runat="server"   ></asp:TextBox>
</div>
</div> 

<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" 
Text="Show"     />
</div>
                                              
                                      
</div>
                                               
</div>
</ContentTemplate>
</asp:UpdatePanel>

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-left:0px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
                                                          
                                                       

<a  href='<%# "../Report_Bond/GatePassLogo_Print.aspx?GPNo=" & (DataBinder.Eval(Container.DataItem, "GPNo")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>


                                                  
<asp:BoundField DataField="GPNo" HeaderText="Gate Pass Number"></asp:BoundField>
<asp:BoundField DataField="Date" HeaderText="Gate Pass Date"></asp:BoundField>
<asp:BoundField DataField="NOCNo" HeaderText="NOC No"></asp:BoundField>
<asp:BoundField DataField="exboeno" HeaderText="BOE No"></asp:BoundField>
<asp:BoundField DataField="igmno" HeaderText="IGM No"></asp:BoundField>
<asp:BoundField DataField="itemno" HeaderText="Item No"></asp:BoundField>

<%--<asp:BoundField DataField="BondNo" HeaderText="Bond No"></asp:BoundField>--%>
                                                
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
<%-- <script type="text/javascript">
function checkRadioBtn(id) {
var gv = document.getElementById('<%=grdcontainer.ClientID%>');

for (var i = 1; i < gv.rows.length; i++) {
var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

// Check if the id not same
if (radioBtn[0].id != id.id) {
radioBtn[0].checked = false;
}
}
}
</script>--%>
<%--  <script type="text/javascript">
 
function BondExPrint() {
            
var NOCNo1= document.getElementById('<%= txtNOCNo.ClientID%>').value;
             
var url = "../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1;
//alert("hi")
                
window.open(url);

}


</script>--%>
</asp:Content>
