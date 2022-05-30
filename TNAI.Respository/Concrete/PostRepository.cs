using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

using TNAI.Model.Entities;
using TNAI.Respository.Abstract;

namespace TNAI.Respository.Concrete
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        /// <inheritdoc />
        public async Task<Post> GetPostAsync(int id)
        {
            return await Context.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await Context.Posts.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool> SavePostAsync(Post post)
        {
            if (post == null)
                return false;

            try
            {
                Context.Entry(post).State = post.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await GetPostAsync(id);
            if (post == null)
                return true;

            Context.Posts.Remove(post);

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

