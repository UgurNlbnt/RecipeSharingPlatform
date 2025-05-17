using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarifPaylasim.Data;
using TarifPaylasim.Dtos;
using TarifPaylasim.Dtos.Recipe;
using TarifPaylasim.Helpers;
using TarifPaylasim.Interface;
using TarifPaylasim.Mappers;
using TarifPaylasim.Repository;

namespace TarifPaylasim.Controller
{
   
    [ApiController]
    [Route("api/recipe")]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecipeRepository _recipeRepo;


        public RecipeController(ApplicationDbContext context, IRecipeRepository recipeRepo)
        {
            _context = context;
            _recipeRepo = recipeRepo;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipes = await _recipeRepo.GetAllAsync(query);

            var recipeDto = recipes.Select(x => x.toRecipeDto()).ToList();

            return Ok(recipes);
        }


        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = await _recipeRepo.GetByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe.toRecipeDto());
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateRecipeRequestDto recipeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeModel = recipeDto.ToRecipeFromCreateDTO();
            await _recipeRepo.CreatAsync(recipeModel);
            return CreatedAtAction(nameof(GetById), new { id = recipeModel.Id }, recipeModel.toRecipeDto());
        }


        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRecipeRequestDto recipeDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipemodel = await _recipeRepo.UpdateAsync(id,recipeDto);
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

            return Ok(recipemodel.toRecipeDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipemodel = await _recipeRepo.DeleteAsync(id);
            if (recipemodel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
