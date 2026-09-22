using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    //field variable type is the interface = Dependency Inversion Principle from SOLID
    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task Show()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Body: ");
        string body = Console.ReadLine();
        
        Console.Write("Comment: ");
        string comment = Console.ReadLine();

        Post post = new Post
        {
            title = title,
            body = body,
            UserId = 1,
            commentId = 1,
            commentBody = comment
        };

        await postRepository.AddAsync(post);

        
    }
}