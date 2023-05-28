<%@ Page Title="DAR" Language="VB"  AutoEventWireup="false" CodeFile="CFSDMR.aspx.vb" Inherits="FG_DailyActivityPDF" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" href="../Images/Phoenix-logo-Bird.png" type="image/png"/>
   
   
</head>
<body style="height: 1000px">
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblerror" runat="server" Text=" " ></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <asp:Panel ID="pnlPerson" runat="server" font-family="Segoe UI">
             <asp:AccessDataSource ID="AccessDataSource1" runat="server"></asp:AccessDataSource>
        <rsweb:ReportViewer ID="ReportViewer1" Width="900px" Height="1100px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Report_Bond\CFSDMRPdf1.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
             
             </asp:Panel>
    </div>
        
    </form>
</body>
</html>


