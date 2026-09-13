using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<Post>AddAsync(Post post);
    Task<Post> GetAsync(int postId);
}