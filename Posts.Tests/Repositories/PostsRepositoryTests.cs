using System;
using System.Linq;
using Posts.DataAccess.Repositories;
using Posts.DomainEntities.Entities;
using Xunit;

namespace Posts.Tests.Repositories
{
    public class PostsRepositoryTests : RepositoryTestBase
    {
        public PostsRepositoryTests()
        {
        }

        [Fact]
        public void Create_Writes_To_Database()
        {
            var options = GetInMemoryOptions("Add_writes_to_database");

            var post = CreateEntity();

            // run "Create"
            RunDbContext(options, context =>
            {
                var repository = new PostsRepository(context);
                post.Id = repository.Create(post).Id;
            });

            // assert from another context
            RunDbContext(options, context =>
            {
                var restoredPost = context.Posts.FirstOrDefault(p => p.Id == post.Id);
                AssertEntity(post, restoredPost);
            });
        }

        private Post CreateEntity()
        {
            return new Post()
            {
                Author = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            };
        }

        private void AssertEntity(Post sourse, Post target)
        {
            Assert.Equal(sourse.Author, target.Author);
            Assert.Equal(sourse.Content, target.Content);
            Assert.Equal(sourse.Title, target.Title);
        }
    }
}
