using System;

namespace Posts.DomainEntities.Entities.Base
{
    public class DomainEntity
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
