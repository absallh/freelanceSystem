using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using w.Models;

[assembly: OwinStartupAttribute(typeof(w.Startup))]
namespace w
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefalutRolesAndUsers();
        }

        public void CreateDefalutRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "a@a.com";
                user.Email = "a@a.com";
                var check = userManager.Create(user, "Aa*123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
