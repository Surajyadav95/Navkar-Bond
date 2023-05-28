<%@ Page Title="Bond | NOC Extend" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="NocExtend.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

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
<h1>NOC-Extend <small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>                           
</div>
</div>              
                    
<div class="row">

<div class="col-sm-3 col-xs-8">
<div class="form-group text-label">
<b  >NOC No</b>
<asp:TextBox ID="txtnoc" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Noc"
runat="server"   ></asp:TextBox>

</div>
</div>

    <div class="col-md-1 col-xs- 12">
                    <div class="form-group pull-left" style="padding-top:20px" >
                    <asp:Button ID="btnsearch" runat="server" 
                    class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" 
                        OnClientClick="return ValidationSave()"/>

                    
                    </div>              
                    </div>
</div>
    <div class="row">
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b >Valid Upto Date</b>
<asp:TextBox ID="txtvalidupto" ReadOnly="true"  placeholder="DD-MM-yyyy"   runat="server" TextMode="Date"   Class="form-control text-label"></asp:TextBox>
</div>                        
</div>
        </div>
    <div class="row">
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Weeks</b>
<asp:TextBox ID="txtweeks" Style="text-transform:uppercase" AutoPostBack="true"  class="form-control text-label" OnTextChanged="txtweeks_TextChanged"   placeholder="Weeks"
runat="server"   ></asp:TextBox>
</div>
</div>
        </div>
    <div class="row">
    <div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b >Extend Upto</b>
<asp:TextBox ID="txtExtend" ReadOnly="true"  placeholder="DD-MM-yyyy"   runat="server" TextMode="Date"   Class="form-control text-label"></asp:TextBox>
</div>                        
</div>
        </div>

             <div class="row">
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnSave_Click"    
Text="Save"    OnClientClick="return Validationadd()" />
</div>
                                              
                                      
</div>
</div>
 
</div>

             

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

 

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
                   
<a href="NocExtend.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
             
</div>
</div>
                 

</div>
</div>
</div>

<script type="text/javascript">
function ValidationSave() {
                 
    var txtnoc = document.getElementById('<%= txtnoc.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

if (txtnoc == "") {
document.getElementById('<%= txtnoc.ClientID%>').style.borderColor = "red";
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
        function Validationadd() {

            var txtweeks = document.getElementById('<%= txtweeks.ClientID%>').value;


    var blResult = Boolean;
    blResult = true;


    if (txtweeks == "") {
        document.getElementById('<%= txtweeks.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;
}

    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>
</asp:Content>


