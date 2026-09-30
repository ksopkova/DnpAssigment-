using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");

IUserRepository userRepository = new UserFileRepository(); //Old : UserInMemoryRepository();
ICommentRepository commentRepository = new CommentFileRepository(); //Old : CommentInMemoryRepository();
IPostRepository postRepository = new PostFileRepository(); //Old : PostInMemoryRepository();

CliApp cliApp = new CliApp(userRepository, commentRepository,postRepository);

await cliApp.StartAsync();
