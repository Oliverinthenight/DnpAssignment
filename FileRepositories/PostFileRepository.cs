using System.Text.Json;
using Entities;
using RepositoryContracts;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await ReadFromFileAsync();
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        await WriteToFileAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await ReadFromFileAsync();
        Post existingPost = posts.SingleOrDefault(p => p.Id == post.Id);

        if (existingPost == null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found.");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        await WriteToFileAsync(posts);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await ReadFromFileAsync();
        Post postToRemove = posts.SingleOrDefault(p => p.Id == id);

        if (postToRemove == null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }

        posts.Remove(postToRemove);
        await WriteToFileAsync(posts);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await ReadFromFileAsync();
        Post post = posts.SingleOrDefault(p => p.Id == id);

        if (post == null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }

        return post;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();
        return posts.AsQueryable();
    }
    private async Task<List<Post>> ReadFromFileAsync()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();
    }
    private async Task WriteToFileAsync(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
}
