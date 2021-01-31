using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Coach { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }

    }
}