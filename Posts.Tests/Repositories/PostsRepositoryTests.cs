using Posts.DataAccess.Repositories;
using Posts.Tests.Common;
using Posts.Tests.Repositories.Base;
using Xunit;

namespace Posts.Tests.Repositories
{
    public class PostsRepositoryTests : RepositoryTestBase
    {
       [Fact]
        public void Create_Writes_To_Database()
        {
            var options = GetInMemoryOptions();
            var post = EntityBuilder.CreatePostEntity();

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(post);
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var restoredPost = await new PostsRepository(context).Get(post.Id);
                AssertHelper.AssertPostEntity(post, restoredPost);
            });
        }

        [Fact]
        public void Update_Write_To_Database()
        {
            var options = GetInMemoryOptions();
            var post = EntityBuilder.CreatePostEntity();

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(post);
            });

            // run "Update"
            var updatedPost = EntityBuilder.CreatePostEntity();
            updatedPost.Id = post.Id;

            RunDbContext(options, async context =>
            {
                await new PostsRepository(context).Update(updatedPost);
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var restoredPost =  await new PostsRepository(context).Get(post.Id);

                AssertHelper.AssertPostEntity(updatedPost, restoredPost);
                // create on was not changed
                Assert.Equal(post.CreatedOn, restoredPost.CreatedOn);
                // modified on changed on update
                Assert.NotEqual(post.ModifiedOn, restoredPost.ModifiedOn);
            });
        }

        [Fact]
        public void GetList_Retrieves_From_Database()
        {
            var options = GetInMemoryOptions();

            // run "Create"
            RunDbContext(options, async context =>
            {
                var repository = new PostsRepository(context);
                await repository.Create(EntityBuilder.CreatePostEntity());
                await repository.Create(EntityBuilder.CreatePostEntity());
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
            var options = GetInMemoryOptions();

            var post = EntityBuilder.CreatePostEntity();

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
    }
}
