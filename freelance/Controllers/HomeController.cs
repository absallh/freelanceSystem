using freelance.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w.Models;

namespace w.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string Query = "select * from Posts where Accept='accept'";
            SqlCommand sqlcomm = new SqlCommand(Query, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            var ds = new DataSet();
            sda.Fill(ds);
            List<Post> Posts = new List<Post>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Posts.Add(new Post
                {
                    id = Convert.ToString(dr["Id"]),
                    ClientName = Convert.ToString(dr["ClientName"]),
                    JobDescription = Convert.ToString(dr["PostText"]),
                    postTime = Convert.ToString(dr["Date"]),
                    state = Convert.ToString(dr["Accept"]),
                    budget = Convert.ToDouble(dr["JopBudget"]),
                    ClientEmail = Convert.ToString(dr["ClientEmail"])
                });
            }

            sqlconn.Close();
            return View(new LoginRegisterViewModel { post = Posts });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}