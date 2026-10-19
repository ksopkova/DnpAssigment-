using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly ILogger<PostsController> _logger;

    public PostsController(IPostRepository postRepository, ILogger<PostsController> logger)
    {
        _postRepository = postRepository;
        _logger = logger;
    }
    
    [HttpGet(Name = "GetAllPosts")]
    public IEnumerable<Post> GetAll()
    {
        return _postRepository.GetMany().ToList();
    }

    [HttpGet("{id:int}", Name = "GetSinglePost")]
    public async Task<Post> GetSingle(int id)
    {
        Post post = await _postRepository.GetSingleAsync(id);
        return post;
    }
    
    [HttpPost(Name = "PostNewPost")]
    public async Task<Post> Add(Post post)
    {
        Post newPost = await _postRepository.AddAsync(post);
        return newPost;
    }
    
}
