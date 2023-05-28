<%@ Page Title="Bond | Credit Note" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="CreditNote.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Credit Note</title>
       
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            max-height:500px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Credit Note
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Credit Note 
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
    <div class="row">
        <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Credit Note No</b>
<asp:TextBox ID="txtcreditnoteNo" ReadOnly="true"  Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>  
       
</div>
</div>
        <div class="col-sm-4 col-xs-12" style="display:none">
<asp:TextBox runat="server" ID="txtcreditno" ></asp:TextBox>
<asp:TextBox runat="server" ID="txtcredityear" ></asp:TextBox>
<asp:TextBox runat="server" ID="txtCategoryID" ></asp:TextBox>


</div>
        <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Credit Note Date</b>
<asp:TextBox ID="txtcreditnotedate" ReadOnly="true" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>  
        <div class="col-md-2 col-xs-12">                                      
<div class="form-group text-label">
<b >Category </b>
<asp:DropDownList ID="ddlCategory" runat="server" class="form-control text-label">

</asp:DropDownList>

</div>
</div>      
        <div class="col-sm-1 col-xs-6">
                       
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return OpenItembond();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
        <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
            <div class="col-sm-2 col-xs-12" style="display:none" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Work Year</b>
<asp:TextBox ID="txtworkyear" MaxLength="7" onkeypress="return ValidateYear()" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Work Year"></asp:TextBox>     
</div>
</div>
<div class="col-sm-4 col-xs-12" style="display:none" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Assess No</b>
<asp:TextBox ID="txtinvno" AutoPostBack="true" onkeypress="return ValidateNumber()" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Enter Assess No."></asp:TextBox> 
    
       
</div>
</div>
        <div class="col-sm-4 col-xs-12" style="display:none">
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice No</b>
<asp:TextBox ID="txtInvoiceNo" Style="text-transform: uppercase;" ReadOnly="true" TextMode="MultiLine" Rows="2" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Invoice No"></asp:TextBox> 
    
       
</div>
</div>
    </div>
<div class="row">
   
    <div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
    <b>GST Party Name</b>
<asp:TextBox  ID="txtgstname" ReadOnly="true" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" placeholder="GST Name" class="form-control text-label">
                                      
</asp:TextBox>
    <asp:Label runat="server" ID="lblpartyid" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblInvoiceNo" Visible="false"></asp:Label>
</div>
</div>
<asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label> 
</div>

    <div class="row">
        <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Address</b>
<asp:TextBox ID="txtaddress" ReadOnly="true" TextMode="MultiLine" Rows="2" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Address"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>
    <div class="row">
      <div class="col-md-2 col-xs-12">                                      
<div class="form-group text-label">
<b >Criteria </b>
<asp:DropDownList ID="ddlCriteria" runat="server" class="form-control text-label" onchange="CriteriaChange()">
    <asp:ListItem Value="1">Invoice Amount</asp:ListItem>
    <asp:ListItem Value="2">Manual Amount</asp:ListItem>
</asp:DropDownList>
</div>
</div> 
      <div class="col-md-4 col-xs-12" runat="server" id="divAccountHeads" style="display:none">                                      
<div class="form-group text-label">
<b >Account Heads </b>
<asp:DropDownList ID="ddlManualAccountHeads" runat="server" class="form-control text-label">
    <asp:ListItem Value="0">--Select--</asp:ListItem>
</asp:DropDownList>
</div>
</div>
      <div class="col-md-2 col-xs-12" runat="server" id="divAmount" style="display:none">
<div class="form-group text-label">
<b  >Amount</b>
<asp:TextBox ID="txtManualAmount" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Manual Amount"
runat="server"   ></asp:TextBox>
</div>
</div>
      <div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnApply" class="btn btn-primary " runat="server" OnClick="btnApply_Click"  
Text="Apply"  OnClientClick="return ValidationApply()" />
</div>                                                                                  
</div>
  </div>
    <asp:UpdatePanel runat="server" ID="Up_grid" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdcharges" />
        </Triggers>
        <ContentTemplate>
            
      
     <div class="row">
         <div class="col-lg-10 text-label">
             <div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;">
                 <asp:GridView ID="grdcharges" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
                     <Columns>
                         
                         <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
        <asp:Label ID="lblInvoiceNo" runat="server" text='<%#Eval("InvoiceNo")%>'></asp:Label>
        <asp:Label ID="lblAssessNo" Visible="false" runat="server" text='<%#Eval("assessno")%>'></asp:Label>
        <asp:Label ID="lblTax" Visible="false" runat="server" text='<%#Eval("IsTax")%>'></asp:Label>
        <asp:Label ID="lblJONo" Visible="false" runat="server" text='<%#Eval("JONo")%>'></asp:Label>
        <asp:Label ID="lblTaxID" Visible="false" runat="server" text='<%#Eval("TaxID")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="left" Width="150px"  />
</asp:TemplateField>  
                         <asp:TemplateField HeaderText="Workyear" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
        <asp:Label ID="lblWorkyear" runat="server" text='<%#Eval("Workyear")%>'></asp:Label>

            </ItemTemplate>
    <ItemStyle HorizontalAlign="left" Width="150px"  />
</asp:TemplateField> 
                         <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblaccntid" runat="server" Visible="false" text='<%#Eval("AccountID")%>'></asp:Label>
        <asp:Label ID="lblaccntname" runat="server" text='<%#Eval("accountname")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
                       <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
        <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("CONTAINERNO")%>'></asp:Label>

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right" Width="150px"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblSize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right" Width="150px"  />
</asp:TemplateField>                         
    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
        <asp:Label ID="lblntamnt" runat="server" text='<%#Eval("amount")%>'></asp:Label>

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right" Width="150px"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Credited Amount" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
        <asp:Label ID="lblcreditamount" runat="server" text='<%#Eval("creditamount")%>'></asp:Label>

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right" Width="150px"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
       
        <asp:TextBox ID="txtcreditamt" AutoPostBack="true" runat="server" OnTextChanged="txtcreditamt_TextChanged" Width="100px" class="form-control text-label" Style="text-align:right" text='<%#Eval("PaidAmount")%>' ></asp:TextBox>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right" Width="150px"  />
</asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </div>
         </div>
         </div>
      
    <div class="row">
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Net Amount</b>
<asp:TextBox ID="txtnettotal" ReadOnly="true" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="Net Total"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >SGST</b>
<asp:TextBox ID="txtsgst"  ReadOnly="true" Width="80%" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="SGST"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >CGST</b>
<asp:TextBox ID="txtcgst" ReadOnly="true" Width="80%" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="CGST"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >IGST</b>
<asp:TextBox ID="txtigst" ReadOnly="true" Width="80%" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="IGST"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Grand Total</b>
<asp:TextBox ID="txtgrandtotal" ReadOnly="true" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="Grand Total"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>
    <div class="row">
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Credit Amount</b>
<asp:TextBox ID="txtcreditamount" ReadOnly="true" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="Credit Amount"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >SGST</b>
<asp:TextBox ID="txtsgstcredit" ReadOnly="true" Width="80%" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="SGST"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >CGST</b>
<asp:TextBox ID="txtcgstcredit" ReadOnly="true" Width="80%" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="CGST"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >IGST</b>
<asp:TextBox ID="txtigstcredit" ReadOnly="true" Width="80%" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="IGST"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Credit Total</b>
<asp:TextBox ID="txtcredittotal" ReadOnly="true" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder="Credit Total"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
       <div class="row">
       <div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b  >Remark</b>
<asp:TextBox ID="txtremarks" TextMode="MultiLine" Rows="2" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Remark"
runat="server"   ></asp:TextBox>
</div>
</div>
 </div>
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>                                                                                  
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">                           
<a href="CreditNote.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a>                               
</div>                                                                                    
</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;display:Block">
<div class="form-group">
<a href="CreditNoteSummary.aspx" target="_blank"><b style="color:blue">Click here to view Credit Note summary</b> </a>
</div>
</div>
      
<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>                   
</div>                      
</asp:Panel>
                        
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
                   
<button class="btn btn-info btn-block " id="Button5" data-dismiss="modal" runat="server" onserverclick="Button3_ServerClick" aria-hidden="true">
OK 
</button>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
          <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">

<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I1" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">

<asp:Label ID="lblsession1" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">
  <button class="btn btn-info " id="Button1" data-dismiss="modal" runat="server" onserverclick="Button1_ServerClick"  aria-hidden="true">
Yes 
</button>                 
<button id="Button2" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>                                
</div>
</div>

</div>
</div>   
    <div class="modal fade control-label" id="myModalforupdate2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>

      
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I2" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">

<asp:Label ID="Label1" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">
  <button class="btn btn-info " id="Button3" data-dismiss="modal" runat="server" onclick="OpenWOPrint()" aria-hidden="true">
Yes 
</button>                 
<button id="Button4" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>                                
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
        var popup;
        function OpenWOPrint() {
            var txtcredityear = document.getElementById('<%= txtcredityear.ClientID%>').value;
            var txtcreditno = document.getElementById('<%= txtcreditno.ClientID%>').value;
            var txtCategoryID = document.getElementById('<%= txtCategoryID.ClientID%>').value;

            var url = "../Report_Bond/CreditNotePrint.aspx?CreditNoteNo=" + txtcreditno + "&WorkYear=" + txtcredityear + "&Category=" + txtCategoryID
           window.open(url);

       }

</script>
   <%--<script type="text/javascript">
       function ValidateAssessNo() {
           
           var ddlCategory = document.getElementById('<%= ddlCategory.ClientID%>').value;
           var txtinvno = document.getElementById('<%= txtinvno.ClientID%>').value;
           var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;         

           var blResult = Boolean;
           blResult = true;

           if (txtinvno == "") {
               document.getElementById('<%= txtinvno.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
           if (txtworkyear == "") {
               document.getElementById('<%= txtworkyear.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
           if (ddlCategory == 0) {
               document.getElementById('<%= ddlCategory.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
          
           
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    else {
        
        document.getElementById('<%= btnInvchange.ClientID%>').click();
    }
    return blResult;
}
</script>--%>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtinvno = document.getElementById('<%= txtinvno.ClientID%>').value;
    var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;
    document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary disabled");  
               

var blResult = Boolean;
blResult = true;
 

                   
<%--if (txtinvno == "") {
document.getElementById('<%= txtinvno.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
    if (txtworkyear == "") {
        document.getElementById('<%= txtworkyear.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }--%>
//alert('hi')
if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= btnSave.ClientID%>').value = "Save";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary");  
}
return blResult;
}
</script>

<script type="text/javascript">
function ValidateYear() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 45)
return event.returnValue;
return event.returnValue = '';
}
function ValidateNumber() {
    //alert('hii')
    if ((event.keyCode > 47 && event.keyCode < 58))
        return event.returnValue;
    return event.returnValue = '';
}
function ValidateAmount() {
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
    <script type="text/javascript">
        var popup;
        //alert('hi')
        function OpenItembond() {
            //alert('hi')
            var ddlCategory = document.getElementById('<%= ddlCategory.ClientID%>').value;
            var blResult = Boolean;
            blResult = true;
            if (ddlCategory == 0) {
                document.getElementById('<%= ddlCategory.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            else {
                var url = "CreditNoteInvoices.aspx?Category=" + ddlCategory
                //window.open(url);
                popup = window.open(url, "Popup", "width=800,height=600");
                popup.focus();
            }
            return blResult;
        }
        function CriteriaChange() {
            var ddlCriteria = document.getElementById('<%= ddlCriteria.ClientID%>').value;
            if (ddlCriteria == 2) {
                document.getElementById('<%= divAccountHeads.ClientID%>').style.display = "block";
                document.getElementById('<%= ddlManualAccountHeads.ClientID%>').value = 0;
                document.getElementById('<%= divAmount.ClientID%>').style.display = "block";
                document.getElementById('<%= txtManualAmount.ClientID%>').value = "";
            }
            else {
                document.getElementById('<%= divAccountHeads.ClientID%>').style.display = "none";
                document.getElementById('<%= divAmount.ClientID%>').style.display = "none";
                document.getElementById('<%= ddlManualAccountHeads.ClientID%>').value = 0;
                document.getElementById('<%= txtManualAmount.ClientID%>').value = "";
            }
        }
        function ValidationApply() {
            var ddlCriteria = document.getElementById('<%= ddlCriteria.ClientID%>').value;
            var ddlManualAccountHeads = document.getElementById('<%= ddlManualAccountHeads.ClientID%>').value;
            var txtManualAmount = document.getElementById('<%= txtManualAmount.ClientID%>').value;

            var blResult = Boolean;
            blResult = true;

            if (ddlCriteria == 2) {
                if (ddlManualAccountHeads == 0) {
                    document.getElementById('<%= ddlManualAccountHeads.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
                if ((txtManualAmount == "") || (txtManualAmount == 0) || (txtManualAmount < 0)) {
                    document.getElementById('<%= txtManualAmount.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }

            return blResult;
        }
</script>
</asp:Content>
