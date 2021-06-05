﻿using freelance.Models;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        DataSet ds;
        string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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

        public ActionResult EditProfile(string fname, string lname, string email, string phone, string password)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update AspNetUsers set firstName='" + fname + "',lastName= '" + lname + "',PhoneNumber = '" + phone + "'  Where Email = '" + email + "'";
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
                    Budget = Convert.ToString(dr["JopBudget"])
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
            cmd.CommandText = "delete from AspNetUsers Where Id ='" + id + "'";
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
            cmd.CommandText = "delete from Posts Where Id =" + id + "";
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
            cmd.CommandText = "update Posts set Accept='accept' Where Id =" + id + "";
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return RedirectToAction("AllPosts", "Admin");
        }
        [HttpPost]
        public ActionResult EditPost(String id)
        {

            return RedirectToAction("AllPosts", "Admin");
        }
    }
}