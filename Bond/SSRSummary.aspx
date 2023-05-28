<%@ Page Title="Bond | SSR Summary" Language="VB" MasterPageFile="../Bond/User.master" AutoEventWireup="false"
CodeFile="SSRSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond | SSR Summary .</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i> SSR Summary  
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >                                                  
                                                
<div class="row">
    <div class="col-md-5 col-xs-12" style="width: 400px;">
                                <div class="form-group date text-label">
                                    Date
                                           
                                    <div class="input-group input-append date input-daterange" id="datePicker">
                                        <asp:TextBox ID="txtfromDate" Style="width: 150px;" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                        <div class="input-group-addon text-label" style="width: 40px;">To</div>
                                        <asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                    </div>

                                </div>


                            </div>
    <div class="col-md-2 col-xs-12">
                                <div class="form-group text-label">
                            Category
                                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" class="form-control text-label">
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="NOC No">NOC No</asp:ListItem>
                                        <asp:ListItem Value="Customer">Customer</asp:ListItem>
                                        <asp:ListItem Value="CHA">CHA</asp:ListItem>                                        
                                    </asp:DropDownList>

                                </div>

                            </div>
    <div class="col-md-4 col-xs-12" style="display: none;" id="divCustomer" runat="server">
                                <div class="form-group text-label">
                                    Customer Name
                                            <asp:DropDownList ID="ddlcustomer" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>
    <div class="col-md-4 col-xs-12" style="display: none;" id="divCHA" runat="server">
                                <div class="form-group text-label">
                                    CHA Name
                                    <asp:DropDownList ID="ddlcha" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>

                                </div>
                            </div>
<div class="col-md-2 col-xs-12" style="display: none;" id="divnocno" runat="server">
<div class="form-group text-label">
<b >NOC No</b>
<asp:TextBox ID="txtnocnosearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter NOC No"
runat="server"></asp:TextBox>
</div>
</div> 

<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click" Text="Show"/>
</div>                                                                                   
</div>  
    <div class="col-md-2 col-xs- 12">
                                <div class="form-group pull-Left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>                                             
</div>
                     
<div class="row">

<div class="row">
<div class=" col-md-12 col-xs-12 text-label "  style="padding-left:0px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField>
<ItemTemplate>                                                                                                              
<a  href='<%# "../Report_Bond/SSRPrint.aspx?SSRNo=" & (DataBinder.Eval(Container.DataItem, "SSR No")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="70px" />
</asp:TemplateField>                                                  
<asp:BoundField DataField="SSR No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="SSR No"></asp:BoundField>
<asp:BoundField DataField="SSR Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="SSR Date"></asp:BoundField>
<asp:BoundField DataField="SSR Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="SSR Type"></asp:BoundField>
<asp:BoundField DataField="NOC No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="NOC No"></asp:BoundField>
<asp:BoundField DataField="NOC Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="NOC Date"></asp:BoundField>

<asp:BoundField DataField="Customer Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left" HeaderText="Customer Name"></asp:BoundField>
<asp:BoundField DataField="CHA Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Container No"></asp:BoundField>

<asp:BoundField DataField="Size" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Size"></asp:BoundField>      
<asp:BoundField DataField="Account Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left" HeaderText="Account Name"></asp:BoundField>                                               
<asp:BoundField DataField="Service Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Service Type"></asp:BoundField>                                               
<asp:BoundField DataField="Material Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Material Type"></asp:BoundField>                                               
<asp:BoundField DataField="Packages" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" HeaderText="Packages"></asp:BoundField>                                               
<asp:BoundField DataField="Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right" HeaderText="Amount"></asp:BoundField>                                               
                                             
                                                   
</Columns>
</asp:GridView>
</div>
</div>
</div>
</div>
</div>                                                                
</div>
</div>                                                                                            
</div>                 
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
