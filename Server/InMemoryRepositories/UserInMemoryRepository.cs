using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users = new();

    public Task<User> AddAsync(User user)
    {
        user.UserId = users.Any()
            ? users.Max(u => u.UserId) + 1
            : 1;

        users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> GetAsync(int userId)
    {
        User? user = users.SingleOrDefault(u => u.UserId == userId);
        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{userId}' not found");
        }

        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.UserId == user.UserId);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{user.UserId}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(p => p.UserId == id);
        if (user is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        return Task.FromResult(user);
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(p => p.UserId == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    public UserInMemoryRepository()
    {
        users = new List<User>
        {
            new User()
            {
                Password = "Klokanica", UserId = 1, UserName = "Kristinka"
            },
            new User()
            {
                Password = "Sarkanica", UserId = 2, UserName = "Radka"
            },
            new User()
            {
                Password = "Patkanica", UserId = 3, UserName = "Wilma The Maltipoo"
            }
        };
    }
}
