using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class NewBeerViewModel
    {
        public int BreweryID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public double ABV { get; set; }

        [Required]
        public int IBU { get; set; }

        [Required]
        public string HintsOf { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
