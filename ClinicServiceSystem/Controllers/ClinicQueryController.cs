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
                        ClinicId = dr.Field<int>("ClinicId"),
                        ClinicName = dr.Field<string>("ClinicName")
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