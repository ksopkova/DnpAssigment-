using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string _filePath = "comments.json";

    //The constructor ensures there actually is a file. If none exists (e.g., the first time the program is run),
    //a new file is created with the content of an empty list, i.e., no entities. The "[]" is an empty collection in JSON
    public CommentFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    //Asynchronous method returning the Task with an exact comment (e.g. comment with ID)
    //Using JSON and the file to get a list :
    private async Task<List<Comments>> LoadAsync()
    {
        string commentsAsJson = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<Comments>>(commentsAsJson) ?? new List<Comments>();
    }

    private async Task SaveAsync(List<Comments> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(_filePath, commentsAsJson);
    }

    public async Task<Comments> AddAsync(Comments comment)
    {
        List<Comments> comments = await LoadAsync();

        int maxId = comments.Any() ? comments.Max(c => c.commentId) : 0;

        comment.commentId = maxId + 1;

        comments.Add(comment);

        await SaveAsync(comments);

        return comment;
    }

    public async Task UpdateAsync(Comments comment)
    {
        List<Comments> comments = await LoadAsync();
        Comments? existingComment = comments.SingleOrDefault(c => c.commentId == comment.commentId);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.commentId}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);
        await SaveAsync(comments);
    }

    public async Task DeleteAsync(int commentId)
    {
        List<Comments> comments = await LoadAsync();
        Comments? commentToDelete = comments.SingleOrDefault(c => c.commentId == commentId);
        if (commentToDelete is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{commentId}' not found");
        }

        comments.Remove(commentToDelete);
        await SaveAsync(comments);
    }

    public async Task<Comments> GetSingleAsync(int commentId)
    {
        List<Comments> comments = await LoadAsync();
        Comments? comment = comments.SingleOrDefault(c => c.commentId == commentId);
        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{commentId}' not found");
        }

        return comment;
    }

    public IQueryable<Comments> GetMany()
    {
        List<Comments> comments = LoadAsync().GetAwaiter().GetResult();
        return comments.AsQueryable();
    }
}
