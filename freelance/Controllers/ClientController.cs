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
        string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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
        [HttpPost]
        public ActionResult CreatePost(String id, int JopBudget, string JopDescription)
        {
            string Jop = Convert.ToString(JopBudget);
            mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = sqlconn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Posts  Values('2','Ahmed','" + JopDescription + "','15.9.9','" + Jop + "')";
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            return View();
        }

    }
}