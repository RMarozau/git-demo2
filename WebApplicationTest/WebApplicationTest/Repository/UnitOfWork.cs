using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationTest.Context;

namespace WebApplicationTest.Repository
{
    public class UnitOfWork:IDisposable
    {

        //public UnitOfWork(string ConntectionString)
        //{
        //    db = new AppContext(ConntectionString);
        //}

        private AppContext db = new AppContext();
        private PlayerRepository playerRepository;
        private TeamRepository teamRepository;

        public PlayerRepository Players
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new PlayerRepository(db);
                return playerRepository;
            }


        }

        public TeamRepository Teams
        {
            get
            {
                if(teamRepository == null)               
                    teamRepository = new TeamRepository(db);
                return teamRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }

    public interface IUnitOfWork:IDisposable
    {

    }
}