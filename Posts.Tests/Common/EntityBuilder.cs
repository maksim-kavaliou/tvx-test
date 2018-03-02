using System;
using System.Collections.Generic;
using Posts.DomainEntities.Entities;
using Posts.Web.Core.Models;

namespace Posts.Tests.Common
{
    public static class EntityBuilder
    {
        public static Dictionary<string, Func<PostModel>> PostModelResolver = new Dictionary<string, Func<PostModel>>()
        {
            { "empty", () => new PostModel()},
            { "random", CreatePostModel}
        };

        public static Dictionary<string, Func<CommentModel>> CommentModelResolver = new Dictionary<string, Func<CommentModel>>()
        {
            { "empty", () => new CommentModel()},
            { "random", CreateCommentModel}
        };

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

        public static PostModel CreatePostModel()
        {
            return new PostModel()
            {
                Author = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            };
        }

        public static CommentModel CreateCommentModel()
        {
            return new CommentModel()
            {
                Author = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString()
            };
        }
    }
}
