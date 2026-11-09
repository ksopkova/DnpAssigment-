using System.Net;
using System.Net.Http.Json;
using Entities;

namespace BlazorApp1.Services;

public class BlogApiClient(HttpClient httpClient)
{
    public async Task<List<Post>> GetPostsAsync(CancellationToken cancellationToken = default)
        => await httpClient.GetFromJsonAsync<List<Post>>("Posts", cancellationToken) ?? [];

    public async Task<Post?> GetPostAsync(int postId, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"Posts/{postId}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Post>(cancellationToken);
    }

    public async Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.PostAsJsonAsync("Posts", post, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Post>(cancellationToken) ?? post;
    }

    public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken = default)
        => await httpClient.GetFromJsonAsync<List<User>>("Users", cancellationToken) ?? [];

    public async Task<User?> GetUserAsync(int userId, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"Users/{userId}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken);
    }

    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.PostAsJsonAsync("Users", user, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken) ?? user;
    }

    public async Task<List<Comments>> GetCommentsForPostAsync(int postId, CancellationToken cancellationToken = default)
        => await httpClient.GetFromJsonAsync<List<Comments>>($"Posts/{postId}/Comments", cancellationToken) ?? [];

    public async Task<Comments> CreateCommentAsync(int postId, Comments comment, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.PostAsJsonAsync($"Posts/{postId}/Comments", comment, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Comments>(cancellationToken) ?? comment;
    }
}
