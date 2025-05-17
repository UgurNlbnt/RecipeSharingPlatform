using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifPaylasim.Models;

namespace TarifPaylasim.Interface
{
    public interface IPortfolioRepository
    {
        Task<List<Recipes>> GetUserPortfolio(AppUser user);

        Task<Portfolio> CreateAsync(Portfolio portfolio);

        Task<Portfolio> DeletePortfolio(AppUser user, string slug);
    }
}