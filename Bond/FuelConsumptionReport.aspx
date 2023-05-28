<%@ Page Title="Bond |Fuel Consumption Report" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="FuelConsumptionReport.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Bond |Fuel Consumption Report</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:500px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Fuel Consumption Report
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
                 

                                    
                                                
<div class="row">
 <div class="col-sm-6 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria"  runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" AutoPostBack="true"   class="form-control text-label">
<asp:ListItem Value="ALL">ALL</asp:ListItem> 
 
<asp:ListItem Value="1">Cost Center</asp:ListItem>
<asp:ListItem Value="2">Vehicle No</asp:ListItem>
   
</asp:DropDownList>
                                               
</div>

</div>

   

     <div class="col-md-3 col-xs-12" id="divLine" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Cost Center</b>
<asp:DropDownList ID="ddlCostCenter" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                               
                                         </asp:DropDownList>
</div>
</div>

<div class="col-md-3 col-xs-12" style="display:none;"  id="DivVehicle" runat="server">
<div class="form-group text-label">
<b >Vehicle No</b>
<asp:TextBox ID="txtVehicle" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Assess No"
runat="server"   ></asp:TextBox>
</div>
</div>
      
     </ContentTemplate>
</asp:UpdatePanel>
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
    </div>   
    
                                        
                                      
</div>
               
                     <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive  ">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" 
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"    >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px"  verticalalign="Bottom" horizontalalign="center" /> 
<Columns>

</Columns>

</asp:GridView>
</div>
    
</div>
</div>
        
          
   <div class="col-md-2 col-xs-12 pull-rigth">
                                <div class="form-group pull-rigth" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"  
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>  
  </div>
    <br />
    


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
