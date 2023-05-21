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
            Clinic model = new Clinic()
            {
                ClinicTypeSelectList = GetSelectList(svc.SelectKeyCodeClinicType(), ""),
                ServiceTypeSelectList = GetSelectList(svc.SelectKeyCodeServiceType(), ""),
                OutPatientTypeSelectList = GetSelectList(svc.SelectKeyCodeOutPatientType(), ""),
                CoopTypeSelectList = GetSelectList(svc.SelectKeyCodeCoopType(), ""),
                BusinessHourSelectList = GetSelectList(svc.SelectKeyCodeBusinessTime(), "")
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
                DataTable dtQuery = svc.SelectClinic(model.ClinicName, model.ClinicType, model.ServiceType, model.OutPatientType, model.BusinessHour);

                foreach(DataRow dr in dtQuery.Rows)
                {
                    lstResult.Add(new Clinic()
                    {
                        ClinicId = dr.Field<string>("clinic_id"),
                        ClinicName = dr.Field<string>("clinic_name"),
                        ClinicType = dr.Field<string>("clinic_type"),
                        ServiceType = dr.Field<string>("service_name"),
                        OutPatientType = dr.Field<string>("outpatient_name"),
                        CoopType = dr.Field<string>("coop_type_name"),

                        BusinessHour= dr.Field<string>("business_time_frame"),
                        Phone = dr.Field<string>("phone"),
                        Address = dr.Field<string>("remark"),
                        Remark = dr.Field<string>("address"),
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
                        DataTable dtQuery = svc.SelectClinicById(model.ClinicId);

                        model = new Clinic()
                        {
                            ClinicId = dtQuery.Rows[0].Field<string>("clinic_id"),
                            ClinicName = dtQuery.Rows[0].Field<string>("clinic_name"),
                            ClinicType = dtQuery.Rows[0].Field<string>("clinic_type"),
                            ServiceType = dtQuery.Rows[0].Field<string>("service_id"),
                            OutPatientType = dtQuery.Rows[0].Field<string>("outpatient_id"),
                            CoopType = dtQuery.Rows[0].Field<string>("coop_type_id"),
                            BusinessHour = dtQuery.Rows[0].Field<string>("business_time_id"),
                            Phone = dtQuery.Rows[0].Field<string>("phone"),
                            Address = dtQuery.Rows[0].Field<string>("address"),
                            Remark = dtQuery.Rows[0].Field<string>("remark"),
                            DepartmentName = dtQuery.Rows[0].Field<string>("department_name"),
                            CountyId = dtQuery.Rows[0].Field<int?>("county_id"),

                            ClinicTypeSelectList = GetSelectList(svc.SelectKeyCodeClinicType(), dtQuery.Rows[0].Field<string>("clinic_type")),
                            ServiceTypeSelectList = GetSelectList(svc.SelectKeyCodeServiceType(), dtQuery.Rows[0].Field<string>("service_id")),
                            OutPatientTypeSelectList = GetSelectList(svc.SelectKeyCodeOutPatientType(), dtQuery.Rows[0].Field<string>("outpatient_id")),
                            CoopTypeSelectList = GetSelectList(svc.SelectKeyCodeCoopType(), dtQuery.Rows[0].Field<string>("coop_type_id")),
                            BusinessHourSelectList = GetSelectList(svc.SelectKeyCodeBusinessTime(), dtQuery.Rows[0].Field<string>("business_time_id")),

                            OperCode = model.OperCode
                        };

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

        [HttpPost]
        public ActionResult Save(Clinic model)
        {
            Result result = new Result();

            try
            {
                string strMessage = string.Empty;

                switch (model.OperCode)
                {
                    case "add":
                        svc.InsertClinic(model.ClinicName, model.ClinicType, model.ServiceType, model.OutPatientType, model.CoopType, model.BusinessHour, model.Phone, model.Address, model.Remark, model.DepartmentName, model.CountyId, out strMessage);
                        
                        break;

                    case "detail":
                        break;

                    case "edit":
                        // Edit
                        svc.UpdateClinic(model.ClinicId, model.ClinicName, model.ClinicType, model.ServiceType, model.OutPatientType, model.CoopType, model.BusinessHour, model.Phone, model.Address, model.Remark, model.DepartmentName, model.CountyId, out strMessage);
                        break;

                    case "delete":
                        // Delete
                        break;

                    default:
                        break;
                }

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