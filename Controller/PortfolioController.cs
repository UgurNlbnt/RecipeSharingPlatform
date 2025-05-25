using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TarifPaylasim.Extentions;
using TarifPaylasim.Interface;
using TarifPaylasim.Models;

namespace TarifPaylasim.Controller
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRecipeRepository _recipeRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IRecipeRepository recipeRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _recipeRepo = recipeRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolios = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolios);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string slug)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var recipe = await _recipeRepo.GetBySlugAsync(slug);

            if (recipe == null)
            {
                return BadRequest("Tarif bulunamadı");
            }

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if (userPortfolio.Any(x => x.Slug.ToLower() == slug.ToLower()))
            {
                return BadRequest("Tarif zaten portföyde mevcut");
            }

            var portfolioModel = new Portfolio
            {
                RecipeId = recipe.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepo.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Portföy oluşturulamadı");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string slug)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            var filteredRecipe = userPortfolio.Where(x => x.Slug.ToLower() == slug.ToLower()).ToList();

            if (filteredRecipe.Count == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser, slug);
            }

            else
            {
                return BadRequest("Tarif portfoyde bulunamadı");
            }

            return Ok();
        }
    } 
}