using System.Linq;
using NUnit.Framework;
using Sql.Context;

namespace Tester.Sql.Repositories.IntegrationTests
{
    [TestFixture]
    public class RepositoryFixture
    {
        [TearDown]
        public void CleanDatabase()
        {
            using (var context = new QuizContext())
            {
                var tableNames = context.Database
                    .SqlQuery<string>(
                        "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%Migration%'")
                    .ToList();

                foreach (var tableName in tableNames)
                {
                    context.Database.ExecuteSqlCommand($"DELETE FROM dbo.[{tableName}]; DBCC CHECKIDENT ('dbo.[{tableName}]', RESEED, 0)");
                }

                context.SaveChanges();
            }
        }
    }
}