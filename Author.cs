using System;
using System.Collections.Generic;

namespace EFViewAndManyToMany
{
    public partial class Author
    {
        public Author()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
