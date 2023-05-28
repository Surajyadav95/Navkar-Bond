<%@ Page Title="Bond |Bond View Assess Details" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="BondViewAssessDetails.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond |Bond View Assess Details</title>
       
</head>
    <style>
        .header-center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Bond View Assess Details
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
Bond View Assess Details
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Assessment No</b>
<asp:TextBox ID="txtassessmentno"   Style="text-transform: uppercase;"  runat="server"  class="form-control text-label" placeholder="Assessment No">
                                            
</asp:TextBox> 
</div>
</div>

    <div class="col-sm-1 col-xs-12">                                   
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary btn-sm outline '  runat="server"
    OnClientClick="return OpenItemsearch();">                               
<i class=" fa fa-search" aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
    <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click"/>

    
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
 OnClick="btnsearch_Click"  />

                 
</div>              
</div>

    <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Assessment Date</b>
<asp:TextBox ID="txtassessmentdate" placeholder="dd-MM-yyyy" runat="server" ReadOnly="true" class="form-control text-label"
></asp:TextBox>
</div>
</div>


        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Valid Upto Date</b>
<asp:TextBox ID="txtvalidupto" placeholder="dd-MM-yyyy" runat="server" ReadOnly="true" class="form-control text-label"
></asp:TextBox>
</div>
</div>

    <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >Assess Type</b>
<asp:TextBox ID="txtAssessType"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Assess Type">                                        
</asp:TextBox> 
</div>
</div>


</div>
 
 <div class="row">
    <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >Delivery Type</b>
<asp:TextBox ID="txtdeliverytpe"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Delivery Type">                                        
</asp:TextBox> 
</div>
</div>

         <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >IGM No</b>
<asp:TextBox ID="txtigmno"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="IGM No">                                        
</asp:TextBox> 
</div>
</div>

  <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >Item No</b>
<asp:TextBox ID="txtitemno"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Item No">                                        
</asp:TextBox> 
</div>
</div>

       <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >Tariff ID</b>
<asp:TextBox ID="txttariffid"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Tariff ID">                                        
</asp:TextBox> 
</div>
</div>

            <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Tariff Description</b>
<asp:TextBox ID="txtDescription"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Description">                                        
</asp:TextBox> 
</div>
</div>

 </div>

    <div class="row">
  

         <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >CHA Name</b>
<asp:TextBox ID="txtchaname"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="CHA Name">                                        
</asp:TextBox> 
</div>
</div>

         <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Importer Name</b>
<asp:TextBox ID="txtImporter"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Importer">                                        
</asp:TextBox> 
</div>
</div>

    </div>
 
 
    <div class="row">
    

        <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Value</b>
<asp:TextBox ID="txtvalue"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Value">                                        
</asp:TextBox> 
</div>
</div>

           <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Duty</b>
<asp:TextBox ID="txtduty"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Duty">                                        
</asp:TextBox> 
</div>
</div>

                                  <div class="col-md-6 col-xs-12  " >
<div class="form-group text-label">
<b  >Remarks</b>
<asp:TextBox ID="txtremarks"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Remarks">                                        
</asp:TextBox> 
</div>
</div>  


    </div>
 

</asp:Panel>
    <%--grid--%> 
 <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="panel panel-default" style=" padding: 10px; margin: 10px;">
<div id="Tabs" role="tabpanel">
<!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist" >
 
<li class="active"><a href="#ListOfContainer" aria-controls="BOND EX" role="tab" data-toggle="tab">List Of Container And Its Charges</a></li>

<li><a href="#OtherCharges" aria-controls="Invoice" role="tab" data-toggle="tab"> Other Charges </a></li>

<li><a href="#ModeOfPayment" aria-controls="Bond Gate Pass" role="tab" data-toggle="tab">Mode Of Payment</a></li>

<li><a href="#CreditNote" aria-controls="Balance" role="tab" data-toggle="tab">Credit Note</a></li>

<li><a href="#DiscountAmount" aria-controls="Container Details" role="tab" data-toggle="tab">Discount Amount</a></li>

</ul>
<!-- Tab panes -->
<div class="tab-content" style="padding-top: 20px">
 
<div role="tabpanel" class="tab-pane active" id="ListOfContainer">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-sm-8 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdListOfContainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>
                                                  
<asp:BoundField DataField="AccountName" HeaderStyle-CssClass="header-center" HeaderText="Account Name"></asp:BoundField>
<asp:BoundField DataField="NOCNo" HeaderStyle-CssClass="header-center" HeaderText="NOC No"></asp:BoundField>
<asp:BoundField DataField="NetTotal" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right" HeaderText="Total"></asp:BoundField>
 
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="OtherCharges">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-sm-6 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdOtherCharges" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                
<asp:BoundField DataField="Account Name" HeaderStyle-CssClass="header-center" HeaderText="Account Name"></asp:BoundField>
<asp:BoundField DataField="Net Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right" HeaderText="Net Amount"></asp:BoundField>
 
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="ModeOfPayment">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-sm-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdModeOfPayment" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                 
 
<asp:BoundField DataField="Mode No" HeaderStyle-CssClass="header-center" HeaderText="Mode No"></asp:BoundField>
<asp:BoundField DataField="Bank Name" HeaderStyle-CssClass="header-center" HeaderText="Bank Name"></asp:BoundField>
<asp:BoundField DataField="Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right" HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Added By" HeaderStyle-CssClass="header-center" HeaderText="Added By"></asp:BoundField>
<asp:BoundField DataField="Added On" HeaderStyle-CssClass="header-center" HeaderText="Added On"></asp:BoundField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="CreditNote">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-sm-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdCreditNote" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                
<asp:BoundField DataField="CreditNoteNo" HeaderStyle-CssClass="header-center" HeaderText="Credit Note No"></asp:BoundField>
<asp:BoundField DataField="Credit Note Date" HeaderStyle-CssClass="header-center" HeaderText="Credit Note Date"></asp:BoundField>
<asp:BoundField DataField="CreditAmt" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right" HeaderText="Credit Amt"></asp:BoundField>
<asp:BoundField DataField="UserName" HeaderStyle-CssClass="header-center" HeaderText="User Name"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderStyle-CssClass="header-center" HeaderText="Remarks"></asp:BoundField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="DiscountAmount">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-sm-10 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdDiscountAmount" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                 
<asp:BoundField DataField="AssessNo" HeaderStyle-CssClass="header-center" HeaderText="Assess No"></asp:BoundField>
<asp:BoundField DataField="WorkYear" HeaderStyle-CssClass="header-center" HeaderText="Work Year"></asp:BoundField>
<asp:BoundField DataField="AccountName" HeaderStyle-CssClass="header-center" HeaderText="Account Name"></asp:BoundField>
<asp:BoundField DataField="DiscAmount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right" HeaderText="Discount Amount"></asp:BoundField>
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
            <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Assessment Generated By</b>
<asp:TextBox ID="txtAssessmentGenerated"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Assessment Generated By">                                        
</asp:TextBox> 
</div>
</div>

        <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Assessment Generated On</b>
<asp:TextBox ID="txtAssessmentGeneratedOn"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Assessment Generated On">                                        
</asp:TextBox> 
</div>
</div>

            <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >No of 20</b>
<asp:TextBox ID="txt20"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="No of 20">                                        
</asp:TextBox> 
</div>
</div>

               <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >No of 40</b>
<asp:TextBox ID="txt40"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="No of 40">                                        
</asp:TextBox> 
</div>
</div>

              <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >No of 45</b>
<asp:TextBox ID="txt45"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="No of 45">                                        
</asp:TextBox> 
</div>
</div>

</div>


    <div class="row">
          <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Receipt Generated By</b>
<asp:TextBox ID="txtReceiptGeneratby"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Receipt Generated By">                                        
</asp:TextBox> 
</div>
</div>

          <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Receipt Generated On</b>
<asp:TextBox ID="txtReceiptGeneratOn"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Receipt Generated On">                                        
</asp:TextBox> 
</div>
</div>

         <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >GST IN Number</b>
<asp:TextBox ID="txtGstinnumber"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="GST IN Number">                                        
</asp:TextBox> 
</div>
</div>

        <div class="col-md-3   col-xs-12">
<div class="form-group text-label">
<b  >Party Name</b>
<asp:TextBox ID="txtpartyName"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Party Name">                                        
</asp:TextBox> 
</div>
</div>

 </div>

    <div class="row">

             <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >Net Total</b>
<asp:TextBox ID="txtNettotal"   Style="text-transform: uppercase;text-align:right" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Net Total">                                        
</asp:TextBox> 
</div>
</div>

          <div class="col-md-2   col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Service Tax</b>
<asp:TextBox ID="txtservicetax"   Style="text-transform: uppercase;text-align:right" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Service Tax">                                        
</asp:TextBox> 
</div>
</div>

         <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >SGST</b>
<asp:TextBox ID="txtsgst"   Style="text-transform: uppercase;text-align:right" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="SGST">                                        
</asp:TextBox> 
</div>
</div>

         <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >CGST</b>
<asp:TextBox ID="txtcgst"   Style="text-transform: uppercase;text-align:right" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="CGST">                                        
</asp:TextBox> 
</div>
</div>

         <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >IGST</b>
<asp:TextBox ID="txtigst"   Style="text-transform: uppercase;text-align:right" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="IGST">                                        
</asp:TextBox> 
</div>
</div>

    

       

         <div class="col-md-2   col-xs-12">
<div class="form-group text-label">
<b  >Grand Total</b>
<asp:TextBox ID="txtGrandtotal"   Style="text-transform: uppercase;text-align:right" ReadOnly="true"  runat="server"  class="form-control text-label"  placeholder="Grand Total">                                        
</asp:TextBox> 
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
 
function OpenItemsearch() {
    
    var blResult = Boolean;
    blResult = true;

    var url = "Search.aspx"
 
//window.open(url);
popup = window.open(url, "Popup", "width=800,height=550");
popup.focus();
}
</script>
</asp:Content>
