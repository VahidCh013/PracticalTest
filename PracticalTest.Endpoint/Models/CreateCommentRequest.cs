namespace PracticalTest.Domain.Write.Users;

public class CreateCommentRequest
{
    public string Comment { get; set; }
    public long BlogpostId { get; set; }
}