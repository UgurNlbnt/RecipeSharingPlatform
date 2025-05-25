using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TarifPaylasim.Data;
using TarifPaylasim.Dtos;
using TarifPaylasim.Helpers;
using TarifPaylasim.Interface;
using TarifPaylasim.Models;

namespace TarifPaylasim.Repository
{
    public class RecipeRepository : IRecipeRepository
    {

        public readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recipes> CreatAsync(Recipes recipeModel)
        {
            await _context.Recipe.AddAsync(recipeModel);
            await _context.SaveChangesAsync();
            return recipeModel;
        }

        public async Task<Recipes?> DeleteAsync(int id)
        {
            var recipemodel = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);
            if (recipemodel == null)
            {
                return null;
            }

            _context.Recipe.Remove(recipemodel);
            await _context.SaveChangesAsync();
            return recipemodel;
        }

        public async Task<bool> RecipeExists(int id)
        {
            return await _context.Recipe.AnyAsync(x => x.Id == id);
           
        }

        public async Task<List<Recipes>> GetAllAsync(QueryObject query)
        {
            var recipes =  _context.Recipe.Include(c => c.Comments).ThenInclude(z => z.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.RecipeName))
            {
                recipes = recipes.Where(x => x.RecipeName.Contains(query.RecipeName));
            }

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                recipes = recipes.Where(x => x.Category.Contains(query.Category));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
               if (query.SortBy.Equals("RecipeName", StringComparison.OrdinalIgnoreCase))
                {
                recipes = query.IsDecsending ? recipes.OrderByDescending(x => x.RecipeName) : recipes.OrderBy(x => x.RecipeName);
                }

                else if (query.SortBy.Equals("Category", StringComparison.OrdinalIgnoreCase))
                {
                    recipes = query.IsDecsending ? recipes.OrderByDescending(x => x.Category) : recipes.OrderBy(x => x.Category);
                }

                else if (query.SortBy.Equals("CookTime", StringComparison.OrdinalIgnoreCase))
                {
                    recipes = query.IsDecsending ? recipes.OrderByDescending(x => x.CookTime) : recipes.OrderBy(x => x.CookTime);
                }
               
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            
            return await recipes.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }


        public async Task<Recipes?> GetByIdAsync(int id)
        {
            return await _context.Recipe
                .Include(c => c.Comments)
                .ThenInclude(c => c.AppUser) // yorumların yazarını da dahil et
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Recipes?> UpdateAsync(int id, UpdateRecipeRequestDto recipeDto)
        {
            var recipeModel = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);
            if (recipeModel == null)
            {
                return null;
            }

            recipeModel.Slug = recipeDto.Slug;
            recipeModel.RecipeName = recipeDto.RecipeName;
            recipeModel.PreparationTime = recipeDto.PreparationTime;
            recipeModel.CookTime = recipeDto.CookTime;
            recipeModel.Category = recipeDto.Category;
            recipeModel.ServingSize = recipeDto.ServingSize;
            recipeModel.Ingredients = recipeDto.Ingredients;
            recipeModel.Instructions = recipeDto.Instructions;

            return recipeModel;
        }

        public async Task<Recipes?> GetBySlugAsync(string slug)
        {
            return await  _context.Recipe.FirstOrDefaultAsync(x => x.Slug == slug);
        }
    }
}