using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace openBasculaNetWeb.Reports
{
    public partial class Index : System.Web.UI.Page
    {
        string ReportPath = "";
        string ViewName = "";
        Dictionary<string, string> parametrosInforme = new Dictionary<string, string>();

        /// <summary>
        /// Inicio de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (LoadParemetersCorrecto())
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = @"Reports\" + ReportPath;
                    EliminarFicherosTemporales();
                    ReportViewer1.Visible = true;

                    var startDate = DateTime.Now.ToShortDateString();

                    var reportParameterCollection = new ReportParameter[parametrosInforme.Keys.Count];
                    int i = 0;
                    foreach (var parametro in parametrosInforme)
                    {
                        reportParameterCollection[i] = new ReportParameter { Name = parametro.Key };
                        reportParameterCollection[i].Values.Add(parametro.Value);
                        reportParameterCollection[i].Visible = true;
                        i++;
                    }

                    ReportViewer1.LocalReport.SetParameters(reportParameterCollection);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (cmbExportFormat.Text.ToUpper() == "PDF") ExportarPDF();
            else if (cmbExportFormat.Text.ToUpper() == "DOC") ExportarDOC_2003();
            else if (cmbExportFormat.Text.ToUpper() == "DOCX") ExportarDOCX();
        }

        #region Utiles 
        /// <summary>
        /// Genera/obtiene la ruta de temporales
        /// </summary>
        /// <returns></returns>
        private string ReportingTempDir()
        {
            string dirpath = HttpContext.Current.Server.MapPath("./Temp");
            if (System.IO.Directory.Exists(dirpath) == false) System.IO.Directory.CreateDirectory(dirpath);

            return dirpath + "\\";
        }

        /// <summary>
        /// elimina de forma asincrona los ficheros temporales 
        /// </summary>
        private async void EliminarFicherosTemporales()
        {
            System.IO.DirectoryInfo directorio = new DirectoryInfo(ReportingTempDir());
            DateTime ayer = DateTime.Now.AddDays(-1);
            var ficherosEliminar = directorio.GetFiles().Where(x => x.CreationTime <= ayer).ToList();

            foreach (var finfo in ficherosEliminar)
            {
                try
                {
                    File.Delete(finfo.FullName);
                }
                catch { }
            }
        }

        private bool LoadParemetersCorrecto()
        {
            bool correcto = true;

            ReportPath = Request.Params["report"] == null ? string.Empty : Request.Params["report"].ToString();
            ViewName = Request.Params["openBasculaViewName"] == null ? string.Empty : Request.Params["openBasculaViewName"].ToString();

            if (string.IsNullOrEmpty(ReportPath) || string.IsNullOrEmpty(ViewName)) correcto = false;
            else
            {
                LoadViews();
            }

            return correcto;
        }

        #endregion //Utiles

        #region Exportadores
        private void ExportarPDF()
        {
            string fileName = ReportingTempDir() + Guid.NewGuid().ToString("N") + ".pdf";
            string deviceInfo =
              @"<DeviceInfo>
                                <OutputFormat>PDF</OutputFormat>
                                <PageWidth>21cm</PageWidth>
                                <PageHeight>29.7cm</PageHeight>
                                <MarginLeft>0.15in</MarginLeft>
                                <MarginRight>0.09in</MarginRight>
                               <MarginBottom>0.02in</MarginBottom>
                            </DeviceInfo>";
            Warning[] warnings;
            Byte[] mybytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo);
            //Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            using (FileStream fs = File.Create(fileName))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }

            FileInfo finfo = new FileInfo(fileName);

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=Datos_paciente.pdf");
            Response.AddHeader("Content-Length", finfo.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.Flush();
            Response.TransmitFile(finfo.FullName);
            Response.End();
        }

        private void ExportarDOCX()
        {
            string fileName = ReportingTempDir() + Guid.NewGuid().ToString("N") + ".docx";
            string deviceInfo =
              @"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>21cm</PageWidth>
                    <PageHeight>29.7cm</PageHeight>                    
                    <MarginLeft>0.64cm</MarginLeft>
                    <MarginRight>0.64cm</MarginRight>
                    <MarginBottom>2cm</MarginBottom>
                    <MarginTop>2cm</MarginTop>
                </DeviceInfo>";
            Warning[] warnings;
            Byte[] mybytes = ReportViewer1.LocalReport.Render("WORDOPENXML");
            //Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            using (FileStream fs = File.Create(fileName))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }

            FileInfo finfo = new FileInfo(fileName);

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=Datos_paciente.docx");
            Response.AddHeader("Content-Length", finfo.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.Flush();
            Response.TransmitFile(finfo.FullName);
            Response.End();

        }

        private void ExportarDOC_2003()
        {
            string fileName = ReportingTempDir() + Guid.NewGuid().ToString("N") + ".doc";
            string deviceInfo =
              @"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>8.5in</PageWidth>
                    <PageHeight>11in</PageHeight>
                    <MarginTop>0.25in</MarginTop>
                    <MarginLeft>0.25in</MarginLeft>
                    <MarginRight>0.25in</MarginRight>
                    <MarginBottom>0.25in</MarginBottom>
                </DeviceInfo>";
            Warning[] warnings;
            Byte[] mybytes = ReportViewer1.LocalReport.Render("WORD");

            using (FileStream fs = File.Create(fileName))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }

            FileInfo finfo = new FileInfo(fileName);

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=Datos_paciente.doc");
            Response.AddHeader("Content-Length", finfo.Length.ToString());
            Response.ContentType = "application/ms-word";
            Response.Flush();
            Response.TransmitFile(finfo.FullName);
            Response.End();

        }
        #endregion

        /// <summary>
        /// Zona en la que se cargan los datos para los informes
        /// </summary>
        private void LoadViews()
        {
            switch (ViewName)
            {
                case "informeAlbaranReport":

                    var informe = new openBasculaNet.Core.Structures.Reporting.InformeAlbaran();
                    informe.ReportPath = ReportPath;
                    informe.ViewName = ViewName;
                    informe.IdHistorico = Convert.ToInt32(Request.Params["idHistorico"]);
                    informe.ParametrosReporte = new Dictionary<string, string>()
                    {
                        {"startDate" , "32" }
                    };
                    parametrosInforme = informe.ParametrosReporte;

                    break;

                default: break;
            }
        }
    }
}