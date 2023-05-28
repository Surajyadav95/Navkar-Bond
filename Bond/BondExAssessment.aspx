<%@ Page Title="Bond | Bond Ex Assessment " Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="BondExAssessment.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Bond |    Bond Ex Assessment</title>
      
</head>
<style>
.scrolling-table-container {
height: 150px;
overflow-y: auto;
overflow-x: auto;
}
</style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Bond Ex Assessment 
</h3>
           
</div>
       
<div id="page-content">
        
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
          
<div class="page-container" ><asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                                
                                         
<div class="col-md-8 pull-md-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Bond Ex Assessment
                
<%--<td style ="width:50%;text-align:left"><b >SGST</b></td>--%>
</h3>
</div>
            
<div class="panel-body">
                        
<asp:Panel ID="Panel2" runat="server" Enabled="true">
<div class="row">
<asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
<asp:Button ID="btnIndentlist" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentlist_Click" />
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label" style="text-decoration-color:black">
<b >Bill No</b>
<asp:TextBox ID="txtbillno" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>                                        
</div>
</div>
<div class="col-sm-4 col-xs-12" style="display:none">
<asp:TextBox runat="server" ID="txtassessno" ></asp:TextBox>
<asp:TextBox runat="server" ID="txtworkyear" ></asp:TextBox>

</div>
                              
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
<b >Assessment Date</b>
<asp:TextBox ID="txtAssessmentDate"  placeholder="dd-MM-yyyy" TextMode="Date" runat="server"  Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
<b >Valid Upto Date</b>
<asp:TextBox ID="txtvalidDate" ReadOnly="true"  placeholder="dd-mm-yyyy" TextMode="Date"  runat="server" Class="dummy1 form-control text-label"></asp:TextBox>
    <asp:Label runat="server" ID="lblfrom" Visible="false"></asp:Label>
        
</div>                        
</div>
    <div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b >NOC No</b>
<asp:TextBox ID="txtNocNo" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label"  placeholder="NOC"
runat="server"    ></asp:TextBox>

</div>
</div>
</div>
<div class="row">

       <div class="col-sm-2 col-xs-12" ">                                      
<div class="form-group date text-label">
<b >Deposite No</b>
<asp:TextBox ID="txtdepositno" placeholder="Deposite No" runat="server" Class="form-control text-label" AutoPostBack="true" OnTextChanged="txtNocNo_TextChanged"></asp:TextBox>
            

</div>                        
</div>                         
<div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="LinkButton2" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return BondEx();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
<b >NOC Date</b>
<asp:TextBox ID="txtNocDate"  placeholder="dd-mm-yyyy"  ReadOnly="true"  runat="server" Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
    <%--<div class="col-sm-4 col-xs-12">
        <div class="form-group date text-label">
            <asp:TextBox runat="server" ID="TextBox1" ReadOnly = "true"></asp:TextBox>
           <img src="../img/calender.png" />
        </div>
    </div>--%>
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b >Bond In No</b>
<asp:TextBox ID="txtbondinno" runat="server" placeholder="Bond In No" ReadOnly="true" class="form-control text-label" AutoPostBack="false">                                        
</asp:TextBox>

</div>
</div>
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
<b >Bond In Date</b>
<asp:TextBox ID="txtbondindate"  placeholder="dd-mm-yyyy"  ReadOnly="true"  runat="server" Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
</div>

<div class="row">

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
                                        
<b >BOE No</b>
<asp:TextBox ID="txtboeno" Style="text-transform:uppercase" placeholder="BOE No"  ReadOnly="true" AutoPostBack="true"  class="form-control text-label"    
runat="server"  ></asp:TextBox>
</div>
</div>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >IGM-No</b>
<asp:TextBox ID="txtigm" Style="text-transform:uppercase" placeholder="IGM No"  ReadOnly="true" class="form-control text-label"   
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b >Bond Type</b>
<asp:TextBox ID="txtbondtype" runat="server" placeholder="Bond Type" ReadOnly="true" class="form-control text-label" AutoPostBack="false">                                        
</asp:TextBox>

</div>
</div>
    <div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
<b >Gate Pass Date</b>
<asp:TextBox ID="txtGatePassDate"  placeholder="dd-mm-yyyy" TextMode="Date" runat="server" Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
</div>

                         
<div class="row">
                            
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>  Customer Name  </b>
<asp:TextBox ID="txtCustomer" placeholder="Customer Name" AutoPostBack="false"  ReadOnly="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:TextBox> 
<asp:Label runat="server" ID="lblcustid" Visible="false"></asp:Label>   
</div>
</div>
<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b  >Customer Address</b>
<asp:TextBox ID="txtcustaddress" Style="text-transform:uppercase" TextMode="MultiLine" Rows="2"  ReadOnly="true" class="form-control text-label"   
runat="server"   ></asp:TextBox>
</div>
</div>

                                

</div>
                          

<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
                               
                             
                           
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> Importer Name</b>
<asp:TextBox ID="txtimporter" placeholder="Importer Name" AutoPostBack="false"  ReadOnly="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:TextBox> 
<asp:Label runat="server" ID="lblimporterid" Visible="false"></asp:Label>   
</div>
</div>

                        

                         
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> CHA Name</b>
<asp:TextBox ID="txtcha" AutoPostBack="false" placeholder="CHA Name"  ReadOnly="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:TextBox>  
<asp:Label runat="server" ID="lblchaid" Visible="false"></asp:Label> 
<asp:Label runat="server" ID="lblinsvalidupto" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblinsValidToDate" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblToDate" Visible="false"></asp:Label>
        <asp:Label runat="server" ID="lblTaxID" Visible="false"></asp:Label>
</div>
</div>                           
                              
<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b >Commodity</b>
<asp:TextBox ID="txtcommodity" placeholder="Commodity" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"   
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >No of Vehicles</b>
<asp:TextBox ID="txtNoVehicles" placeholder="Vehicles" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"   
runat="server"   ></asp:TextBox>
</div>
</div>
     <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Item No</b>
<asp:TextBox ID="txtItemNo" placeholder="Item No" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"   
runat="server"   ></asp:TextBox>
</div>
</div>
</div>
<div class="row">
                            
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Quantity</b>
<asp:TextBox ID="txtqty" Style="text-transform:uppercase" placeholder="Quantity"  ReadOnly="true" class="form-control text-label" onkeypress="return ValidateQty()"   
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Unit</b>
<asp:TextBox ID="txtunit" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="Unit"
runat="server"   ></asp:TextBox>
</div>
</div>
                           
                              
<div class="col-sm-3  col-xs-12" >
<div class="form-group text-label">
<b>Area</b>
<asp:TextBox ID="txtArea" AutoPostBack="false" placeholder="Area"  ReadOnly="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                           
</asp:TextBox>   
</div>
</div>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b > Charges Area</b>
<asp:TextBox ID="txtChargesArea" placeholder="Charges Area" Style="text-transform:uppercase" class="form-control text-label"     
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-sm-3  col-xs-12" >
<div class="form-group text-label">
<b> Cargo Type</b>
<asp:TextBox ID="txtcargo" AutoPostBack="false" placeholder="Cargo Type"  ReadOnly="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                           
</asp:TextBox>   
</div>
</div>

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Gross Wt(kgs)</b>
<asp:TextBox ID="txtgrosskgs" Style="text-transform:uppercase" placeholder="Gross Wt(kgs)"  ReadOnly="true" class="form-control text-label"   onkeypress="return ValidateQty()"     
runat="server"   ></asp:TextBox>
</div>
</div>

                        
                             
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Value</b>
<asp:TextBox ID="txtvalue" Style="text-transform:uppercase" placeholder="Value"  ReadOnly="true" class="form-control text-label"  onkeypress="return Validatevalue()"   
runat="server"   ></asp:TextBox>
<asp:TextBox ID="txtvalduty"  Style="text-transform: uppercase; display:none "   class="form-control text-label form-cascade-control"
runat="server" Text=""></asp:TextBox>
</div>
</div>
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Duty</b>
<asp:TextBox ID="txtduty" Style="text-transform:uppercase" placeholder="Duty"  ReadOnly="true" class="form-control text-label" onkeypress="return ValidateDuty()"   
runat="server"   ></asp:TextBox>
</div>
</div>
                            
</div>
<div class="row">
<%-- <div class="row">
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
    </div>--%>
                 
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >20</b>
<asp:TextBox ID="txt20" Style="text-transform:uppercase" placeholder="20"  ReadOnly="true" class="form-control text-label"    
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >40</b>
<asp:TextBox ID="txt40" Style="text-transform:uppercase" placeholder="40"  ReadOnly="true" class="form-control text-label"    
runat="server"   ></asp:TextBox>
</div>
</div>
</div>
         <div class="row">
             <div class="col-sm-12 col-xs-12">
<div class="form-group text-label">
<b >Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" placeholder="Remarks" TextMode="MultiLine" Rows="2" class="form-control text-label"    
runat="server"   ></asp:TextBox>
</div>
</div>
         </div>                     

<%--   <div class="col-md-6 col-xs-12"  >
<div class="form-group text-label" style="padding-top:18px;">
<asp:CheckBox ID="chkisActive" runat="server"  />
<asp:hiddenfield ID="Hiddenfield1" runat="server" Value="0" />
<asp:Label ID="Label1" runat="server" AssociatedControlID="chkisActive" CssClass="inline"> Insuranca Appliction</asp:Label>
</div>
</div>--%>

     </div>  
                       
</ContentTemplate>
</asp:UpdatePanel>
                        
</asp:Panel>                                                                                                  
</div>
<div class="panel">
<div class="panel-body">
                         
<asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
<div class="col-sm-5 col-xs-12">
<div class="form-group text-label">
    <b >GST In Number</b>
<asp:TextBox  ID="txtgstin" placeholder="GST Number" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
    <asp:Label runat="server" ID="lblpartyid" Visible="false"></asp:Label>
</div>
</div>
<div class="col-sm-1 col-xs-3" style="display:none">
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnkUpdate" ControlStyle-CssClass='btn btn-info'  runat="server"
OnClientClick="return Container()" OnClick="lnkUpdate_Click">  
<i class=" fa fa-check"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
<div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
<div class="col-sm-6 col-xs-12" style="padding-top:20px">
<div class="form-group text-label">
<asp:TextBox  ID="txtgstname" placeholder="GST Name" ReadOnly="true" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
<asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
</div>
                                       
                         
                             
</ContentTemplate>
</asp:UpdatePanel>
<div class="row">
<div class="col-sm-4 col-xs-12" style="padding-top:15px">
<div class="form-group text-label">
    <b>Tariff  </b>
<asp:DropDownList  ID="ddltraiff" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:DropDownList>
</div>
</div>
<asp:label ID="lblArea" Visible="false"  AutoPostBack="true" runat="server" ></asp:label>
<div class="col-lg-5 text-label" style="padding-top:35px" >
<asp:Button ID="btncal" data-layout="center" data-type="confirm" visible="true" 
class="btn btn-success btn-sm outline" runat="server" Text="Calculate" OnClick="btncal_Click"  OnClientClick=" return ValidationCalcu()" />
</div>

         <div class="col-md-3 col-xs-12" style="padding-top:25px">
<div class="form-group text-label">
<b  >Commodity</b>
 
    <asp:DropDownList ID="ddlCommodity"  Style="text-transform: uppercase;" OnTextChanged="ddlCommodity_TextChanged" AutoPostBack="true" runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>

       <div class="col-md-2 col-xs-12" style="padding-top:25px">
<div class="form-group text-label">
<b  >Tax</b>
 
    <asp:DropDownList ID="ddltxtTax"  Style="text-transform: uppercase;"  runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>
</div>
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
                   
<%-- <a href="BondNocAssessment.aspx" class="btn btn-info btn-block">OK</a>--%>
<button class="btn btn-info btn-block" id="saveQuoOK" data-dismiss="modal" runat="server" onserverclick="saveQuoOK_ServerClick" aria-hidden="true">
        OK 
    </button>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
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
<asp:Label ID="lblsession1"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="Button1" data-dismiss="modal" runat="server" onserverclick="Button1_ServerClick"  aria-hidden="true">
Yes 
</button>
<button id="Button2" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>
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
<a href="BondExAssessment.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>         
</div>
</fieldset>
               
</div>
       
<div class="col-md-4 col-xs-12 pull-md-right sidebar" >

<div menuitemname="Client Details" class="panel panel-sidebar" style="height:722px">
   
<div class="panel-body">
<asp:UpdatePanel ID="upModalSave1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>

                                         
<div class="row text-label">
&nbsp;&nbsp; <b><asp:Label ID="lblname" runat="server" ForeColor="Blue" text="Charges to be collected"></asp:Label></b>
<div class="text-label pull-right" style="padding-right:5px">
<b><asp:Label ID="lblchargescount" Visible="false" runat="server" ForeColor="Blue" text="Count:"></asp:Label></b>
<asp:Label ID="LBLNO"  runat="server" ForeColor="Black" text=""></asp:Label>
</div>
                              
<br /><br />
<div class="col-lg-12 text-label">
<div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;height:400px;">
<asp:GridView ID="rptIndentLIst" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>
    <asp:TemplateField Visible="false">
    <ItemTemplate>
            <asp:HiddenField ID="hfEntryid" runat="server" Value='<%#Eval("accountid")%>' />
        <asp:Label ID="lblaccntid" runat="server" text='<%#Eval("accountid")%>'></asp:Label>
        <asp:CheckBox id="chkindent"  runat="server" Visible="false"/>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Left"  />
</asp:TemplateField>
                                                   
    <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblIndentNo" runat="server" Visible="false" text='<%#Eval("accountname")%>'></asp:Label>
        <asp:Label ID="lblaccntname" runat="server" text='<%#Eval("accountname")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>

    <asp:TemplateField HeaderText="From" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblFromDate" runat="server"   text='<%#Eval("P_from")%>'></asp:Label>
        

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />

</asp:TemplateField>      
    
    <asp:TemplateField HeaderText="To" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblToDate" runat="server"   text='<%#Eval("P_To")%>'></asp:Label>
        

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />

</asp:TemplateField>  

    <asp:TemplateField HeaderText="Weeks" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblWeeks" runat="server"   text='<%#Eval("Weeks")%>'></asp:Label>
        

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />

</asp:TemplateField>

    <asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblRates" runat="server"   text='<%#Eval("Rate")%>'></asp:Label>
        

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />

</asp:TemplateField>  
                                                
    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblIndentNo1" runat="server" Visible="false" text='<%#Eval("amount")%>'></asp:Label>
        <asp:Label ID="lblntamnt" runat="server" text='<%#Eval("amount")%>'></asp:Label>
        
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
<div id="divtblWOTOtal" runat="server" style="display:none;">                                         
<table forecolor="Black" class="table table-striped table-bordered table-hover" style="border-top:5px solid #7bc144;margin-left:-5px;margin-right:-5px">
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b ">Net Total</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblPercentage" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Discount</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="Label1" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lbldisc" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<%--<td style ="width:50%;text-align:left"><b >CGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblCgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblCGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<%--<td style ="width:50%;text-align:left"><b >SGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblSgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblSGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<%--<td style ="width:50%;text-align:left"><b >IGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblIgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblIGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Grand Total</b></td>
<%--<td style ="width:20%;text-align:right"></td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblAllTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
</table>
</div>
 
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>  
                        
</div>
                      
</div>
<div class="row" style="padding-top:14px;">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">

<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
          
<div class="col-lg-12" style="margin-left:6px;">
<a href="BondExAssessment.aspx" class="btn btn-primary btn-sm outline pull-left"
runat="server">Clear</a>
                                 
<asp:Button ID="btnsave" data-layout="center" Visible="true"  data-type="success" ValidationGroup="Groupsubmit" class="btn btn-success btn-sm outline pull-right "
runat="server" Text="Save"  OnClientClick="return ValidationSave()"  />
</div>
<br /><br /><br />
<div class="col-lg-12" style="margin-left:6px;">
<div class="form-group ">
<a href="ListofInvoiceDetails.aspx" target="_blank"><b style="color:blue">Click here to view Assessment Summary</b> </a>
</div>
</div>
      <br /><br />
    <div class="col-lg-12" style="margin-left:6px;">
<div class="form-group ">
<a href="AdditionalCharges.aspx" target="_blank"><b style="color:blue">Click here to add Additional Charges</b> </a>
</div>
</div>         
</div>
</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
var txtNocNo = document.getElementById('<%= txtdepositno.ClientID%>').value;
var txtgstin = document.getElementById('<%= txtgstin.ClientID%>').value;
    var ddltraiff = document.getElementById('<%= ddltraiff.ClientID%>').value;
    document.getElementById('<%= btnsave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnsave.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline pull-right disabled");
var blResult = Boolean;
blResult = true;

if (txtNocNo == "") {
document.getElementById('<%= txtdepositno.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
if (txtgstin == "") {
document.getElementById('<%= txtgstin.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
    if (ddltraiff == 0) {
        document.getElementById('<%= ddltraiff.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

//alert('hi')
if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= btnsave.ClientID%>').value = "Save";
    document.getElementById('<%= btnsave.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline pull-right");
}
return blResult;
}
</script>
<script type="text/javascript">
var popup;
function OpenWOPrint() {
var txtassessno = document.getElementById('<%= txtassessno.ClientID%>').value;
var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;

var url = "../Report_Bond/BondAssessmentPrint.aspx?AssessNo=" + txtassessno + "&WorkYear=" + txtworkyear
window.open(url);
   
									}

</script>
<script type="text/javascript">
function ValidationCalcu() {
var txtNocNo = document.getElementById('<%= txtdepositno.ClientID%>').value;
var txtgstin = document.getElementById('<%= txtgstin.ClientID%>').value;
var txtgstname = document.getElementById('<%= txtgstname.ClientID%>').value;

var ddltraiff = document.getElementById('<%= ddltraiff.ClientID%>').value;

var blResult = Boolean;
blResult = true;

if (txtNocNo == "") {
document.getElementById('<%= txtdepositno.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
if (txtgstin == "") {
document.getElementById('<%= txtgstin.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
if (txtgstname == "") {
document.getElementById('<%= txtgstname.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
if (ddltraiff == 0) {
document.getElementById('<%= ddltraiff.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
<script type="text/javascript">
var popup;
function gstsearch() {

var url = "GSTPartySearch.aspx" 

popup = window.open(url, "Popup", "width=710,height=555");
popup.focus();

}
</script>
<script type="text/javascript">
var popup;
function BondEx() {

var url = "AssessmentSearch.aspx"

popup = window.open(url, "Popup", "width=710,height=555");
popup.focus();

}
</script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#<%=TextBox1.ClientID%>").dynDateTime({
            showsTime: true,
            ifFormat: "%Y/%m/%d %H:%M",
            daFormat: "%l;%M %p, %e %m, %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
</script>--%>
</asp:Content>
