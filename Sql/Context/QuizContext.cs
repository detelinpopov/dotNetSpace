using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Sql.Entities;

namespace Sql.Context
{
    public class QuizContext : DbContext
    {
        public QuizContext()
            : base("QuizContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<QuizContext>());
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<PublicFeedbackMessage> FeedbackMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}