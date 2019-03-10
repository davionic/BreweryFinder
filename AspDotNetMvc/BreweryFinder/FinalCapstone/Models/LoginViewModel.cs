using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class LoginViewModel
    {
        public int BreweryID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public static List<SelectListItem> Breweries = new List<SelectListItem>();

        public void PopulateSelectList(List<Brewery> breweries)
        {
            Breweries.Clear();
            foreach (Brewery brewery in breweries)
            {
                Breweries.Add(new SelectListItem() { Text = brewery.Name, Value = brewery.ID.ToString() });
            }
        }
    }
}
