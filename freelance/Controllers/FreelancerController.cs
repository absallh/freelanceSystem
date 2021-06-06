using freelance.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w;
using w.Models;

namespace freelance.Controllers
{
    [Authorize(Roles = "Freelancer")]
    public class FreelancerController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db;

        public FreelancerController()
        {
            db = new ApplicationDbContext();
        }

        public FreelancerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Freelancer
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

        public ActionResult EditProfile()
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userID).SingleOrDefault();
            RegisterViewModel registerModel = new RegisterViewModel
            {
                Email = user.Email,
                firstName = user.firstName,
                lastName = user.lastName,
                UserType = user.UserType,
                phone = user.PhoneNumber
            };
            return View(new LoginRegisterViewModel { register = registerModel });
        }

        [HttpPost]
        public ActionResult EditProfile(LoginRegisterViewModel inputModel)
        {
            var Model = inputModel.register;
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userID).SingleOrDefault();
            user.firstName = Model.firstName;
            user.lastName = Model.lastName;
            user.PhoneNumber = Model.phone;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(new LoginRegisterViewModel { register = Model });
        }
    }
}