using System.Collections.Generic;
using System.Threading.Tasks;

using TNAI.Model.Entities;

namespace TNAI.Repository.Abstract
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentAsync(int id);
        Task<List<Comment>> GetAllCommentsAsync();
        Task<bool> SaveCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
    }
}
