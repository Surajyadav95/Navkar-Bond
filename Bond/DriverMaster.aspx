<%@ Page Title="Bond |Driver Master" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="DriverMaster.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
<div class="container" style="background-color: white">

<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Driver Master<small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>
               
                    
<div class="row">


<div class="col-sm-12 col-xs-12">
<div class="form-group text-label">
<b  >Driver Name</b>
<asp:TextBox ID="txtDriverName" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Driver Name"
runat="server"   ></asp:TextBox>
</div>
</div>

    
<div class="col-sm-6 col-xs-8">
<div class="form-group text-label">
<b  >Driver No</b>
<asp:TextBox ID="txtDriverNo" Style="text-transform:uppercase" onkeypress="return ValidateQty();" MaxLength="10" class="form-control text-label"  placeholder="Driver No"
runat="server"   ></asp:TextBox>

</div>
</div>

             
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"   
Text="Save"  OnClientClick="return ValidationSave()"  />
</div>
                                              
                                      
</div>

<div class="col-sm-1 col-xs-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:18px">
<a href="DriverMaster.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
</div>
                                              
                                      
</div>
</div>

             

<asp:Label ID="LblDriverID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblDriverName" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblDriverNo" Visible="false" runat="server" Text=""></asp:Label>

<div class="row">
                   

<div class="row">
<div class="col-md-6 col-xs-8 text-label " style="padding-right: 60px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="4">
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
    <asp:TemplateField>
<ItemTemplate>


<a  href='<%# "DriverMaster.aspx?DriverIDEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "DriverID")).ToString())%>' 
Class='btn btn-info btn-xs outline' 
>Edit</a>
   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="60px" />
</asp:TemplateField>
                                                 
<asp:BoundField DataField="DriverID"   HeaderText="Driver ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="DriverName"   HeaderText="Driver Name"></asp:BoundField>
<asp:BoundField DataField="DriverNo"   HeaderText="Driver No"></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
                        
</div>

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
         
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
                   
<a href="DriverMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
             
</div>
</div>
                 

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtDriverName = document.getElementById('<%= txtDriverName.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

if (txtDriverName == "") {
document.getElementById('<%= txtDriverName.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
 
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
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


