<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="InventoryNOCDetsUpdate.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btnsearch").click();
    self.close();
}
</script>
    <style>
        .text-center{
            text-align:center
        }
        .scrolling-table-container{
            height:200px;
            overflow:auto
        }
    </style>
<div class="container" style="background-color: white;">    
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="panel-body">
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="form-group">
    <div class="row">
        <div class="col-sm-4 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >NOC No</b>
<asp:TextBox ID="txtnoc" Style="text-transform: uppercase;"  class="form-control text-label form-cascade-control"
runat="server" placeholder="NOC No" AutoPostBack="true" OnTextChanged="txtnoc_TextChanged"></asp:TextBox>     
</div>
</div>
<div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b >NOC Expiry Date</b>
<asp:TextBox ID="txtNOCExpiryDate"  placeholder="DD-MM-yyyy"   runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
<%--<input type="date" id="txtnocDate" runat="server" class="form-control text-label" />--%>
</div>                        
</div>
    </div>                                
<div class="row">
    <div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b >Weight</b>
<asp:TextBox ID="txtWeight"  placeholder="Weight" runat="server" Class="form-control text-label"></asp:TextBox>
<%--<input type="date" id="txtnocDate" runat="server" class="form-control text-label" />--%>
</div>                        
</div>
    <div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b >Area</b>
<asp:TextBox ID="txtArea"  placeholder="Area" runat="server" Class="form-control text-label"></asp:TextBox>
<%--<input type="date" id="txtnocDate" runat="server" class="form-control text-label" />--%>
</div>                        
</div>
</div>
         <div class="row">
    <div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b >Prev Invoice Storage Validity</b>
<asp:TextBox ID="txtStorageVal"  placeholder="DD-MM-yyyy"   runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
<%--<input type="date" id="txtnocDate" runat="server" class="form-control text-label" />--%>
</div>                        
</div>
    <div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b >Prev Invoice Insurance Validity</b>
<asp:TextBox ID="txtInsuranceVal"  placeholder="DD-MM-yyyy"   runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
<%--<input type="date" id="txtnocDate" runat="server" class="form-control text-label" />--%>
</div>                        
</div>
</div> 
    <div class="row">
        <div class="col-sm-8  col-xs-12" >
<div class="form-group text-label">
<b>  Customer Name  </b>
<asp:DropDownList ID="ddlCustomer"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>   
</div>
</div>

    </div>
<div class="row">
<div class="col-sm-1 col-xs-1" style="padding-right:10px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm " runat="server" OnClientClick="return ValidationUpdate()" OnClick="btnSave_Click"  
Text="Update"  />                                                                                 
</div> 
</div>     
</div>
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-xs">
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

<%--<button class="btn btn-info " ID="Button1" data-dismiss="modal" runat="server" onclick="callparentfunction()" aria-hidden="true">
                                Ok 
                            </button>--%>
    <a runat="server" class="btn btn-info" href="InventoryNOCDetsUpdate.aspx">OK</a>

</div>
</div>

</ContentTemplate>

</asp:UpdatePanel>
</div>
</div>
</div>
</div>

    <script type="text/javascript">
        function ValidationUpdate() {
            var txtnoc = document.getElementById('<%= txtnoc.ClientID%>').value;
            var txtNOCExpiryDate = document.getElementById('<%= txtNOCExpiryDate.ClientID%>').value;
            var txtWeight = document.getElementById('<%= txtWeight.ClientID%>').value;
            var txtArea = document.getElementById('<%= txtArea.ClientID%>').value;
            var txtStorageVal = document.getElementById('<%= txtStorageVal.ClientID%>').value;
            var txtInsuranceVal = document.getElementById('<%= txtInsuranceVal.ClientID%>').value;


            //var result = confirm('Are you sure to Update this noc?')
            var blResult = Boolean;
            blResult = true;

            if (txtnoc == "") {
                document.getElementById('<%= txtnoc.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtNOCExpiryDate == "") {
                document.getElementById('<%= txtNOCExpiryDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtArea == "") {
                document.getElementById('<%= txtArea.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtWeight == "") {
                document.getElementById('<%= txtWeight.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtStorageVal == "") {
                document.getElementById('<%= txtStorageVal.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtInsuranceVal == "") {
                document.getElementById('<%= txtInsuranceVal.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;

        }
</script>
</asp:Content>


