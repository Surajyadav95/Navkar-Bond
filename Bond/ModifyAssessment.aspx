﻿<%@ Page Title="Bond | Modify Assessment" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ModifyAssessment.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Modify Assessment</title>
       
</head>
    <style>
        .header-center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Modify Assessment
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Modify Assessment 
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
    <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Work Year</b>
<asp:TextBox ID="txtworkyear" MaxLength="7" onkeypress="return ValidateYear()" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Work Year"></asp:TextBox>     
</div>
</div>
<div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice No</b>
<asp:TextBox ID="txtinvno" AutoPostBack="true" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Enter Invoice No."></asp:TextBox>     
</div>
</div>
    
    <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice Date</b>
<asp:TextBox ID="txtinvdate" ReadOnly="true" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Valid Upto Date</b>
<asp:TextBox ID="txtvaliddate" ReadOnly="true" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
</div>
    <div class="row">
<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >GST In Number</b>
<asp:TextBox  ID="txtgstin" placeholder="GST Namber" ReadOnly="true"  MaxLength="15" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
    
<asp:Label runat="server" ID="lblpartyid" Visible="false"></asp:Label>
</div>
</div>
<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
    <b>GST Party Name</b>
<asp:TextBox  ID="txtgstname" placeholder="GST Name" ReadOnly="true" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
<asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
</div>
    <div class="row">
        <div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b  >Address</b>
<asp:TextBox ID="txtaddress" ReadOnly="true" TextMode="MultiLine" Rows="2" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Address"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>
  <div class="row">
           <div class="col-md-3 col-xs-12" style="padding-top:25px">
<div class="form-group text-label">
<b  >Commodity</b>
 
    <asp:DropDownList ID="ddlCommodity"  Style="text-transform: uppercase;" OnTextChanged="ddlCommodity_TextChanged" AutoPostBack="true" runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>

       <div class="col-md-2 col-xs-12" style="padding-top:25px">
<div class="form-group text-label">
<b  >Tax</b>
 
    <asp:DropDownList ID="ddltxtTax"  Style="text-transform: uppercase;"  runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>
  </div>
<div class="row">
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Account Heads</b>
<asp:DropDownList ID="ddlaccntheads"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                      
</asp:DropDownList> 
</div>
</div>
  <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
    <b  >Amount</b>
<asp:TextBox  ID="txtamount" onkeypress="return ValidateAmount()" Style="text-transform: uppercase;" placeholder="Amount" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
       <asp:Label runat="server" ID="lblTaxID" Visible="false"></asp:Label>
</div>
</div>
    <div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnkadd" ControlStyle-CssClass='btn btn-info'  runat="server"
OnClientClick="return ValidationAdd();">  
<i class=" fa fa-check"  aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
</div>

 
     <div class="row">
         <div class="col-lg-8 text-label">
             <div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;">
                 <asp:GridView ID="grdcharges" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
                     <Columns>
                         <asp:TemplateField>
    <ItemTemplate>
            <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger' OnClick="lnkCancel_Click"                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoID")%>' runat="server" 
                                                            ><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" Width="40px"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblaccntid" runat="server" Visible="false" text='<%#Eval("AccountID")%>'></asp:Label>
        <asp:Label ID="lblaccntname" runat="server" text='<%#Eval("accountname")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
                                                
    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
        <asp:TextBox ID="txtNetAmount" runat="server" AutoPostBack="true" OnTextChanged="txtNetAmount_TextChanged" Text='<%#Eval("amount")%>' CssClass="form-control"></asp:TextBox>
        <asp:Label ID="lblntamnt" Visible="false" runat="server" text='<%#Eval("amount")%>'></asp:Label>
        <asp:Label ID="lblAutoID" Visible="false" runat="server" text='<%#Eval("AutoID")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </div>
         </div>
         </div>
    <div class="row">
        <div class="col-lg-8 text-label" style="padding-right:0px">
        <div id="divtblWOTOtal" runat="server" style="display:none;">                                         
<table forecolor="Black" class="table table-striped table-bordered table-hover" style="border-top:5px solid #7bc144;margin-left:-5px;margin-right:-5px">
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b ">Net Total</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblPercentage" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Discount</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="Label1" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lbldisc" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >CGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblCgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblCGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >SGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblSgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblSGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >IGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblIgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblIGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Grand Total</b></td>
<%--<td style ="width:20%;text-align:right"></td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblAllTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
</table>
</div>
    </div>
        </div>
       <div class="row">
       <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Remarks</b>
<asp:TextBox ID="txtremarks" TextMode="MultiLine" Rows="2" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>
 </div>
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="ModifyAssessment.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;display:none">
<div class="form-group">
<a href="AccountSummary.aspx" target="_blank"><b style="color:blue">Click here to view Account summary</b> </a>
</div>
</div>
      
<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>                   
</div>                      
</asp:Panel>
                        
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
                   
<a href="ModifyAssessment.aspx" class="btn btn-info btn-block">OK</a>
                                
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
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
       
         
</div>
   <script type="text/javascript">
       function ValidationAdd() {
           var txtinvno = document.getElementById('<%= txtinvno.ClientID%>').value;
           var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;
           var ddlaccntheads = document.getElementById('<%= ddlaccntheads.ClientID%>').value;
           var txtamount = document.getElementById('<%= txtamount.ClientID%>').value;

           //document.getElementById('<%= lnkadd.ClientID%>').value = "Please Wait...";
           document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info disabled");
           var blResult = Boolean;
           blResult = true;

           if (txtinvno == "") {
               document.getElementById('<%= txtinvno.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
           if (txtworkyear == "") {
               document.getElementById('<%= txtworkyear.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
           if (ddlaccntheads == 0) {
               document.getElementById('<%= ddlaccntheads.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
           if (txtamount == 0) {
               document.getElementById('<%= txtamount.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }

    if (blResult == false) {
        alert('Please fill the required fields!');
        //document.getElementById('<%= btnsave.ClientID%>').value = "Save";
        document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info");
}
    return blResult;
}
</script>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtinvno = document.getElementById('<%= txtinvno.ClientID%>').value;
    var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;
    document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary disabled");  
               

var blResult = Boolean;
blResult = true;
 

                   
if (txtinvno == "") {
document.getElementById('<%= txtinvno.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
    if (txtworkyear == "") {
        document.getElementById('<%= txtworkyear.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
//alert('hi')
if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= btnSave.ClientID%>').value = "Save";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary");  
}
return blResult;
}
</script>

<script type="text/javascript">
function ValidateYear() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 45)
return event.returnValue;
return event.returnValue = '';
}
function ValidateNumber() {
    //alert('hii')
    if ((event.keyCode > 47 && event.keyCode < 58))
        return event.returnValue;
    return event.returnValue = '';
}
function ValidateAmount() {
    //alert('hii')
    if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
        return event.returnValue;
    return event.returnValue = '';
}
function checkEmail(str) {
var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

if (reg.test(emailField.value) == false) {
alert('Invalid Email Address');
return false;
}

return true;
}

function CheckTelephone(tel) {

if (tel.length < 7)
alert("Invalid Telephone number.")
}

function CheckMobile(mob) {
if (mob.length < 10)
alert("Invalid Mobile number.");

}
</script>
</asp:Content>
