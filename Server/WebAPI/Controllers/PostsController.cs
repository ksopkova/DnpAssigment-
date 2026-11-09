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
    public async Task<ActionResult<Post>> GetSingle(int id)
    {
        try
        {
            var post = await _postRepository.GetSingleAsync(id);
            return post;
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpPost(Name = "Posts_Create")]
    public async Task<Post> Add(Post post)
    {
        Post newPost = await _postRepository.AddAsync(post);
        return newPost;
    }
    
    [HttpPut(Name = "Posts_Update")]
    public async Task<IActionResult> Update(Post post)
    {
        try
        {
            await _postRepository.UpdateAsync(post);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{id:int}", Name = "Posts_Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _postRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    
    
}
