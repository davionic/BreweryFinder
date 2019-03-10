using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class BreweryListViewModel
    {
        public List<Brewery> Breweries { get; set; }
        public bool IsAdminLoggedIn { get; set; }
        public bool IsBrewerLoggedIn { get; set; }
    }
}
