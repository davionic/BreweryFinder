using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class User
    {
        public int ID { get; set; }
        public int BreweryID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
      //  public bool IsAdmin { get; set; }
    }
}
