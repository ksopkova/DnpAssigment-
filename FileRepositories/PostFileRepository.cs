using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository: IPostRepository
{
    private readonly string _filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    private async Task<List<Post>> LoadAsync()
    {
        string postsAsJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson)?? new List<Post>();
    }

    private async Task SaveAsync(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(_filePath, postsAsJson);
    }

    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await LoadAsync();
        post.postId = posts.Any() ? posts.Max(p => p.postId) + 1 : 1;
        posts.Add(post);
        await SaveAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await LoadAsync();
        Post? existingPost = posts.SingleOrDefault(p => p.postId == post.postId);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.postId}' not found");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        await SaveAsync(posts);
    }
    
    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await LoadAsync();
        Post? postToRemove = posts.SingleOrDefault(p => p.postId == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        posts.Remove(postToRemove);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await LoadAsync();
        Post? post = posts.SingleOrDefault(p => p.postId == id);
        if (post is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return post;
    }

    public IQueryable<Post> GetMany()
    {
        List <Post> posts = LoadAsync().GetAwaiter().GetResult();
        return posts.AsQueryable();
    }
}