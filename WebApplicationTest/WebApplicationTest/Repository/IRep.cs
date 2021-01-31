using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTest.Context;
using WebApplicationTest.Models;

namespace WebApplicationTest.Repository
{
    public interface IRep<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int? id);

        Task<T> Create(T item, string TeamName);

        Task<T> Update(T item);

        Task<T> Delete(int? id);

    }

    public class PlayerRep : IRep<Player>
    {
        private AppContext db;

        public PlayerRep(AppContext context)
        {
            this.db = context;
            db.Configuration.LazyLoadingEnabled = false;
        }


        public async Task<IEnumerable<Player>> GetAll()
        {
            return await db.Players.ToListAsync();
        }

        public async Task<Player> Get(int? id)
        {
            return await db.Players.FindAsync(id);
        }

        public async Task<Player> Create(Player player, string teamName)
        {
            Team team = await db.Teams.FirstOrDefaultAsync(t => t.Name == teamName);
            player.TeamId = team.Id;
            db.Players.Add(player);

            await db.SaveChangesAsync();

            return player;
        }

        public async Task<Player> Update(Player _player)
        {
            
            db.Entry(_player).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();

            return _player;


        }

        public async Task<Player> Delete(int? id)
        {
            Player player = await db.Players.FindAsync(id);

            if (player != null)
            {
                db.Players.Remove(player);
                await db.SaveChangesAsync();
            }

            return player;


        }



    }
}
