using System;
using System.Collections.Generic;

namespace EFViewAndManyToMany
{
    public partial class PostView
    {
        public PostView()
        {
            Post_Tag = new HashSet<Post_Tag>();
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Post_Tag> Post_Tag { get; set; }
  }
}
