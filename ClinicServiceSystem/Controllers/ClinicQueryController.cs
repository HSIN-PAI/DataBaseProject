using ClinicServiceSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicServiceSystem.Controllers
{
    public class ClinicQueryController : SharedController
    {
        ClinicServiceWebService.ClinicService svc = new ClinicServiceWebService.ClinicService();

        // GET: ClinicQuery
        override public ActionResult Index()
        {
            Clinic model = new Clinic();

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(Clinic model)
        {
            Result result = new Result();

            try
            {
                List<Clinic> lstResult = new List<Clinic>();
                DataTable dtQuery = svc.QueryClinic(model.ClinicName);

                foreach(DataRow dr in dtQuery.Rows)
                {
                    lstResult.Add(new Clinic()
                    {
                        ClinicId = dr.Field<decimal>("clinic_id"),
                        ClinicName = dr.Field<string>("clinic_name"),
                        ClinicType = dr.Field<int>("clinic_type"),
                        ServiceType = dr.Field<int>("service_type"),
                        OutPatientType = dr.Field<int>("outpatient_type"),
                        CoopType = dr.Field<int>("coop_type"),
                        BusinessHour= dr.Field<int>("business_hour"),
                        Phone = dr.Field<string>("phone"),
                        Address = dr.Field<string>("remark"),
                        Rmark = dr.Field<string>("address"),
                        DepartmentName = dr.Field<string>("department_name"),
                        CountyId = dr.Field<int?>("county_id"),
                    });
                }

                result.Data = JsonConvert.SerializeObject(lstResult);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return new JsonResult { Data = result };
        }
    }
}