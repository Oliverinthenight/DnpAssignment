using CLI.UI;
using RepositoryContracts;  

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting CLI app...");
        
        IPostRepository postRepository = new PostFileRepository();
        IUserRepository userRepository = new UserFileRepository();
        ICommentRepository commentRepository = new CommentFileRepository();

        CliApp cliApp = new CliApp(userRepository, postRepository, commentRepository);
        await cliApp.StartAsync();

    }
}