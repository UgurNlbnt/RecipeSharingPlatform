using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TarifPaylasim.Data;
using TarifPaylasim.Interface;
using TarifPaylasim.Models;

namespace TarifPaylasim.Repository
{
    
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolio.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser user, string slug)
        {
            var portfolioModel = await _context.Portfolio.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Recipe.Slug.ToLower() == slug.ToLower());

            if (portfolioModel == null)
            {
                return null;
            }

            _context.Portfolio.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Recipes>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolio
            .Where(p => p.AppUserId == user.Id)
            .Select(a => new Recipes
            {
                Id = a.Recipe.Id,
                Slug = a.Recipe.Slug,
                RecipeName = a.Recipe.RecipeName,
                Category = a.Recipe.Category,
                Ingredients = a.Recipe.Ingredients,
                Instructions = a.Recipe.Instructions,
                PreparationTime = a.Recipe.PreparationTime,
                CookTime = a.Recipe.CookTime,
                ServingSize = a.Recipe.ServingSize
            }).ToListAsync();
        }
    }
}