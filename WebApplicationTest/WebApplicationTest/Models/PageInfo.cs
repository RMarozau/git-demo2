using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationTest.Models;

namespace WebApplicationTest.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

    }

    public class IndexViewModel
    {
        public IEnumerable<Player> Players { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}