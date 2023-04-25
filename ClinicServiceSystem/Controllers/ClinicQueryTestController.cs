using System.Data;
using System.Text;
using System.Web.Mvc;

namespace ClinicServiceSystem.Controllers
{
    public class ClinicQueryTestController : SharedController
    {
        // GET: ClinicQuery
        override public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Test(string param)
        {
            string strResult;

            strResult = "Your Input is: " + param;

            return new JsonResult { Data = strResult };
        }

        [HttpPost]
        public ActionResult Search(string clinicName, string a, string b, string c, string d, string e)
        {
            string strResult;
            DataTable dt = new DataTable();

            // Todo: Query from datatbase.
            #region Make fake data
            dt.Columns.Add("ClinicName");
            dt.Columns.Add("ClinicId");

            dt.Rows.Add("Clinic1", "00001");
            dt.Rows.Add("Clinic2", "00002");
            #endregion

            strResult = ExportDatatableToHtml(dt);

            return new JsonResult { Data = strResult };
        }

        string ExportDatatableToHtml(DataTable dt)
        {
            StringBuilder strHtmlBuilder = new StringBuilder();

            strHtmlBuilder.Append("<html>");
            strHtmlBuilder.Append("<head>");
            strHtmlBuilder.Append("</head>");
            strHtmlBuilder.Append("<body>");
            strHtmlBuilder.Append("<table border='1px' cellpadding='1' cellspacing='1' bgcolor='lightyellow' style='font-family:Garamond; font-size:smaller'>");
            strHtmlBuilder.Append("<tr>");

            foreach (DataColumn dc in dt.Columns)
            {
                strHtmlBuilder.Append("<td>");
                strHtmlBuilder.Append(dc.ColumnName);
                strHtmlBuilder.Append("</td>");
            }

            strHtmlBuilder.Append("</tr>");
            
            foreach (DataRow dr in dt.Rows)
            {
                strHtmlBuilder.Append("<tr>");

                foreach (DataColumn dc in dt.Columns)
                {
                    strHtmlBuilder.Append("<td>");
                    strHtmlBuilder.Append(dr[dc.ColumnName].ToString());
                    strHtmlBuilder.Append("</td>");
                }
                strHtmlBuilder.Append("</tr>");
            }
            
            strHtmlBuilder.Append("</table>");
            strHtmlBuilder.Append("</body>");
            strHtmlBuilder.Append("</html>");

            string strHtml = strHtmlBuilder.ToString();

            return strHtml;
        }
    }
}