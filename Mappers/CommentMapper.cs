using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifPaylasim.Dtos.Comment;
using TarifPaylasim.Models;

namespace TarifPaylasim.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto toCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = commentModel.AppUser.UserName,
                RecipeId = commentModel.RecipeId
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentDto commentDto, int recipeId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                RecipeId = recipeId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto, int recipeId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }
    }
}