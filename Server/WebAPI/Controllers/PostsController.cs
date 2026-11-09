using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    //dependency has been delivered through constructor (Constructor Injection)
    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    [HttpGet(Name = "Posts_GetMany")]
    public IEnumerable<Post> GetMany()
    {
        return _postRepository.GetMany().ToList();
    }

    [HttpGet("{id:int}", Name = "Posts_GetSingle")]
    public async Task<Post> GetSingle(int id)
    {
        Post post = await _postRepository.GetSingleAsync(id);
        return post;
    }
    
    [HttpPost(Name = "Posts_Create")]
    public async Task<Post> Add(Post post)
    {
        Post newPost = await _postRepository.AddAsync(post);
        return newPost;
    }
    
    [HttpPut(Name = "Posts_Update")]
    public async Task Update(Post post)
    {
        await _postRepository.UpdateAsync(post);
    }
    
    [HttpDelete("{id:int}", Name = "Posts_Delete")]
    public async Task Delete(int id)
    {
        await _postRepository.DeleteAsync(id);
    }
    
    
    
}
