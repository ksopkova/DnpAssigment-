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
        // Ask the user for an optional substring filter before listing users.
        Console.Write("Filter by user name contains (leave empty for all): ");
        var userNameContains = Console.ReadLine();

        // Start with all users and narrow the list only when the filter is provided.
        var users = _userRepository.GetMany();

        if (!string.IsNullOrWhiteSpace(userNameContains))
        {
            // Keep only users whose name contains the entered text, case-insensitively.
            users = users.Where(user => user.UserName.Contains(userNameContains, StringComparison.OrdinalIgnoreCase));
        }

        foreach (var user in users)
        {
            Console.WriteLine($"User ID: {user.UserId}");
            Console.WriteLine($"User name: {user.UserName}");
            Console.WriteLine($"Password: {user.Password}");
            Console.WriteLine("----------------");
        }
    }
}
