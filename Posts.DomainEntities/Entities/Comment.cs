using System;
using Posts.DomainEntities.Entities.Base;

namespace Posts.DomainEntities.Entities
{
    public class Comment : DomainEntity
    {
        public int PostId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }
    }
}
