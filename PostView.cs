using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFViewAndManyToMany
{
    public partial class PostView
    {
        public PostView()
        {
            Post_Tag = new HashSet<Post_Tag>();
            Author_Tag = new HashSet<Author_Tag>();
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Post_Tag> Post_Tag { get; set; }
        public virtual ICollection<Author_Tag> Author_Tag { get; set; }
    }
}
