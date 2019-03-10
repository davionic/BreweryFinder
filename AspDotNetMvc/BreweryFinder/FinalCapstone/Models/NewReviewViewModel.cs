using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class NewReviewViewModel
    {
        public int BeerID { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string BodyText { get; set; }

        [Required]
        public int Rating { get; set; }

        public static List<SelectListItem> Ratings = new List<SelectListItem>()
        {
            new SelectListItem() {Text = "1"},
            new SelectListItem() {Text = "2"},
            new SelectListItem() {Text = "3"},
            new SelectListItem() {Text = "4"},
            new SelectListItem() {Text = "5"}
        };
    }
}
