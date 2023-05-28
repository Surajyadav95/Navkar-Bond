<%@ Page Title="Bond |Ex Assessment Summary" Language="VB" MasterPageFile="../Bond/User.master" AutoEventWireup="false"
CodeFile="ExAssessmentSummary.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title> Bond |  Ex Assessment Summmary </title>
</head>
<style>
.text-center{
text-align:center
}
</style>
<div class="page-container">
<div class="pageheader">
<h3>
Ex Assessment Summmary  
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
<div class="col-md-5 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate"  style="width: 150px;" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
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
</asp:DropDownList>
                                               
</div>

</div>
<div class="col-md-2 col-xs-12" style="display:none;"  id="divassessno" runat="server">
<div class="form-group text-label">
<b >AssessNo</b>
<asp:TextBox ID="txtAssessno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Assess No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-2 col-xs-12" style="display:none;"  id="divnocno" runat="server">
<div class="form-group text-label">
<b >NOC NO</b>
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
                                        
<asp:Label ID="lblFD_ID" Visible="false" runat="server" Text=""></asp:Label>

<div class="col-md-1  col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" ></asp:Button>
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
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="OnPageIndexChanging"  AllowPaging="true" PageSize="6" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField>
<ItemTemplate>

<a  href='<%# "../Report_Bond/BondAssessmentPrint.aspx?AssessNo=" & (DataBinder.Eval(Container.DataItem, "AssessNo")).ToString() & "&WorkYear=" & (DataBinder.Eval(Container.DataItem, "WorkYear")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
                                                          
                                                          
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="70px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Assess No" HeaderStyle-CssClass="text-center">
<ItemTemplate>

<asp:Label runat="server" ID="lblassessNo" Text='<%#Eval("AssessNo")%>'></asp:Label>

</ItemTemplate>

<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>

<asp:BoundField  DataField="AssessDate" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" HeaderText="Assess Date"></asp:BoundField>
<asp:BoundField DataField="NOCNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="NOC No"  ></asp:BoundField>
<asp:BoundField DataField="GSTName" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="left" HeaderText="Party Name"  ></asp:BoundField>
<asp:BoundField DataField="GrandTotal" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderText="Grand Total"  ></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
                           
</div>
</div>
</div>
</div>
</div>
</div>                              
</div>



      
</asp:Content>
