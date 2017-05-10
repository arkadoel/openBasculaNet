<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Index.aspx.cs" Inherits="openBasculaNetWeb.Reports.Index" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
     <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/themes/base/selectable.css" rel="stylesheet" />
    <link rel="stylesheet" href="../Content/OpenBasculaNet.css" />
    <script src="../Scripts/modernizr-2.8.3.js"></script>
      <style>
    html,body,form,#div1 {
    height: 100%; 
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" style="margin: 0 auto;">
        <div id="div1" >

        <br />
            <asp:DropDownList ID="cmbExportFormat" runat="server" class="btn-default selectpicker" >
                <asp:ListItem Value="PDF"></asp:ListItem>
                <asp:ListItem Value="doc">Word 2003</asp:ListItem>
                <asp:ListItem Value="docx">Word 2007</asp:ListItem>
        </asp:DropDownList>
    
            <asp:Button ID="btnExportar" class="btn btn-primary btn-asvin" runat="server" Text="Exportar" OnClick="btnExportar_Click" />
            <asp:Button ID="btnImprimir" class="btn btn-primary btn-asvin" runat="server" Text="Imprimir" OnClientClick="PrintElem('ReportViewer1_ctl09')" />
        <br />
        
        <br />
        <br />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
                                Font-Names="Verdana" Font-Size="8pt" 
                                Height="100%" WaitMessageFont-Names="Verdana" 
                                ShowPrintButton="true" WaitMessageFont-Size="14pt" 
                                Width="100%" SizeToReportContent="True" ShowExportControls="false">
                
            </rsweb:ReportViewer>
    <asp:ScriptManager runat="server"></asp:ScriptManager>        
        

    </div>
    </form>
    <script>
        function PrintElem(elem) {
            var mywindow = window.open('', 'PRINT', 'height=400,width=600');


            mywindow.document.write('<html><head><title>' + document.title + '</title>');

            mywindow.document.write('</head><body >');
            mywindow.document.write('<h1>' + document.title + '</h1>');
            console.log(document.getElementById(elem).innerHTML);
            mywindow.document.write(document.getElementById(elem).parentElement.innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;

        }
    </script>
</body>
</html>
