using System.Web.Mvc;

namespace ClinicServiceSystem.Controllers
{
    public class FeedbackTestController : SharedController
    {
        // GET: Feedback
        override public ActionResult Index()
        {
            return View();
        }
    }
}