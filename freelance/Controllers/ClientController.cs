using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using freelance.Models;
using Microsoft.AspNet.Identity;
using w.Models;

namespace Client.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        DataSet ds;
        string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ApplicationDbContext db;

        public object CorrelationIdGenerator { get; private set; }

        public ClientController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Client
        public new ActionResult Profile()
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
        public new ActionResult Profile(RegisterViewModel Model)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userID).SingleOrDefault();
            user.firstName = Model.firstName;
            user.lastName = Model.lastName;
            user.PhoneNumber = Model.phone;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(Model);
        }

        public ActionResult CreateNewPost()
        {
            Post model = new Post();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNewPost(Post model)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userID).SingleOrDefault();
            if (model.budget != null)
            {//string Jop = Convert.ToString(JopBudget);
                mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                sqlconn.Open();
                SqlCommand cmd = sqlconn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                model.id = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                cmd.CommandText = "insert into Posts Values('" + model.id + "','" + user.firstName + "','" + model.JobDescription + "','" +
                     DateTime.Now.ToString("MM.dd.yyyy") + "','Wait','" + model.budget + "','" + user.Email + "' )";
                cmd.ExecuteNonQuery();
                sqlconn.Close();
            }
            return RedirectToAction("Myposts", "Client", user.Email);
        }

        public ActionResult Myposts(string Email)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userID).SingleOrDefault();
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string Query = "Select ClientName,PostText from Posts where ClientEmail='" + user.Email + "'";
            SqlCommand sqlcomm = new SqlCommand(Query, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            ds = new DataSet();
            sda.Fill(ds);
            List<Post> posts = new List<Post>();
            foreach (DataRow de in ds.Tables[0].Rows)
            {
                posts.Add(new Post
                {
                    jobType = Convert.ToString(de["ClientName"]),
                    JobDescription = Convert.ToString(de["PostText"])
                });
            }
            sqlconn.Close();
            return View(posts);
        }
        public ActionResult ReceivedProposals( )
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userID).SingleOrDefault();
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string Query = "select * from Proposal where ClientEmail ='" + user.Email + "' AND State ='wait'" ;
            SqlCommand sqlcomm = new SqlCommand(Query, sqlconn);
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sqlconn.Open();
            ds = new DataSet();
            sda.Fill(ds);
            List<ProposalViewModel> Proposal = new List<ProposalViewModel>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Proposal.Add(new ProposalViewModel
                {
                    RowId = Convert.ToString(dr["Id"]),
                    PostText = Convert.ToString(dr["PostText"]),
                    FreeLancerName = Convert.ToString(dr["FreeLancerName"]),
                    Time = Convert.ToString(dr["Time"]),
                    State = Convert.ToString(dr["State"]),
                    FreeLancerId = Convert.ToString(dr["FreeLancerId"]),
                    ClientEmail = Convert.ToString(dr["ClientEmail"])
                    
                });
            }
                return View(Proposal);
        }

        [HttpPost]
        public ActionResult ReceivedProposals(string id)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Proposal set State='accept' Where Id ='" + id + "'";
            cmd.ExecuteNonQuery();
            sqlconn.Close();

            return RedirectToAction("Myposts", "Client");
        }
        [HttpPost]
        public ActionResult DeleteProposal(string id)
        {
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Proposal Where PostText ='" + id + "'";
            cmd.ExecuteNonQuery();
            sqlconn.Close();

            return RedirectToAction("Myposts", "Client");
        }
    }
}