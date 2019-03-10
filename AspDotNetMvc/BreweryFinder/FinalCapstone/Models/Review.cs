using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int BeerID { get; set; }
        public string User { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public int Rating { get; set; }
    }
}
