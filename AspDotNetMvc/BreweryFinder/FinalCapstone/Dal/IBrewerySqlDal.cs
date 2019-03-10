using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public interface IBrewerySqlDal
    {
        List<Brewery> GetBreweries();
        Brewery GetBrewery(int breweryId);
        bool AddBrewery(Brewery brewery);
        bool UpdateBrewery(Brewery brewery);
        bool DeleteBrewery(int breweryId);
    }
}
