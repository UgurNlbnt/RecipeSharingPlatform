using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TarifPaylasim.Data;
using TarifPaylasim.Dtos;
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


        public async Task<List<Recipes>> GetAllAsync()
        {
            return await _context.Recipe.ToListAsync();
        }


        public async Task<Recipes?> GetByIdAsync(int id)
        {
            return await _context.Recipe.FindAsync(id);
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
    }
}