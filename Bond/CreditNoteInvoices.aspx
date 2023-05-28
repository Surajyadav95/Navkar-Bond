<%@ Page Title="Bond" Language="VB" EnableEventValidation="false" MasterPageFile="~/Bond/PopUp.master" AutoEventWireup="false" CodeFile="CreditNoteInvoices.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .scrolling-table-container{
            height:300px;
            overflow:auto;
        }
        .scrolling-table-container1{
            height:200px;
            overflow:auto;
        }
    </style>
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
    
<div class="container" style="background-color: white;margin-top:-60px">
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>Invoice Search  <small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>
<div class="row"> 
<div class="col-md-4 col-xs-4">
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="1">Party Name</asp:ListItem>
<asp:ListItem Value="2">Invoice No</asp:ListItem>
</asp:DropDownList>                                               
</div>
</div>
<div class="col-md-6 col-xs-6" runat="server" id="divSearchText" style="display:none">
<div class="form-group text-label">
<b  >Search Text</b>
<asp:TextBox ID="txtsearc"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">                                            
</asp:TextBox> 
</div>
</div>                                               
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
</div>                                             
</div>
<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>


<div class="row">
<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container" style="margin-left: 12px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
    <asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary btn-sm'                                                       
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AssessNo")%>' runat="server" OnClick="lnkselect_Click"
><i class="fa fa-check"></i></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="40px" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Assess No">
<ItemTemplate>
<asp:Label runat="server" ID="lblAssessNo" Text='<%#Eval("AssessNo")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Work Year">
<ItemTemplate>
<asp:Label runat="server" ID="lblWorkyear" Text='<%#Eval("WorkYear")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Invoice No">
<ItemTemplate>
<asp:Label runat="server" ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Party Name">
<ItemTemplate>
<asp:Label runat="server" ID="lblGSTName" Text='<%#Eval("GSTName")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Address">
<ItemTemplate>
<asp:Label runat="server" ID="lblAddress" Text='<%#Eval("GSTAddress")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>

</Columns>
</asp:GridView>
</div>
</div>
</div>    
    <br /><br /><br />
<div class="row">
<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container1" style="margin-left: 12px; margin-right: 0px;">
<asp:GridView ID="GridView1" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>

    <asp:TemplateField HeaderText="Assess No">
<ItemTemplate>
<asp:Label runat="server" ID="lblAssessNo" Text='<%#Eval("AssessNo")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Work Year">
<ItemTemplate>
<asp:Label runat="server" ID="lblWorkyear" Text='<%#Eval("WorkYear")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Invoice No">
<ItemTemplate>
<asp:Label runat="server" ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField> 
    <asp:TemplateField HeaderText="Party Name" Visible="false">
<ItemTemplate>
<asp:Label runat="server" ID="lblGSTName" Text='<%#Eval("GSTName")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Address" Visible="false">
<ItemTemplate>
<asp:Label runat="server" ID="lblAddress" Text='<%#Eval("GSTAddress")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
</div>
</div>   
    <div class="row">  
        <div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-success " runat="server"
OnClick="btnAdd_Click"
Text="Add"     />
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
<%-- <a href="BondSearch.aspx" class="btn btn-info btn-block">OK</a>--%>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
<script type="text/javascript" >
function callparentfunction() {
window.opener.document.getElementById("ContentPlaceHolder1_btnIndentItem").click();
self.close();
}
</script>  
    
    <script type = "text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        inputList[i].checked = true;

                    }

                    else {
                        inputList[i].checked = false;
                    }

                }

            }

        }

</script> 
    <script type = "text/javascript">

        function Check_Click(objRef) {

            var row = objRef.parentNode.parentNode;

            var GridView = row.parentNode;

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                var headerCheckBox = inputList[0];

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;
        }

</script>
      
</asp:Content>


