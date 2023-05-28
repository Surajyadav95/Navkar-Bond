<%@ Page Title="Bond |Fuel Outward Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="FuelConsumption.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond |Fuel Outward Entry</title>
       
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
<i class="glyphicon glyphicon-transfer"></i>Fuel Outward Entry
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

 
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Fuel Outward Entry
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
 
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
<b>Issue Date</b>                                         
<asp:TextBox ID="txtIssuedate"  placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>
</div>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
<ContentTemplate>
             <div class="col-md-3 col-xs-12">
               <div class="form-group text-label">
<b>Fuel Type</b>
<asp:DropDownList ID="ddlFuelType"   Style="text-transform: uppercase;" OnSelectedIndexChanged="ddlFuelType_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
               </div>
    </ContentTemplate>
            </asp:UpdatePanel>
       
                <div class="col-md-3 col-xs-12">
         <div class="form-group text-label">
<b>Cost Center</b>
<asp:DropDownList ID="ddlCostCenter" OnSelectedIndexChanged="ddlCostCenter_SelectedIndexChanged" AutoPostBack="true"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
   
    
           <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Bal Qty</b>
<asp:TextBox ID="txtBalQty" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="Bal Qty"
runat="server"   ></asp:TextBox>
</div>
</div>

         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Issue Qty</b>
<asp:TextBox ID="txtIssueQty" Style="text-transform:uppercase" onkeypress="return ValidateQty();" OnTextChanged="txtIssueQty_TextChanged" AutoPostBack="true" class="form-control text-label"  placeholder="Issue Qty"
runat="server"   ></asp:TextBox>
</div>
</div>
                  </div>
    <div class="row">
           <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Reading From</b>
<asp:TextBox ID="txtReadingFrom" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Reading From"
runat="server"   ></asp:TextBox>
</div>
</div>

           <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Reading To</b>
<asp:TextBox ID="txtReadingTo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Reading To"
runat="server"   ></asp:TextBox>
</div>
</div>

          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Vehicle Type</b>
<asp:DropDownList ID="ddlVehicleType"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

         </div>


    <div class="row">

         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Trailer No</b>
<asp:TextBox ID="txtTrailerNo" Style="text-transform:uppercase" OnTextChanged="txtTrailerNo_TextChanged" AutoPostBack="true" class="form-control text-label"  placeholder="Trailer No"
runat="server"   ></asp:TextBox>
</div>
</div>
        <asp:Panel ID="Panel1" runat="server" Enabled="false"> 
         <div class="col-md-3 col-xs-12" style="padding-top:20px;" >
            <div class="form-group text-label">

<asp:DropDownList ID="ddlTraileId" Enabled="true"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

         <div class="col-md-6 col-xs-12" >
            <div class="form-group text-label">

<b>Driver Name</b>
<asp:DropDownList ID="ddlDriverName"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
            </asp:Panel>

      

    </div>
    <div class="row">
           <div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b>Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Remarks"
runat="server" TextMode="MultiLine" rows="2"></asp:TextBox>
</div>
</div>
        </div>
   
 
 


 
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server"  OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="FuelConsumption.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
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
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

        <asp:TemplateField>
        <ItemTemplate>
                                                               
                                                            
        <asp:LinkButton ID="lnkCancel"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Cancel"  OnClick="lnkCancel_Click"                                                        
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' runat="server" 
        ></asp:LinkButton>

   
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" Width="100px" />
        </asp:TemplateField>

<asp:BoundField DataField="ID" HeaderText="Fuel Ref No"></asp:BoundField>
 <asp:BoundField DataField="Issue Date" HeaderText="Issue Date"></asp:BoundField>                                                 
<asp:BoundField DataField="Cost Center" HeaderText="Cost Center"></asp:BoundField>
<asp:BoundField DataField="Driver Name" HeaderText="Driver Name"></asp:BoundField>
    <asp:BoundField DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundField>
    <asp:BoundField DataField="Fuel Type" HeaderText="Fuel Type"></asp:BoundField>
    <asp:BoundField DataField="Qty" HeaderText="Qty"></asp:BoundField>
    <asp:BoundField DataField="Issue Qty" HeaderText="Issue Qty"></asp:BoundField>
    <asp:BoundField DataField="Last Reading" HeaderText="Reading From"></asp:BoundField>
    <asp:BoundField DataField="Current Reading" HeaderText="Reading To"></asp:BoundField>
    <asp:BoundField DataField="Added By" HeaderText="Added By"></asp:BoundField>
     


</Columns>

</asp:GridView>
</div>
</div>
</div>

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
                   
<a href="FuelConsumption.aspx" class="btn btn-info btn-block">OK</a>
                                
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
       
 
   <script type="text/javascript">
       function ValidationSave() {
           var ddlFuelType = document.getElementById('<%= ddlFuelType.ClientID%>').value;
           var ddlCostCenter = document.getElementById('<%= ddlCostCenter.ClientID%>').value;
           var ddlVehicleType = document.getElementById('<%= ddlVehicleType.ClientID%>').value;
           var txtIssueQty = document.getElementById('<%= txtIssueQty.ClientID%>').value;
           var txtTrailerNo = document.getElementById('<%= txtTrailerNo.ClientID%>').value;



    var blResult = Boolean;
    blResult = true;

    if (ddlFuelType == "0") {
        document.getElementById('<%= ddlFuelType.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

    }
           if (ddlCostCenter == "0") {
               document.getElementById('<%= ddlCostCenter.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;

           }
           if (ddlVehicleType == "0") {
               document.getElementById('<%= ddlVehicleType.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;

           }
           if (txtIssueQty == "") {
               document.getElementById('<%= txtIssueQty.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;

           }
           if (txtTrailerNo == "") {
               document.getElementById('<%= txtTrailerNo.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;

           }


    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>
  <script>
      function OpenCancelFuel() {
          var txtFuelRegNo = document.getElementById('<%= txtFuelRegNo.ClientID%>').value;
             <%--var TxtWorkYear = document.getElementById('<%= TxtWorkYear.ClientID%>').value;--%>

          var url = "FuelConsumCancel.aspx?FuelRegID=" + txtFuelRegNo
             popup = window.open(url, "Popup", "top=100,left=400,width=700,height=215");
             popup.focus();
         }
    </script> 

    <script type="text/javascript">
        function ValidateQty() {
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
