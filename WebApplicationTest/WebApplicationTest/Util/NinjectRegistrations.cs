using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using WebApplicationTest.Repository;
using WebApplicationTest.Models;

namespace WebApplicationTest.Util
{
    public class NinjectRegistrations: NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Player>>().To<PlayerRepository>();
        }

    }
}