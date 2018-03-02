using System.Linq;
using Posts.DataAccess.Repositories;
using Posts.DomainEntities.Entities;
using Posts.Tests.Common;
using Posts.Tests.Repositories.Base;
using Xunit;

namespace Posts.Tests.Repositories
{
    public class CommentsRepositoryTests : RepositoryTestBase
    {
       [Fact]
        public void Create_Writes_To_Database()
        {
            var options = GetInMemoryOptions();
            Post post = null;
            Comment comment = null;

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(EntityBuilder.CreatePostEntity());
                comment = await new CommentsRepository(context).Create(EntityBuilder.CreateCommentEntity(post.Id));
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var restoredComments = await new CommentsRepository(context).GetByPostId(post.Id);
                AssertHelper.AssertCommentEntity(comment, restoredComments.First());
            });
        }

        [Fact]
        public void Update_Write_To_Database()
        {
            var options = GetInMemoryOptions();
            Post post = null;
            Comment comment = null;

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(EntityBuilder.CreatePostEntity());
                comment = await new CommentsRepository(context).Create(EntityBuilder.CreateCommentEntity(post.Id));
            });

            // run "Update"
            var updatedComment = EntityBuilder.CreateCommentEntity(post.Id);
            updatedComment.Id = comment.Id;

            RunDbContext(options, async context =>
            {
                await new CommentsRepository(context).Update(updatedComment);
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var restoredComments = await new CommentsRepository(context).GetByPostId(post.Id);
                var newComment = restoredComments.First();

                AssertHelper.AssertCommentEntity(updatedComment, newComment);
                // create on was not changed
                Assert.Equal(comment.CreatedOn, newComment.CreatedOn);
                // modified on changed on update
                Assert.NotEqual(comment.ModifiedOn, newComment.ModifiedOn);
            });
        }

        [Fact]
        public void Delete_Drops_From_Database()
        {
            var options = GetInMemoryOptions();

            Post post = null;
            Comment comment = null;

            // run "Create"
            RunDbContext(options, async context =>
            {
                post = await new PostsRepository(context).Create(EntityBuilder.CreatePostEntity());
                comment = await new CommentsRepository(context).Create(EntityBuilder.CreateCommentEntity(post.Id));
            });

            // run "Delete"
            RunDbContext(options, async context =>
            {
                await new CommentsRepository(context).Delete(comment.Id);
            });

            // assert from another context
            RunDbContext(options, async context =>
            {
                var comments = await new CommentsRepository(context).GetByPostId(post.Id);

                Assert.Equal(0, comments.Count);
            });
        }
    }
}
