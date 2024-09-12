using Entities;

namespace App.Repositories;

public class UserRepository : IUserRepository
{
    private List<User> _users = new List<User>(); //Temperary storing list within-memory

    public void Create(User user)
    {
        user.Id = _users.Count + 1; //Basic id making
        _users.Add(user);
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public void Update(User user)
    {
        var existingUser = GetById(user.Id);
        if (existingUser != null)
        {
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
        }
    }

    public void Delete(int id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }
}