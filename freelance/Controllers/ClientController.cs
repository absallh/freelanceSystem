using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using freelance.Models;

namespace Client.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        // GET: Client
        public new ActionResult Profile()
        {
            return View();
        }




        public ActionResult CreateNewPost()
        {
            return View();
        }




        public ActionResult Myposts()
        {
            return View();
        }
        public ActionResult ReceivedProposals()
        {
            return View();
        }

    }
}