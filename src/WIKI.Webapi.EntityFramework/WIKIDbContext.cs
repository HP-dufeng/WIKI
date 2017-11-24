
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Core.Entities;

namespace WIKI.EntityFramework
{
    public class WIKIDbContext : DbContext
    {
        public virtual IDbSet<Account> Account { get; set; }


        public virtual IDbSet<Tag> Tag { get; set; }

        public virtual IDbSet<Content> Content { get; set; }

        public virtual IDbSet<Question> Question { get; set; }
        public virtual IDbSet<Answer> Answer { get; set; }

        public virtual IDbSet<Document> Document { get; set; }
        public virtual IDbSet<DocumentAttachment> DocumentAttachment { get; set; }


        public virtual IDbSet<DocumentView> DocumentView { get; set; }
        public virtual IDbSet<QuestionView> QuestionView { get; set; }

        // Statistic
        public virtual IDbSet<ViewLogDaily> ViewLogDaily { get; set; }
        public virtual IDbSet<ViewLogHistory> ViewLogHistory { get; set; }
        public virtual IDbSet<ViewLogStatistic> ViewLogStatistic { get; set; }
        public virtual IDbSet<DownLoadLogDaily> DownLoadLogDaily { get; set; }

        public WIKIDbContext() : base("WIKI")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Question>().Ignore(t => t.Tags);
            //modelBuilder.Entity<Document>().Ignore(t => t.Tags);
            //modelBuilder.Ignore<QuestionView>();

            //modelBuilder.Types<DocumentView>()
            //    .Configure(ctc => 
            //    {
            //        ctc.Property(m => m.ExpandProperty.CreatedBy).HasColumnName("CreatedBy_FullName");
            //        ctc.Property(m => m.ExpandProperty.CreatedByDepartment).HasColumnName("CreatedBy_Department");
            //    });

            modelBuilder
                .Entity<Account>()
                .Property(t => t.WUCC_UserId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true}));

            modelBuilder
                .Entity<Account>()
                .Property(t => t.UserName)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            modelBuilder.Entity<Question>().Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Document>().Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<ViewLogStatistic>().Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            base.OnModelCreating(modelBuilder);
        }


    }
}