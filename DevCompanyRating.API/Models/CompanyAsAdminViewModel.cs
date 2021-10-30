using System.Collections.Generic;

namespace DevCompanyRating.API.Models
{
    public class CompanyAsAdminViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<CompanyRatingAsAdminViewModel> Ratings { get; set; }
    }
}
