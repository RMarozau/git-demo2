using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTest.Context;
using WebApplicationTest.Models;
using System.Data.Entity;

namespace WebApplicationTest.Repository
{
    public interface IRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int? id);

        void Create(T item);

        void Update(T item);

        void Delete(int? id);

    }

    public class PlayerRepository : IRepository<Player>
    {
        private AppContext db;

        public PlayerRepository(AppContext context)
        {
            this.db = context;
        }


        public async Task<IEnumerable<Player>> GetAll()
        {
            return await db.Players.ToListAsync();
        }


        public async Task<IEnumerable<Player>> GetAllPage(int page, int pagesize)
        {
            return await db.Players.OrderBy(p => p.Id)
                .Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
        }
        

        public async Task<Player> Get(int? id)
        {
            return await db.Players.FindAsync(id);
        }

        public void Create(Player player)
        {
            db.Players.Add(player);
        }

        public void Update(Player player)
        {
            db.Entry(player).State = System.Data.Entity.EntityState.Modified;

        }

        public void Delete(int? id)
        {
            Player player = db.Players.Find(id);

            if(player != null)
            {
                db.Players.Remove(player);
            }
           
            
        }

        public int GetCountPlayer()
        {
            return db.Players.Count();
        }



    }

    public class TeamRepository : IRepository<Team>
    {
        private AppContext db;

        public TeamRepository(AppContext context)
        {
            this.db = context;
        }


        public async Task<IEnumerable<Team>> GetAll()
        {
            return await db.Teams.ToListAsync();
        }

        public async Task<Team> Get(int? id)
        {
            return await db.Teams.FindAsync(id);
        }

        public void Create(Team team)
        {
            db.Teams.Add(team);
        }

        public void Update(Team team)
        {
            db.Entry(team).State = System.Data.Entity.EntityState.Modified;

        }

        public void Delete(int? id)
        {
            Team team = db.Teams.Include(p => p.Players).FirstOrDefault(t => t.Id == id);

            if (team != null)
            {
                db.Teams.Remove(team);
            }

        }



    }
}
