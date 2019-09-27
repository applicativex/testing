using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Accounting.Host
{
    public class AccountingContext : DbContext
    {
        public AccountingContext(DbContextOptions<AccountingContext> options)
            : base(options)
        {
        }

        public DbSet<AccountData> Accounts { get; set; }

        public DbSet<AccountEntryData> AccountEntries { get; set; }

        public DbSet<AccountTransactionData> AccountTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntryData>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Entries)
                .HasForeignKey(x => x.AccountId);

            modelBuilder.Entity<AccountTransactionData>()
                .HasOne<AccountEntryData>()
                .WithOne()
                .HasForeignKey<AccountTransactionData>(x => x.DebitEntryId);

            modelBuilder.Entity<AccountTransactionData>()
                .HasOne<AccountEntryData>()
                .WithOne()
                .HasForeignKey<AccountTransactionData>(x => x.CreditEntryId);
        }
    }
        public class AccountData
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        public int Currency { get; set; }

        public List<AccountEntryData> Entries { get; set; }
    }

    public class AccountTransactionData
    {
        public Guid Id { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public Guid DebitEntryId { get; set; }

        public Guid CreditEntryId { get; set; }
    }

    public class AccountEntryData
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public AccountData Account { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset EntryDate { get; set; }
    }
}
