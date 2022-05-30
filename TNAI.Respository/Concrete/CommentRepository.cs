using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

using TNAI.Model.Entities;
using TNAI.Respository.Abstract;

namespace TNAI.Respository.Concrete
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        /// <inheritdoc />
        public async Task<Comment> GetCommentAsync(int id)
        {
            return await Context.Comments.Include(x => x.Post).SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await Context.Comments.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool> SaveCommentAsync(Comment comment)
        {
            if (comment == null)
                return false;

            try
            {
                Context.Entry(comment).State = comment.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await GetCommentAsync(id);
            if (comment == null)
                return true;

            Context.Comments.Remove(comment);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}

