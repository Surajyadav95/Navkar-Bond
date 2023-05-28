<%@ Page Title="Bond | NOC Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="NocSummary.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Bond |  Bond NOC Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Bond NOC Summary  
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
           
<div class="page-container">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
                                 
<div class="row">
<div class="col-md-5  col-xs-12"  style="width: 400px;" >                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate"  style="width: 150px;" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>

</div>

                                          
</div>

                       
                                   
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Category
<asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"  class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">CHA</asp:ListItem>
<asp:ListItem Value="2">Importer</asp:ListItem>
<asp:ListItem Value="3">Customer</asp:ListItem>
<asp:ListItem Value="4">Bond Type</asp:ListItem>
<asp:ListItem Value="5">NOC No</asp:ListItem>
<asp:ListItem Value="6">Warehouse</asp:ListItem>

</asp:DropDownList>
                                               
</div>

</div>
<div class="col-md-4 col-xs-12" style="display:none;" id="divCHA" runat="server">                                      
<div class="form-group text-label">
CHA Name
<asp:DropDownList ID="ddlcha" runat="server" class="form-control text-label" >                                         
</asp:DropDownList>

</div>
</div>

<div class="col-md-4 col-xs-12" style="display:none;" id="divImpoeter" runat="server">                                      
<div class="form-group text-label">
Importer Name
<asp:DropDownList ID="ddlimpor" runat="server" class="form-control text-label" >                                          
</asp:DropDownList>
</div>
</div>

<div class="col-md-4 col-xs-12" style="display:none;" id="divCustomer" runat="server">                                      
<div class="form-group text-label">
Customer Name
<asp:DropDownList ID="ddlcustomer" runat="server" class="form-control text-label" >                                          
</asp:DropDownList>
</div>
</div>
     <div class="col-md-3  col-xs-12" style="display:none;"  id="divWarehouse" runat="server">                                      
<div class="form-group text-label">
<b >Warehouse </b>
<asp:DropDownList ID="ddlWarehouse" runat="server" class="form-control text-label" AutoPostBack="false">

</asp:DropDownList>

</div>
</div> 
<div class="col-md-3 col-xs-12" style="display:none;"  id="divBond" runat="server">                                      
<div class="form-group text-label">
Bond Type
<asp:DropDownList ID="ddlBondType" runat="server" class="form-control text-label" >  
<asp:ListItem Value="0">All</asp:ListItem>
<asp:ListItem Value="1">Open Bond</asp:ListItem>                                                     
<asp:ListItem Value="2">Close Bond</asp:ListItem>
<asp:ListItem Value="3">OPEN BOND-SEC-49</asp:ListItem>
<asp:ListItem Value="4">CLOSE BOND-SEC-49</asp:ListItem>  
    <asp:ListItem Value="5">General Closed</asp:ListItem>
<asp:ListItem Value="6">General Opened</asp:ListItem>                                           
</asp:DropDownList>

</div>
</div>
                                            
<div class="col-md-2 col-xs-12" style="display:none;"  id="divNoc" runat="server">
<div class="form-group text-label">
<b >NOC No</b>
<asp:TextBox ID="txtNoc" Style="text-transform:uppercase" class="form-control text-label "  placeholder="NOCNo"
runat="server"   ></asp:TextBox>
</div>
</div>
                                          

                                        
<asp:Label ID="lblFD_ID" Visible="false" runat="server" Text=""></asp:Label>
                                         
<div class="col-md-1 col-xs- 12" >
<div class="form-group pull-right" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show"   OnClick="btnsearch_Click"></asp:Button>
</div>              
</div>
<div class="col-md-1 col-xs- 12" style="display:none" >
<div class="form-group" style="padding-top:20px" >
<asp:Button ID="btnprint" target="_blank" runat="server"   
class=" btn btn-warning btn-sm outline" Text="Print" OnClick="btnprint_Click"  ></asp:Button>
</div>                
</div>
</div>
                                         
                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class="col-lg-8 text-label "  style="padding-left:0px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField Visible="false">
<ItemTemplate>
<asp:RadioButton ID="redbutton"  runat="server" onclick="checkRadioBtn(this);"   />
</ItemTemplate>
</asp:TemplateField>
                                         
</Columns>
<Columns>

<asp:TemplateField>
<ItemTemplate>

<a  href='<%# "../Report_Bond/BondLogo_Print.aspx?NOCNo=" & (DataBinder.Eval(Container.DataItem, "NOCNo")).ToString()%>'target="_blank"  
Class='btn btn-primary btn-xs outline ' 
>Print</a>
                                                          
<a  href='<%# "BondNoc.aspx?NOCNoView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "NOCNo")).ToString())%>' target="_blank"
Class='btn btn-success btn-xs outline' 
>View</a>

<%--<a  href='<%# "BondNoc.aspx?NOCNoEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "NOCNo")).ToString())%>' target="_blank"
Class='<%#Eval("Editcss")%>' 
>Edit</a>--%>
   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="120px" />
</asp:TemplateField>
                                                 
<asp:TemplateField HeaderText="NOC No">
<ItemTemplate>

<asp:Label runat="server" ID="lblNOCNo" Text='<%#Eval("NOCNo")%>'></asp:Label>

</ItemTemplate>

<ItemStyle  />
</asp:TemplateField>

<asp:BoundField DataField="WR_CODE" HeaderText="Warehouse"></asp:BoundField>
<asp:BoundField DataField="BOENo" HeaderText="BE No"></asp:BoundField>
<asp:BoundField DataField="BondType" HeaderText="Bond Type"></asp:BoundField>
<asp:BoundField DataField="IGMNo" HeaderText="IGM No"></asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Quantity"></asp:BoundField>
                                                 

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
<script type="text/javascript">
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
</script>
    
    
<script>
function pop(url, w, h, T, L) {
n = window.open(url, null, 'top=' + T + ',left=' + L + ',toolbar=0,location=0,directories=0,status=1,menubar=0,titlebar=0,scrollbars=1,resizable=0,width=' + w + ',height=' + h);
if (n == null) {
return true;
}
return false;
}
</script>
</div>
</asp:Content>
