using System;
using System.Collections.Generic;

namespace EFViewAndManyToMany
{
    public partial class Post_Tag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
