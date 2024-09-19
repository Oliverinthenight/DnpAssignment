using Entities;
using RepositoryContracts;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUserAsync()
    {
        Console.WriteLine("Enter a username:");
        string username = Console.ReadLine();

        // Check om username er taget på forhånd
        var existingUsers = _userRepository.GetMany();
        if (existingUsers.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Error: Username already taken.");
            return;
        }
        
        Console.WriteLine("Enter a password:");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Error: Password cannot be empty.");
            return;
        }
        
        User user = new User
        {
            Username = username,
            Password = password
        };

        // Tilføj user asynchkron.. Asynchronisk? Ja..
        User createdUser = await _userRepository.AddAsync(user);
        
        Console.WriteLine($"User created successfully with ID: {createdUser.Id}");
    }
}