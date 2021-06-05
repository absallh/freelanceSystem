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

namespace Client.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        DataSet ds;
        string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        String ClientEmail;
        // GET: Client
        public new ActionResult Profile(string Email)
        {
            ClientEmail = Email;
            return View();
        }

        public ActionResult CreateNewPost(string id, string Description, string Price)
        {
            if (Price != null)
            {//string Jop = Convert.ToString(JopBudget);
                mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                sqlconn.Open();
                SqlCommand cmd = sqlconn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Posts Values('4','Ahmed','" + Description + "','1599','Wait','" + Price + "')";
                cmd.ExecuteNonQuery();
                sqlconn.Close();
            }
            return View();
        }

        public ActionResult Myposts(string Email)
        {
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string Query = "Select ClientName,PostText from Posts where ClientEmail='" + Email + "'";
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
        public ActionResult ReceivedProposals()
        {
            return View();
        }

    }
}