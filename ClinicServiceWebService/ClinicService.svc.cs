using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
            string strConnStr = ConfigurationManager.AppSettings["ConnectionString"]
                                                    .Replace("{BaseDirectory}", AppDomain.CurrentDomain.BaseDirectory)
                                                    .Replace("\\" + Assembly.GetCallingAssembly().GetName().Name, "");
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConnStr))
            {
                string strSql = "select a.clinic_id, a.clinic_name, a.clinic_type, a.coop_type, a.phone, a.remark, a.address, a.department_name, a.county_id, " +
                                " b.service_name service_type, c.outpatient_name outpatient_type, d.business_time_frame business_hour " +
                                " from clinic a " +
                                " join service_type b on a.service_type = b.service_id " +
                                " join outpatient_type c on a.outpatient_type = c.outpatient_id" +
                                " join business_time d on a.business_hour = d.business_time_id";

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

        public DataTable QueryClinicById(decimal dClinicId)
        {
            string strConnStr = ConfigurationManager.AppSettings["ConnectionString"]
                                                    .Replace("{BaseDirectory}", AppDomain.CurrentDomain.BaseDirectory)
                                                    .Replace("\\" + Assembly.GetCallingAssembly().GetName().Name, "");
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConnStr))
            {
                string strSql = "select a.clinic_id, a.clinic_name, a.clinic_type, a.coop_type, a.phone, a.remark, a.address, a.department_name, a.county_id, " +
                                " b.service_name service_type, c.outpatient_name outpatient_type, d.business_time_frame business_hour " +
                                " from clinic a " +
                                " join service_type b on a.service_type = b.service_id " +
                                " join outpatient_type c on a.outpatient_type = c.outpatient_id" +
                                " join business_time d on a.business_hour = d.business_time_id " +
                                " where a.clinic_id = @clinic_id";

                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.Add("clinic_id", SqlDbType.Decimal).Value = dClinicId;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtResult);
                    }
                }
            }

            return dtResult;
        }
    }
}
