using System;
using System.Collections.Generic;

namespace EFViewAndManyToMany
{
    public partial class Tag
    {
        public Tag()
        {
            Post_Tag = new HashSet<Post_Tag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post_Tag> Post_Tag { get; set; }
    }
}
