using DevCompanyRating.API.Domain;
using System.Collections.Generic;

namespace DevCompanyRating.API.Repositories
{
    public interface ICompanyRepository
    {
        List<Company> GetAll();
        Company GetByIdWithRatings(int id);
        void Add(Company company);
        void AddRating(CompanyRating rating);
    }
}
