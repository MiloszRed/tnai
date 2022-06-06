using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using TNAI.Model.Entities;
using TNAI.Repository.Abstract;

namespace TNAI.Repository.Concrete
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        /// <inheritdoc />
        public async Task<Post> GetPostAsync(int id)
        {
            return await Context.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<List<Post>> GetAuthorPostsAsync(string author)
        {
            var posts = await Context.Posts
                .Where(x => x.Author.Equals(author))
                .OrderByDescending(post => post.DateTime)
                .ToListAsync();

            //posts.Sort(delegate (Post x, Post y)
            //{
            //    if (x.DateTime == null && y.DateTime == null) return 0;
            //    else if (x.DateTime == null) return -1;
            //    else if (y.DateTime == null) return 1;
            //    else return -x.DateTime.CompareTo(y.DateTime);
            //});

            return posts;
        }

        /// <inheritdoc />
        public async Task<List<Post>> GetAllPostsAsync()
        {
            var posts = await Context.Posts
                .OrderByDescending(post => post.DateTime)
                .ToListAsync();

            return posts;
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

