<%@ Page Title="Bond | Bills Verification" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="BillsVerification.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Bills Verification</title>
       
</head>
    <style>
        .header-center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Bills Verification
</h3>
           
</div>
       
<div id="page-content">            
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-9 main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Bills Verification
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Verification No</b>
<asp:TextBox ID="txtVerificationNo" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" placeholder="NEW"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-3 col-xs-12" style="display:none">
        <asp:TextBox ID="txtverificationnoprint" runat="server" ></asp:TextBox>
    </div>
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Verification Date</b>
<asp:TextBox ID="txtVerificationDate" TextMode="Date" class="form-control text-label"  placeholder="dd-MM-yyyy"
runat="server"></asp:TextBox>
</div>
</div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>

    <div class="col-md-6 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Vendor</b>
<asp:DropDownList Style="text-transform: uppercase;" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AutoPostBack="true" runat="server"  class="form-control text-label" ID="ddlVendor" >      
</asp:DropDownList>    
    <asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
</div>
</div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <div class="row">
    <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Vendor Bill No</b>
<asp:TextBox ID="txtVendorBillNo" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Vendor Bill No"></asp:TextBox>     
</div>
</div>
    <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Vendor Bill Date</b>
<asp:TextBox ID="txtVendorBillDate" Style="text-transform: uppercase;" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
    <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Bill Type</b>
<asp:DropDownList Style="text-transform: uppercase;" runat="server"  class="form-control text-label" ID="ddlBillType" >
    
</asp:DropDownList>    
</div>
</div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Activity</b>
<asp:DropDownList Style="text-transform: uppercase;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged"  class="form-control text-label" ID="ddlActivity" >
    <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="1">Bond In</asp:ListItem>
    <asp:ListItem Value="2">Bond Ex</asp:ListItem>    
</asp:DropDownList>    
</div>
</div>
             
               </ContentTemplate>
    </asp:UpdatePanel>
</div>

<div class="row">
    <div class="col-md-6 col-sm-6 col-xs-12">                                      
<div class="form-group date text-label">
<b>Date</b>
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" style="width:190px;text-transform: uppercase;"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" style="width:190px;text-transform: uppercase;"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>

    <div class="col-md-1 col-sm-1 col-xs-12">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnshow" class="btn btn-primary " runat="server" 
Text="Show"  OnClientClick="return ValidationShow()" />
</div>                                                                                    
</div>
     </div>

<div class="row">
         <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >NOC No</b>
<asp:TextBox ID="txtNOCNo" onkeypress="return ValidateNumber()" Style="text-transform:uppercase" class="form-control text-label" placeholder="NOC No"
runat="server"></asp:TextBox>
</div>
</div>
    <div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnkadd" ControlStyle-CssClass='btn btn-info'  runat="server"
OnClientClick="return ValidationAdd();">  
<i class=" fa fa-check"  aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
</div>
    
     <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="row">
         <div class="col-lg-12 col-xs-12 text-label">
             <div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;">
                 <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
                     <Columns>
         <asp:TemplateField Visible="false">
    <ItemTemplate>
            <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger' OnClick="lnkCancel_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoId")%>' runat="server" 
                                                            ><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" Width="40px"  />
</asp:TemplateField>
         <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblSrNo" runat="server" text='<%#Eval("SrNo")%>'></asp:Label>
        <asp:Label ID="lblautoid" runat="server" Visible="false" text='<%#Eval("AutoId")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
         <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>                                                                       
    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblSize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
        <asp:TemplateField HeaderText="Packages" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblPackages" runat="server" text='<%#Eval("Packages")%>'></asp:Label>        
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center" />
</asp:TemplateField> 
        <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblWeight" runat="server" text='<%#Eval("Weight")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
        <asp:TemplateField HeaderText="Equipment" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblEquipment" runat="server" text='<%#Eval("Equipment")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>              
        <asp:TemplateField HeaderText="NOC No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblNOCNo" runat="server" text='<%#Eval("NOCNo")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>           
         <asp:TemplateField HeaderText="Ex Deposite No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblExDepositeNo" runat="server" text='<%#Eval("ExdepositeNo1")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>           
       <asp:TemplateField HeaderText="Activity Date" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblActivityDate" runat="server" text='<%#Eval("ActivityDate1")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField> 
          <asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblRate" runat="server" text='<%#Eval("Rate")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>             
        <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblTotal" runat="server" text='<%#Eval("Total")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>                                                  
                     </Columns>
                 </asp:GridView>
             </div>
         </div>
         </div>   
    <div class="row">
        <div class="col-md-6 col-xs-12">
<div class="form-group text-label" style="padding-top: 22px;">
<asp:CheckBox ID="chkIsGST" runat="server" Checked="true" OnCheckedChanged="chkIsGST_CheckedChanged" AutoPostBack="true" />
<asp:HiddenField ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="lblIsGST" runat="server" AssociatedControlID="chkIsGST" CssClass="inline">Is GST Applicable?</asp:Label>
</div>
</div>
    </div>
    <div class="row">
        <div class="col-lg-12 text-label" style="padding-right:0px">
        <div id="divtblWOTOtal" runat="server" style="display:none;">                                         
<table forecolor="Black" class="table table-striped table-bordered table-hover" style="border-top:5px solid #7bc144;margin-left:-5px;margin-right:-5px">
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b ">Net Total</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblPercentage" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblTotal" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Discount</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="Label1" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lbldisc" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >CGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblCgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblCGST" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >SGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblSgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblSGST" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >IGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblIgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblIGST" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Grand Total</b></td>
<%--<td style ="width:20%;text-align:right"></td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblAllTotal" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
</table>
</div>
    </div>
        </div>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
                        
</div>
</div>

<asp:Panel ID="Panel1" runat="server" Enabled="true">
<div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="BillsVerification.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
  
<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="BillsVerificationSummary.aspx" target="_blank"><b style="color:blue">Click here to view Bills Verification Summary</b> </a>
</div>
</div>
                         
</div>
    </asp:Panel>
<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
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
                   
<button class="btn btn-info btn-block " id="Button1" data-dismiss="modal" runat="server" onserverclick="Button1_ServerClick" aria-hidden="true">
OK 
</button>
<%--<button id="Button2" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>--%>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
        <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnYes" data-dismiss="modal" runat="server" onserverclick="btnYes_ServerClick" aria-hidden="true">
Yes 
</button>
<a href="BillsVerification.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>  
    <div class="modal fade control-label" id="myModalforupdate2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="Label1"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<%--<button class="btn btn-info " id="Button3" data-dismiss="modal" runat="server" onserverclick="btnYes_ServerClick" aria-hidden="true">
Yes 
</button>--%>
<a href="BillsVerification.aspx" class="btn btn-info btn-block">Ok</a>
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
           <%--var txtssrnoprint = document.getElementById('<%= txtssrnoprint.ClientID%>').value;--%>
    
           var url = "../Report_Bond/SSRPrint.aspx?SSRNo=" + txtssrnoprint
    window.open(url);

}

</script>
    <script type="text/javascript">
        function ValidationShow() {

            var ddlActivity = document.getElementById('<%= ddlActivity.ClientID%>').value;
            var ddlVendor = document.getElementById('<%= ddlVendor.ClientID%>').value;
            var txtVendorBillNo = document.getElementById('<%= txtVendorBillNo.ClientID%>').value;
            var ddlBillType = document.getElementById('<%= ddlBillType.ClientID%>').value;
            var txtVendorBillDate = document.getElementById('<%= txtVendorBillDate.ClientID%>').value;
            var txtfromDate = document.getElementById('<%= txtfromDate.ClientID%>').value;
            var txttoDate = document.getElementById('<%= txttoDate.ClientID%>').value;

    document.getElementById('<%= btnshow.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnshow.ClientID%>').setAttribute("class", "btn btn-primary disabled");


    var blResult = Boolean;
    blResult = true;

    if (ddlActivity == 0) {
        document.getElementById('<%= ddlActivity.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

            if (ddlVendor == 0) {
                document.getElementById('<%= ddlVendor.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtVendorBillNo == "") {
                document.getElementById('<%= txtVendorBillNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlBillType == 0) {
                document.getElementById('<%= ddlBillType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtVendorBillDate == "") {
                document.getElementById('<%= txtVendorBillDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtfromDate == "") {
                document.getElementById('<%= txtfromDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txttoDate == "") {
                document.getElementById('<%= txttoDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
        document.getElementById('<%= btnshow.ClientID%>').value = "Show";
    document.getElementById('<%= btnshow.ClientID%>').setAttribute("class", "btn btn-primary");
}
    return blResult;
}
</script>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtVerificationDate = document.getElementById('<%= txtVerificationDate.ClientID%>').value;
    var ddlVendor = document.getElementById('<%= ddlVendor.ClientID%>').value;
    var txtVendorBillNo = document.getElementById('<%= txtVendorBillNo.ClientID%>').value;
    var txtVendorBillDate = document.getElementById('<%= txtVendorBillDate.ClientID%>').value;
    var ddlBillType = document.getElementById('<%= ddlBillType.ClientID%>').value;
    var ddlActivity = document.getElementById('<%= ddlActivity.ClientID%>').value;

    document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary disabled");      
               

var blResult = Boolean;
blResult = true;
                   
if (txtVerificationDate == "") {
document.getElementById('<%= txtVerificationDate.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if (ddlVendor == 0) {
        document.getElementById('<%= ddlVendor.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtVendorBillNo == "") {
        document.getElementById('<%= txtVendorBillNo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtVendorBillDate == "") {
        document.getElementById('<%= txtVendorBillDate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlBillType == "") {
        document.getElementById('<%= ddlBillType.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlActivity == "") {
        document.getElementById('<%= ddlActivity.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
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
        function ValidationAdd() {
            var ddlVendor = document.getElementById('<%= ddlVendor.ClientID%>').value;
            var txtVendorBillNo = document.getElementById('<%= txtVendorBillNo.ClientID%>').value;
            var txtVendorBillDate = document.getElementById('<%= txtVendorBillDate.ClientID%>').value;
            var ddlBillType = document.getElementById('<%= ddlBillType.ClientID%>').value;
            var ddlActivity = document.getElementById('<%= ddlActivity.ClientID%>').value;
            var txtNOCNo = document.getElementById('<%= txtNOCNo.ClientID%>').value;

           
           document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info disabled");
           var blResult = Boolean;
           blResult = true;

           if (ddlVendor == 0) {
                document.getElementById('<%= ddlVendor.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtVendorBillNo == "") {
                document.getElementById('<%= txtVendorBillNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtVendorBillDate == "") {
                document.getElementById('<%= txtVendorBillDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

            if (ddlBillType == 0) {
                document.getElementById('<%= ddlBillType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlActivity == 0) {
                document.getElementById('<%= ddlActivity.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

            if (txtNOCNo == "") {
               document.getElementById('<%= txtNOCNo.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }

           if (blResult == false) {
               alert('Please fill the required fields!');               
               document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info");
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
</asp:Content>
