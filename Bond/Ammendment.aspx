<%@ Page Title="Bond | Admin Amendments" Language="VB" MasterPageFile="~/Bond/User.Master" AutoEventWireup="false" CodeFile="Ammendment.aspx.vb" Inherits="RA_asd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!--Page Title-->
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<form id="form1" >
<div class="pageheader">
<h3>
<i class="fa fa-home"></i>Admin Amendments
</h3>
<div class="breadcrumb-wrapper">
 
<ol class="breadcrumb">
 
</ol>
</div>
</div>
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<!--End page title-->
<!--Page content-->
<!--===================================================-->
<div id="page-content">
<!--Widget-4 -->
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="row">

<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelNOC" style="width:280px;vertical-align:middle;">            
<div class="col-md-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(231, 84, 90);margin-right:32px;padding:initial;border-color:rgb(231, 84, 90);">
                                 
<div class="col-md-9 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
<a href="CancelNoc.aspx" style="color:white;" onclick='return pop("CancelNoc.aspx",900,550,70,300);'> Cancel Noc
</a>
                               
<br /><br />
</div>             
<br />           
</div>
           
</div> 
    
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelBondIN" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(254, 106, 0);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 
<div class="col-md-9 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="CancelBondIn.aspx"  style="color:white" onclick='return pop("CancelBondIn.aspx",900,550,70,300);'> Cancel Bond In
</a>                               
<br /><br />
</div>
            
<br />
            
</div>
           
</div> 
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelBondEx" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(127, 12, 217);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 

<div class="col-md-9 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="CancelBondEx.aspx"  style="color:white" onclick='return pop("CancelBondEx.aspx",900,550,70,300);'>  Cancel Bond Ex
</a>                               
<br /><br />
</div>
      
<br />
            
</div>
           
</div> 
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelGatePass" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(0, 148, 254);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 
<div class="col-md-10 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
    <a href="CancelGatePass.aspx"  style="color:white" onclick='return pop("CancelGatePass.aspx",900,550,70,300);'> Cancel Bond Gate Pass
</a>  
<br />

<br /><br />
</div>
     
<br />
            
</div>
           
</div> 
<%--</div>
<div class="row">--%>

<div class="col-md-4 col-xs-6 text-label" runat="server" id="divModifyNOC"  style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(0, 189, 242);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
<div class="col-md-9 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >

         <a href="ModifyNoc.aspx"  style="color:white" onclick='return pop("ModifyNoc.aspx",900,550,70,300);'> Modify Noc
</a>                         
<br /><br />
</div>
      
<br />
            
</div>
           
</div>

<div class="col-md-5 col-xs-6 text-label" runat="server" id="divModifyBondIn" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(88, 170, 71);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
<div class="col-md-9 col-xs-8 pull-right" style="margin-top:-12px;text-align:left;color:white;font-size:large" >

<br />
 <a href="ModifyBondIn.aspx"  style="color:white" onclick='return pop("ModifyBondIn.aspx",900,550,70,300);'>Modify Bond-In
</a>     

<br /><br />
</div>
                
<br />
            
</div>
           
</div>

        

<div class="col-md-5 col-xs-6 text-label" runat="server" id="divModifyBondEx" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(192, 37, 84);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
                              
<div class="col-md-9 col-xs-8 pull-right" style="margin-top:-12px;text-align:left;color:white;font-size:large" >

<br />
 <a href="ModifyBondEx.aspx"  style="color:white" onclick='return pop("ModifyBondEx.aspx",900,550,70,300);'>Modify Bond-Ex 
</a>
                  
<br /><br />
</div>
       
<br />
            
</div>
           
</div>

<div class="col-md-5 col-xs-6 text-label" runat="server" id="divNOCUpdate" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(254, 106, 0);padding:initial;border-color:rgb(241, 240, 248);">
                              
<div class="col-md-12 col-xs-12 pull-right" style="margin-top:-12px;text-align:center;color:white;font-size:medium" >

<br />
<a href="InventoryNOCDetsUpdate.aspx"  style="color:white" onclick='return pop("InventoryNOCDetsUpdate.aspx",900,550,70,300);'>NOC Details Update (Inventory) 
</a>
                  
<br /><br />
</div>
       
<br />
            
</div>
           
</div>


</div>
        
</div>
<!--===================================================-->
<!--End page content-->
<script src="../js/jQuery.min.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function () {
$('[data-toggle="tooltip"]').tooltip();
});

       
</script>
</form>
</asp:Content>

