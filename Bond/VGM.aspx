<%@ Page Title="VGM" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="VGM.aspx.vb" Inherits="Summary_BCYMovement" culture="en-GB" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <head>
<title>VGM</title>       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> VGM
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
           
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
                    <div class="col-sm-4 col-xs-12">
<div class="form-group text-label" style="text-decoration-color:black">
<b >Container No</b>
<asp:TextBox ID="txtContainerNo" Style="text-transform: uppercase;" MaxLength="11"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Container No"></asp:TextBox>   
    
    <asp:TextBox ID="txtVGMNo" Style="text-transform: uppercase;display:none" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Container No"></asp:TextBox> 
                                    
</div>
</div>
                    <div class="col-md-2 col-xs-12" style="display:none">                 
<div class="form-group text-label" style="padding-top: 25px;">
<asp:CheckBox ID="chkISVGM" runat="server"  Checked="true"  />
<asp:HiddenField ID="hdltax" runat="server" Value="0" />
<asp:Label ID="TaxLabel" runat="server" AssociatedControlID="chkISVGM" CssClass="inline"></asp:Label>
<b>Is VGM?</b>
</div>
</div>
                    <div class="col-sm-1">
<div class="form-group" style="padding-top: 20px">
<asp:Button ID="btnVGMShow" class="btn btn-info " runat="server" OnClick="btnVGMShow_Click" OnClientClick="GetContainerNoDetails()"
Text="Show" />
</div>


</div>
                </div>
    <div class="row">
         <div class="col-sm-12 col-lg-12 text-label" runat="server">
        <div class="table-responsive scrolling-table-container1">
<asp:GridView ID="grdVGMDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<Columns>
         <asp:TemplateField HeaderText="Container No" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblContainerNo" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContainerNo")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Size" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblSize" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "size")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Type" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lbltype" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Type")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Cargo Type" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblCargoType" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CargoType")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Tare Weight" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

    <asp:TextBox ID="txtTareweight" onchange="return TextChange(this)" runat="server" CssClass="form-control" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "Tareweight")%>'></asp:TextBox>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Cargo Wt" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>


        <asp:TextBox ID="txtCargoWt" onchange="return TextChange(this)" runat="server" CssClass="form-control" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "CargoWt")%>'></asp:TextBox>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Gross Wt" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>
    <asp:TextBox ID="txtGrossWt" ReadOnly="true" runat="server" CssClass="form-control" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "GrossWt")%>'></asp:TextBox>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Class" Visible="false"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblClass" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Class")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="UN No" Visible="false"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblUNNO" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UNNO")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Slip No" Visible="false"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblSlipNo" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SlipNo")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>  
    <asp:TemplateField HeaderText="Slip Date" Visible="false"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblSlipdate" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Slipdate")%>'></asp:Label>
<asp:Label ID="lblShipperID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "shipperID")%>'></asp:Label>
<asp:Label ID="lblEntryID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "entryID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField> 
    <asp:TemplateField HeaderText="Pay Load" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:TextBox ID="txtPayLoadID" runat="server" CssClass="form-control" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "PayLoadID")%>'></asp:TextBox>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Contact Person" Visible="true" HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" Width="250px" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>'></asp:TextBox>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField> 
    <asp:TemplateField HeaderText="Contact No" Visible="true" HeaderStyle-CssClass="center1">
<ItemTemplate>
<asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" Width="150px" Text='<%# DataBinder.Eval(Container.DataItem, "ContactNo")%>'></asp:TextBox>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>                             
</Columns>
</asp:GridView>
</div>
    </div>
    </div>

           <div class="col-sm-1">
<div class="form-group" style="padding-top: 0px">
<asp:Button ID="btnVGMSend" class="btn btn-primary" runat="server" OnClick="btnVGMSend_Click" OnClientClick="GetVMGSend"
Text="Send" />
</div>
</div>  
      <asp:Panel ID="pnlPerson" Visible="false" runat="server" font-family="Segoe UI">
        <rsweb:ReportViewer ID="ReportViewer1" Width="900PX" Height="1100px" runat="server">
         
        </rsweb:ReportViewer>
             </asp:Panel>
    <div class="row" style="display:none">
        <div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="VGMReport.aspx" target="_blank"><b style="color:blue">Click here to view VGM Report</b> </a>
</div>
</div>
    </div>                  
</asp:Panel>
                        
</div>
</div>
               
</fieldset>

</div>
</div>
                               
</div>
                           
<div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I1" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">

<asp:Label ID="Label2" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">

<button class="btn btn-info " id="Button1" data-dismiss="modal" runat="server" onclick="OpenVGMPrint()"  aria-hidden="true">
Ok 
</button>
</div>
</div>

</ContentTemplate>

</asp:UpdatePanel>
</div>
</div>              
                     
                       
                       
</div>

</div>
       
         
</div>
   
    <script>
        function GetVMGSend() {
            document.getElementById('<%= btnVGMSend.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnVGMSend.ClientID%>').setAttribute("class", "btn btn-primary disabled");
        }
        function GetContainerNoDetails() {
            var txtContainerNo = document.getElementById('<%= txtContainerNo.ClientID%>').value;

            document.getElementById('<%= btnVGMShow.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnVGMShow.ClientID%>').setAttribute("class", "btn btn-info disabled");

            var blResult = Boolean;
            blResult = true;

            if (txtContainerNo == "") {
                document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
                document.getElementById('<%= btnVGMShow.ClientID%>').value = "Show";
                document.getElementById('<%= btnVGMShow.ClientID%>').setAttribute("class", "btn btn-info");
            }
            return blResult;
        }
        function OpenVGMPrint() {
            var txtVGMNo = document.getElementById('<%= txtVGMNo.ClientID%>').value;

            var url = "../Report_Bond/VGMRDLC.aspx?VGMNo=" + Server.UrlEncode(Encrypt(txtVGMNo.ToString()))
            window.open(url);

        }
        function TextChange(obj) {
            var row = obj.parentNode.parentNode;
            var sum = 0;
            var multiply = 0;
            if ((parseFloat(row.cells[4].getElementsByTagName("input")[0].value) != 0) && (parseFloat(row.cells[5].getElementsByTagName("input")[0].value) != 0)) {
                sum = (row.cells[4].getElementsByTagName("input")[0].value);
                multiply = (row.cells[5].getElementsByTagName("input")[0].value);
                row.cells[6].getElementsByTagName("input")[0].value = parseFloat(multiply) + parseFloat(sum);
            }
            
        }
    </script>
</asp:Content>
