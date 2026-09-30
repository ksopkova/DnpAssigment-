using RepositoryContracts;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CliApp(
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1 - Manage users");
            Console.WriteLine("2 - Manage posts");
            Console.WriteLine("0 - Exit");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var manageUsersView = new ManageUsersView(_userRepository);
                    await manageUsersView.Show();
                    break;

                case "2":
                    var managePostsView = new ManagePostsView(_postRepository, _commentRepository);
                    await managePostsView.Show();
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
