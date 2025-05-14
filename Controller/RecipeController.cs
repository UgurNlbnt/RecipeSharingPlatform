using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var recipes = _context.Recipe.ToList().Select(s => s.toRecipeDto());;
            
            return Ok(recipes);
        }


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var recipe = _context.Recipe.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe.toRecipeDto());
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateRecipeRequestDto recipeDto)    
        {
        var recipeModel = recipeDto.ToRecipeFromCreateDTO();
        _context.Recipe.Add(recipeModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = recipeModel.Id }, recipeModel.toRecipeDto());
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateRecipeRequestDto recipeDto)
        {
            var recipemodel = _context.Recipe.FirstOrDefault(x => x.Id == id);
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

            _context.SaveChanges();

            return Ok(recipemodel.toRecipeDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var recipemodel = _context.Recipe.FirstOrDefault(x => x.Id == id);
            if (recipemodel == null)
            {
                return NotFound();
            }

            _context.Recipe.Remove(recipemodel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
