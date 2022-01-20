using Microsoft.EntityFrameworkCore;
using RevenueCompanion.Domain.ExternalEntities;

namespace RevenueCompanion.Infrastructure.Persistence.Contexts
{
    public partial class AssessmentsContext : DbContext
    {
        public AssessmentsContext()
        {
        }

        public AssessmentsContext(DbContextOptions<AssessmentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assessment> Assessment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.AgencyCode).IsUnicode(false);

                entity.Property(e => e.AgencyName).IsUnicode(false);

                entity.Property(e => e.AgentUtin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AmountPaid)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AssessmentApprovedBy).IsUnicode(false);

                entity.Property(e => e.AssessmentBalance)
                    .HasColumnType("decimal(21, 2)")
                    .HasComputedColumnSql("([TotalAmount]-[AmountPaid])");

                entity.Property(e => e.AssessmentCreatedBy).IsUnicode(false);

                entity.Property(e => e.AssessmentCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AssessmentDateApproved).HasColumnType("datetime");

                entity.Property(e => e.AssessmentRefNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DatePushtoXpress).HasColumnType("datetime");

                entity.Property(e => e.DateReversed).HasColumnType("datetime");

                entity.Property(e => e.IsReversed).HasDefaultValueSql("((0))");

                entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.MerchantCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Narration).IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.PartPaymentAllow).HasDefaultValueSql("((0))");

                entity.Property(e => e.PayerName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Platformcode)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousYearAssessmentRefNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RevenueCode).IsUnicode(false);

                entity.Property(e => e.RevenueName).IsUnicode(false);

                entity.Property(e => e.Reversedby)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TaxYear)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
