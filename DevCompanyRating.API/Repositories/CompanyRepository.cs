using DevCompanyRating.API.Domain;
using DevCompanyRating.API.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevCompanyRating.API.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DevCompanyRatingDbContext _dbContext;
        public CompanyRepository(DevCompanyRatingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Company company)
        {
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();
        }

        public void AddRating(CompanyRating rating)
        {
            _dbContext.CompanyRatings.Add(rating);
            _dbContext.SaveChanges();
        }

        public List<Company> GetAll()
        {
            return _dbContext.Companies.ToList();
        }

        public Company GetByIdWithRatings(int id)
        {
            return _dbContext
                .Companies
                .Include(c => c.Ratings)
                .SingleOrDefault(c => c.Id == id);
        }
    }
}
