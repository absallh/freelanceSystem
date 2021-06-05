using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w.Models;

namespace freelance.Controllers
{
    [Authorize(Roles = "Freelancer")]
    public class FreelancerController : Controller
    {
        private ApplicationDbContext db;

        public FreelancerController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Freelancer
        public ActionResult Index()
        {
            return View();
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