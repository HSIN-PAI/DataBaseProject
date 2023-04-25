using System;
using System.Collections.Generic;
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
    }
}