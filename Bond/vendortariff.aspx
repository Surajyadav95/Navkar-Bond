<%@ Page Title="Bond |Tariff Settings of Vendor" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="vendortariff.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Tariff Settings of Vendor</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Tariff Settings of Vendor
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
Tariff Settings of Vendor
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">
 <div class="col-md-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Entry ID </b>
<asp:TextBox ID="txtentry" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>

    <div class="col-md-2 col-xs-12">                                      
<div class="form-group text-label">
<b>Entry Date</b>                                         
<asp:TextBox ID="txtentryDate"  placeholder="yyyy-mm-dd " TextMode="Date"  runat="server"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
       </div> 
    <div class="row">
    <div class="col-md-2  col-xs-12">                                      
<div class="form-group date text-label">
<b>Effective From</b>                                           
<asp:TextBox ID="txtfrom"    placeholder="dd-MM-yyyy"   TextMode="Date"    runat="server"   Class="   form-control text-label"></asp:TextBox>

</div>
</div>
 

<div class="col-md-2  col-xs-12" ">                                      
<div class="form-group date text-label">
<b >Effective To</b>
<asp:TextBox ID="txtUpTo"  placeholder="dd-MM-yyyy"   TextMode="Date"    runat="server"   Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>     
    </div>

    <div class="row">
    <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Tariff ID</b>
<asp:TextBox ID="txttariffId" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="Tariff ID"
runat="server"   ></asp:TextBox>
</div>
</div>     

  

           <div class="col-md-3 col-xs-12">
               <div class="form-group text-label">
<b>Vendor Invoice Type</b>
<asp:DropDownList ID="ddlvendorInvoice"   Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
</asp:DropDownList>
</div>
</div>
           <div class="col-md-3 col-xs-12">
               <div class="form-group text-label">
<b>Account Head</b>
<asp:DropDownList ID="ddlAccount"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
               </div>

 


    
  
</div>
    <div class="row">
                         <div class="col-md-2 col-xs-12" style="display:none">
                             <div class="form-group text-label">
<b>Service Type</b>
<asp:DropDownList ID="ddlservice"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
                   <div class="col-md-3 col-xs-12">
                             <div class="form-group text-label">

<b>Material Type</b>
<asp:DropDownList ID="ddlmaterial"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

                   <div class="col-md-3 col-xs-12">
                       <div class="form-group text-label">
<b>Equipment Type</b>
<asp:DropDownList ID="ddlequipment"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
    </div>

    <div class="row">

    
                   <div class="col-md-3 col-xs-12">
                        <div class="form-group text-label">
<b>Stuffing Type</b>
<asp:DropDownList ID="ddlstuffing"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
  </div>
  

 
       <div class="col-md-3 col-xs-12">
            <div class="form-group text-label">
<b>Based On</b>
<asp:DropDownList ID="ddlbased"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
           </div>

  </div>
           <div class="row">
             <div class="col-md-3 col-xs-12">
<b>Size</b>
<asp:DropDownList ID="ddlSize"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
    <asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="1">20</asp:ListItem>
<asp:ListItem Value="2">40</asp:ListItem>
<asp:ListItem Value="3">45</asp:ListItem>
</asp:DropDownList>
</div>

          <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Vendor Rate</b>
<asp:TextBox ID="txtvendorrate" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="Vendor Rate"
runat="server"   ></asp:TextBox>
</div>
</div> 

              <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Our Rate</b>
<asp:TextBox ID="txtourrate" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="Our Rate"
runat="server"   ></asp:TextBox>
</div>
</div> 

    </div>
 

</asp:Panel>
 <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top: 15px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()" />
</div>


</div>

<div class="col-sm-1" style="padding-left: 8px;">
<div class="form-group" style="padding-top: 15px">

<a href="#" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">Clear
</a>

</div>


</div>

     <div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="VendorSummary.aspx" target="_blank"><b style="color:blue">Click here to view Vendor Summary</b> </a>
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
                   
<a href="vendortariff.aspx" class="btn btn-info btn-block">OK</a>
                                
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
            
    var txttariffId = document.getElementById('<%= txttariffId.ClientID%>').value;
    var ddlAccount = document.getElementById('<%= ddlAccount.ClientID%>').value;
    var ddlvendorInvoice = document.getElementById('<%= ddlvendorInvoice.ClientID%>').value;
    var ddlservice = document.getElementById('<%= ddlservice.ClientID%>').value;
    var ddlmaterial = document.getElementById('<%= ddlmaterial.ClientID%>').value;
    var ddlequipment = document.getElementById('<%= ddlequipment.ClientID%>').value;
    var ddlstuffing = document.getElementById('<%= ddlstuffing.ClientID%>').value;
                  


var blResult = Boolean;
blResult = true;
              
if (txttariffId == "") {
document.getElementById('<%= txttariffId.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

    if (ddlAccount == 0) {
document.getElementById('<%= ddlAccount.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
    }

    if (ddlvendorInvoice == 0) {
        document.getElementById('<%= ddlvendorInvoice.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

    if (ddlservice == 0) {
        document.getElementById('<%= ddlservice.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

    if (ddlmaterial == 0) {
        document.getElementById('<%= ddlmaterial.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

    if (ddlstuffing == 0) {
        document.getElementById('<%= ddlstuffing.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

                
              
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
     
</asp:Content>
