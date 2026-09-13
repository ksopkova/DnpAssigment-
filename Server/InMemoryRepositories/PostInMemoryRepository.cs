using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts = new();

    public Task<Post> AddAsync(Post post)
    {
        post.postId = posts.Any()
            ? posts.Max(p => p.postId) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.postId == post.postId);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.postId}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }
    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.postId == id);
        if (post is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        return Task.FromResult(post);
    }
    
    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
    
    public Task DeleteAsync(int id)
    {
        Post? postToDelete = posts.SingleOrDefault(p => p.postId == id);
        if (postToDelete is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToDelete);
        return Task.CompletedTask;
    }
}