<%@ Page Title="Bond |Bond Ex Summary" Language="VB" MasterPageFile="../Bond/User.master" AutoEventWireup="false"
CodeFile="BondExSummary.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond |  Bond Ex Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Bond Ex Summary  
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
<div class="col-md-4  col-xs-12" style="width: 400px;">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
                  
<asp:TextBox ID="txtfromDate"  style="width: 150px;" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 0px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
           
</div>

</div>   
</div>
 
<div class="row">
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Category
<asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">Bond Type</asp:ListItem>
<asp:ListItem Value="2">Entry ID</asp:ListItem>
<asp:ListItem Value="3">Bond No</asp:ListItem>
                                                     
                                                      
</asp:DropDownList>
                                               
</div>

</div>
<div class="col-md-2 col-xs-12" style="display:none;"  id="divDepos" runat="server">
<div class="form-group text-label">
<b >Search Text</b>
<asp:TextBox ID="txtDeposNo" Style="text-transform:uppercase" class="form-control text-label "  placeholder="search Text"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-2 col-xs-12" style="display:none;"  id="divbondn" runat="server">
<div class="form-group text-label">
<b >Bond No</b>
<asp:TextBox ID="textbondn" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Bond No"  
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-md-3 col-xs-12" style="display:none;" id="divBondtype" runat="server">                                      
<div class="form-group text-label">
Bond Type
<asp:DropDownList ID="ddltype" runat="server" class="form-control text-label" >  
<asp:ListItem Value="0">All</asp:ListItem>
<asp:ListItem Value="1">Open Bond</asp:ListItem>                                                     
<asp:ListItem Value="2">Close Bond</asp:ListItem>
<asp:ListItem Value="3">OPEN BOND-SEC-49</asp:ListItem>
<asp:ListItem Value="4">CLOSE BOND-SEC-49</asp:ListItem>                                          
</asp:DropDownList>

</div>
</div>

<div class="row" style="display:none">
<asp:TextBox runat="server" ID="txtNOCNo" Text=""></asp:TextBox>

</div>
                                        
<asp:Label ID="lblFD_ID" Visible="false" runat="server" Text=""></asp:Label>

<div class="col-md-1 col-xs-12">
<div class="form-group" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" ></asp:Button>
</div>              
</div>
<div class="col-md-1 col-xs-12">
<div class="form-group" style="padding-top:22px" >
<asp:Button ID="btnprint" runat="server"   
class=" btn btn-warning btn-sm outline" Text="Print" OnClick="btnprint_Click"></asp:Button>
</div>              
</div>
                                          

</div>
                                   
                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class="col-md-8 col-xs-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:RadioButton ID="redbutton"  runat="server" onclick="checkRadioBtn(this);"   />
</ItemTemplate>
</asp:TemplateField>
                                         
</Columns>
<Columns>
                                                  
<asp:TemplateField>
<ItemTemplate>

                                                        
<a  href='<%# "../Report_Bond/BondEx_Print.aspx?EntryID=" & (DataBinder.Eval(Container.DataItem, "EntryID")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
                                                          
<a  href='<%# "BondEX.aspx?EntryIDView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "EntryID")).ToString())%>' target="_blank"
Class='btn btn-success btn-xs outline' 
>View</a>

<%--<a href='<%# "BondEX.aspx?EntryIDEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "EntryID")).ToString())%>' target="_blank"
Class='btn btn-info btn-xs outline' 
>Edit</a>--%>
   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="120px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="NOC No">
<ItemTemplate>

<asp:Label runat="server" ID="lblNOCNo" Text='<%#Eval("NOCNo")%>'></asp:Label>
<asp:Label runat="server" Visible="false" ID="lblbondexno" Text='<%#Eval("EntryID")%>'></asp:Label>

</ItemTemplate>

<ItemStyle  />
</asp:TemplateField>

<asp:BoundField DataField="EntryID" ItemStyle-Width="140px" HeaderText="Bond Ex No"></asp:BoundField>
<asp:BoundField DataField="BondNo" HeaderText="Bond No"></asp:BoundField>
<%--<asp:BoundField DataField="NOCNo" HeaderText="Noc No"></asp:BoundField>--%>
<asp:BoundField DataField="Qty" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Unit" HeaderText="Unit"></asp:BoundField>
<asp:BoundField DataField="AreaBalance" HeaderText="SQM"></asp:BoundField>

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
<script type="text/javascript">
 
function BondExPrint() {
            
var NOCNo1= document.getElementById('<%= txtNOCNo.ClientID%>').value;
             
var url = "../Report_Bond/BondEx_logo_print.aspx?EntryID=" + NOCNo1;
//alert("hi")
                
window.open(url);

}


</script>
</asp:Content>
