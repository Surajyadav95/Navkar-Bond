<%@ Page Title="Bond | Loading Sheet" Language="VB" MasterPageFile="User.master" AutoEventWireup="false" enableEventValidation="false"
    CodeFile="LoadingSheet.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond | Loading Sheet</title>
        
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i>Loading Sheet
            </h3>
           
        </div>
        <div id="page-content">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
          
                    <div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
                         <asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                      
                            <div class="panel-body">

                                  <div class="row">
                                         
                <div class="col-md-9 pull-md-left main-content" >
                     <fieldset class="register">
                          <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
        
            
                 <div class="panel-body">
                         <asp:Panel ID="Panel2" runat="server" Enabled="true">
                         <div class="row">
                              <div class="col-md-3 col-xs-12" >
                               <div class="form-group text-label" style="text-decoration-color:black">
                                <b >Sheet No </b>
                               <asp:TextBox ID="txtentry" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly  class="form-control text-label form-cascade-control"
                                       runat="server" Text="" placeholder="NEW"></asp:TextBox>     
                                    </div>
                                </div>

<div class="col-sm-1 col-xs-6" style="display:none">
                                     
                                          <div class="form-group pull-left" style="padding-top:20px; height: 40px;">
                            <asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
                                 OnClientClick="return OpenItembondEx();">  
                                         <i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
                                     </div>
                                  
                                 </div>
                             <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />


                               <div class="col-sm-3  col-xs-12" ">                                      
                                       <div class="form-group text-label">
                                      <b >Sheet Date</b>
                                            <asp:TextBox ID="txttoDate"  placeholder="dd-mm-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>

        </div>                        
              </div>
                              <div class="col-md-2 col-xs-12">
                                     <div class="form-group text-label">
                                        
                                          <b >Unloading Sheet No</b>
                                         <asp:TextBox ID="txtUnloadingSheetNo" Style="text-transform:uppercase" AutoPostBack="true" class="form-control text-label"  placeholder="Unloading No"
                                                runat="server" OnTextChanged="txtnocn_TextChanged" ></asp:TextBox>
                                     </div>
                                 </div>

                             <div class="col-sm-1 col-xs-6">
                                     
                                          <div class="form-group pull-left" style="padding-top:20px; height: 40px;">
                            <asp:LinkButton ID="LinkButton1" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
                                 OnClientClick="return OpenItembondEx();">  
                                         <i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
                                     </div>
                                  
                                 </div> 
                              
                          <div class="col-md-3 col-xs-12">
                                     <div class="form-group text-label">
                                        
                                          <b >NOC No</b>
                                         <asp:TextBox ID="txtnocn" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label"  placeholder="NOC No"
                                                runat="server" ></asp:TextBox>
                                     </div>
                                 </div>
                 
                      <div class="col-md-3  col-xs-12" style="display:none">                                      
                                       <div class="form-group text-label" tabindex="50">
                                      <b >Bond Type</b>
                                   <asp:TextBox ID="txtbondt" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="BE No"
                                                runat="server"   ></asp:TextBox>
                                           </div>
                                            </div>
                         </div>

                            </asp:Panel>
                     
                         <div class="row">
                             <asp:Panel runat="server" Enabled="false">
                             <div class="col-md-3 col-xs-12">
                                     <div class="form-group text-label">
                                        
                                          <b >BOE No</b>
                                         <asp:TextBox ID="txtboe" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="BOE No"
                                                runat="server"   ></asp:TextBox>
                                     </div>
                                 </div>

                              <div class="col-md-3  col-xs-12" ">                                      
                                       <div class="form-group date text-label">
<b >BOE Date</b>
 <asp:TextBox ID="txtdate" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="dd-MM-YYYY"
                                                runat="server"   ></asp:TextBox>
        </div>                        
              </div>
                                 </asp:Panel> 
                              <div class="col-md-3 col-xs-12">
                                 <div class="form-group text-label">
                                     <b  >Bond-No</b>
                                     <asp:TextBox ID="txtbond" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Bond-No"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                             <div class="col-md-3  col-xs-12" ">                                      
                                       <div class="form-group date text-label">
                                      <b >Bond Date</b>


 <asp:TextBox ID="txtbonddate" Style="text-transform:uppercase" class="form-control text-label" TextMode="Date"
                                                runat="server"   ></asp:TextBox>
        </div>                        
              </div>

                         <asp:Panel ID="Panel6" runat="server" Enabled="false">
                             <div class="col-md-3 col-xs-12" style="display:none">
                                 <div class="form-group text-label">
                                     <b  >IGM-No</b>
                                     <asp:TextBox ID="txtigm" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="IGM-No"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                             <div class="col-md-3 col-xs-12" style="display:none">
                                 <div class="form-group text-label">
                                    <b  >Deposite No</b>
                                     <asp:TextBox ID="txtDeposit" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Deposite No"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                              
                             <div class="col-sm-6  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b>  Customer Name  </b>
                                      <asp:DropDownList ID="ddlCustomer" AutoPostBack="false" Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div>
                                  </asp:Panel>
                                
                          
                        
                                 <asp:Panel ID="Panel4" runat="server" Enabled="False">
                             <div class="col-md-6  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b> Importer Name</b>
                                      <asp:DropDownList ID="ddlimport" AutoPostBack="false" Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div>
                                     
                             
                             <div class="col-md-6  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b>CHA Name</b>
                                      <asp:DropDownList ID="ddlcha" AutoPostBack="false" Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div>
                                   
                            </asp:Panel>
                             
                                             
                     <div class="col-md-3 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >EX BE No</b>
                                     <asp:TextBox ID="txtEx" Style="text-transform:uppercase" class="form-control text-label"  placeholder="EX BE No"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                   <div class="col-md-3  col-xs-12" ">                                      
                                       <div class="form-group date text-label">
                                      <b > EX BE Date</b>
            <asp:TextBox ID="txtExBedate"  placeholder="dd-mm-yyyy" TextMode="date"  runat="server" Class="  form-control text-label"></asp:TextBox>

        </div>                        
              </div>

                         </div>
                                 
                      
       
                        </div>
                              </div>
                           <asp:UpdatePanel runat="server" ID="UpdateWarehouse" UpdateMode="Conditional">
                                 <ContentTemplate>

                             <asp:Panel ID="Panel1" runat="server" Enabled="true">
                          <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
                              <div class="panel-heading">
            <h3 class="panel-title">
                     Warehouse Details
                
                                <%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
            </h3>
             </div>
                             
                              <div class="panel-body">
                                   
                                           
                                       <div class="row">
                                            <asp:Panel ID="Panel7" runat="server" Enabled="false">
                                  <div class="col-md-3  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b> Warehouse</b>
                                      <asp:DropDownList ID="ddlware" AutoPostBack="true" Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div>
                                                </asp:Panel>

                                        <div class="col-md-3  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b> Godown</b>
                                      <asp:DropDownList ID="ddlgodown" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div>

                                            <div class="col-md-3  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b> Bin No</b>
                                      <asp:DropDownList ID="ddllot" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" OnTextChanged="ddllot_TextChanged" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div>
                                           <div class="col-md-3  col-xs-12" >
                                       <div class="form-group text-label">
                                    <b> Batch No</b>
                                      <asp:DropDownList ID="ddlBatch" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" OnTextChanged="ddlBatch_TextChanged" class="form-control text-label">
                                      </asp:DropDownList>   
                                    </div>
                                </div>

                                            <div class="col-md-3 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Delivered Qty</b>
                                     <asp:TextBox ID="txtdelivered" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()"  placeholder="Delivered Qty"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>
                                             
                                    <div class="col-md-3 col-xs-12">
                            <div class="form-group text-label">
                                <b >Received Quantity</b>
                                <asp:TextBox ID="txtrecevied" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label" onkeypress="return ValidateQty()" placeholder="Received Quantity"
                                        runat="server"   ></asp:TextBox>
                            </div>
                        </div>

                                           <div class="col-md-2 col-xs-12" style="padding-right:8px;">
                                 <div class="form-group" style="padding-top:20px">
                            <asp:Button ID="btnGodown" class="btn btn-primary btn-sm" runat="server"    
                                Text="Add" OnClick="btnGodown_Click"  OnClientClick="return ValidationHous()"/>
                        </div>
                                              
                                    </div>
                                            
                                           
                                           
                                  
                            

                                             <div class="row">
                             <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                          <div class="col-md-12 col-xs-12 text-label "  style="padding-right:50px;">
                                             <div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
                                                 <asp:GridView ID="grdgodown" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                              AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                    <ItemTemplate>
                                                               
                                                            
                                                        <asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete"                                                         
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoIdTemp")%>' runat="server" OnClick="lnkDelete_Click1"
                                                            ></asp:LinkButton>

   
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:TemplateField>


                                                <%-- <asp:BoundField DataField="Warehous" HeaderText="Warehous "></asp:BoundField>--%>
                                                 <asp:BoundField DataField="Godown" HeaderText="Godown "></asp:BoundField>

                                                <asp:BoundField DataField="LotNO" HeaderText="Bin No"></asp:BoundField>
                                                <asp:BoundField DataField="BatchNo" HeaderText="Batch No"></asp:BoundField>

                                                  <asp:BoundField DataField="DeliveredQty" HeaderText=" Delivered Quantity"></asp:BoundField>
                                                         <asp:BoundField DataField="ReceivedQty" HeaderText=" Received Quantity"></asp:BoundField>

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
                              </asp:Panel>
                                                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                         <asp:Panel ID="Panel3" runat="server" Enabled="true">
                         <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
                              <div class="panel-heading">
            <h3 class="panel-title">
                     Loading Details
                
                                <%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
            </h3>
             </div>
                              <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                             <div class="panel-body">

                                 
                                 <div class="row">

                                      <div class="col-md-4 col-xs-12" tabindex="true">
                                 <div class="form-group text-label">
                                     <b > Quantity Received</b>
                                     <asp:TextBox ID="txtqtyrecei" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label" onkeypress="return ValidateQty()"  placeholder="Quantity Received"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Balance Qty</b>
                                     <asp:TextBox ID="txtbalqty" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  onkeypress="return ValidateQty()" placeholder="Balance Quantity"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                     <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Quantity Delivered</b>
                                     <asp:TextBox ID="txtqtydeliv" Style="text-transform:uppercase" AutoPostBack="true" class="form-control text-label" onkeypress="return ValidateQty()" placeholder="Quantity Delivered"
                                                runat="server" OnTextChanged="txtqtydeliv_TextChanged"   ></asp:TextBox>
                                 </div>
                             </div>

                                     

                                
                                 </div>

                                 <div class="row">
                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Unit</b>
                                     <asp:TextBox ID="textuit" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Unit"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                        <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Unit Delivered</b>
                                     <%--<asp:TextBox ID="txtunitdeliver" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Unit Delivered"
                                                runat="server"   ></asp:TextBox>--%>

                                     <asp:DropDownList ID="ddlunit" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                     </asp:DropDownList>
                                 </div>
                             </div>
                                     </div>
                                     <div class="row">
                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Gross Wt(kgs)</b>
                                     <asp:TextBox ID="txtgross" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Gross Wt(kgs)"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Balance Wt(kgs)</b>
                                     <asp:TextBox ID="textbalwt" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Balance Wt(kgs)"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Gross Wt Delivered</b>
                                     <asp:TextBox ID="txtgrosswdeli" Style="text-transform:uppercase" OnTextChanged="txtgrosswdeli_TextChanged" AutoPostBack="true" class="form-control"  placeholder="Gross Wt Delivered"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                 </div>

                                 <div class="row">
                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Area Occupied</b>
                                     <asp:TextBox ID="txtArea" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Area Occupied"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Balance sqm</b>
                                     <asp:TextBox ID="textsqm" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Bal sqm"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Area Released</b>
                                     <asp:TextBox ID="textrelea" Style="text-transform:uppercase" AutoPostBack="true" class="form-control text-label"  placeholder="Area Released"
                                                runat="server" OnTextChanged="textrelea_TextChanged"   ></asp:TextBox>
                                 </div>
                             </div>
                                      
                                 </div>

                                 <div class="row">

                                     <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Value</b>
                                     <asp:TextBox ID="txtvalue" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label" onkeypress="return ValidateQty()" placeholder="Value"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>
                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b > Balance Value</b>
                                     <asp:TextBox ID="txtbalvalue" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label" onkeypress="return ValidateQty()" placeholder=" Balance Value"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                      <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Value</b>
                                     <asp:TextBox ID="txtvalu" Style="text-transform:uppercase" OnTextChanged="txtvalu_TextChanged" class="form-control text-label"
                                          AutoPostBack="true" onkeypress="return ValidateQty()" placeholder="Value"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>
                                     </div>
                                     <div class="row">
                                     <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Duty</b>
                                     <asp:TextBox ID="txtduty" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label" onkeypress="return ValidateDuty()"  placeholder="Duty"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                     <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b > Balance Duty</b>
                                     <asp:TextBox ID="txtbalduty" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label" onkeypress="return ValidateDuty()" placeholder=" Balance Duty"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>
                                     <div class="col-md-4 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Duty</b>
                                     <asp:TextBox ID="txtdut" Style="text-transform:uppercase" OnTextChanged="txtdut_TextChanged" class="form-control text-label" AutoPostBack="true" onkeypress="return ValidateDuty()" placeholder="Duty"
                                                runat="server"   ></asp:TextBox>
                                 </div>
                             </div>

                                 </div>
                                    
                             </div>
                            </ContentTemplate>
                                  </asp:UpdatePanel>
                         </div>
                                    </asp:Panel>

                         <asp:Panel ID="Panel5" runat="server" Enabled="true">
                         <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar" style="padding-left:0px;padding-right:0px">
        <div class="panel-heading">
            <h3 class="panel-title">
                   Container Details
                
                                <%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
            </h3>
             </div>
                                 <div class="panel-body">
                                   <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                            <div class="row">
                   
                       <div class="col-md-3 col-xs-12">
                                             <div class="form-group text-label">
                                                  <b >Container No</b>
                                     <asp:TextBox ID="txtcontainer" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Container No"  
                                                runat="server" MaxLength="11"></asp:TextBox>
                                             </div>
                                           </div>

                                           
                                           <div class="col-md-3 col-xs-12">
                                             <div class="form-group text-label">
                                                  <b >Quantity</b>
                                     <asp:TextBox ID="textqt" Style="text-transform:uppercase" class="form-control text-label"  onkeypress="return ValidateQty()"  placeholder="Quantity"  
                                                runat="server"   ></asp:TextBox>
                                             </div>
                                           </div>

                                            <div class="col-md-2 col-xs-12">
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

                                                  
                                        <div class="col-md-1  col-xs-12" style="padding-left:20px;">
                                 <div class="form-group" style="padding-top:20px">
                            <asp:Button ID="btncontainer" class="btn btn-primary btn-sm" runat="server" OnClick="btncontainer_Click"    
                                Text="Add" OnClientClick="return ValidationAdd()"/>
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
                                 <div class="col-md-4 col-xs-12"  style="margin-left:24px">
                                     <div class="col-sm-4 col-xs-4">
                                     <b>20:</b>
                                        <asp:Label runat="server" ID="lblA" ></asp:Label>
                                        &nbsp</div>
                                     <div class="col-md-4 col-xs-4">
                                    <b>40:</b>
                                     <asp:Label runat="server" ID="lblB" ></asp:Label>
                                        &nbsp</div>
                                     <div class="col-md-4 col-xs-4">
                                     <b>45:</b>
                                     <asp:Label runat="server" ID="lblC" ></asp:Label>
                                        &nbsp</div>
                                 </div>
                                 
                             </div>
                                                    </div>
                             <div class="row">
                                <div class="col-md-12  col-xs-12 text-label "  style="padding-right:60px;">
                                    <div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
                                        <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                              AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
                                            <Columns>
                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                               
                                                            
                                                        <asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete"                                                         
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoIdTemp")%>' runat="server" OnClick="lnkDelete_Click"
                                                            ></asp:LinkButton>
                                                        <asp:Label runat="server" ID="lblAutoIdTemp" Visible="false" Text='<%#Eval("AutoIdTemp")%>'></asp:Label>

   
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkCheck" Checked="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Container No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCNo" Text='<%#Eval("Container_Num")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size">
                                                    <ItemTemplate>
                                                       <asp:Label runat="server" ID="lblCSize" Text='<%#Eval("Container_Size")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtLoadingQty" CssClass="form-control" Width="60px" OnTextChanged="txtLoadingQty_TextChanged" Text='<%#Eval("Qty")%>'></asp:TextBox>
                                                        <asp:Label runat="server" ID="lblLoadingQty" Visible="false" Text='<%#Eval("Qty")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <%-- <asp:BoundField DataField="Type" HeaderText="Type"></asp:BoundField>--%>
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
                               
                        <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar" style="padding-left:0px;padding-right:0px">
        <div class="panel-heading">
            <h3 class="panel-title">
                  Others
                
                               <%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
            </h3>
             </div>   
                            <div class="panel-body">
                          <div class="col-md-6 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Equipment Type</b>
                                     <asp:DropDownList  ID="ddlEquipment" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
                                             </asp:DropDownList>
                                 </div>
                             </div>
                 <div class="col-md-6 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Executive Name</b>
                                     <asp:DropDownList ID="ddlSurveyor" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">  
                                          <asp:ListItem Value="0">--Select--</asp:ListItem>
                                           <asp:ListItem Value="1">values</asp:ListItem>
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
                                <div class="col-md-3 col-xs-12">
                                             <div class="form-group text-label">
                                                  <b >No of Vehicles</b>
                                     <asp:TextBox ID="txtNoVehicles" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateNo()"    placeholder="Enter No of Vehicles"  
                                                runat="server"></asp:TextBox>
                                             </div>
                                           </div>
                           <div class="col-md-12 col-xs-12">
                                 <div class="form-group text-label">
                                     <b >Remark</b>
                                     <asp:TextBox ID="txtremarks" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Remark"  
                                         TextMode="MultiLine"   runat="server"   ></asp:TextBox>
                                 </div>
                             </div>
                                </div>
                            </div>    
                           <div class="row">
                                 <div class="col-md-1  col-xs-12" style="padding-left:22px;">
                                 <div class="form-group" style="padding-top:15px">
                            <asp:Button ID="Button2" class="btn btn-primary btn-sm" runat="server" OnClick="Button2_Click"
                                OnClientClick="return ValidationSearch()"   
                                Text="Save"/>
                        </div>
                                              
                                    </div>

                             <div class="col-md-1  col-xs-12" style="padding-left:28px;">
                                 <div class="form-group" style="padding-top:15px">
                            <asp:Button ID="btnClear" class="btn btn-primary btn-sm" runat="server"    
                                Text="Clear"/>
                        </div>
                                              
                                    </div>
                                <div class="col-sm-5 pull-right" style="padding-top:25px;">
                              <div class="form-group">
                                 <a href="LoadingSheetSummary.aspx" target="_blank"><b style="color:blue">Click here to view Loading Summary</b> </a>
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
                            <%--<a href="LoadingSheet.aspx" class="btn btn-info btn-block">OK</a>--%>
                                
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
<asp:Label ID="lblPrintQue"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenLoadingPrint()"  aria-hidden="true">
Yes 
</button>
<a href="LoadingSheet.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div> 
                         </fieldset>

                    

                              </div>
                                      <div class="col-md-3 pull-md-right sidebar" >
                    <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
        <div class="panel-heading">
            <h3 class="panel-title" style="padding-bottom: 0px !important; ">
                <i class="fa fa-cube"></i>&nbsp;    Loading Sheets
                               <%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
            </h3>
        </div>   
                          <div class="panel-body" Style="text-transform:lowercase;">
                         <asp:Repeater ID="rptNOLIst" runat="server">
                        <ItemTemplate>
                         <div  >
                             
                        <a  href="#">                         
                                      <strong><asp:Label runat="server"  Text='<%#Eval("EntryID")%>' ID="lblNONumber" style="text-transform:lowercase;"></asp:Label></strong></a>                                                     
                              &nbsp;&nbsp; | &nbsp;&nbsp; <asp:Label runat="server" Text='<%#Eval("AddDate")%>' Id="lblDate" style="text-transform:lowercase;"></asp:Label>
                                                             <br/>Generated By: &nbsp;<asp:Label runat="server" Text='<%#Eval("Users")%>' Id="lbluser" style="text-transform:lowercase;"></asp:Label>

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
                 
           
        </div>
       
         
    </div>

    <script type="text/javascript">
        var popup;
        function OpenLoadingPrint() {

            var txtentry = document.getElementById('<%= txtentry.ClientID%>').value;
            var url = "../Report_Bond/LoadingSheetPrint.aspx?LSheetNo=" + txtentry
        document.getElementById('<%= txtentry.ClientID%>').value = "";
        window.open(url);
    }
</script>
    <script type="text/javascript">
        function ValidateDuty() {
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
         function ValidateNo() {
             //alert('hii')
             if ((event.keyCode > 47 && event.keyCode < 58))
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
        function ValidationAdd() {
            var txtcontainer = document.getElementById('<%= txtcontainer.ClientID%>').value;
            var textqt = document.getElementById('<%= textqt.ClientID%>').value;
            var ddlsize = document.getElementById('<%= ddlsize.ClientID%>').value;



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
            }
            return blResult;
        }
    </script>

    <script type="text/javascript">
        function ValidationHous() {

           <%-- var ddlware = document.getElementById('<%= ddlware.ClientID%>').value;--%>
            var ddlgodown = document.getElementById('<%= ddlgodown.ClientID%>').value;
            var ddllot = document.getElementById('<%= ddllot.ClientID%>').value;
          <%--  var txtrecevied = document.getElementById('<%= txtrecevied.ClientID%>').value;--%>
            var txtdelivered = document.getElementById('<%= txtdelivered.ClientID%>').value;
            var ddlBatch = document.getElementById('<%= ddlBatch.ClientID%>').value;


            var blResult = Boolean;
            blResult = true;

              <%--if (ddlware == 0) {
                  document.getElementById('<%= ddlware.ClientID%>').style.borderColor = "red";
                  blResult = blResult && false;
              }--%>



            if (ddlgodown == 0) {
                document.getElementById('<%= ddlgodown.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

            <%--if (ddllot == 0) {
                document.getElementById('<%= ddllot.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlBatch == 0) {
                document.getElementById('<%= ddlBatch.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }--%>
            <%--if (txtrecevied == "") {
                  document.getElementById('<%= txtrecevied.ClientID%>').style.borderColor = "red";
                  blResult = blResult && false;
            }--%>

            if (txtdelivered == "") {
                document.getElementById('<%= txtdelivered.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }


            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
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
        function ValidationSearch() {

            var txtUnloadingSheetNo = document.getElementById('<%= txtUnloadingSheetNo.ClientID%>').value;
            var txtEx = document.getElementById('<%= txtEx.ClientID%>').value;
            var txtExBedate = document.getElementById('<%= txtExBedate.ClientID%>').value;
            
            var txtqtydeliv = document.getElementById('<%= txtqtydeliv.ClientID%>').value;
            var ddlunit = document.getElementById('<%= ddlunit.ClientID%>').value;
            var txtgrosswdeli = document.getElementById('<%= txtgrosswdeli.ClientID%>').value;
            var txtvalu = document.getElementById('<%= txtvalu.ClientID%>').value;
            var txtdut = document.getElementById('<%= txtdut.ClientID%>').value;
            var textrelea = document.getElementById('<%= textrelea.ClientID%>').value;
            var txtNoVehicles = document.getElementById('<%= txtNoVehicles.ClientID%>').value;
            var txtbond = document.getElementById('<%= txtbond.ClientID%>').value;
            var txtbonddate = document.getElementById('<%= txtbonddate.ClientID%>').value;
            

            var blResult = Boolean;
            blResult = true;

            if (txtUnloadingSheetNo == "") {
                document.getElementById('<%= txtUnloadingSheetNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }         
            if (txtEx == "") {
                document.getElementById('<%= txtEx.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtExBedate == "") {
                document.getElementById('<%= txtExBedate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

            if (txtqtydeliv == "") {
                document.getElementById('<%= txtqtydeliv.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlunit == 0) {
                document.getElementById('<%= ddlunit.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtgrosswdeli == "") {
                document.getElementById('<%= txtgrosswdeli.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtvalu == "") {
                document.getElementById('<%= txtvalu.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtdut == "") {
                document.getElementById('<%= txtdut.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (textrelea == "") {
                document.getElementById('<%= textrelea.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtNoVehicles == "") {
                document.getElementById('<%= txtNoVehicles.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtbond == "") {
                document.getElementById('<%= txtbond.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtbonddate == "") {
                document.getElementById('<%= txtbonddate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>

     <script type="text/javascript">
         var popup;
         //alert('hi')
         function OpenItembondEx() {
             //alert('hi')
             var url = "PendingUnloadingSheet.aspx"
             //window.open(url);
             popup = window.open(url, "Popup", "width=800,height=550");
             popup.focus();
         }
</script>

</asp:Content>
