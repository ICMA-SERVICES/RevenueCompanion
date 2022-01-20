using Microsoft.EntityFrameworkCore;
using RevenueCompanion.Domain.ExternalEntities;

namespace RevenueCompanion.Infrastructure.Persistence.Contexts
{
    public partial class IReconcileContext : DbContext
    {
        public IReconcileContext()
        {
        }

        public IReconcileContext(DbContextOptions<IReconcileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collection> Collection { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Collection>().Property(u => u.CollectionID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasKey(e => e.PaymentRefNumber);

                entity.ToTable("Collection", "Reconcile");

                entity.Property(e => e.PaymentRefNumber)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Branchcode)
                    .HasColumnName("branchcode")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Branchname)
                    .HasColumnName("branchname")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Channel)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeValueDate).HasColumnType("datetime");

                //entity.Property(e => e.CollectionID)
                //    .HasColumnName("CollectionID")
                //    .ValueGeneratedOnAdd();

                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.PayerId)
                    .HasColumnName("PayerID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Payername)
                    .HasColumnName("payername")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Provider)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PushToReemsBy).IsUnicode(false);

                entity.Property(e => e.PushToReemsOn).HasColumnType("datetime");

                entity.Property(e => e.RevenueCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RevenueName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UsedByPlatform)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
