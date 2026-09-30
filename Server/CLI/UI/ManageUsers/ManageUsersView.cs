using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository _userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Show()
    {
        while (true)
        {
            Console.WriteLine("=== Manage Users ===");
            Console.WriteLine("1 - List users");
            Console.WriteLine("2 - Create user");
            Console.WriteLine("3 - Single user");
            Console.WriteLine("0 - Back");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var listUsersView = new ListUsersView(_userRepository);
                    listUsersView.Show();
                    break;

                case "2":
                    var createUserView = new CreateUserView(_userRepository);
                    await createUserView.Show();
                    break;

                case "3":
                    Console.Write("User ID: ");
                    if (int.TryParse(Console.ReadLine(), out var userId))
                    {
                        var singleUserView = new SingleUserView(_userRepository);
                        await singleUserView.Show(userId);
                    }
                    else
                    {
                        Console.WriteLine("Neplatné ID.");
                    }
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
