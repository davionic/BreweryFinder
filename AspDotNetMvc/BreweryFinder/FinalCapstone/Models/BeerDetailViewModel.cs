using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class BeerDetailViewModel
    {
        public Beer ThisBeer { get; set; }
        public bool IsBrewerLoggedIn { get; set; }
        public bool IsAdminLoggedIn { get; set; }
    }
}
