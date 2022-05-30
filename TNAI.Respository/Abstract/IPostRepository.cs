using System.Collections.Generic;
using System.Threading.Tasks;

using TNAI.Model.Entities;

namespace TNAI.Respository.Abstract
{
    public interface IPostRepository
    {
        Task<Post> GetPostAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
        Task<bool> SavePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
    }
}
