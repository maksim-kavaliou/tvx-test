using System;
using Posts.Web.Core.Models.Base;

namespace Posts.Web.Core.Models
{
    public class CommentModel : BaseModel
    {
        public int PostId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
