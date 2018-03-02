using System;
using Microsoft.EntityFrameworkCore;
using Posts.DataAccess.Context;

namespace Posts.Tests.Repositories.Base
{
    public class RepositoryTestBase
    {
        public DbContextOptions GetInMemoryOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<DefaultDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        public void RunDbContext(DbContextOptions options, Action<DefaultDbContext> runAction)
        {
            // Run the test against one instance of the context
            using (var context = new DefaultDbContext(options))
            {
                runAction(context);
            }
        }
    }
}
