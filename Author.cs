﻿using System;
using System.Collections.Generic;

namespace EFViewAndManyToMany
{
    public partial class Author
    {
        public Author()
        {
            Author_Tag = new HashSet<Author_Tag>();
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Author_Tag> Author_Tag { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
