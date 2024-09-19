using Entities;
using RepositoryContracts;

public class CreatePostView
{
    private readonly IPostRepository _postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task CreatePostAsync()
    {
        Console.WriteLine("Enter the title of the post:");
        string title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Title cannot be empty.");
            return;
        }

        Console.WriteLine("Enter the body of the post:");
        string body = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Error: Body cannot be empty.");
            return;
        }
        
        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = 1
        };

        await _postRepository.AddAsync(post);
        Console.WriteLine("Post created successfully!");
    }
}