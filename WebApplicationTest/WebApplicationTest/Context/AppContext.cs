using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplicationTest.Models;

namespace WebApplicationTest.Context
{
    public class AppContext:DbContext
    {
        public AppContext():base("Data Source=Роман-ПК;Integrated Security=True; Initial Catalog= PlayersTeam;")
        {

        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }
    }
}