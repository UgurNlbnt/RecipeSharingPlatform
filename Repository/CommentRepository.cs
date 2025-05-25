using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarifPaylasim.Data;
using TarifPaylasim.Interface;
using TarifPaylasim.Models;

namespace TarifPaylasim.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.Include(c => c.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.Include(c=>c.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            commentModel.AppUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == commentModel.AppUserId);
            await _context.Comment.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;

        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return null;
            }

            comment.Title = commentModel.Title;
            comment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return comment;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Comment?> DeleteAsync([FromRoute] int id)
        {
            var commentModel = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
            {
                return null;
            }

            _context.Comment.Remove(commentModel);

            await _context.SaveChangesAsync();
            
            return commentModel;
        }
    }
}