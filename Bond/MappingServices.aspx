﻿<%@ Page Title="Bond |Mapping Services" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="MappingServices.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond |Mapping Services</title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Mapping Services
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
<div class="col-md-3 pull-md-left sidebar" style="padding-top:12px;">
 
                           
</div>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Mapping Services
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 <div class="row">

         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Entry ID </b>
<asp:TextBox ID="txtentry" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
       
 
    <div class="col-md-3 col-xs-12">                                      
<div class="form-group text-label">
<b>Entry Date</b>                                         
<asp:TextBox ID="txtentryDate"  placeholder="yyyy-mm-dd " TextMode="Date"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>
</div>
    <div class="row">
             <div class="col-md-3 col-xs-12">
               <div class="form-group text-label">
<b>Account Head</b>
<asp:DropDownList ID="ddlAccount"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
               </div>
       
          <div class="col-md-3 col-xs-12">
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
        </div>

    <div class="row">
<div class="col-sm-4 col-xs-4 pull-left">
<div class="form-group text-label" style="padding-top:10px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline"> Is Active?</asp:Label>
</div>
</div>
</div>

</asp:Panel>
 
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="MappingServices.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
 </div>
     <div class="row">
        <div class="col-sm-4 col-xs-12 ">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearchm" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

        <div class="col-sm-1 col-xs-2 pull-left">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btnsearch_Click"   
Text="Search"  />
</div>
                                              
                                      
</div>
                         
</div>

    <div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:5px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"    AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

 

<asp:BoundField DataField="EntryID" HeaderText="Entry ID"></asp:BoundField>
 <asp:BoundField DataField="Entry Date" HeaderText="Entry Date"></asp:BoundField>                                                 
<asp:BoundField DataField="AccountName" HeaderText="Account Name"></asp:BoundField>
<asp:BoundField DataField="Service_Name" HeaderText="Service Name"></asp:BoundField>
    <asp:BoundField DataField="Material_Name" HeaderText="Material Name"></asp:BoundField>
    <asp:BoundField DataField="Added By" HeaderText="Added By"></asp:BoundField>
     


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



<asp:Label ID="lblcode" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
                   
<a href="MappingServices.aspx" class="btn btn-info btn-block">OK</a>
                                
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
            
     
    var ddlAccount = document.getElementById('<%= ddlAccount.ClientID%>').value;
     
    var ddlservice = document.getElementById('<%= ddlservice.ClientID%>').value;
    var ddlmaterial = document.getElementById('<%= ddlmaterial.ClientID%>').value;
  
                  


var blResult = Boolean;
blResult = true;
              
 

    if (ddlAccount == 0) {
document.getElementById('<%= ddlAccount.ClientID%>').style.borderColor = "red";
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

    
                
              
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
 
</asp:Content>
