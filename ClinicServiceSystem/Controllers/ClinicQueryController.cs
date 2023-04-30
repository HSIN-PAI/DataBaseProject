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
            DataTable dt = new DataTable();
            dt.Columns.Add("0");
            dt.Columns.Add("1");
            dt.Rows.Add("A", "A");
            dt.Rows.Add("B", "B");

            Clinic model = new Clinic()
            {
                ClinicTypeSelectList = GetSelectList(dt, ""),
                ServiceTypeSelectList = GetSelectList(dt, ""),
                OutPatientTypeSelectList = GetSelectList(dt, ""),
                CoopTypeSelectList = GetSelectList(dt, ""),
                BusinessHourSelectList = GetSelectList(dt, "")
            };

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
                        ClinicType = dr.Field<string>("clinic_type"),
                        ServiceType = dr.Field<string>("service_type"),
                        OutPatientType = dr.Field<string>("outpatient_type"),
                        CoopType = dr.Field<string>("coop_type"),
                        BusinessHour= dr.Field<string>("business_hour"),
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

        [HttpPost]
        public ActionResult Action(Clinic model)
        {
            Result result = new Result();

            try
            {
                switch (model.OperCode)
                {
                    case "add":
                        break;

                    case "detail":
                    case "edit":
                    case "delete":

                        break;

                    default:
                        break;
                }

                result.Data = RenderPartialViewToString("_PartialCRUD", model);
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