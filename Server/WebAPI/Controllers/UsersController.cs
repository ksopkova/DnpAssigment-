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
    public async Task<User> GetSingle(int id)
    {
        var user = await _userRepository.GetSingleAsync(id);
        return user;
    }

    [HttpPost(Name = "Users_Create")]
    public Task<User>AddAsync(User user)
    {
        return _userRepository.AddAsync(user);
    }

    [HttpPut(Name = "Users_Update")]
    public Task UpdateAsync(User user)
    {
        return _userRepository.UpdateAsync(user);
    }
    
    [HttpDelete("{id:int}", Name = "Users_Delete")]
    public Task DeleteAsync(int id)
    {
        return _userRepository.DeleteAsync(id);
    }
    
}