using freelance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNet.Identity;
using w.Models;
using w;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace freelance.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        DataSet ds;
        string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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


        // GET: Admin
        public ActionResult Index(string email)
        {
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string Query2 = "select PhoneNumber,UserName,UserType, Id from AspNetUsers where Email ='"+ email+"'";
            string Query = "select Email,PhoneNumber,UserName,UserType, Id from AspNetUsers ";
            SqlCommand sqlcomm = new SqlCommand(Query, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            ds = new DataSet();
            sda.Fill(ds);
            List<Admin> admins = new List<Admin>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                admins.Add(new Admin
                {
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["PhoneNumber"]),
                    Name = Convert.ToString(dr["UserName"]),
                    UserType = Convert.ToString(dr["UserType"]),
                    Id = Convert.ToString(dr["Id"])
                });
            }
            sqlconn.Close();
            return View(admins);
        }

        public ActionResult EditProfile()
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userID).SingleOrDefault();
            RegisterViewModel model = new RegisterViewModel
            {
                Email = user.Email,
                firstName = user.firstName,
                lastName = user.lastName,
                UserType = user.UserType,
                phone = user.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(RegisterViewModel model)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update AspNetUsers set firstName='" + model.firstName + "',lastName= '" + model.lastName 
                + "',PhoneNumber = '" + model.phone + "'  Where Email = '" + model.Email + "'";
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return View();
        }

        public ActionResult AllPosts()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string Query = "select * from Posts ";
            SqlCommand sqlcomm = new SqlCommand(Query, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            ds = new DataSet();
            sda.Fill(ds);
            List<Admin> Posts = new List<Admin>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Posts.Add(new Admin
                {
                    Id = Convert.ToString(dr["Id"]),
                    Name = Convert.ToString(dr["ClientName"]),
                    PostText = Convert.ToString(dr["PostText"]),
                    Date = Convert.ToString(dr["Date"]),
                    State = Convert.ToString(dr["Accept"]),
                    Budget = Convert.ToString(dr["JopBudget"]),
                    ClientEmail = Convert.ToString(dr["ClientEmail"])
                });
            }
            sqlconn.Close();
            return View(Posts);

        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from AspNetUsers Where PostText ='" + id + "'";
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult DeletePosts(String id)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Posts Where PostText ='" + id + "'";
            cmd.ExecuteNonQuery();
            sqlconn.Close();

            return RedirectToAction("AllPosts", "Admin");
        }
        [HttpPost]
        public ActionResult AcceptPost(String id)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Posts set Accept='accept' Where PostText ='" + id + "'";
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return RedirectToAction("AllPosts", "Admin");
        }
        [HttpPost]
        public ActionResult EditClientPost(string JobDescription, string id)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Posts set PostText='"+JobDescription+"' Where Id ='" + id+ "'";
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return RedirectToAction("AllPosts", "Admin");
        }
        public ActionResult EditClientPost(string id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Post model = new Post();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                string Query = "select * from Posts where Id ='" + id + "'";
                SqlCommand cmd = new SqlCommand(Query, con);
                
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    if (rdr.HasRows)
                    {
                        rdr.Read(); // get the first row

                        model.id = id;
                        model.ClientName = Convert.ToString(rdr["ClientName"]);
                        model.JobDescription = Convert.ToString(rdr["PostText"]);
                        model.postTime = Convert.ToString(rdr["Date"]);
                        model.state = Convert.ToString(rdr["Accept"]);
                        model.budget = Convert.ToInt64(rdr["JopBudget"]);
                        model.ClientEmail = Convert.ToString(rdr["ClientEmail"]);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditMember(RegisterViewModel model)
        {
            var user = db.Users.Where(a => a.Email == model.Email).SingleOrDefault();
            user.firstName = model.firstName;
            user.lastName = model.lastName;
            user.PhoneNumber = model.phone;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(model);
        }

        public ActionResult AddMember()
        {
            return View();
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddMember(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserType = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(), "Name", "Name");

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserType = model.UserType };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, model.UserType);
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Admin");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}