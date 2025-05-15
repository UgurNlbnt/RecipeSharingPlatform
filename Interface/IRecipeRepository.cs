using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifPaylasim.Dtos;
using TarifPaylasim.Models;

namespace TarifPaylasim.Interface
{
   public interface IRecipeRepository
    {
        Task<List<Recipes>> GetAllAsync();

        Task<Recipes?> GetByIdAsync(int id);

        Task<Recipes> CreatAsync(Recipes recipeModel);

        Task<Recipes?> UpdateAsync(int id, UpdateRecipeRequestDto recipeDto);

        Task<Recipes?> DeleteAsync(int id);

        Task<bool> RecipeExists(int id);}
}