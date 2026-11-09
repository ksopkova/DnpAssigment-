using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    //dependency has been delivered through constructor (Constructor Injection)
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet(Name = "Users_GetMany")]
    public IEnumerable<User> GetMany([FromQuery] string? userNameContains = null)
    {
        var users = _userRepository.GetMany();

        if (!string.IsNullOrWhiteSpace(userNameContains))
        {
            users = users.Where(user => user.UserName.Contains(userNameContains, StringComparison.OrdinalIgnoreCase));
        }

        return users;
    }
    
    [HttpGet("{id:int}", Name = "Users_GetSingle")]
    public async Task<ActionResult<User>> GetSingle(int id)
    {
        try
        {
            var user = await _userRepository.GetSingleAsync(id);
            return user;
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(Name = "Users_Create")]
    public Task<User>AddAsync(User user)
    {
        return _userRepository.AddAsync(user);
    }

    [HttpPut(Name = "Users_Update")]
    public async Task<IActionResult> UpdateAsync(User user)
    {
        try
        {
            await _userRepository.UpdateAsync(user);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{id:int}", Name = "Users_Delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _userRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}
