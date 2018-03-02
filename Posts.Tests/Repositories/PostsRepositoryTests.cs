using System;
using System.Linq;
using Posts.DataAccess.Repositories;
using Posts.DomainEntities.Entities;
using Posts.Tests.Repositories.Base;
using Xunit;

namespace Posts.Tests.Repositories
{
    public class PostsRepositoryTests : RepositoryTestBase
    {
        private const string CreateWritesDbName = "Posts_Create_writes_to_database";
        private const string UpdateWritesDbName = "Posts_Update_writes_to_database";
        private const string DeleteDropsDbName = "Posts_Delete_drops_from_database";
        private const string GetListRetrievesDbName = "Posts_GetList_retrieves_from_database";

        [Fact]
        public void Create_Writes_To_Database()
        {
            var options = GetInMemoryOptions(CreateWritesDbName);
            var post = CreateEntity();

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(post);
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var restoredPost = await new PostsRepository(context).Get(post.Id);
                AssertEntity(post, restoredPost);
            });
        }

        [Fact]
        public void Update_Write_To_Database()
        {
            var options = GetInMemoryOptions(UpdateWritesDbName);
            var post = CreateEntity();

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(post);
            });

            // run "Update"
            var updatedPost = CreateEntity();
            updatedPost.Id = post.Id;

            RunDbContext(options, async context =>
            {
                await new PostsRepository(context).Update(updatedPost);
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var restoredPost =  await new PostsRepository(context).Get(post.Id);

                AssertEntity(updatedPost, restoredPost);
                // create on was not changed
                Assert.Equal(post.CreatedOn, restoredPost.CreatedOn);
                // modified on changed on update
                Assert.NotEqual(post.ModifiedOn, restoredPost.ModifiedOn);
            });
        }

        [Fact]
        public void GetList_Retrieves_From_Database()
        {
            var options = GetInMemoryOptions(GetListRetrievesDbName);

            // run "Create"
            RunDbContext(options, async context =>
            {
                var repository = new PostsRepository(context);
                await repository.Create(CreateEntity());
                await repository.Create(CreateEntity());
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var getListResult = await new PostsRepository(context).GetList();

                Assert.Equal(2, getListResult.Count);
            });
        }

        [Fact]
        public void Delete_Drops_From_Database()
        {
            var options = GetInMemoryOptions(DeleteDropsDbName);

            var post = CreateEntity();

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(post);
            });

            // run "Delete"
            RunDbContext(options, async context =>
            {
                await new PostsRepository(context).Delete(post.Id);
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var postResult = await new PostsRepository(context).Get(post.Id);

                Assert.Null(postResult);
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
