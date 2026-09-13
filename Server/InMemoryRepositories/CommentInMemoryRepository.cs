using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comments> comments = new();

    public Task<Comments> AddAsync(Comments comment)
    {
        comment.commentId = comments.Any()
            ? comments.Max(c => c.commentId) + 1
            : 1;

        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comments comment)
    {
        Comments? existingComment = comments.SingleOrDefault(c => c.commentId == comment.commentId);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.commentId}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int commentId)
    {
        Comments? commentToDelete = comments.SingleOrDefault(c => c.commentId == commentId);
        if (commentToDelete is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{commentId}' not found");
        }

        comments.Remove(commentToDelete);
        return Task.CompletedTask;
    }

    public Task<Comments> GetSingleAsync(int commentId)
    {
        Comments? comment = comments.SingleOrDefault(c => c.commentId == commentId);
        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{commentId}' not found");
        }

        return Task.FromResult(comment);
    }
    

    public IQueryable<Comments> GetMany()
    {
        return comments.AsQueryable();
    }

    public CommentInMemoryRepository()
    {
        comments = new List<Comments>()
        {
            new Comments()
            {
                body = "Omg this is sooo nicee <333", commentId = 1, UserId = 1, postId = 1
            },
            new Comments()
            {
                body = "I love this post!", commentId = 2, UserId = 2, postId = 2
            },
            new Comments()
            {
                body = "I am so happy to see this post!", commentId = 3, UserId = 3, postId = 3
            },
            new Comments()
            {
                body = "I am so happy to see this post!", commentId = 4, UserId = 1, postId = 1
            }
        };
    }
}