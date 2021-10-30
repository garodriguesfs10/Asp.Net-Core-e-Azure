using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCompanyRating.API.Models
{
    public class CreateCompanyRatingInputModel
    {
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
