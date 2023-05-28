<%@ Page Title="Bond | Generate XML Receipt" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="GenerateXMLReceipt.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title> Bond | Generate XML Receipt </title>
</head>
<style>
.text-center{
text-align:center
}
.scrolling-table-container{
    height:430px;
    overflow:auto
}
</style>
<div class="page-container">
<div class="pageheader">
<h3> Generate XML Receipt  
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
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
As On Date
<asp:TextBox ID="txtAsOnDate" ReadOnly="true" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>


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
<asp:Button ID="btnXMLInvoice" runat="server" 
class="btn btn-success btn-sm outline" Text="Generate XML" ></asp:Button>
</div>              
</div>
         <div class="col-md-1  col-xs- 12" style="display:none">
             <asp:TextBox runat="server" ID="strfilename"></asp:TextBox>
             <asp:Button ID="btnHidden" runat="server" 
class="btn btn-success btn-sm outline" Text="" ></asp:Button>
         </div>                                 

</div>
                                 

                     
<asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
    <ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label"  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-right:0px;">
<asp:GridView ID="grdSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" DataKeyNames="WorkYear" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField>
    <ItemTemplate>
        <asp:CheckBox runat="server" ID="chkInv" Checked="true" />
    </ItemTemplate>
    <ItemStyle HorizontalAlign="center" Width="20px" />
</asp:TemplateField>
    <asp:TemplateField>
        <ItemTemplate>
                <asp:LinkButton ID="lnkLock" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Lock Invoice" OnClick="lnkLock_Click"
                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AssessNo")%>' runat="server" 
                                                            ></asp:LinkButton>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="left" Width="40px" />
    </asp:TemplateField>
<asp:BoundField DataField="Srno" HeaderText="Sr No."></asp:BoundField>

    <asp:TemplateField HeaderText="Receipt No" HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblReceiptNo" Text='<%#Eval("ReceiptNo")%>'></asp:Label>
    <asp:Label runat="server" ID="lblWorkYear" Visible="false" Text='<%#Eval("WorkYear")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Receipt Date" HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblReceiptDate" Text='<%#Eval("ReceiptDate")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
<asp:BoundField DataField="AssessNo" Visible="false" HeaderText="Assessment No." ></asp:BoundField>
    <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
    <asp:Label runat="server" ID="lblTallyName" Visible="false" Text='<%#Eval("tallyledgername")%>'></asp:Label>
    <asp:Label runat="server" ID="lblDepositeBank" Visible="false" Text='<%#Eval("DepositeBank")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
<asp:BoundField DataField="NOCNo" HeaderText="Noc No." ></asp:BoundField>
<asp:BoundField DataField="invoiceType" HeaderText="Invoice Type" ></asp:BoundField>
<asp:BoundField DataField="Pay_From" HeaderText="Pay Name "  ></asp:BoundField>
<asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
<asp:BoundField DataField="CHAName" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="agentName" HeaderText="Customer Name"></asp:BoundField>
    <asp:TemplateField HeaderText="Receipt Amount" HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblReceiptAmount" Text='<%#Eval("ReceiptAmount")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>
<asp:BoundField DataField="UserName" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="ReceiptType" HeaderText="Receipt Type"></asp:BoundField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
    
    </ContentTemplate>
</asp:UpdatePanel>
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
    function ShowAlert()
    {

        //$("#Loader").close();
        //alert('hi')
        document.getElementById('<%=btnHidden.ClientID%>').click();
    }
</script>

      
</asp:Content>
