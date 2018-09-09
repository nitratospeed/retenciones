using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Retenciones.Models;

[assembly: OwinStartupAttribute(typeof(Retenciones.Startup))]
namespace Retenciones
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUsers();
        }

        private void CreateUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Supervisor"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Supervisor";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.Nombres = "administrador";
                user.UserName = "admin";
                user.UsuarioE = "adminE";

                string userPWD = "admin2017!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Supervisor");

                }
            }

            if (!roleManager.RoleExists("Asesor"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Asesor";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("AsesorCarruseles"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "AsesorCarruseles";
                roleManager.Create(role);
            }
        }
    }
}
