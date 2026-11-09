using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("Posts/{postId:int}/Comments")]

public class CommentsController:ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    
    public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet(Name = "Comments_GetManyByPost")]
    public IEnumerable<Comments> GetMany(int postId, [FromQuery] int? userId = null, [FromQuery] string? userName = null)
    {
        IQueryable<Comments> comments = _commentRepository.GetMany().Where(comment => comment.postId == postId);

        if (userId.HasValue)
        {
            comments = comments.Where(comment => comment.UserId == userId.Value);
        }

        if (!string.IsNullOrWhiteSpace(userName))
        {
            HashSet<int> matchingUserIds = _userRepository.GetMany()
                .Where(user => string.Equals(user.UserName, userName, StringComparison.OrdinalIgnoreCase))
                .Select(user => user.UserId)
                .ToHashSet();

            comments = comments.Where(comment => matchingUserIds.Contains(comment.UserId));
        }

        return comments.ToList();
    }

    [HttpGet("{id:int}", Name = "Comments_GetSingleByPost")]
    public async Task<Comments> GetSingle(int postId, int id)
    {
        var comment = await _commentRepository.GetSingleAsync(id);

        if (comment.postId != postId)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found for post '{postId}'");
        }

        return comment;
    }

    [HttpPost(Name = "Comments_CreateForPost")]
    public async Task<Comments> Add(int postId, Comments comment)
    {
        comment.postId = postId;
        var newComment = await _commentRepository.AddAsync(comment);
        return newComment;
    }

    [HttpPut("{id:int}", Name = "Comments_UpdateForPost")]
    public async Task Update(int postId, int id, Comments comment)
    {
        comment.commentId = id;
        comment.postId = postId;
        await _commentRepository.UpdateAsync(comment);
    }

    [HttpDelete("{id:int}", Name = "Comments_DeleteForPost")]
    public async Task Delete(int postId, int id)
    {
        var comment = await _commentRepository.GetSingleAsync(id);

        if (comment.postId != postId)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found for post '{postId}'");
        }

        await _commentRepository.DeleteAsync(id);
    }
    
    
}