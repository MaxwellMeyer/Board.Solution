using System.Collections.Generic;

namespace Board.Models
{
  public class Post
  {
    public Post()
    {
      this.JoinEntities = new HashSet<TopicPost>();
    }

    public int PostId { get; set; }
    public string Body { get; set; }
    // public bool Status { get; set; }

    public virtual ICollection<TopicPost> JoinEntities { get; }
  }
}