using System;
using Posts.DomainEntities.Entities;

namespace Posts.Tests.Common
{
    public static class EntityBuilder
    {
        public static Post CreatePostEntity()
        {
            return new Post()
            {
                Author = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            };
        }

        public static Comment CreateCommentEntity(int postId = 0)
        {
            return new Comment()
            {
                Author = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                PostId = postId
            };
        }
    }
}
