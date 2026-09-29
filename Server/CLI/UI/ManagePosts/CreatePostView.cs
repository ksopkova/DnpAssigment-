using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    //field variable type is the interface = Dependency Inversion Principle from SOLID
    public CreatePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task Show()
    {
        Console.Write("Title: ");
        var title = Console.ReadLine() ?? string.Empty;

        Console.Write("Body: ");
        var body = Console.ReadLine() ?? string.Empty;
        
        Console.Write("Comment: ");
        var comment = Console.ReadLine() ?? string.Empty;

        var post = new Post
        {
            title = title,
            body = body,
            UserId = 1,
            commentId = 0,
            commentBody = string.Empty
        };

        post = await _postRepository.AddAsync(post);

        if (!string.IsNullOrWhiteSpace(comment))
        {
            var newComment = new Comments
            {
                body = comment,
                postId = post.postId,
                UserId = 1
            };

            newComment = await _commentRepository.AddAsync(newComment);

            post.commentId = newComment.commentId;
            post.commentBody = newComment.body;
            await _postRepository.UpdateAsync(post);
        }
    }
}
