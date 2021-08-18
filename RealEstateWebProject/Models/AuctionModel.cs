using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebProject.Models
{
    public class AuctionModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string propname { get; set; }

        [Required]
        public string custname { get; set; }

        [Required]
        public int bid { get; set; }
    }
}
