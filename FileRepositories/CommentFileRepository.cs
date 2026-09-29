using System.Text.Json;
using Entities;
using RepositoryContracts;
namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string _filePath = "comments.json";

    
    //Line 15. The constructor ensures there actually is a file. If none exists (e.g., the first time the program is run),
    //a new file is created with the content of an empty list, i.e., no entities. The "[]" is an empty collection in JSON.
    //
    public CommentFileRepository()
    {
        if(!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    // Asynchronous method returning the Task with an exact comment (e.g. comment with ID)
    public async Task<Comments> AddAsync(Comments comment)
    { 
        //We read all the content from the file; this is, of course, in JSON format
        string commentsAsJson = await File.ReadAllTextAsync(_filePath);
        
        //The JSON is deserialized into a list of comments
        List<Comments> commentsList = JsonSerializer.Deserialize<List<Comments>>(commentsAsJson)!;
        int maxId = commentsList.Count > 0 ? commentsList.Max(c => c.Id) : 1;
        comment.commentId = maxId + 1;
        commentsList.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(commentsList);
        await File.WriteAllTextAsync(_filePath, commentsAsJson);
        return comment;
    }
    
    
    
    
    
}