using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplicationTest.Context
{
    public class AppContextIdentity: IdentityDbContext<ApplicationUser>
    {
        public AppContextIdentity(): base("Data Source=Роман-ПК;Integrated Security=True; Initial Catalog= PlayersTeam;")
        {

        }

        public static AppContextIdentity Create()
        {
            return new AppContextIdentity();
        }

    }

    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
        public ApplicationUser()
        {
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public string Description { get; set; }
    }


}