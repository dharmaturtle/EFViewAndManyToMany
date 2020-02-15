using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFViewAndManyToMany
{
    public partial class Author_Tag
    {
        public int AuthorId { get; set; }
        public int TagId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Tag Tag { get; set; }

        //public virtual PostView PostView { get; set; }
    }
}
