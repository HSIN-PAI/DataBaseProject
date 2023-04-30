using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicServiceSystem.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        virtual public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// A Result contains what processed inneed in controller.
        /// </summary>
        public class Result
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Code { get; set; }
            public string Data { get; set; }

            public Result()
            {
                Success = false;
                Message = string.Empty;
                Code = string.Empty;
                Data = string.Empty;
            }
        }

        /// <summary>
        /// To generate an Html String with the certain View and View Model.
        /// </summary>
        /// <param name="viewName">The used partial view name.</param>
        /// <param name="viewModel">The used view model.</param>
        /// <returns>The Html String.</returns>
        virtual public string RenderPartialViewToString(string viewName, object viewModel)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = viewModel;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Get the Select List from Enum Type.
        /// </summary>
        /// <param name="dtKeyValue">The DataTable of Key Value.</param>
        /// <param name="strSelected">The Selected Value.</param>
        /// <returns>The Select List.</returns>
        public SelectList GetSelectList(DataTable dtKeyValue, string strSelected)
        {
            List<string> lstSelected = string.IsNullOrEmpty(strSelected) ? new List<string>() : strSelected.Split(',').ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (DataRow dr in dtKeyValue.AsEnumerable())
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = dr[0].ToString(),
                    Text = dr[1].ToString(),
                    Selected = lstSelected.Contains(dr[1].ToString())
                });
            }

            return new SelectList(selectListItems, "Value", "Text", lstSelected);
        }

        /// <summary>
        /// Get the Select List with a Empty selection from Enum Type.
        /// </summary>
        /// <param name="dtKeyValue">The DataTable of Key Value.</param>
        /// <param name="strSelected">The String of Selected Value.</param>
        /// <returns>The Select List.</returns>
        public SelectList GetSelectListWithEmpty(DataTable dtKeyValue, string strSelected)
        {
            List<string> lstSelected = string.IsNullOrEmpty(strSelected) ? new List<string>() : strSelected.Split(',').ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "",
                    Text = "",
                    Selected = lstSelected.Count.Equals(0)
                }
            };

            foreach (DataRow dr in dtKeyValue.AsEnumerable())
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = dr[0].ToString(),
                    Text = dr[1].ToString(),
                    Selected = lstSelected.Contains(dr[1].ToString())
                });
            }

            return new SelectList(selectListItems, "Value", "Text", lstSelected);
        }
    }
}