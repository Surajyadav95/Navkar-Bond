<%@ Page Title="Bond | List of UnBilled" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="Generatedinvoice.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Bond | List of UnBilled</title>
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i> List of UnBilled
            </h3>

        </div>
        <div id="page-content">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="page-container">
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

                <div class="panel">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-2 col-xs-12" >
                                <div class="form-group date text-label">
                                    As On Date
                                     
                                        <asp:TextBox ID="txtfromDate" placeholder="DD-MM-YYYY" runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
                                        <%--<div class="input-group-addon text-label" style="width: 40px;">To</div>
                                        <asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>--%>                                   

                                </div>


                            </div>

                            <div class="col-md-2 col-xs-12">
                                <div class="form-group text-label">
                                    Category
                                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" class="form-control text-label">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">CHA</asp:ListItem>
                                        <asp:ListItem Value="2">Importer</asp:ListItem>
                                        <asp:ListItem Value="3">Customer</asp:ListItem>
                                         
                                        <asp:ListItem Value="4">NOC No</asp:ListItem>
                                        <asp:ListItem Value="5">Bond No</asp:ListItem>
                                        <asp:ListItem Value="6">Warehouse</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>
                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCHA" runat="server">
                                <div class="form-group text-label">
                                    CHA Name
                                    <asp:DropDownList ID="ddlcha" runat="server"  Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-md-4 col-xs-12" style="display: none;" id="divImpoeter" runat="server">
                                <div class="form-group text-label">
                                    Importer Name
                                    <asp:DropDownList ID="ddlimpor" runat="server"  Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCustomer" runat="server">
                                <div class="form-group text-label">
                                    Customer Name
                                    <asp:DropDownList ID="ddlcustomer" runat="server"  Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                           

                            <div class="col-md-2 col-xs-12" style="display: none;" id="divNoc" runat="server">
                                <div class="form-group text-label">
                                    <b>Search Text</b>
                                    <asp:TextBox ID="txtNoc" Style="text-transform: uppercase" class="form-control text-label " placeholder="Search Text"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>


                             <div class="col-md-2 col-xs-12" style="display: none;" id="divcargo" runat="server">
                                <div class="form-group text-label">
                                    <b>Bond No</b>
                                    <asp:TextBox ID="txtCargo" Style="text-transform: uppercase" class="form-control text-label " placeholder="Search Text"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2 col-xs-12" style="display: none;" id="divWarehouse" runat="server">
                                <div class="form-group text-label">
                                    Warehouses
                                    <asp:DropDownList ID="ddlWarehouse" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <asp:Label ID="lblPDCode" Visible="false" runat="server" Text=""></asp:Label>

                            <div class="col-md-1 col-xs- 12">
                                <div class="form-group pull-right" style="padding-top: 20px">
                                    <asp:Button ID="btnsearch" runat="server"
                                        class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click"></asp:Button>
                                </div>
                            </div>


                              <div class="col-md-2 col-xs- 12">
                                <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                    <asp:Label ID="lblCancel" Visible="false" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                        </div>


                        <div class="row">
                            <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="grdcontainer" />
                                </Triggers>
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-lg-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />

                                                    <Columns>
                                                            <asp:TemplateField>
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkCancel" Visible='<%#Eval("Visisbility")%>' ControlStyle-CssClass='btn btn-danger btn-sm outline' Text="Not Required Invoice"                                                          
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EntryID")%>' OnClick="lnkCancel_Click" OnClientClick="return confirm('Are you sure to mark this document as not required?')" runat="server" 
                                                            ></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="40px"  />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No"></asp:BoundField>
                                                            <asp:BoundField DataField="NOC No" HeaderText="NOC No"></asp:BoundField>
                                                            <asp:BoundField DataField="NOC Date" HeaderText="NOC Date"></asp:BoundField>
                                                            <asp:BoundField DataField="Bond No" HeaderText="Bond No"></asp:BoundField>
                                                            <asp:BoundField DataField="Bond In date" HeaderText="Bond In Date"></asp:BoundField>
                                                            <asp:BoundField DataField="Bond Ex Date" HeaderText="Bond Ex Date"></asp:BoundField>
                                                            <asp:BoundField DataField="Ex Boe No" HeaderText="Ex Boe No"></asp:BoundField>
                                                            <asp:BoundField DataField="Invoice Type" HeaderText="Invoice Type"></asp:BoundField>
                                                        <asp:BoundField DataField="Warehouse" HeaderText="Warehouse"></asp:BoundField>
                                                            <asp:BoundField DataField="Customer Name" HeaderText="Customer Name"></asp:BoundField>
                                                            <asp:BoundField DataField="CHA Name" HeaderText="CHA Name"></asp:BoundField>
                                                            <asp:BoundField DataField="Importer Name" HeaderText="Importer Name"></asp:BoundField>
                                                            <asp:BoundField DataField="Dwell days" HeaderText="Dwell Days"></asp:BoundField>                                                        
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
            <div class="modal fade" id="myModal" role="dialog"  aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" style="width:600px">
                <asp:UpdatePanel ID="upModalCancel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <Triggers>
                             <asp:Postbacktrigger controlid="btnsave" />
                              
                               
                         </Triggers>
                    <ContentTemplate>
                        
 <div>
                        <div class="modal-content" >
                            <div class="modal-header">
                                <center>
                                   
                                        <asp:Label ID="lblModifyTitle" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
                              <asp:TextBox ID="txtremarks" Style="text-transform:uppercase" Rows="4" class="form-control text-label"    placeholder="Remarks"  
                    TextMode="MultiLine"   runat="server"   ></asp:TextBox>
                                   
                                </center>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnsave" class="btn btn-success"  
                                                runat="server" Text="Save" OnClientClick="return ValidationSave();" />

                                <button id="Button3" runat="server" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancel</button>
                                
                               <%-- OnClientClick="return confirm('Are you sure to cancel this receipt?')"" --%>
                            </div>
                        </div>
                    </ContentTemplate>
                   <Triggers>
        
                </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>



        </div>

    </div>
 

    <script type="text/javascript">
        function ValidationSave() {


            var txtremarks = document.getElementById('<%= txtremarks.ClientID%>').value;

           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= txtremarks.ClientID%>').style.borderColor = "Gainsboro";

           if (txtremarks == "") {
               document.getElementById('<%= txtremarks.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }

           if (blResult == false) {
               alert('Please fill the required fields!');

           }
           return blResult;
       }
</script>
    <script>
        function pop(url, w, h, T, L) {
            n = window.open(url, null, 'top=' + T + ',left=' + L + ',toolbar=0,location=0,directories=0,status=1,menubar=0,titlebar=0,scrollbars=1,resizable=0,width=' + w + ',height=' + h);
            if (n == null) {
                return true;
            }
            return false;
        }
    </script>
    </div>
</asp:Content>
