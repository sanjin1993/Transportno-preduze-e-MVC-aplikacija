<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Instradacija.aspx.cs" Inherits="Web.Reports.Instradacija" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:reportviewer ID="ReportViewer1" runat="server" width="100%"></rsweb:reportviewer>
    </form>
</body>
</html>
