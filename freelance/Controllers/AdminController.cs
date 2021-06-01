using freelance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace freelance.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string Query = "select Email,PhoneNumber,UserName,UserType from AspNetUsers ";
            SqlCommand sqlcomm = new SqlCommand(Query,sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            List<Admin> admins = new List<Admin>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                admins.Add(new Admin
                {
                    Email    = Convert.ToString(dr["Email"]),
                    Phone    = Convert.ToString(dr["PhoneNumber"]),
                    Name     = Convert.ToString(dr["UserName"]),
                    UserType = Convert.ToString(dr["UserType"])
                });
            }
            sqlconn.Close();
            return View(admins);
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        public ActionResult AllPosts()
        {
            return View();
        }
    }
}