using Posts.DomainEntities.Entities;
using Posts.Web.Core.Models;
using Xunit;

namespace Posts.Tests.Common
{
    public static class AssertHelper
    {
        public static void AssertPostEntity(Post sourse, Post target)
        {
            Assert.Equal(sourse.Author, target.Author);
            Assert.Equal(sourse.Content, target.Content);
            Assert.Equal(sourse.Title, target.Title);
        }

        public static void AssertCommentEntity(Comment sourse, Comment target)
        {
            Assert.Equal(sourse.Author, target.Author);
            Assert.Equal(sourse.Content, target.Content);
        }

        public static void AssertPostEntityToModel(Post entity, PostModel model)
        {
            Assert.Equal(entity.Author, model.Author);
            Assert.Equal(entity.Content, model.Content);
            Assert.Equal(entity.Title, model.Title);
        }
    }
}
