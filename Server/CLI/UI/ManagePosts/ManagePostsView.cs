using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    
    public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task Show()
    {
        while (true)
        {
            Console.WriteLine("=== Manage Posts ===");
            Console.WriteLine("1 - List posts");
            Console.WriteLine("2 - Create post");
            Console.WriteLine("3 - Single post");
            Console.WriteLine("0 - Back");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var listPostsView = new ListPostsView(_postRepository);
                    listPostsView.Show();
                    break;

                case "2":
                    var createPostView = new CreatePostView(_postRepository, _commentRepository);
                    await createPostView.Show();
                    break;

                case "3":
                    Console.Write("Post ID: ");
                    if (int.TryParse(Console.ReadLine(), out var id))
                    {
                        var singlePostView = new SinglePostView(_postRepository, _commentRepository);
                        await singlePostView.Show(id);
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
