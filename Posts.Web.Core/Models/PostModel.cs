using System;
using Posts.Web.Core.Models.Base;

namespace Posts.Web.Core.Models
{
    public class PostModel : BaseModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
