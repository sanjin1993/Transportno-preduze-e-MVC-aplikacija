using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportnoPreduzece.Web.Reports;

namespace Web.Reports
{
    public partial class Instradacija : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // int instradacijaId = int.Parse(Request.QueryString["dispozicijaId"]);
                var Header = InstradacijaRM.GetHeader(3);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Head", Header));


                var Info = InstradacijaRM.GetInstradacijaInfos(3);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsInstradacije", Info));




                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/Instradacija.rdlc";
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();

            }
        }
    }
}