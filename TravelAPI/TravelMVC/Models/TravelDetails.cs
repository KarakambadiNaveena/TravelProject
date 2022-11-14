using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class TravelDetails
    {
        [Key]
        public int TravelId { get; set; }
        public string Location { get; set; }

        public int CostOfTravel { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User UserDetails { get; set; }
        //   public List<User> users { get; set; }




        //public List<String>PlacesToVisit { get; set; }
        // public string[] SuggestedHotels  { get; set; }

        //  public string Images { get; set; }

        //  public virtual ICollection<Comments> comments { get; set; }


    }
}