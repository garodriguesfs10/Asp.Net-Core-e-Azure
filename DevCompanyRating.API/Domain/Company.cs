using System.Collections.Generic;

namespace DevCompanyRating.API.Domain
{
    public class Company
    {
        protected Company() { }
        public Company(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CompanyRating> Ratings { get; set; }
    }
}
