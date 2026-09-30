using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;
    
    public CreateUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Show()
    {
        Console.Write("User name: ");
        var userName = Console.ReadLine() ?? string.Empty;

        Console.Write("Password: ");
        var password = Console.ReadLine() ?? string.Empty;

        var user = new Entities.User
        {
            UserName = userName,
            Password = password
        };

        await _userRepository.AddAsync(user);
        Console.WriteLine($"Created user with ID {user.UserId}");
    }
    
}
