<%@ Page Title="Bond |Fuel Inward Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="FuelMaster.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond |Fuel Inward Entry</title>
       
</head>
       <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:250px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Fuel Inward Entry
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
Fuel Inward Entry
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 <div class="row">

         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Fuel Reg No</b>
<asp:TextBox ID="txtFuelRegNo" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
       
 
    <div class="col-md-3 col-xs-12">                                      
<div class="form-group text-label">
<b>Fuel Reg Date</b>                                         
<asp:TextBox ID="txtfueldate"  placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>
</div>
    <div class="row">
             <div class="col-md-3 col-xs-12">
               <div class="form-group text-label">
<b>Fuel Type</b>
<asp:DropDownList ID="ddlFuelType"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
               </div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Vehicle No</b>
<asp:TextBox ID="txtVehicleNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Vehicle No"
runat="server"   ></asp:TextBox>
</div>
</div>
       
  
          
       
                <div class="col-md-3 col-xs-12">
         <div class="form-group text-label">
<b>Cost Center</b>
<asp:DropDownList ID="ddlCostCenter"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
                  </div>
    <div class="row">
           <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Fuel Qty</b>
<asp:TextBox ID="txtFuelQty" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Fuel Qty"
runat="server"   ></asp:TextBox>
</div>
</div>

           <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Rate</b>
<asp:TextBox ID="txtRate" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Rate"
runat="server"   ></asp:TextBox>
</div>
</div>

          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Vendor Name</b>
<asp:DropDownList ID="ddlVendorName"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
         </div>
   
 
      </div>


 
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="FuelMaster.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
 </div>
    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>
     <div class="row">
        <div class="col-sm-4 col-xs-12 ">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearchm" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

        <div class="col-sm-1 col-xs-2 pull-left">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btnsearch_Click"   
Text="Search"  />
</div>
                                              
                                      
</div>
                         
</div>

    <div class="row">


<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!"  ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

        <asp:TemplateField>
        <ItemTemplate>
                                                               
                                                            
        <asp:LinkButton ID="lnkCancel"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Cancel"                                                         
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' runat="server" OnClick="lnkCancel_Click"
        ></asp:LinkButton>

   
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" Width="100px" />
        </asp:TemplateField>

<asp:BoundField DataField="ID" HeaderText="Entry ID"></asp:BoundField>
 <asp:BoundField DataField="Date" HeaderText="Fuel Date"></asp:BoundField>                                                 
<asp:BoundField DataField="Fuel Type" HeaderText="Fuel Type"></asp:BoundField>
<asp:BoundField DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundField>
    <asp:BoundField DataField="Qty" HeaderText="Qty"></asp:BoundField>
    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
    <asp:BoundField DataField="Cost Center" HeaderText="Cost Center"></asp:BoundField>
    <asp:BoundField DataField="Vendor Name" HeaderText="Vendor Name"></asp:BoundField>
    <asp:BoundField DataField="Added By" HeaderText="Added By"></asp:BoundField>
     


</Columns>

</asp:GridView>
</div>
</div>
</div>

</div>
    </asp:Panel>                        
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
                   
<a href="FuelMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
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
            
     
    var ddlFuelType = document.getElementById('<%= ddlFuelType.ClientID%>').value;
     
    var ddlCostCenter = document.getElementById('<%= ddlCostCenter.ClientID%>').value;
    var ddlVendorName = document.getElementById('<%= ddlVendorName.ClientID%>').value;
  
                  


var blResult = Boolean;
blResult = true;
              
 

if (ddlCostCenter == 0) {
document.getElementById('<%= ddlFuelType.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
    }

    

    if (ddlservice == 0) {
        document.getElementById('<%= ddlCostCenter.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

    if (ddlVendorName == 0) {
        document.getElementById('<%= ddlVendorName.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

    
                
              
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
  <script>
      function OpenCancelInvoice() {
          var txtFuelRegNo = document.getElementById('<%= txtFuelRegNo.ClientID%>').value;
             <%--var TxtWorkYear = document.getElementById('<%= TxtWorkYear.ClientID%>').value;--%>

          var url = "FuelCancel.aspx?FuelRegID=" + txtFuelRegNo
             popup = window.open(url, "Popup", "top=100,left=400,width=700,height=215");
             popup.focus();
         }
    </script> 
</asp:Content>
