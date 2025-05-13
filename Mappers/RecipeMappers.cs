using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifPaylasim.Dtos;
using TarifPaylasim.Models;

namespace TarifPaylasim.Mappers
{
    public static class RecipeMappers
    {
        public static RecipeDto toRecipeDto(this Recipes recipeModel)
        {
            return new RecipeDto
            {
                Id = recipeModel.Id,
                Slug = recipeModel.Slug,
                RecipeName = recipeModel.RecipeName,
                PreparationTime = recipeModel.PreparationTime,
                CookTime = recipeModel.CookTime,
                Category = recipeModel.Category,
                ServingSize = recipeModel.ServingSize
           };
        }
    }
}