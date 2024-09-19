using Entities;
using RepositoryContracts;

namespace InMemoryRepositories
{
    public class UserInMemoryRepository : IUserRepository
    {
        private readonly List<User> users = new();
        
        public UserInMemoryRepository()
        {
            users.Add(new User { Id = 1, Username = "JohnDoe", Password = "password1" });
            users.Add(new User { Id = 2, Username = "JaneDoe", Password = "password2" });
        }
        
        public Task<User> AddAsync(User user)
        {
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            return Task.FromResult(user);
        }
        
        public Task UpdateAsync(User user)
        {
            var existingUser = users.SingleOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with ID '{user.Id}' not found");
            }

            users.Remove(existingUser);  
            users.Add(user);             

            return Task.CompletedTask;
        }
        
        public Task DeleteAsync(int id)
        {
            var userToRemove = users.SingleOrDefault(u => u.Id == id);
            if (userToRemove == null)
            {
                throw new InvalidOperationException($"User with ID '{id}' not found");
            }

            users.Remove(userToRemove);  
            return Task.CompletedTask;
        }
        
        public Task<User> GetSingleAsync(int id)
        {
            var user = users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID '{id}' not found");
            }

            return Task.FromResult(user);
        }

        public IQueryable<User> GetMany()
        {
            return users.AsQueryable();
        }
    }
}
