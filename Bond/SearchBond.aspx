<%@ Page Title="Bond |  Bond Search" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="SearchBond.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond |Bond Search</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>  Bond Search
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Bond Search 
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >NOC No</b>
<asp:TextBox ID="txtNoc"   Style="text-transform: uppercase;" AutoPostBack="true" runat="server" OnTextChanged="txtNoc_TextChanged" class="form-control text-label" placeholder="Noc No">
                                            
</asp:TextBox> 
</div>
</div>
<div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return OpenItembond();"> 
                                 
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>

<asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
                              
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Bond Type</b>
<asp:TextBox ID="txtbondtype" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Bond Type                                                                                                                                                                                                                                                                                                                                                                                                                     "
runat="server" ReadOnly="true"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >NOC Date</b>
<asp:TextBox ID="txtNocdate" placeholder="yyyy-mm-dd " runat="server" ReadOnly="true" class="form-control text-label"
></asp:TextBox>
</div>
</div>
                                 
                               
<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
<b>NOC Valid Upto</b>                                         
<asp:TextBox ID="txtNocValid"  placeholder="yyyy-mm-dd "   runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
                                  
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >BE No</b>
<asp:TextBox ID="txtbeno"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Be No">                                           
</asp:TextBox> 
</div>
</div>
<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
<b>BE Date</b>                                         
<asp:TextBox ID="txtbedate"  placeholder="yyyy-mm-dd "   runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
</div>
<div class="row">
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>Importer Name</b>
<asp:textbox ID="txtImporter" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Importer Name " >
</asp:textbox>   
</div>
</div>
                                      
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> CHA Name</b>
<asp:textbox ID="txtCha" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Cha Name" >
</asp:textbox>   
</div>
</div>
</div>

<div class="row">
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>Cargo Description</b>
<asp:textbox ID="txtCargo" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Cargo Descrption" >
</asp:textbox>   
</div>
</div>
</div>

</asp:Panel>
                                             
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="panel panel-default" style=" padding: 10px; margin: 10px;">
<div id="Tabs" role="tabpanel">
<!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist" >
<li class="active"><a href="#InDetails" aria-controls="In Details" role="tab" data-toggle="tab">
In Details</a></li>
<li><a href="#BONDEX" aria-controls="BOND EX" role="tab" data-toggle="tab">Bond Ex</a></li>

<li><a href="#Invoice" aria-controls="Invoice" role="tab" data-toggle="tab">Invoice</a></li>

<li><a href="#BondGatePass" aria-controls="Bond Gate Pass" role="tab" data-toggle="tab">Bond Gate Pass</a></li>

<li><a href="#Balance" aria-controls="Balance" role="tab" data-toggle="tab">Balance</a></li>

<li><a href="#ContainerDetails" aria-controls="Container Details" role="tab" data-toggle="tab">Container Details</a></li>

</ul>
<!-- Tab panes -->
<div class="tab-content" style="padding-top: 20px">
<div role="tabpanel" class="tab-pane active" id="InDetails">
<div class="row">
<asp:UpdatePanel ID="up_grid1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
          
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdInDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                  
<asp:BoundField DataField="SerialNo" HeaderText="Serial No"></asp:BoundField>
<asp:BoundField DataField="BondNo" HeaderText="Bond No"></asp:BoundField>
<asp:BoundField DataField="BondDate" HeaderText="Bond Date "></asp:BoundField>
<asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
<asp:BoundField DataField="AreaOccp" HeaderText="Area Occp"></asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Unit" HeaderText="Unit"></asp:BoundField>
<asp:BoundField DataField="Grosswt" HeaderText="Gross wt"></asp:BoundField>
<asp:BoundField DataField="Value" HeaderText="Value"></asp:BoundField>
<asp:BoundField DataField="Duty" HeaderText="Duty"></asp:BoundField>
<asp:BoundField DataField="TotalValueDuty" HeaderText="Total Value& Duty"></asp:BoundField>
<asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
<asp:BoundField DataField="Tot20" HeaderText="Tot20"></asp:BoundField>
<asp:BoundField DataField="Tot40" HeaderText="Tot40"></asp:BoundField>
<asp:BoundField DataField="AddedBy" HeaderText="Added By"></asp:BoundField>
</Columns>
</asp:GridView>
</div>
</div>
       
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="BONDEX">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdbondex" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>
                                                  
<asp:BoundField DataField="Bond No" HeaderText="Bond No"></asp:BoundField>
<%--<asp:BoundField DataField="Ex Boe No" HeaderText="Ex Boe No""></asp:BoundField>--%>
<asp:BoundField DataField="Ex BOE Date" HeaderText="Ex BOE Date "></asp:BoundField>
<asp:BoundField DataField="CHA Name" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="Importer Name" HeaderText="Importer Name"></asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Qty"></asp:BoundField>
<%--<asp:BoundField DataField="Unit" HeaderText="Unit"></asp:BoundField>--%>
<asp:BoundField DataField="Gross wt" HeaderText="Gross wt"></asp:BoundField>
<asp:BoundField DataField="Value" HeaderText="Value"></asp:BoundField>
<asp:BoundField DataField="Duty" HeaderText="Duty"></asp:BoundField>                                              
<asp:BoundField DataField="Added By" HeaderText="Added By"></asp:BoundField>
<asp:BoundField DataField="Bond Ex No" HeaderText="Bond Ex No"></asp:BoundField>
<asp:BoundField DataField="Bond Ex Date" HeaderText="Bond Ex Date"></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Invoice">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdInvoice" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                
<asp:BoundField DataField="Assess Type" HeaderText="Assess Type"></asp:BoundField>
<asp:BoundField DataField="Assess No" HeaderText="Assess No"></asp:BoundField>
<asp:BoundField DataField="Assess Date" HeaderText="Assess Date"></asp:BoundField>
<asp:BoundField DataField="CHA Name" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="Storage Valid Upto" HeaderText="Storage Valid Upto"></asp:BoundField>
<asp:BoundField DataField="Insurance Valid Upto" HeaderText="Insurance Valid Upto"></asp:BoundField>
<asp:BoundField DataField="Receipt/Trans No" HeaderText="Receipt/Trans No"></asp:BoundField>                                              
<asp:BoundField DataField="Prepared By" HeaderText="Prepared By"></asp:BoundField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="BondGatePass">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdBondGatePass" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                 
<asp:BoundField DataField="GP No" HeaderText="GP No"></asp:BoundField>
<asp:BoundField DataField="GP Date" HeaderText="GP Date"></asp:BoundField>
<asp:BoundField DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundField>
<asp:BoundField DataField="Stuffed Qty" HeaderText="Stuffed Quantity"></asp:BoundField>
<asp:BoundField DataField="Stuffed Wt" HeaderText="Stuffed Wt"></asp:BoundField>
<asp:BoundField DataField="Generated BY" HeaderText="Generated BY"></asp:BoundField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="Balance">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdBalance" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                
<asp:BoundField DataField="Balance Qty" HeaderText="Balance Quantity"></asp:BoundField>
<asp:BoundField DataField="Balance Value" HeaderText="Balance Value"></asp:BoundField>
<asp:BoundField DataField="Balance Duty" HeaderText="Balance Duty"></asp:BoundField>
<asp:BoundField DataField="Balance Area" HeaderText="Balance Area"></asp:BoundField>
<asp:BoundField DataField="Balance Weight" HeaderText="Balance Weight"></asp:BoundField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="ContainerDetails">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdContainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                 
<asp:BoundField DataField="Container No" ItemStyle-Width="100px" HeaderText="Container No"></asp:BoundField>
<asp:BoundField DataField="Size" HeaderText="Size"></asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
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

<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Bond No</b>
<asp:TextBox ID="txtbondNo"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Bond No">                                           
</asp:TextBox> 
</div>
</div>

<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
<b>Bond Date</b>                                         
<asp:TextBox ID="txtBondDate"  placeholder="yyyy-mm-dd "   runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
                                    

<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
<b>Bond Expiry</b>                                         
<asp:TextBox ID="txtbondExpiry"  placeholder="yyyy-mm-dd "   runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Status</b>
<asp:TextBox ID="txtstatus"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Status">                                           
</asp:TextBox> 
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Bond Reg No</b>
<asp:TextBox ID="txtBondReg"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Bond Reg No">                                           
</asp:TextBox> 
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Location</b>
<asp:TextBox ID="txtLocation"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Location">                                           
</asp:TextBox> 
</div>
</div>
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Serial No</b>
<asp:TextBox ID="txtSerial"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Serial no">                                           
</asp:TextBox> 
</div>
</div>
     <div class="col-md-2 col-xs- 12">
                                <div class="form-group pull-Left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export TO Excel" ></asp:Button>
                                </div>
                            </div>
</div>
</div>
</div>
                              
</div>
                     
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
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
                   
<a href="#" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 
              
</div>
       
         
</div>
   
<script type="text/javascript">
var popup;
//alert('hi')
function OpenItembond() {
//alert('hi')
var url = "BondNocSearch.aspx"
//window.open(url);
popup = window.open(url, "Popup", "width=800,height=550");
popup.focus();
}
</script>
</asp:Content>
