using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{

    public class NewBreweryViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Hours { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsBrewerLoggedIn { get; set; }
        public bool IsAdminLoggedIn { get; set; }
    }
}
