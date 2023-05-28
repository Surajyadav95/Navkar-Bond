<%@ Page Title="Bond | Generate SSR" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="GenerateSSR.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Generate SSR</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Generate SSR
</h3>
           
</div>
       
<div id="page-content">            
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-10 main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">

            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >SSR No</b>
<asp:TextBox ID="txtssrno" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" placeholder="NEW"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-3 col-xs-12" style="display:none">
        <asp:TextBox ID="txtssrnoprint" runat="server" ></asp:TextBox>
    </div>
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >SSR Date</b>
<asp:TextBox ID="txtssrdate" TextMode="Date" class="form-control text-label"  placeholder="dd-MM-yyyy"
runat="server"></asp:TextBox>
</div>
</div>
    <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >SSR Type</b>
<asp:DropDownList Style="text-transform: uppercase;" runat="server" class="form-control text-label" ID="ddlSSRType" >
 
</asp:DropDownList>    
</div>
</div>
    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
        <ContentTemplate>
            </ContentTemplate>
    </asp:UpdatePanel>--%>
    <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >NOC No</b>
<asp:TextBox ID="txtnocno"  Style="text-transform: uppercase;" AutoPostBack="true" OnTextChanged="txtnocno_TextChanged"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NOC No"></asp:TextBox>     
</div>
</div>
            
    <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >NOC Date</b>
<asp:TextBox ID="txtnocdate" Style="text-transform: uppercase;" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
</div>

<div class="row">
    <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Customer Name</b>
<asp:TextBox ID="txtcustomer" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label" placeholder="Customer Name"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >CHA Name</b>
<asp:TextBox ID="txtcha" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder="CHA Name"
runat="server"   ></asp:TextBox>
</div>
</div> 
</div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="row">
        <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Container No</b>
<asp:TextBox ID="txtcontainerno" Style="text-transform: uppercase;" MaxLength="11"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Container No"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Size</b>
<asp:DropDownList Style="text-transform: uppercase;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsize_SelectedIndexChanged"  class="form-control text-label" ID="ddlsize" >
    <asp:ListItem Value="20">20</asp:ListItem>
    <asp:ListItem Value="40">40</asp:ListItem>
    <asp:ListItem Value="45">45</asp:ListItem>

</asp:DropDownList>    
</div>
</div>
        <div class="col-sm-5 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Account Heads</b>
<asp:DropDownList Style="text-transform: uppercase;" runat="server"  class="form-control text-label" ID="ddlaccntheads" >

</asp:DropDownList>    
</div>
</div>
        
    </div>
            <div class="row">
                <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Service Type</b>
<asp:DropDownList Style="text-transform: uppercase;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlservicetype_SelectedIndexChanged"  class="form-control text-label" ID="ddlservicetype" >

</asp:DropDownList>    
</div>
</div>
        <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Material Type</b>
<asp:DropDownList Style="text-transform: uppercase;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmaterialtype_SelectedIndexChanged"  class="form-control text-label" ID="ddlmaterialtype" >
</asp:DropDownList>      
</div>
</div>
                <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Amount</b>
<asp:TextBox ID="txtamount" onkeypress="return ValidateAmount()" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" placeholder="Amount"></asp:TextBox>     
</div>
</div>
                
                <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Packages</b>
<asp:TextBox ID="txtpkgs" onkeypress="return ValidateAmount()" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" placeholder="Packages"></asp:TextBox>     
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
    

            <div class="row">
         <div class="col-lg-12 col-xs-12 text-label">
             <div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;">
                 <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnRowDataBound="grdcontainer_RowDataBound">
                     <Columns>
                         <asp:TemplateField>
    <ItemTemplate>
            <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger' OnClick="lnkCancel_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Auto_id")%>' runat="server" 
                                                            ><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
        <asp:Label ID="lblautoid"  runat="server" Visible="false" text='<%#Eval("Auto_id")%>'></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" Width="40px"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblntamnt" runat="server" text='<%#Eval("Containerno")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>                       
                                                
    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblsize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
                          <asp:TemplateField HeaderText="Packages" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblpkgs" runat="server" text='<%#Eval("Pkgs")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblaccntid"  runat="server" Visible="false" text='<%#Eval("AccountID")%>'></asp:Label>
        <asp:Label ID="lblaccntname" Visible="false" runat="server" text='<%#Eval("accountname")%>'></asp:Label>

        <asp:DropDownList Style="text-transform: uppercase;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlaccntheadsgrid_SelectedIndexChanged"  class="form-control text-label" ID="ddlaccntheadsgrid" >
</asp:DropDownList>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField> 
                         <asp:TemplateField HeaderText="Service Type" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblservicetype" Visible="false" runat="server" text='<%#Eval("SerivceName")%>'></asp:Label>
        <asp:DropDownList Style="text-transform: uppercase;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlservicetypegrid_SelectedIndexChanged" class="form-control text-label" ID="ddlservicetypegrid" >
</asp:DropDownList>  
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Material Type" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblmaterialtype" Visible="false" runat="server" text='<%#Eval("MaterialType")%>'></asp:Label>
        <asp:DropDownList Style="text-transform: uppercase;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmaterialtypegrid_SelectedIndexChanged"  class="form-control text-label" ID="ddlmaterialtypegrid" >
</asp:DropDownList> 
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>                                                 
                          <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblamount" Visible="false" runat="server" text='<%#Eval("Amount")%>'></asp:Label>
        <asp:TextBox ID="txtamountgrid" Width="100px" AutoPostBack="true" OnTextChanged="txtamountgrid_TextChanged"  onkeypress="return ValidateAmount()" text='<%#Eval("Amount")%>' Style="text-transform: uppercase;text-align:right" class="form-control text-label form-cascade-control"
runat="server" placeholder="Amount"></asp:TextBox> 
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>

                        
                     </Columns>
                 </asp:GridView>
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
</asp:Panel>
                        
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
                           
<a href="GenerateSSR.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="SSRSummary.aspx" target="_blank"><b style="color:blue">Click here to view SSR Summary</b> </a>
</div>
</div>
                         
</div>
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
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenWOPrint()"  aria-hidden="true">
Yes 
</button>
<a href="GenerateSSR.aspx" class="btn btn-danger ">No</a>
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
           var txtssrnoprint = document.getElementById('<%= txtssrnoprint.ClientID%>').value;
    
           var url = "../Report_Bond/SSRPrint.aspx?SSRNo=" + txtssrnoprint
    window.open(url);

}

</script>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtnocno = document.getElementById('<%= txtnocno.ClientID%>').value;
    var ddlSSRType = document.getElementById('<%= ddlSSRType.ClientID%>').value;

    document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary disabled");      
               

var blResult = Boolean;
blResult = true;
 

                   
if (txtnocno == "") {
document.getElementById('<%= txtnocno.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
    if (ddlSSRType == 0) {
        document.getElementById('<%= ddlSSRType.ClientID%>').style.borderColor = "red";
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
            var txtcontainerno = document.getElementById('<%= txtcontainerno.ClientID%>').value;
            var ddlaccntheads = document.getElementById('<%= ddlaccntheads.ClientID%>').value;
            var ddlservicetype = document.getElementById('<%= ddlservicetype.ClientID%>').value;
            var ddlmaterialtype = document.getElementById('<%= ddlmaterialtype.ClientID%>').value;
            var txtpkgs = document.getElementById('<%= txtpkgs.ClientID%>').value;
           var txtamount = document.getElementById('<%= txtamount.ClientID%>').value;

           //document.getElementById('<%= lnkadd.ClientID%>').value = "Please Wait...";
           document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info disabled");
           var blResult = Boolean;
           blResult = true;

           if (txtinvno == "") {
               document.getElementById('<%= txtcontainerno.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
            if (ddlaccntheads == 0) {
                document.getElementById('<%= ddlaccntheads.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlservicetype == 0) {
                document.getElementById('<%= ddlservicetype.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlmaterialtype == 0) {
                document.getElementById('<%= ddlmaterialtype.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtpkgs == "") {
               document.getElementById('<%= txtpkgs.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
           
           if (txtamount == "") {
               document.getElementById('<%= txtamount.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }

           if (blResult == false) {
               alert('Please fill the required fields!');
               //document.getElementById('<%= btnsave.ClientID%>').value = "Save";
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
