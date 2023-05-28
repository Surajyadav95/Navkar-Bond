<%@ Page Title="Bond | Search Receipt" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="SearchReceipt.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Search Receipt</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Search Receipt
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
Search Receipt
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">

<div class="col-md-3 col-xs-12"  >
<div class="form-group text-label">
<b  >Search By</b>
<asp:DropDownList ID="ddlSearch"   Style="text-transform: uppercase;" runat="server"  class="form-control text-label">     
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="Cheque/DD/Po No">Cheque/DD/Po No</asp:ListItem>
<asp:ListItem Value="Remarks">Remarks</asp:ListItem>    
<asp:ListItem Value="Amount">Amount</asp:ListItem>                                      
</asp:DropDownList> 
</div>
</div>

<div class="col-md-3 col-xs-12" style="padding-top:18px;">
<div class="form-group text-label">
<b  ></b>
<asp:TextBox ID="txtsearch"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label" placeholder="">
                                            
</asp:TextBox> 
</div>
</div>
<div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
    OnClientClick="return OpenItemsearch();"> 
                                 
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>

<asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
                  
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Receipt No</b>
<asp:DropDownList ID="ddlReceiptNo"   Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
 
</asp:DropDownList> 
</div>
</div>

<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Work Year</b>
<asp:TextBox ID="txtWorkYear"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                                        
</asp:TextBox> 
</div>
</div>

<div class="col-md-1 col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show"
    OnClientClick="return ValidationSave()"
     OnClick="btnsearch_Click"/>

                 
</div>              
</div>
                
<div class="col-md-3 col-xs-12" style="display:none;">
<div class="form-group text-label">
<b  >Bond Type</b>
<asp:TextBox ID="txtbondtype" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Bond Type"
     runat="server" ReadOnly="true"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Receipt Date</b>
<asp:TextBox ID="txtReceiptdate" placeholder="dd-MM-yyyy " runat="server" ReadOnly="true" class="form-control text-label"
></asp:TextBox>
</div>
</div>
 
                                  
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Receipt Amount</b>
<asp:TextBox ID="txtReceiptAmount"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Receipt Amount">                                           
</asp:TextBox> 
</div>
</div>

</div>
<div class="row">
<div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Assessment No</b>
<asp:TextBox ID="txtAssessmentNo"   Style="text-transform: uppercase;" runat="server"  ReadOnly="true" class="form-control text-label"  placeholder="Assessment No">                                        
</asp:TextBox> 
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Assessment Date</b>
<asp:TextBox ID="txtAssessmentDate" placeholder="dd-MM-yyyy" runat="server" ReadOnly="true" class="form-control text-label"
></asp:TextBox>
</div>
</div>
</div>
<div class="row">
<div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Bond Number</b>
<asp:TextBox ID="txtbondnumber"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Bond Number">                                        
</asp:TextBox> 
</div>
</div>

<div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Assessment Type</b>
<asp:TextBox ID="txtAssessmentType"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Assessment Type">                                        
</asp:TextBox> 
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
</div>
<div class="row">                             
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> CHA Name</b>
<asp:textbox ID="txtCha" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="CHA Name" >
</asp:textbox>   
</div>
</div>
</div>

<div class="row">
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>Remark</b>
<asp:textbox ID="txtRemarks" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Remark" >
</asp:textbox>   
</div>
</div>
</div>

</asp:Panel>
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:12px;margin-right:0px;">
<asp:GridView ID="grdgodown" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>
<asp:BoundField DataField="Mode No" HeaderText="Mode No"></asp:BoundField>
<%--<asp:BoundField DataField="Mode Name" HeaderText="Mode Name"></asp:BoundField>--%>
<asp:BoundField DataField="Bank Name" HeaderText="Bank Name"></asp:BoundField>
<asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
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
        function ValidationSave() {

            var ddlReceiptNo = document.getElementById('<%= ddlReceiptNo.ClientID%>').value;
  



    var blResult = Boolean;
    blResult = true;

    

    if (ddlReceiptNo == 0) {
        document.getElementById('<%= ddlReceiptNo.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;
}



    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>
   
<script type="text/javascript">
var popup;
 
function OpenItemsearch() {
    var ddlSearch = document.getElementById('<%= ddlSearch.ClientID%>').value;
    var blResult = Boolean;
    blResult = true;

    var url = "Receiptsearch.aspx?Category=" + ddlSearch
 
//window.open(url);
popup = window.open(url, "Popup", "width=800,height=550");
popup.focus();
}
</script>
</asp:Content>
