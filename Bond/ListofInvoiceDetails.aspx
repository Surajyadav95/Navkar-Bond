<%@ Page Title="Bond | Assessment Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ListofInvoiceDetails.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title> Bond | Invoice Details </title>
</head>
<style>
.text-center{
text-align:center
}
</style>
<div class="page-container">
<div class="pageheader">
<h3> List of Invoice Details  
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
           
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
<div class="col-md-4 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate"  style="width: 150px;" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 20px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" style="width: 150px;"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
 
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="1">Assess No</asp:ListItem>
<asp:ListItem Value="2">NOC No</asp:ListItem>
<asp:ListItem Value="3">GST Name</asp:ListItem> 
<asp:ListItem Value="4">Invoice Type</asp:ListItem> 

</asp:DropDownList>
                                               
</div>

</div>
<div class="col-md-2 col-xs-12" style="display:none;"  id="divassessno" runat="server">
<div class="form-group text-label">
<b >Assess No</b>
<asp:TextBox ID="txtAssessno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Assess No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-2 col-xs-12" style="display:none;"  id="divnocno" runat="server">
<div class="form-group text-label">
<b >NOC No</b>
<asp:TextBox ID="txtnocno" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Search NOC No"  
runat="server"   ></asp:TextBox>
</div>
</div>                                                      
<div class="col-md-4 col-xs-12" style="display:none;"  id="divgstname" runat="server">
<div class="form-group text-label">
<b >GST Name</b>
<asp:TextBox ID="txtgstname" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Search GST Name"  
runat="server"   ></asp:TextBox>
</div>
</div>  
          <div class="col-md-2 col-xs-12" style="display:none;"  id="divinvtype" runat="server">
<div class="form-group text-label">
Invoice Type
<asp:DropDownList ID="ddlinvoicetype" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="N">NOC</asp:ListItem>
<asp:ListItem Value="E">Bond Ex</asp:ListItem>
<asp:ListItem Value="Other">Other</asp:ListItem>

</asp:DropDownList>
                                               
</div>

</div>                              
<asp:Label ID="lblFD_ID" Visible="false" runat="server" Text=""></asp:Label>

<div class="col-md-1  col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" ></asp:Button>
</div>              
</div>

    <div class="col-md-1  col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnGeneratePDF" runat="server" 
class="btn btn-purple btn-sm outline" Text="Generate PDF"  OnClientClick="return confirm('Are you sure to Generate PDF?')" OnClick="btnGeneratePDF_Click" ></asp:Button>
</div>              
</div>
         <div class="col-md-1  col-xs- 12" style="display:none">
             <asp:TextBox runat="server" ID="strfilename"></asp:TextBox>
         </div>                                 

</div>
                                 

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" DataKeyNames="WorkYear" OnPageIndexChanging="OnPageIndexChanging"  AllowPaging="true" PageSize="40" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField>
<ItemTemplate>

<a  href='<%# "../Report_Bond/BondAssessmentPrint.aspx?AssessNo=" & (DataBinder.Eval(Container.DataItem, "AssessNo")).ToString() & "&WorkYear=" & (DataBinder.Eval(Container.DataItem, "WorkYear")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
    <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Cancel" OnClick="lnkCancel_Click"
                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo")%>' runat="server" 
                                                            ></asp:LinkButton>
        <asp:LinkButton ID="lnkmail" ControlStyle-CssClass="btn btn-success btn-xs outline"  Text="Mail" OnClick="lnkmail_Click" Visible="true"
                                                            OnClientClick="return confirm('Are you sure to mail?')"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AssessNo")%>' runat="server"
                                                            ></asp:LinkButton>                                                  
                                                          
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="180px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Assess No" HeaderStyle-CssClass="text-center">
<ItemTemplate>

<asp:Label runat="server" ID="lblassessNo" Text='<%#Eval("AssessNo")%>'></asp:Label>

</ItemTemplate>

<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
<asp:BoundField  DataField="InvoiceNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" HeaderText="Invoice No"></asp:BoundField>

    <asp:TemplateField HeaderText="Work Year" HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblworkyear" Text='<%#Eval("WorkYear")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
<asp:BoundField  DataField="AssessDate" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" HeaderText="Assess Date"></asp:BoundField>
<asp:BoundField DataField="NOCNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="NOC No"  ></asp:BoundField>
<asp:BoundField DataField="InvoiceType" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="Invoice Type"  ></asp:BoundField>
<asp:BoundField DataField="GSTName" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="left" HeaderText="Party Name"  ></asp:BoundField>
<asp:BoundField DataField="GrandTotal" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderText="Grand Total"  ></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
    <asp:Panel ID="pnlPerson" Visible="false" runat="server" font-family="Segoe UI">
       <rsweb:ReportViewer ID="ReportViewer1" Width="1000px" Height="600px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Report_Bond\BondAssessmentPrint.rdlc" >
        </LocalReport>
        </rsweb:ReportViewer>  
        </asp:Panel>                  
</div>
</div>
</div>
</div>
</div>
</div>                              
</div>
        <div class="modal fade control-label" id="myModalforupdate2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblsession"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info btn-block" id="Button1" data-dismiss="modal" runat="server" onserverclick="btnsearch_Click"  aria-hidden="true">
Ok 
</button>

</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>  

    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
         <div class="modal-dialog modal-sm">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                 <ContentTemplate>
                     <div class="modal-content">
                         <div class="modal-header">
                             <center>
                                 <h4 class="modal-title">
                                     <asp:Label ID="lblsession1"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
                                 </h4>
                             </center>
                         </div>
                         <div class="modal-footer">
                             <%--<button class="btn btn-info " ID="BtnApprove" data-dismiss="modal" runat="server" onserverclick="OnQuotApproveby" aria-hidden="true">Yes </button>--%><%-- <asp:Button ID="Button2" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true" Text="No" ></asp:Button>--%><a href="ListofInvoiceDetails.aspx" class="btn btn-info btn-block ">OK</a>
                         </div>
                     </div>
                 </ContentTemplate>
             </asp:UpdatePanel>
         </div>
     </div>
<script>
    function sendEmail() {
        try {            
            var strfilename = document.getElementById('<%= strfilename.ClientID%>').value;
            //alert(strfilename)
            //window.open("mailto:?attachment=" + strfilename)
            document.location = "mailto:"
        }
        catch (err) {
            alert(err.message);
        }
    }
</script>

      
</asp:Content>
