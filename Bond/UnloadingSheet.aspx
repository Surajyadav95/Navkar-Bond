<%@ Page Title="Bond | Unloading Sheet" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="UnloadingSheet.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>
 


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Bond | Unloading Sheet</title>
      
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Unloading Sheet Entry 
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
         
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-9 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">

            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true">
<div class="row">
<div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Unloading Sheet No</b>
<asp:TextBox ID="txtbondno" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>



<div class="col-sm-1 col-xs-6" style="display:none">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
OnClientClick="return OpenItembond();">  
<i class=" fa fa-search" aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
<asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />


<div class="col-sm-3  col-xs-12" ">                                      
<div class="form-group text-label">
<b >Unloading Date</b>
<asp:TextBox ID="txtbondindate"  placeholder="dd-mm-yyyy" runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
</div>                        
</div>
                                    
<div class="col-sm-3  col-xs-12" style="display:none">                                      
<div class="form-group text-label">
<b >Bond Type</b>
<asp:TextBox ID="textbond" runat="server" ReadOnly="true"  class="form-control text-label " AutoPostBack="true">
</asp:TextBox>
</div>
</div>

<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">                                        
<b >NOC No</b>
<asp:TextBox ID="txtNOCNo" Style="text-transform:uppercase"  AutoPostBack="true"    class="form-control text-label"  placeholder="NOC No"
runat="server" OnTextChanged="txtnocno_TextChanged"></asp:TextBox>
</div>
</div>

<div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="LinkButton1" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return OpenItembond();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>

</div>
</asp:Panel>
    <asp:Panel runat="server" Enabled="false">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
                                        
<b >BE No</b>
<asp:TextBox ID="txtbe" Style="text-transform:uppercase" class="form-control text-label"  placeholder="BE No"
runat="server"></asp:TextBox>
</div>
</div>

</ContentTemplate></asp:UpdatePanel>

<div class="col-sm-3  col-xs-12"  >                                      
<div class="form-group text-label">
<b >BE Date</b>
<asp:TextBox ID="txtbedate"  placeholder="dd-mm-yyyy" TextMode="Date" runat="server" Class="form-control text-label"></asp:TextBox>

</div>                        
</div>
                          
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >IGM-No</b>
<asp:TextBox ID="Txtigm" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="IGM-No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Item-No</b>
<asp:TextBox ID="txtitm" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Item-No"
runat="server"   ></asp:TextBox>
</div>
</div>
                            
                              
</div>
<div class="row">
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Weeks</b>
<asp:TextBox ID="txtweek" Style="text-transform:uppercase" AutoPostBack="true" class="form-control text-label"  placeholder="Weeks"
runat="server"   ></asp:TextBox>
</div>
</div>
    <asp:Panel runat="server" Enabled="false">
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Days</b>
<asp:TextBox ID="txtday" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Days"
runat="server"   ></asp:TextBox>
</div>
</div>

                             
<div class="col-sm-3  col-xs-12" style="display:none">                                      
<div class="form-group text-label">
<b >NOC Date</b>
<asp:TextBox ID="txtNocDate"  placeholder="dd-mm-yyyy" runat="server"  Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
<div class="col-sm-3  col-xs-12"">                                      
<div class="form-group text-label">
                                             
<b >Expiry Date</b>
<asp:TextBox ID="txtExpiryDate" placeholder="dd-mm-yyyy" TextMode="Date" runat="server" Class="form-control text-label"></asp:TextBox>
</div>
</div>
        </asp:Panel>
</div>
<div class="row">
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>Importer</b>
<%--<asp:textbox ID="txtConsi" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"  class="form-control text-label"  placeholder="Consignee">
</asp:textbox>--%> 
    <asp:DropDownList ID="ddlConsignee" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
    </asp:DropDownList>  
</div>
</div>

<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> CHA Name</b>
<%--<asp:textbox ID="txtCha" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"  class="form-control text-label" placeholder="Cha" >
</asp:textbox>--%>   
    <asp:DropDownList ID="ddlCHA" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
    </asp:DropDownList>
</div>
</div>

</div>

<div class="row">
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> Customer Name</b>
<%--<asp:textbox  ID="txtCustomer" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" ReadOnly="true"  class="form-control text-label" placeholder="Customer">
</asp:textbox>--%>   
    <asp:DropDownList ID="ddlCustomer" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
    </asp:DropDownList>
</div>
</div>

<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b >Commodity</b>
<asp:TextBox ID="txtcommodity" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Commodity"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
<div class="row" style="display:none">                                                      

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Quantity</b>
<asp:TextBox ID="txtqty" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label"  onkeypress="return ValidateQty()" placeholder="Quantity"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Unit</b>
<asp:TextBox ID="txtunit" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label"  placeholder="Unit"
runat="server"   ></asp:TextBox>
                                      
</div>
</div>                          

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Gross Wt(kgs)</b>
<asp:TextBox ID="txtgrosss" Style="text-transform:uppercase" ReadOnly="true"  class="form-control"  onkeypress="return ValidateQty()"  placeholder="Gross Wt"  
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b > Storage Area(sqm)</b>
<asp:TextBox ID="txtstorage" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label"    placeholder="Storage Area"  
runat="server"   ></asp:TextBox>
</div>
</div>
    
</div>

<div class="row">
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Value</b>
<asp:TextBox ID="txtvalue" Style="text-transform:uppercase" class="form-control text-label"   onkeypress="return Validatevalue()"  placeholder="Value"  
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Duty</b>
<asp:TextBox ID="txtduty" Style="text-transform:uppercase" class="form-control text-label"  onkeypress="return Validateduty()"   placeholder="Duty"  
runat="server"   ></asp:TextBox>
</div>
</div>
</div>
           </asp:Panel>
</div>
</div>

<asp:Panel ID="Panel1" runat="server" Enabled="true">
                      
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar" style="padding-left:0px;padding-right:0px">
<div class="panel-heading">
<h3 class="panel-title">
Warehouse Details
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
<div class="panel-body">

                            
<div class="row">
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Section</b>
<asp:DropDownList ID="ddlSection" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Bond No</b>
<asp:TextBox ID="txtbondin" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Bond No"  
runat="server"   ></asp:TextBox>
</div>
</div>
<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
<Triggers>
<asp:PostBackTrigger ControlID="textbonddate" />


</Triggers>
<ContentTemplate> 
<div class="col-sm-3  col-xs-12"">                                      
<div class="form-group text-label">
                                             
<b >Bond Date</b>
<asp:TextBox ID="textbonddate" AutoPostBack="True" Text="false" placeholder="dd-mm-yyyy"  OnTextChanged="textbonddate_TextChanged" TextMode="Date"  runat="server" Class="  form-control text-label"></asp:TextBox>
</div>
</div>



        <div class="col-sm-3  col-xs-12"">                                      
<div class="form-group text-label">                                             
<b >Bond Expiry Date</b>
<asp:TextBox ID="txtbondexpdate"   Text=""  readonly="true"  runat="server" Class="  form-control text-label"></asp:TextBox>
</div>
</div>
    </ContentTemplate>                
</asp:UpdatePanel>
</div>
    
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">

<ContentTemplate>
    <asp:Panel runat="server" Enabled="false">
<div class="row">

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Quantity</b>
<asp:TextBox ID="txtqy" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()" placeholder="Quantity"
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Unit</b>
<%--<asp:TextBox ID="txtuni" Style="text-transform:uppercase" class="form-control text-label"  placeholder="UNIT"
runat="server"   ></asp:TextBox>--%>
<asp:DropDownList ID="ddlunit" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b > Storage Area(sqm)</b>
<asp:TextBox ID="txtStoragearea" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Storage Area"  
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Gross Wt(kgs)</b>
<asp:TextBox ID="txtkgs" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()" placeholder="Gross Wt(KGS)"  
runat="server"   ></asp:TextBox>
</div>
</div>


                                                          
</div>
         </asp:Panel>
<div class="row">
    <div class="col-sm-3  col-xs-12"  >
<div class="form-group text-label">
<b> Status</b>
<asp:DropDownList ID="ddlstatus" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="FCL">FCL</asp:ListItem>
<asp:ListItem Value="LCL">LCL</asp:ListItem>
</asp:DropDownList>   
</div>
</div>
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b > Serial No</b>
<asp:TextBox ID="txtserial" Style="text-transform:uppercase" class="form-control text-label"    placeholder="serial No"  
runat="server"   ></asp:TextBox>
</div>
</div> 

    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Location</b>
<asp:TextBox ID="txtlocation" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Location"  
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Bond Reg No/PG No</b>
<asp:TextBox ID="txtreg" Style="text-transform:uppercase" class="form-control text-label"  placeholder=" Reg No/PG No"
runat="server"   ></asp:TextBox>
</div>
</div>
 
                             
                           
</div>
         <div class="row">
             <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b > PO No</b>
<asp:TextBox ID="txtPONo" Style="text-transform:uppercase" MaxLength="20" class="form-control text-label" placeholder="PO No"  
runat="server"></asp:TextBox>
</div>
</div> 
         </div>                             

</ContentTemplate>
</asp:UpdatePanel>
       
</div>
</div>
  
    <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar" style="padding-left:0px;padding-right:0px">
<div class="panel-heading">
<h3 class="panel-title">
Godown Details
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
<div class="panel-body">

      <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
    <asp:Panel runat="server" ID="pnlWarehouse">
    <div class="col-sm-4 col-xs-12"  >
<div class="form-group text-label">
<b> Warehouse</b>
<asp:DropDownList ID="ddlhous" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>   
</div>
</div>
        </asp:Panel>
<div class="col-sm-4 col-xs-12"  >
<div class="form-group text-label">
<b> Godown</b>
<asp:DropDownList ID="ddlgodown" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>   
</div>
</div>

<div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b >Bin No</b>
<asp:DropDownList ID="ddllot" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label"  
></asp:DropDownList>
</div>
</div>

<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Quantity</b>
<asp:TextBox ID="textty" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()" placeholder="Quantity"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Weight</b>
<asp:TextBox ID="txtWeight" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()" placeholder="Weight"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b >Batch No</b>
<asp:TextBox ID="txtBatchNo" Style="text-transform:uppercase" class="form-control text-label" MaxLength="10" placeholder="Batch No"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b >Description</b>
<asp:TextBox ID="txtDescription" Style="text-transform:uppercase" TextMode="MultiLine" class="form-control text-label" placeholder="Description"
runat="server"   ></asp:TextBox>
</div>
</div>

    <div class="col-sm-8 col-xs-12">
<div class="form-group text-label">
<b >Remarks</b>
<asp:TextBox ID="txtRemarksGodown" MaxLength="150" Style="text-transform:uppercase" TextMode="MultiLine" Rows="2" class="form-control text-label" placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-sm-1" style="padding-right:8px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="Button2" class="btn btn-primary btn-sm" runat="server" OnClick="Button2_Click"    
Text="Add"  OnClientClick="return ValidationHous()"/>
</div>
                                              
</div>
</div>

<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:12px;margin-right:0px;">
<asp:GridView ID="grdgodown" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>
<asp:TemplateField>
<ItemTemplate>
                                                               
                                                            
<asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete"                                                         
    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoIdTemp")%>' runat="server" OnClick="lnkDelete_Click1"
    ></asp:LinkButton>

   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="80px" />
</asp:TemplateField>
<asp:BoundField DataField="Godown" HeaderText="Godown"></asp:BoundField>

<asp:BoundField DataField="LotNo" HeaderText="Bin No"></asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="BatchNo" HeaderText="Batch No"></asp:BoundField>
<asp:BoundField DataField="Weight" HeaderText="Weight"></asp:BoundField>
<asp:BoundField DataField="Description" HeaderText="Description"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
</div>
</div>

    </ContentTemplate>
</asp:UpdatePanel>

    </div></div>                                 


                         
                    
</asp:Panel>
<asp:Panel ID="Panel3" runat="server" Enabled="true">
                               
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar" style="padding-left:0px;padding-right:0px">
<div class="panel-heading">
<h3 class="panel-title">
Container Details
                
<%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
<div class="panel-body">
<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
                   
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Container No</b>
<asp:TextBox ID="txtcontainer" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Container No"  
runat="server" MaxLength="11"></asp:TextBox>
</div>
</div>
                                           
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Quantity</b>
<asp:TextBox ID="textqt" Style="text-transform:uppercase" class="form-control text-label"  onkeypress="return ValidateQty()"  placeholder="Quantity"  
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Size</b>
<asp:DropDownList  ID="ddlsize" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="1">20</asp:ListItem>
<asp:ListItem Value="2">40</asp:ListItem>
<asp:ListItem Value="3">45</asp:ListItem>
</asp:DropDownList>
</div>
</div>


                                            
                                            
<div class="col-sm-1" style="padding-right:8px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="Button1" class="btn btn-primary btn-sm" runat="server" OnClick="Button1_Click"   
Text="Add"  OnClientClick="return ValidationAdd()"/>
</div>                                              
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>

<div class="row">
<asp:UpdatePanel ID="up_grid1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
<div class="form-group text-label">
<div class="col-sm-4 col-xs-12"  style="margin-left:24px">
<div class="col-sm-4 col-xs-4">
<b>20:</b>
<asp:Label runat="server" ID="lblA" ></asp:Label>
&nbsp</div>
<div class="col-sm-4 col-xs-4">
<b>40:</b>
<asp:Label runat="server" ID="lblB" ></asp:Label>
&nbsp</div>
<div class="col-sm-4 col-xs-4">
<b>45:</b>
<asp:Label runat="server" ID="lblC" ></asp:Label>
&nbsp</div>
</div>
                                 
</div>
</div>
<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>
<asp:TemplateField>
<ItemTemplate>
                                                               
                                                            
<asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete"                                                         
    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoIdTemp")%>' runat="server"  OnClick="lnkDelete_Click"
    ></asp:LinkButton>

   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="140px" />
</asp:TemplateField>
<asp:BoundField DataField="Container_Num" HeaderText="Container No"></asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Container_Size" HeaderText="Size"></asp:BoundField>
                                                 
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
</asp:Panel>

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
  <button class="btn btn-info btn-block" id="SaveOk" data-dismiss="modal" runat="server" onserverclick="SaveOk_ServerClick">OK</button>                 
<%--<a href="UnloadingSheet.aspx" class="btn btn-info btn-block">OK</a>--%>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
        <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblPrintQue"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenUnLoadingPrint()"  aria-hidden="true">
Yes 
</button>
<a href="UnloadingSheet.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>                 

</fieldset>
<asp:Panel ID="Panel4" runat="server" Enabled="true">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar" style="padding-left:0px;padding-right:0px">
<div class="panel-heading">
<h3 class="panel-title">
Others
                
<%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>

                        
<div class="panel-body">
<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b >Equipment Type</b>
<asp:DropDownList  ID="ddlEquipment" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                        
</asp:DropDownList>
</div>
</div>
<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b >Executive Name</b>
<asp:DropDownList ID="ddlSurveyor" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
<%--<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="1">values</asp:ListItem>--%>
</asp:DropDownList>
</div>
</div> 
    <div class="col-md-6  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b> Equipment No</b>
                                      <asp:DropDownList ID="ddlEquipmentNo" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div> 
<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b >Remark</b>
<asp:TextBox ID="txtremarks" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Remarks"  
TextMode="MultiLine"   runat="server"   ></asp:TextBox>
</div>
</div>
</div></div>
</asp:Panel>
<div class="row">
<div class="col-md-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server"  OnClick="btnsave_Click"     
Text="Save" OnClientClick="return ValidationSearch()"/>
</div>                                
</div>

<div class="col-md-1" style="padding-left:22px;">
<div class="form-group" style="padding-top:0px">
</div>
<a href="BondIn.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm">
Clear
</a>
</div>

<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="UnloadingSheetSummary.aspx" target="_blank"><b style="color:blue">Click here to view Unloading Summary</b> </a>
</div>
</div>
</div>

</div>

    <div class="col-md-3 pull-md-right sidebar" >
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title" style="padding-bottom: 0px !important; ">
<i class="fa fa-cube"></i>&nbsp;    Unloading Sheets
<%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>   
<div class="panel-body">
<asp:Repeater ID="rptNOLIst" runat="server">
<ItemTemplate>
<div>                             
<a href="#">                         
<strong><asp:Label runat="server"  Text='<%#Eval("DepositNo")%>' ID="lblNONumber" style="text-transform:uppercase;"></asp:Label></strong></a>                                                     
&nbsp;&nbsp; | &nbsp;&nbsp; <asp:Label runat="server" Text='<%#Eval("AddDate")%>' Id="lblDate" style="text-transform:uppercase;"></asp:Label>
        <br/>Generated By: &nbsp;<asp:Label runat="server" Text='<%#Eval("Users")%>' Id="lbluser" style="text-transform:uppercase;"></asp:Label>
                                  
<br />
<asp:Label runat="server" >----------------------------------</asp:Label>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
</div>
                           
</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                          
                                     
<script type="text/javascript">
    var popup;
    function OpenUnLoadingPrint() {

        var txtbondno = document.getElementById('<%= txtbondno.ClientID%>').value;
        var url = "../Report_Bond/UnloadingSheetPrint.aspx?SheetNo=" + txtbondno
        document.getElementById('<%= txtbondno.ClientID%>').value = "";
            window.open(url);
        }
</script>

<script type="text/javascript">
    $(document).ready(function () {
        calculateExpiryDate();
    });
    function calculateExpiryDate() {

        var bondInDate = document.getElementById('<%= textbonddate.ClientID%>').value;
        


    var bondOutDate = new Date(new Date(bondInDate).setFullYear(new Date(bondInDate).getFullYear() + 1));
    //var formattedDate = formatDate(bondOutDate);
    //alert(formattedDate);
    var expiryDate;
    var day1 = bondOutDate.getDate();
    //alert(day1);
    if (day1 == 01) {
        //alert("day1 selected");
        expiryDate = formateDateForFirstDate(bondOutDate);
        //alert(expiryDate);
    }
    else {
        expiryDate = formatDate(bondOutDate);
    }

    // document.getElementById('<%= txtbondexpdate.ClientID%>').value = formattedDate;
        document.getElementById('<%= txtbondexpdate.ClientID%>').value = expiryDate;
        

}

    function formatDate(date) {
        var months = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
        var day = date.getDate() - 1;
        var year = date.getFullYear();
        var month = months[date.getMonth()];
        if (day === '0') {
            day = '30';
        }
        if (month == '02') {
            day = '28';
        }
        return day + "-" + month + "-" + year;
    }

    function formateDateForFirstDate(date) {

        var months = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
        var day = date.getDate();
        var year = date.getFullYear();
        var month = months[date.getMonth() - 1];

        if (month === undefined) {
            month = '12';
        }
        //alert(month);
        if (month === '01' || '03' || '05' || '07' || '08' || '10' || '12') {
            day = '31';

        }
        if (month === '04' || '06' || '09' || '11') {
            day = '30';
        }
        if (month === '02') {
            day = '28';
        }
        return day + "-" + month + "-" + year;

    }
</script>              
  
<script type="text/javascript">
function ValidationHous() {


var ddlgodown = document.getElementById('<%= ddlgodown.ClientID%>').value;
var ddllot = document.getElementById('<%= ddllot.ClientID%>').value;
    var textty = document.getElementById('<%= textty.ClientID%>').value;
    var txtBatchNo = document.getElementById('<%= txtBatchNo.ClientID%>').value;
    var txtWeight = document.getElementById('<%= txtWeight.ClientID%>').value;


    document.getElementById('<%= Button2.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= Button2.ClientID%>').setAttribute("class", "btn btn-primary disabled");
var blResult = Boolean;
blResult = true;



if (ddlgodown == 0) {
document.getElementById('<%= ddlgodown.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

<%--if (ddllot == 0) {
document.getElementById('<%= ddllot.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}--%>

if (textty == "") {
document.getElementById('<%= textty.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if (txtBatchNo == "") {
        document.getElementById('<%= txtBatchNo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtWeight == "") {
        document.getElementById('<%= txtWeight.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= Button2.ClientID%>').value = "Add";
    document.getElementById('<%= Button2.ClientID%>').setAttribute("class", "btn btn-primary");
}
return blResult;
}
</script>

<script type="text/javascript">
function ValidationAdd() {
var txtcontainer = document.getElementById('<%= txtcontainer.ClientID%>').value;
var textqt = document.getElementById('<%= textqt.ClientID%>').value;
var ddlsize = document.getElementById('<%= ddlsize.ClientID%>').value;
    document.getElementById('<%= Button1.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= Button1.ClientID%>').setAttribute("class", "btn btn-primary disabled");


var blResult = Boolean;
blResult = true;

if (txtcontainer == "") {
document.getElementById('<%= txtcontainer.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

if (textqt == "") {
document.getElementById('<%= textqt.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

if (ddlsize == 0) {
document.getElementById('<%= ddlsize.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}



if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= Button1.ClientID%>').value = "Add";
    document.getElementById('<%= Button1.ClientID%>').setAttribute("class", "btn btn-primary");
}
return blResult;
}
</script>


<script type="text/javascript">
function ValidationSearch() {
    var txtbe = document.getElementById('<%= txtbe.ClientID%>').value;
    var txtbedate = document.getElementById('<%= txtbedate.ClientID%>').value;
    var txtExpiryDate = document.getElementById('<%= txtExpiryDate.ClientID%>').value;
    var ddlConsignee = document.getElementById('<%= ddlConsignee.ClientID%>').value;
    var ddlCHA = document.getElementById('<%= ddlCHA.ClientID%>').value;
    var ddlCustomer = document.getElementById('<%= ddlCustomer.ClientID%>').value;
    var ddlSection = document.getElementById('<%= ddlSection.ClientID%>').value;
    var txtbondin = document.getElementById('<%= txtbondin.ClientID%>').value;
    var txtbondexpdate = document.getElementById('<%= txtbondexpdate.ClientID%>').value;
    var txtqy = document.getElementById('<%= txtqy.ClientID%>').value;
    var ddlunit = document.getElementById('<%= ddlunit.ClientID%>').value;
    var txtStoragearea = document.getElementById('<%= txtStoragearea.ClientID%>').value;
    var txtkgs = document.getElementById('<%= txtkgs.ClientID%>').value;
    var ddlhous = document.getElementById('<%= ddlhous.ClientID%>').value;
    var ddlEquipmentNo = document.getElementById('<%= ddlEquipmentNo.ClientID%>').value;


var blResult = Boolean;
blResult = true;

if (txtbe == "") {
    document.getElementById('<%= txtbe.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;
}
    if (txtbedate == "") {
        document.getElementById('<%= txtbedate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtExpiryDate == "") {
        document.getElementById('<%= txtweek.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlConsignee == 0) {
document.getElementById('<%= ddlConsignee.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
    }
    if (ddlCHA == 0) {
        document.getElementById('<%= ddlCHA.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlCustomer == 0) {
        document.getElementById('<%= ddlCustomer.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    
    if (txtbondexpdate == "") {
        document.getElementById('<%= textbonddate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    
    if (ddlhous == 0) {
        document.getElementById('<%= ddlhous.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlEquipmentNo == 0) {
        document.getElementById('<%= ddlEquipmentNo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>

     

<script type="text/javascript">
function Validateduty() {
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
function ValidateQty() {
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
function Validatevalue() {
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

$(document).ready(function () {

//alert('hi')
$('.dummy').datepicker({
format: 'dd-mm-yyyy',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,

})

});

</script>

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy1').datepicker({
format: 'dd-mm-yyyy',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,

})


});

</script>
<script type="text/javascript">
var popup;
//alert('hi')
function OpenItembond() {
//alert('hi')
    var url = "NOCSearchforUnloading.aspx"
//window.open(url);
popup = window.open(url, "Popup", "width=800,height=550");
popup.focus();
}
</script>
<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy2').datepicker({
format: 'dd-mm-yyyy',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,

})

});

</script>

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy3').datepicker({
format: 'dd-mm-yyyy',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})




});

</script>





</div>





</asp:Content>
