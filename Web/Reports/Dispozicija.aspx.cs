using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
namespace TransportnoPreduzece.Web.Reports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
                int dispozicijaId = int.Parse(Request.QueryString["dispozicijaId"]);
                var Header = DispozicijaRM.GetHeader(dispozicijaId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Header", Header));

                var Body = DispozicijaRM.GetBody(dispozicijaId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Body", Body));

                var Info = DispozicijaRM.GetDispozicijaInfo(dispozicijaId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Info", Info));


                var Primalac = DispozicijaRM.GetPrimalacInfo(dispozicijaId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Primalac", Primalac));

                var Posiljalac = DispozicijaRM.GetPosiljalacInfo(dispozicijaId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Posiljalac", Posiljalac));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/Dispozicija.rdlc";
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }

        }
    }
}