using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace TransportnoPreduzece.Web.Reports
{
    public partial class Vozilo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int voziloId = int.Parse(Request.QueryString["voziloId"]);

                var VozInfo = VoziloRM.getVozilaInformacije(voziloId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VoziloInfo", VozInfo));

                var Odr = VoziloRM.GetBody(voziloId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Odrzavanja", Odr));

                var Inst = VoziloRM.GetBodyInstradacije(voziloId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Instradacije", Inst));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/Vozilo.rdlc";
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}