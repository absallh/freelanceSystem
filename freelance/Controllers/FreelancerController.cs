using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace freelance.Controllers
{
    [Authorize(Roles = "Freelancer")]
    public class FreelancerController : Controller
    {
        // GET: Freelancer
        public ActionResult Index()
        {
            return View();
        }
    }
}