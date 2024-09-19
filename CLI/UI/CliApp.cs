using System.Threading.Tasks;
using CLI.UI.ManagePosts;
using RepositoryContracts;

namespace CLI.UI
{
    public class CliApp
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Welcome to the CLI App!");
            
            CreatePostView createPostView = new CreatePostView(_postRepository);
            await createPostView.CreatePostAsync();
        }
    }
}