using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarifPaylasim.Data;
using TarifPaylasim.Dtos;
using TarifPaylasim.Dtos.Recipe;
using TarifPaylasim.Mappers;

namespace TarifPaylasim.Controller
{
   
    [ApiController]
    [Route("api/recipe")]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recipes = await _context.Recipe.ToListAsync();

            var recipeDto = recipes.Select(x => x.toRecipeDto());
            
            return Ok(recipes);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var recipe = await  _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe.toRecipeDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecipeRequestDto recipeDto)    
        {
            var recipeModel = recipeDto.ToRecipeFromCreateDTO();
            await _context.Recipe.AddAsync(recipeModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = recipeModel.Id }, recipeModel.toRecipeDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRecipeRequestDto recipeDto)
        {
            var recipemodel = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);
            if (recipemodel == null)
            {
                return NotFound();
            }

            recipemodel.Slug = recipeDto.Slug;
            recipemodel.RecipeName = recipeDto.RecipeName;
            recipemodel.PreparationTime = recipeDto.PreparationTime;
            recipemodel.CookTime = recipeDto.CookTime;
            recipemodel.Category = recipeDto.Category;
            recipemodel.ServingSize = recipeDto.ServingSize;
            recipemodel.Ingredients = recipeDto.Ingredients;
            recipemodel.Instructions = recipeDto.Instructions;

           await _context.SaveChangesAsync();

            return Ok(recipemodel.toRecipeDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var recipemodel = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);
            if (recipemodel == null)
            {
                return NotFound();
            }

            _context.Recipe.Remove(recipemodel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
