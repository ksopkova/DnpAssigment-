using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{
    private readonly IUserRepository _userRepository;

    public SingleUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Show(int userId)
    {
        var user = await _userRepository.GetSingleAsync(userId);

        Console.WriteLine($"User ID: {user.UserId}");
        Console.WriteLine($"User name: {user.UserName}");
        Console.WriteLine($"Password: {user.Password}");
    }
}
