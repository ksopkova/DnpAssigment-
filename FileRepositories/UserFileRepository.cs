using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string _filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    private async Task<List<User>> LoadAsync()
    {
        string usersAsJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
    }

    private async Task SaveAsync(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(_filePath, usersAsJson);
    }
    
    public async Task<User> AddAsync(User user)
    {
        List<User> users = await LoadAsync();
        user.UserId = users.Any() ? users.Max(u => u.UserId) + 1 : 1;
        users.Add(user);
        await SaveAsync(users);
        return user;
    }
    
    public async Task UpdateAsync(User user)
    {
        List<User> users = await LoadAsync();
        User? existingUser = users.SingleOrDefault(u => u.UserId == user.UserId);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.UserId}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        await SaveAsync(users);
    }
    public async Task DeleteAsync(int id)
    {
        List<User> users = await LoadAsync();
        User? userToDelete = users.SingleOrDefault(u => u.UserId == id);
        if (userToDelete is null)
        {
            throw new InvalidOperationException( $"User with ID '{id}' not found");
        }
        users.Remove(userToDelete);
        await SaveAsync(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await LoadAsync();
        User? user = users.SingleOrDefault(u => u.UserId == id);
        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        return user;
    }
    
    public IQueryable<User> GetMany()
    {
        List<User> users = LoadAsync().Result;
        return users.AsQueryable();
    }


}