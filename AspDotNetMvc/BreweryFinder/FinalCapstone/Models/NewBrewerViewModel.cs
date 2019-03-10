using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class NewBrewerViewModel
    {
        public int ID { get; set; }
        public int BreweryID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
