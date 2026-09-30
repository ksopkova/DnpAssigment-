using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository _userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Show()
    {
        foreach (var user in _userRepository.GetMany())
        {
            Console.WriteLine($"User ID: {user.UserId}");
            Console.WriteLine($"User name: {user.UserName}");
            Console.WriteLine($"Password: {user.Password}");
            Console.WriteLine("----------------");
        }
    }
}
