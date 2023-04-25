using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicServiceSystem.Controllers
{
    public class FeedbackController : SharedController
    {
        // GET: Feedback
        override public ActionResult Index()
        {
            return View();
        }
    }
}