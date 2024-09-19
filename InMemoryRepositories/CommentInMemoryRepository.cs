using Entities;
using RepositoryContracts;

namespace InMemoryRepositories
{
    public class CommentInMemoryRepository : ICommentRepository
    {
        private readonly List<Comment> comments = new();
        
        public CommentInMemoryRepository()
        {
            comments.Add(new Comment { Id = 1, Body = "First comment", UserId = 1, PostId = 1 });
            comments.Add(new Comment { Id = 2, Body = "Second comment", UserId = 2, PostId = 1 });
        }
        
        public Task<Comment> AddAsync(Comment comment)
        {
            comment.Id = comments.Any() ? comments.Max(c => c.Id) + 1 : 1; // Assign ID
            comments.Add(comment);
            return Task.FromResult(comment);
        }
        
        public Task UpdateAsync(Comment comment)
        {
            var existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
            if (existingComment == null)
            {
                throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
            }

            comments.Remove(existingComment);
            comments.Add(comment);           

            return Task.CompletedTask;
        }
        
        public Task DeleteAsync(int id)
        {
            var commentToRemove = comments.SingleOrDefault(c => c.Id == id);
            if (commentToRemove == null)
            {
                throw new InvalidOperationException($"Comment with ID '{id}' not found");
            }

            comments.Remove(commentToRemove);
            return Task.CompletedTask;
        }
        
        public Task<Comment> GetSingleAsync(int id)
        {
            var comment = comments.SingleOrDefault(c => c.Id == id);
            if (comment == null)
            {
                throw new InvalidOperationException($"Comment with ID '{id}' not found");
            }

            return Task.FromResult(comment);
        }

        public IQueryable<Comment> GetMany()
        {
            return comments.AsQueryable();
        }
        
        public IQueryable<Comment> GetByUserId(int userId)
        {
            return comments.Where(c => c.UserId == userId).AsQueryable();
        }
    }
}
