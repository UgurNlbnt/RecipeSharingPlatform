using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifPaylasim.Dtos;
using TarifPaylasim.Dtos.Recipe;
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
                ServingSize = recipeModel.ServingSize,
                Ingredients = recipeModel.Ingredients,
                Instructions = recipeModel.Instructions,
                Comments = recipeModel.Comments.Select(c => c.toCommentDto()).ToList()
           };
        }


    public static Recipes ToRecipeFromCreateDTO(this CreateRecipeRequestDto recipeDto)
    {
        return new Recipes
        {
            Slug = recipeDto.Slug,
            RecipeName = recipeDto.RecipeName,
            PreparationTime = recipeDto.PreparationTime,
            CookTime = recipeDto.CookTime,
            Category = recipeDto.Category,
            ServingSize = recipeDto.ServingSize,
            Ingredients = recipeDto.Ingredients,
            Instructions = recipeDto.Instructions
        };
    }
    }    
}