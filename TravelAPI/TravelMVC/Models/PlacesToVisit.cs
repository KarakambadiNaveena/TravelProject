using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class PlacesToVisit
    {
        [Key]
        public int PlaceId { get; set; }

        public String PlaceName { get; set; }

        public String ModeOfTransport { get; set; }
    }
}