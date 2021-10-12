using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1_Core50.Models
{
    public class tblPlaces
    {
        [System.ComponentModel.DataAnnotations.Required]
        public Int32 id { get; set; }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public String Description { get; set; }

    }
}
