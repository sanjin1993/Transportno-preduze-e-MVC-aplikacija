<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dispozicija.aspx.cs" Inherits="TransportnoPreduzece.Web.Reports.WebForm1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100%" Height="842"  WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" >
            <LocalReport ReportPath="Reports\Dispozicija.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    <p>
&nbsp;&nbsp;&nbsp;
        </p>
    </form>
    </body>
</html>
