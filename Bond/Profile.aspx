<%@ Page Title="Bond | Create User" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="Profile.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond | Create User</title>
        
</head>
<div class="page-container">
<div class="pageheader"> 
<h3>
<i class="glyphicon glyphicon-transfer"></i> Create User 
</h3>
           
</div>
       

        
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<Triggers>
<asp:PostBackTrigger ControlID="btnSave" />
</Triggers>
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="col-md-3 pull-md-left sidebar" style="padding-top:12px;">
                    
<div class="col-md-12 pull-md-left sidebar">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title" style="padding-bottom: 0px !important; ">
<i class="fa fa-user"></i>&nbsp;              My Account
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
<h5 class="modal-title">
              
<div class="list-group">
<a menuItemName="My Details" href="#" class="list-group-item active" id="Primary_Sidebar-My_Account-My_Details">
<asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>    
</a> 
                       
</div>
<h5></h5>
<h5></h5>
<h5></h5>
<h5></h5>
<h5></h5>
    <h5></h5>
    <h5></h5>
    <h5></h5>
</h5> 
</div>
                                   
             
</div>
       
                           
</div>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-9 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Create User 
                
<%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true">
<div class="row">
<div class="col-sm-12  col-xs-12"">  
<div class="col-sm-5  col-xs-12"">  
<asp:UpdatePanel ID="UP_image" runat="server" UpdateMode="Conditional">
                                             
<ContentTemplate >
<asp:Image ID="imgAvatar" runat="server"  Height="280px" Width="290px" onclick="OpenFiles()" ToolTip="To change profile picture click on Select Profile"
ImageUrl="../img/user.png"  class="circular--square" />
                                
<br />
<div class="col-sm-3 col-xs-12">
</div>
<h4 class="  col-sm-6 col-xs-12" >
<label class="btn btn-warning btn-xs ">
Select Profile
<a style="display: none">
<asp:FileUpload ID="btnSearchImage" onchange="previewFile()" runat="server" /></a>
</label>
</h4>
</ContentTemplate>
</asp:UpdatePanel>
<div class="col-sm-3 col-xs-12">
</div>
                                        
</div>
<div class="col-sm-7  col-xs-12"">  
<div class="row">
                                           
<div class="col-sm-12  col-xs-12"">                                      
<div class="form-group text-label">
<b>First Name</b>
<asp:TextBox ID="txtName"  Visible="true"   AutoPostBack="false" class="form-control form-cascade-control text-label" placeholder="First Name"
runat="server" Text="" ></asp:TextBox>

</div>
</div>
<div class="col-sm-12  col-xs-12"">                                      
<div class="form-group text-label">
<b>Last Name</b>
<asp:TextBox ID="txtlast"  Visible="true"   AutoPostBack="false" class="form-control form-cascade-control text-label" placeholder="Last Name"
runat="server" Text="" ></asp:TextBox>
<asp:HiddenField runat="server" ID="hdautoid" Value="0" />
</div>
</div>
                                               
<div class="col-sm-12  col-xs-12"">                                      
<div class="form-group text-label">
<b>Login ID</b>
<asp:TextBox ID="txtloging"   class="form-control form-cascade-control text-label" placeholder="Login ID"
runat="server" Text="" ></asp:TextBox>
               
</div>
</div>
                                               
                                              
                                          
<div class="col-sm-5  col-xs-12"">                                      
<div class="form-group text-label">
<b> You are?</b>
<label class="control-label">
                                           
                                       
<asp:radiobutton id="rdmale" AutoPostBack="false" runat="server" GroupName="measurementSystem"  Checked="false"/>&nbsp;Male
&nbsp;&nbsp;&nbsp;&nbsp; <asp:radiobutton id="rdfemale" AutoPostBack="false" runat="server" GroupName="measurementSystem" Checked="false"/>&nbsp;Female
                                       
</label>
</div>
                                          
</div>
                                           
<div class="row" style="padding-left:13PX">
<div class="col-sm-12 col-xs-12"  >                                     
<div class="form-group text-label">
<b>Date of Birth</b>
</div>
                                                                                                           
<div class="form-group text-label" style="margin-top:-5px;padding-left:0px">
                                          
                                    
<asp:textbox type="text" ReadOnly="FALSE" class="form-control" Visible="false" id="txtDOB" name="date1" runat="server" style="border-bottom-left-radius: 4px;
border-bottom-right-radius: 4px;border-top-left-radius: 4px;border-top-right-radius: 4px;" placeholder="dd-MM-YYYY"/>
<div class="col-sm-3  col-xs-12">
<div class="form-group">
<b>Day:</b> <asp:DropDownList ID="ddlDay" runat="server" onchange = "PopulateDays()" Class="form-control text-label"/>
</div>
</div>
<div class="col-sm-3 col-xs-12" >
Month: <asp:DropDownList ID="ddlMonth" runat="server" onchange = "PopulateMonth()"  Class="form-control text-label" />

</div>
<div class="col-sm-4 col-xs-12">
<b>Year:</b> <asp:DropDownList ID="ddlYear" runat="server" onchange = "PopulateYear()"  Class="form-control text-label"  />

</div>

                                          
<br /> 
<asp:CustomValidator ID="Validator" runat="server" ErrorMessage="* Required" ClientValidationFunction = "Validate" />

</div>                                 
 
</div></div>                           
<div class="col-sm-12 col-xs-12" >
<div class="form-group text-label">
<b>Address:</b>
<asp:TextBox ID="txtaddress"  Style="text-transform: uppercase;"  placeholder="Address" class="form-control form-cascade-control text-label" 
TextMode="MultiLine"   runat="server"></asp:TextBox>
                                        

</div>
</div>
<div class="col-sm-12  col-xs-12">
<div class="form-group text-label">
                                        
<b>City</b>
<asp:TextBox ID="txtCity" Style="text-transform: uppercase;" MaxLength="13" class="form-control form-cascade-control text-label" placeholder="City" runat="server"></asp:TextBox>
                                                
</div>
</div>
                           
<div class="col-sm-12  col-xs-12">
<div class="form-group text-label">
                                       
<b>State</b>
<asp:DropDownList ID="ddlState" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
                                                
</div>
</div>
<div class="col-sm-12  col-xs-12">
<div class="form-group text-label">
                                         
<b>  PIN Code</b>
<asp:TextBox Style="text-transform: uppercase;" MaxLength="7" ID="txtPinCode" class="form-control numbersOnly text-label" placeholder="444444" runat="server"></asp:TextBox>
</div>
</div>
</div>
</div>
</div>
</div>
</asp:Panel>
</div>
</div>


<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Office Information
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel3" runat="server" Enabled="true">
<div class="row">


<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
<ContentTemplate>
                             
<asp:Panel runat="server" ID="pnluser" Enabled="true">
<div class="col-sm-4  col-xs-12" id="divuser">
<div class="form-group text-label">
<b> Type of User</b>
                                          
<asp:DropDownList ID="ddluser" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                               
</asp:DropDownList>
                                                
</div>
</div></asp:Panel>

</ContentTemplate>
</asp:UpdatePanel>
                       
                              
<div class="col-sm-4  col-xs-12">
<div class="form-group text-label">
<b> Email (Office)</b>
<asp:TextBox ID="txtEmailID" class="form-control form-cascade-control text-label" placeholder="myemail@example.com"  runat="server"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Red" ErrorMessage="Invalid Email Id" Display="Dynamic"

ControlToValidate="txtEmailID" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" > </asp:RegularExpressionValidator>
</div>
</div>
<div class="col-sm-4  col-xs-12">
<div class="form-group text-label">
<b> Telephone</b>
<asp:TextBox ID="txtTelephone" Style="text-transform: uppercase;" placeholder="+1 (999) 999 9999 " Minlength="7" MaxLength="13" onkeypress="return ValidatePhoneNo()" class="form-control form-cascade-control text-label" runat="server"></asp:TextBox>
</div>
</div>
<div class="col-sm-4  col-xs-12">
<div class="form-group text-label">
<b> Mobile</b>
<asp:TextBox ID="txtMobileNo" Style="text-transform: uppercase;" placeholder="999 999 9999 " MaxLength="13" onkeypress="return ValidatePhoneNo()" class="form-control form-cascade-control text-label" runat="server"></asp:TextBox>
</div>
</div>
<asp:Panel runat="server" ID="pnldesg" Enabled="true">
<div class="col-sm-4  col-xs-12"">                                      
<div class="form-group text-label">
<b>Designation</b>
<asp:DropDownList ID="ddldesignation" Style="text-transform: uppercase;" Visible="true" class="form-control form-cascade-control text-label"
runat="server" ></asp:DropDownList>                                                                                                                                      
</div>
</div></asp:Panel>
<div class="col-sm-4 col-xs-12"">                                      
<div class="form-group text-label">
<b>Department</b>
<asp:DropDownList ID="ddldept" Style="text-transform: uppercase;" Visible="true" class="form-control form-cascade-control text-label"
runat="server" ></asp:DropDownList>
                                              
                                               
                                           

</div>
</div>
</div>
</asp:Panel>

                        
</div>
</div>
<asp:Label ID="lblID" Visible="false" runat="server" Text=""></asp:Label>
                         
<div class="row">
<h4 class="text-overflow mar-top col-sm-12 col-xs-12  " title="Save profile picture and info">
<%--<asp:Button ID="btnUpload" class="btn btn-success btn-xs  "
runat="server" Text="Save Changes" />--%>
<asp:Button ID="btnSave" data-layout="center" data-type="success" ValidationGroup="Groupsubmit" class="btn btn-success btn-sm outline"
runat="server" Text="Save"  OnClientClick="return ValidateWO();" OnClick="btnSave_Click" />
<%-- <asp:Button ID="btnClear" class="btn btn-default btn-xs  " ToolTip="Discard all changes"
runat="server" Text="Cancel"    OnClientClick="return confirm('Are you sure you want to discard your changes?');"  />--%>
</h4>
<div class="col-sm-5 pull-right" style="padding-top:15px;">
<div class="form-group">
<a href="ProfileSummary.aspx" target="_blank"><b style="color:blue">Click here to view Profile summary</b> </a>
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
                           
<a href="Profile.aspx" class="btn btn-info btn-block">OK</a>
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
</fieldset>
                  
<%--<div class="row">
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"    
Text="Save" OnClientClick="return ValidationSave()"/>
</div>
                                              
                                      
</div>

<div class="col-sm-1" style="padding-left:26px;">
<div class="form-group" style="padding-top:0px">
</div>
<a href="BondNoc.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a>
</div>

                         
</div>--%>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
       
         
</div>
<script type="text/javascript">
function previewFile() {
var preview = document.querySelector('#<%=imgAvatar.ClientID %>');
var file = document.querySelector('#<%=btnSearchImage.ClientID%>').files[0];
var reader = new FileReader();
alert(file)

reader.onloadend = function () {

preview.src = reader.result;
}

if (file) {
reader.readAsDataURL(file);
} else {
preview.src = "";
}
}


</script>
         
<script type="text/javascript">
function ValidateWO() {
var txtname = document.getElementById('<%= txtName.ClientID%>').value; 
var txtlast = document.getElementById('<%= txtlast.ClientID%>').value;
var txtaddress = document.getElementById('<%= txtaddress.ClientID%>').value;
var ddluser = document.getElementById('<%= ddluser.ClientID%>').value;
                    

var blResult = Boolean;
blResult = true;

document.getElementById('<%= txtName.ClientID%>').style.borderColor = "Gainsboro"; 
document.getElementById('<%= txtlast.ClientID%>').style.borderColor = "Gainsboro"; 
document.getElementById('<%= txtaddress.ClientID%>').style.borderColor = "Gainsboro";
document.getElementById('<%= ddluser.ClientID%>').style.borderColor = "Gainsboro";
                      


if (txtname == "") {
document.getElementById('<%= txtName.ClientID%>').style.borderColor = "red"
blResult = blResult && false;

} 
if (txtlast == "") {
document.getElementById('<%= txtlast.ClientID%>').style.borderColor = "red"
blResult = blResult && false;

}
if (txtaddress == "") {
document.getElementById('<%= txtaddress.ClientID%>').style.borderColor = "red"
blResult = blResult && false;

}
                 


if (blResult == false) { alert('Please fill the required fields!'); }
return blResult;

}
</script>



</asp:Content>
