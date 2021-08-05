namespace Board.Models
{
  public class TopicPost
  {
    public int TopicPostId { get; set; }
    public int PostId { get; set; }
    public int TopicId { get; set; }
    public virtual Post Post { get; set; }
    public virtual Topic Topic { get; set; }
  }
}