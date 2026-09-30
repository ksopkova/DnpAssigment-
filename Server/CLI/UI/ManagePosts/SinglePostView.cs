using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    
    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task Show(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);

        Console.WriteLine($"Post ID: {post.postId}");
        Console.WriteLine($"Title: {post.title}");
        Console.WriteLine($"Body: {post.body}");
        Console.WriteLine($"User ID: {post.UserId}");

        Console.WriteLine("Comments:");
        foreach (var comment in _commentRepository.GetMany().Where(c => c.postId == id))
        {
            Console.WriteLine($"- Comment ID: {comment.commentId}");
            Console.WriteLine($"  Body: {comment.body}");
            Console.WriteLine($"  User ID: {comment.UserId}");
        }
    }
    
}
