using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ClinicServiceWebService
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼、svc 和組態檔中的類別名稱 "ClinicService"。
    // 注意: 若要啟動 WCF 測試用戶端以便測試此服務，請在 [方案總管] 中選取 ClinicService.svc 或 ClinicService.svc.cs，然後開始偵錯。
    public class ClinicService : IClinicService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        public DataTable QueryClinic(string strClinicName)
        {
            string strConnStr = ConfigurationManager.AppSettings["ConnectionString"];
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConnStr))
            {
                string strSql = "select * from CLINICTEST";

                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtResult);
                    }
                }
            }

            return dtResult;
        }

        public string QueryClinicToJson(string strClinicName)
        {
            string strResult;

            strResult = JsonConvert.SerializeObject(QueryClinic(strClinicName));

            return strResult;
        }
    }
}
