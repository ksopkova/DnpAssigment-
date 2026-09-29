using InMemoryRepositories;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly PostInMemoryRepository _postRepository;
    
    public ListPostsView(PostInMemoryRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public void Show()
    {
        foreach (var post in _postRepository.GetMany())
        {
            Console.WriteLine(post);
            Console.WriteLine("----------------");
        }
    }
    
}