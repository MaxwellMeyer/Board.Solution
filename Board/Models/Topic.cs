using System.Collections.Generic;

namespace Board.Models
{
  public class Topic
  {
    public Topic()
    {
      this.JoinEntities = new HashSet<TopicPost>();
    }

    public int TopicId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public virtual ICollection<TopicPost> JoinEntities { get; set; }
  }
}