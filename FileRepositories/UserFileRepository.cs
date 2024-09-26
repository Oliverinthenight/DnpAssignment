using System.Text.Json;
using Entities;
using RepositoryContracts;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        List<User> users = await ReadFromFileAsync();
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        await WriteToFileAsync(users);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        List<User> users = await ReadFromFileAsync();
        User existingUser = users.SingleOrDefault(u => u.Id == user.Id);

        if (existingUser == null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found.");
        }

        users.Remove(existingUser);
        users.Add(user);
        await WriteToFileAsync(users);
    }

    public async Task DeleteAsync(int id)
    {
        List<User> users = await ReadFromFileAsync();
        User userToRemove = users.SingleOrDefault(u => u.Id == id);

        if (userToRemove == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }

        users.Remove(userToRemove);
        await WriteToFileAsync(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await ReadFromFileAsync();
        User user = users.SingleOrDefault(u => u.Id == id);

        if (user == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }

        return user;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    }

    private async Task<List<User>> ReadFromFileAsync()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
    }

    private async Task WriteToFileAsync(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
}
