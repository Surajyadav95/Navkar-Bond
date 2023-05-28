<%@ Page Title="Bond |Godown Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="GodownSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Bond |  Godown Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Godown Summary  
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
<div class="panel-body" >
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
<ContentTemplate>
                                    
                                                
<div class="row">
<div class="col-md-3 col-xs-12" >
<div class="form-group text-label">
<b >Center Code</b>
<asp:TextBox ID="txtcenter" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
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
</ContentTemplate>
</asp:UpdatePanel>

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
                                                          
<a  href='<%# "GodownMaster.aspx?EntryIDView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "EntryID")).ToString())%>' target="_blank"
Class='btn btn-success btn-xs outline' 
>View</a>

<a  href='<%# "GodownMaster.aspx?EntryIDEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "EntryID")).ToString())%>' target="_blank"
Class='btn btn-info btn-xs outline' 
>Edit</a>
   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>


                                                    
<asp:BoundField DataField="CentreCode" HeaderText="Center Code" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="GodownCode" HeaderText="Godown Code" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Warehousecode" HeaderText="Warehouse Code" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Warehousedesc" HeaderText="Warehouse Description"></asp:BoundField>
<asp:BoundField DataField="conscapacity" HeaderText="Constructive Capacity"></asp:BoundField>
<asp:BoundField DataField="thumbcapacity" HeaderText="Thumbrule Capacity"></asp:BoundField>

<asp:BoundField DataField="width" HeaderText="Width"></asp:BoundField>
<asp:BoundField DataField="Lenght" HeaderText="Length"></asp:BoundField>
<asp:BoundField DataField="height" HeaderText="Height"></asp:BoundField>
<asp:BoundField DataField="Area_In_SQM" HeaderText="Area in SQM"></asp:BoundField>
<asp:BoundField DataField="Area_In_SQFT" HeaderText="Area in SQ. FT."></asp:BoundField>

<asp:BoundField DataField="IsActive" HeaderText="Is Active"></asp:BoundField>
                                               

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
