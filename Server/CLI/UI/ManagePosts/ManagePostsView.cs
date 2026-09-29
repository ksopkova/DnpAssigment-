using InMemoryRepositories;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly PostInMemoryRepository _postRepository;
    private readonly CommentInMemoryRepository _commentRepository;
    
    public ManagePostsView(PostInMemoryRepository postRepository, CommentInMemoryRepository commentRepository)
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

            string? choice = Console.ReadLine();

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
