using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ClinicServiceWebService
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼、svc 和組態檔中的類別名稱 "ClinicService"。
    // 注意: 若要啟動 WCF 測試用戶端以便測試此服務，請在 [方案總管] 中選取 ClinicService.svc 或 ClinicService.svc.cs，然後開始偵錯。
    public class ClinicService : IClinicService
    {
        string _ConnStr = ConfigurationManager.AppSettings["ConnectionString"]
                                             .Replace("{BaseDirectory}", AppDomain.CurrentDomain.BaseDirectory)
                                             .Replace("\\" + Assembly.GetCallingAssembly().GetName().Name, "");

        public string HelloWorld()
        {
            return "Hello World";
        }

        public DataTable SelectClinic(string strClinicName, string strClinicType, string strServiceType, string strOutPatientType, string strBusinessHour)
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                string strSql = "select a.clinic_id, a.clinic_name, a.clinic_type, a.phone, a.remark, a.address, a.department_name, a.county_id, " +
                                " b.service_id, b.service_name, " +
                                " c.outpatient_id, c.outpatient_name, " +
                                " d.business_time_id, d.business_time_frame, " +
                                " e.coop_type_id, e.coop_type_name " +
                                " from clinic a " +
                                " join service_type b on a.service_type = b.service_id " +
                                " join outpatient_type c on a.outpatient_type = c.outpatient_id " +
                                " join business_time d on a.business_hour = d.business_time_id " +
                                " join coop_type e on a.coop_type = e.coop_type_id " +
                                " where 1=1";

                if (!string.IsNullOrEmpty(strClinicName))
                {
                    strSql += $" and a.clinic_name='{strClinicName}'";
                }

                if (!string.IsNullOrEmpty(strClinicType))
                {
                    strSql += $" and (a.clinic_type in ('{string.Join("','", strClinicType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))}')";

                    strClinicType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .ToList()
                                 .ForEach(f => strSql += $" or a.clinic_type like '{f}'");

                    strSql += ")";
                }

                if (!string.IsNullOrEmpty(strServiceType))
                {
                    strSql += $" and (a.service_type in ('{string.Join("','", strServiceType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))}')";

                    strServiceType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                  .ToList()
                                  .ForEach(f => strSql += $" or a.service_type like '{f}'");

                    strSql += ")";
                }

                if (!string.IsNullOrEmpty(strOutPatientType))
                {
                    strSql += $" and (a.oupatient_type in ('{string.Join("','", strOutPatientType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))}')";

                    strOutPatientType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .ToList()
                                     .ForEach(f => strSql += $" or a.oupatient_type like '{f}'");

                    strSql += ")";
                }

                if (!string.IsNullOrEmpty(strBusinessHour))
                {
                    strSql += $" and (a.business_hour in ('{string.Join("','", strBusinessHour.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))}')";

                    strBusinessHour.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                   .ToList()
                                   .ForEach(f => strSql += $" or a.business_hour like '{f}'");

                    strSql += ")";
                }

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

        public DataTable SelectClinicById(string strClinicId)
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                string strSql = "select a.clinic_id, a.clinic_name, a.clinic_type, a.phone, a.remark, a.address, a.department_name, a.county_id, " +
                                " b.service_id, b.service_name, " +
                                " c.outpatient_id, c.outpatient_name, " +
                                " d.business_time_id, d.business_time_frame, " +
                                " e.coop_type_id, e.coop_type_name " +
                                " from clinic a " +
                                " join service_type b on a.service_type = b.service_id " +
                                " join outpatient_type c on a.outpatient_type = c.outpatient_id " +
                                " join business_time d on a.business_hour = d.business_time_id " +
                                " join coop_type e on a.coop_type = e.coop_type_id "+
                                " where a.clinic_id = @ClinicId";

                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.Add("@ClinicId", SqlDbType.NVarChar).Value = strClinicId;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtResult);
                    }
                }
            }

            return dtResult;
        }

        public DataTable SelectKeyCodeClinicType()
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                string strSql = "select * from clinic_type";

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

        public DataTable SelectKeyCodeServiceType()
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                string strSql = "select * from service_type";

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

        public DataTable SelectKeyCodeCoopType()
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                string strSql = "select * from coop_type";

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


        public DataTable SelectKeyCodeOutPatientType()
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                string strSql = "select * from outpatient_type";

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

        public DataTable SelectKeyCodeBusinessTime()
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                string strSql = "select * from business_time";

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

        public bool InsertClinic(string strClinicName, string strClinicType, string strServiceType, string strOutPatientType, string strCoopType, string strBusinessHour, string strPhone, string strAddress, string strRemark, string strDepartmentName, int? iCountyId, out string strMessage)
        {
            bool bResult = false;

            strMessage = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    conn.Open();

                    string strSql = "insert into clinic " +
                                    " values (@ClinicId,@ClinicName,@ClinicType,@ServiceType,@OutPatientType,@CoopType, " +
                                    "  @BusinessHour,@Phone,@Address,@Remark,@DepartmentName,@CountyId)";

                    string strClinicId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

                    using (SqlCommand cmd = new SqlCommand(strSql, conn))
                    {
                        cmd.Parameters.Add("@ClinicId", SqlDbType.NVarChar).Value = strClinicId;
                        cmd.Parameters.Add("@ClinicName", SqlDbType.NVarChar).Value = strClinicName ?? string.Empty;
                        cmd.Parameters.Add("@ClinicType", SqlDbType.NVarChar).Value = strClinicType ?? string.Empty;
                        cmd.Parameters.Add("@ServiceType", SqlDbType.NVarChar).Value = strServiceType ?? string.Empty;
                        cmd.Parameters.Add("@OutPatientType", SqlDbType.NVarChar).Value = strOutPatientType ?? string.Empty;
                        cmd.Parameters.Add("@CoopType", SqlDbType.NVarChar).Value = strCoopType ?? string.Empty;
                        cmd.Parameters.Add("@BusinessHour", SqlDbType.NVarChar).Value = strBusinessHour ?? string.Empty;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = strPhone ?? string.Empty;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = strAddress ?? string.Empty;
                        cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = strRemark ?? string.Empty;
                        cmd.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = strDepartmentName ?? string.Empty;
                        if (Equals(iCountyId, null))
                            cmd.Parameters.Add("@CountyId", SqlDbType.Int).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@CountyId", SqlDbType.Int).Value = iCountyId;

                        cmd.ExecuteNonQuery();

                        bResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                bResult = false;
                strMessage = ex.Message;
            }

            return bResult;
        }

        public bool UpdateClinic(string strClinicId, string strClinicName, string strClinicType, string strServiceType, string strOutPatientType, string strCoopType, string strBusinessHour, string strPhone, string strAddress, string strRemark, string strDepartmentName, int? iCountyId, out string strMessage)
        {
            bool bResult = false;

            strMessage = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    conn.Open();

                    string strSql = "update clinic " +
                                    " set clinic_name=@ClinicName,clinic_type=@ClinicType,service_type=@ServiceType,outpatient_type=@OutPatientType,coop_type=@CoopType, " +
                                    "  business_hour=@BusinessHour,phone=@Phone,address=@Address,remark=@Remark,department_name=@DepartmentName,county_id=@CountyId" +
                                    " where clinic_id=@ClinicId";


                    using (SqlCommand cmd = new SqlCommand(strSql, conn))
                    {
                        cmd.Parameters.Add("@ClinicId", SqlDbType.NVarChar).Value = strClinicId;
                        cmd.Parameters.Add("@ClinicName", SqlDbType.NVarChar).Value = strClinicName ?? string.Empty;
                        cmd.Parameters.Add("@ClinicType", SqlDbType.NVarChar).Value = strClinicType ?? string.Empty;
                        cmd.Parameters.Add("@ServiceType", SqlDbType.NVarChar).Value = strServiceType ?? string.Empty;
                        cmd.Parameters.Add("@OutPatientType", SqlDbType.NVarChar).Value = strOutPatientType ?? string.Empty;
                        cmd.Parameters.Add("@CoopType", SqlDbType.NVarChar).Value = strCoopType ?? string.Empty;
                        cmd.Parameters.Add("@BusinessHour", SqlDbType.NVarChar).Value = strBusinessHour ?? string.Empty;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = strPhone ?? string.Empty;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = strAddress ?? string.Empty;
                        cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = strRemark ?? string.Empty;
                        cmd.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = strDepartmentName ?? string.Empty;
                        cmd.Parameters.Add("@CountyId", SqlDbType.Int).Value = iCountyId ?? 0;

                        cmd.ExecuteNonQuery();

                        bResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                bResult = false;
                strMessage = ex.Message;
            }

            return bResult;
        }
        public bool DeleteClinic(string strClinicId, out string strMessage)
        {
            bool bResult = false;

            strMessage = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    conn.Open();

                    string strSql = "delete clinic " +
                                    " where clinic_id=@ClinicId";


                    using (SqlCommand cmd = new SqlCommand(strSql, conn))
                    {
                        cmd.Parameters.Add("@ClinicId", SqlDbType.NVarChar).Value = strClinicId;

                        cmd.ExecuteNonQuery();

                        bResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                bResult = false;
                strMessage = ex.Message;
            }

            return bResult;
        }
    }
}
