using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations;

namespace WIKI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WIKI.EntityFramework.WIKIDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WIKI";

            DbModelBuilder modelBuilder = new DbModelBuilder();

            modelBuilder.Entity<Users.User>()
                .Property(m => m.WUCCUserId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            modelBuilder.Entity<Tags.Tag>()
                .Property(m => m.Name)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        protected override void Seed(WIKI.EntityFramework.WIKIDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
