using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TarifPaylasim.Dtos.Comment;
using TarifPaylasim.Extentions;
using TarifPaylasim.Interface;
using TarifPaylasim.Mappers;
using TarifPaylasim.Models;

namespace TarifPaylasim.Controller
{
   [ApiController]
   [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IRecipeRepository _recipeRepo;

        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepo, IRecipeRepository recipeRepo, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _commentRepo = commentRepo;
            _recipeRepo = recipeRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(x => x.toCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.toCommentDto());
        }

        [HttpPost("{recipeId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int recipeId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _recipeRepo.RecipeExists(recipeId))
            {
                return BadRequest("Tarif bilgisi bulunamadı");
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreateDTO(recipeId);
            commentModel.AppUserId = appUser.Id;

            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.toCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate(id));

            if (comment == null)
            {
                return NotFound("Yorum bulunamadı.");
            }

            return Ok(comment.toCommentDto());
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

            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("Yorum bulunamadı.");
            }

            return Ok(comment.toCommentDto());
        }
    }
}