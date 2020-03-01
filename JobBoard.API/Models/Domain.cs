using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.API.Models
{
#pragma warning disable CS1591
    public partial class Job
    {
        public Job()
        {
        }
        public Job(int? jobID)
        {
            JobID = jobID;
        }
        public int? JobID { get; set; }
        public string JobTitle { get; set; }
        public int? CompanyID { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal? Salary { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? DatePosted { get; set; }
        public DateTime? DateExpires { get; set; }
    }

    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            // Set configuration for entity
            builder.ToTable("Job", "JobBoard");

            // Set key for entity
            builder.HasKey(p => p.JobID);

            // Set configuration for columns

            builder.Property(p => p.JobTitle).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.CompanyID).HasColumnType("int").IsRequired();
            builder.Property(p => p.Location).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.Salary).HasColumnType("int").IsRequired();
            builder.Property(p => p.ContactEmail).HasColumnType("nvarchar(100)");
            builder.Property(p => p.DatePosted).HasColumnType("datetime2");
            builder.Property(p => p.DateExpires).HasColumnType("datetime2").IsRequired();
          
            // Columns with default value
            builder
                .Property(p => p.JobID)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR [Sequences].[JobID]");

            // Columns with generated value on add or update
      //      builder
      //          .Property(p => p.DatePosted)
      //          .HasColumnType("datetime2")
      //          .IsRequired()
      //          .ValueGeneratedOnAddOrUpdate();
        }
    }

    public class JobBoardDbContext : DbContext
    {
        public JobBoardDbContext(DbContextOptions<JobBoardDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity

            modelBuilder.ApplyConfiguration(new JobConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Job> Job { get; set; }
    }
#pragma warning restore CS1591
}
