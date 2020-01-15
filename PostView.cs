using System;
using System.Collections.Generic;

namespace EFViewAndManyToMany
{
    public partial class PostView
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
