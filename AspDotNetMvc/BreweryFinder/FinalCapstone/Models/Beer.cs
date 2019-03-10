using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Beer
    {
        public int ID { get; set; }
        public int BreweryID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double ABV { get; set; }
        public int IBU { get; set; }
        public string Description { get; set; }
        public string HintsOf { get; set; }
        public double AverageRating { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
