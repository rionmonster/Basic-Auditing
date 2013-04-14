using Basic_Auditing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Basic_Auditing.Controllers
{
    [Authorize]
    public class AuditController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Audit]
        public ActionResult NormalExample()
        {
            return Content("Audit Fired!");
        }

        public ActionResult PostingExample()
        {
            return View();
        }
        
        [Audit]
        [HttpPost]
        public ActionResult PostingExample(PostingModel model)
        {
            return Content("Submitted!");
        }

        public ActionResult AuditRecords()
        {
            var audits = new AuditingContext().AuditRecords;
            return View(audits);
        }
    }

    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Stores the Request in an Accessible object
            var request = filterContext.HttpContext.Request;

            //Generate an audit
            Audit audit = new Audit()
            {
                AuditID = Guid.NewGuid(),
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                URLAccessed = request.RawUrl,
                TimeAccessed = DateTime.UtcNow,
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
            };

            //Stores the Audit in the Database
            AuditingContext context = new AuditingContext();
            context.AuditRecords.Add(audit);
            context.SaveChanges();

            base.OnActionExecuting(filterContext);
        }
    }
}
