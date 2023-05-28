<%@ Page Title="VGM Report" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="VGMReport.aspx.vb" Inherits="Summary_BCYMovement" culture="en-GB" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>VGM Report</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>VGM Report 
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                     
</ContentTemplate>
</asp:UpdatePanel>--%>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body" >
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>                                    
                                                
<div class="row">
    <div class="col-md-6 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
<div class="col-md-3 col-xs-12" >
<div class="form-group text-label">
<b >Container No</b>
<asp:TextBox ID="txtContainerNo" Style="text-transform:uppercase" class="form-control text-label " MaxLength="11"  placeholder="Container No"
runat="server"   ></asp:TextBox>
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


                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="7" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>

<a  href='<%# "../Report_Bond/VGMRDLC.aspx?VGMNo=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "Slip ID")).ToString())%>' target="_blank"
Class='btn btn-info btn-xs outline' 
>Print</a>
           <asp:LinkButton ID="lnkmail" ControlStyle-CssClass='<%# Eval("btnmail") %>'  Text="Mail" OnClick="lnkmail_Click"
                                                            OnClientClick="return confirm('Are you sure to Mail VGM?')"
                                                            CommandArgument='<%# Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "Slip ID")).ToString())%>' runat="server"
                                                            ></asp:LinkButton>                                              
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>

</Columns>

</asp:GridView>
</div>
</div>
</div>

</div>

    <asp:Panel ID="pnlPerson" Visible="false" runat="server" font-family="Segoe UI">
        <rsweb:ReportViewer ID="ReportViewer1" Width="900PX" Height="1100px" runat="server">
         
        </rsweb:ReportViewer>
             </asp:Panel>
</div>
</div>
</div>
</div>
                          
                     
                       
                       
</div>

</div>
         
</div>
<%-- <script type="text/javascript">
function checkRadioBtn(id) {
var gv = document.getElementById('<%=grdcontainer.ClientID%>');

for (var i = 1; i < gv.rows.length; i++) {
var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

// Check if the id not same
if (radioBtn[0].id != id.id) {
radioBtn[0].checked = false;
}
}
}
</script>--%>
<%--  <script type="text/javascript">
 
function BondExPrint() {
            
var NOCNo1= document.getElementById('<%= txtNOCNo.ClientID%>').value;
             
var url = "../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1;
//alert("hi")
                
window.open(url);

}


</script>--%>
</asp:Content>
