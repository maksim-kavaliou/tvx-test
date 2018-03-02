using System;
using System.Collections.Generic;
using Posts.DomainEntities.Entities.Base;

namespace Posts.DomainEntities.Entities
{
    public class Post : DomainEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
