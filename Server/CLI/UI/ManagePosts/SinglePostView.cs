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
    }
    
}