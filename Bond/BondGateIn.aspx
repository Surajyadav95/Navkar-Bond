<%@ Page Title="Bond | Bond Gate In" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="BondGateIn.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond | Bond Gate In</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Bond Gate In
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
<b >Gate No</b>
<asp:TextBox ID="txtgateno" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" placeholder="NEW"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-3 col-xs-12" style="display:none">
        <asp:TextBox ID="txtssrnoprint" runat="server" ></asp:TextBox>
    </div>
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Gate In Date</b>
<asp:TextBox ID="txtgateDate" TextMode="Date" class="form-control text-label"  placeholder="dd-MM-yyyy"
runat="server"></asp:TextBox>
</div>
</div>
  
        
    </div>
            
    <div class="row">
        <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >BOE No</b>
<asp:TextBox ID="txtboeNo"    class="form-control text-label form-cascade-control"
runat="server" placeholder="BOE NO"></asp:TextBox>     
</div>
</div>
   
    
    <div class="col-md-1 col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show"  OnClick="btnsearch_Click" ></asp:Button>
</div>              
</div>
         </div>        
          
    <div class="row">
<div class="col-md-10 col-xs-10 text-label "  >
<div class="table-responsive scrolling-table-container"  >
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"    AllowPaging="true" PageSize="10">
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
     
        <asp:TemplateField HeaderText="Select">
<ItemTemplate>

<asp:CheckBox ID="chkright" Text=""  Checked='<%#Eval("check")%>' runat="server" AutoPostBack="true" OnCheckedChanged="chkright_CheckedChanged"  />
</ItemTemplate>
                                                       
</asp:TemplateField>
     <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblcontainerno" runat="server" text='<%#Eval("Containerno")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>                       
                                                
    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblsize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>

    <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Label ID="lblType" runat="server" text='<%#Eval("ContainerType")%>'></asp:Label>
        <asp:Label ID="lbltypeid" Visible="false" runat="server" text='<%#Eval("Type")%>'></asp:Label>
        <asp:Label ID="lblnocno" Visible="false" runat="server" text='<%#Eval("NocNO")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>

    <asp:TemplateField HeaderText="Trailer No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:TextBox ID="txttrailerno" Width="150px"    text='<%#Eval("TrailerNo")%>' Style="text-transform: uppercase;text-align:left" class="form-control text-label form-cascade-control"
runat="server" placeholder="Trailer No"></asp:TextBox> 
            </ItemTemplate>
    <ItemStyle HorizontalAlign="left"  />
</asp:TemplateField>

      <asp:TemplateField HeaderText="Seal No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:TextBox ID="txtsealno" Width="100px"    text='<%#Eval("SealNo")%>' Style="text-transform: uppercase;text-align:left" class="form-control text-label form-cascade-control"
runat="server" placeholder="Seal No"></asp:TextBox> 
            </ItemTemplate>
    <ItemStyle HorizontalAlign="left"  />
</asp:TemplateField>

</Columns>

</asp:GridView>
</div>
</div>
</div>       

    <div class="col-md-1 col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnSave" runat="server"
class="btn btn-primary btn-sm outline" Text="Save" OnClick="btnSave_Click"   ></asp:Button>
</div>
</div>

<div class="col-md-1 col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnclear" runat="server"
class="btn btn-primary btn-sm outline" Text="Clear" ></asp:Button>
</div>
</div>

    <div class="col-lg-12" style="margin-left:250px; top:-30px;">
<div class="form-group ">
<a href="BondGateInSummary.aspx" target="_blank"><b style="color:blue">Click here to view Bond Gate In Summary</b> </a>
</div>
</div>
</asp:Panel>
                        
</div>
</div>


 
<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         

                                
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I1" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">

<asp:Label ID="lblSession" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">
<a href="BondGateIn.aspx" class="btn btn-info btn-block">OK</a>
</div>
</div>

    </ContentTemplate></asp:UpdatePanel>                  
</div>
</div>       
</fieldset>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 

</div>
       
         
</div>
 
</asp:Content>
