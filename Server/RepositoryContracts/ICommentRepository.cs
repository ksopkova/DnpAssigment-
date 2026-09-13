using Entities;
namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comments> AddAsync(Comments comment);
    Task UpdateAsync(Comments comment);
    Task DeleteAsync(int commentId);
    Task<Comments> GetSingleAsync(int commentId);
    IQueryable<Comments> GetMany();
}
