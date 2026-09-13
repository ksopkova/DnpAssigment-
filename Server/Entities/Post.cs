namespace Entities;

public class Post
{
    public int postId { get; set; }
    public int UserId { get; set; }
    public string title { get; set; }
    public string body { get; set; }
    public int commentId { get; set; }
    public override string ToString()
    {
        return $"Title: {title}\n" +
               $"Body: {body}\n" +
               $"UserId: {UserId}\n"+
               $"CommentId: {commentId}\n";
        
    }
}