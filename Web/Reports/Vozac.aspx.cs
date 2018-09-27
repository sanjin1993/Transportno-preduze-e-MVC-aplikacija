using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace TransportnoPreduzece.Web.Reports
{
    public partial class Vozac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int vozacId = int.Parse(Request.QueryString["vozacId"]);

                var Header = VozacRM.GetHeader(vozacId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Header", Header));

                var Kartice = VozacRM.GetBody(vozacId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Body", Kartice));

                var Odsustva = VozacRM.GetBodyOdsustva(vozacId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("OdsustvaBody", Odsustva));

                var VozacInfo = VozacRM.getVozaciInformacije(vozacId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VozacInfo", VozacInfo));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/Vozac.rdlc";
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}