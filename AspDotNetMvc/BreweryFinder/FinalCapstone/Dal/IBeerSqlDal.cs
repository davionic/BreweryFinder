using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public interface IBeerSqlDal
    {
        List<Beer> GetBeers(int breweryId);
        Beer GetBeer(int beerId);
        bool AddBeer(Beer beer);
        bool UpdateBeer(Beer beer);
        bool DeleteBeer(int beerId);
    }
}
