using Accounting.Domain;
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

        public DbSet<UserData> Users { get; set; }

        public DbSet<AccountData> Accounts { get; set; }

        public DbSet<AccountEntryData> AccountEntries { get; set; }

        public DbSet<AccountTransactionData> AccountTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountData>()
                .HasOne(x => x.User)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.UserId);

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

            modelBuilder.Entity<UserData>().HasData(new UserData() { Id = SystemAccount.SystemUserId, Email = "system@gmail.com", FirstName = "SYSTEM", LastName = "SYSTEM" });

            modelBuilder.Entity<AccountData>().HasData(
                new AccountData() { Id = SystemAccount.FromCurrency(AccountCurrency.UAH).Id, Currency = (int)AccountCurrency.UAH, UserId = SystemAccount.SystemUserId },
                new AccountData() { Id = SystemAccount.FromCurrency(AccountCurrency.EUR).Id, Currency = (int)AccountCurrency.EUR, UserId = SystemAccount.SystemUserId },
                new AccountData() { Id = SystemAccount.FromCurrency(AccountCurrency.USD).Id, Currency = (int)AccountCurrency.USD, UserId = SystemAccount.SystemUserId });
        }
    }

    public class UserData
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<AccountData> Accounts { get; set; }
    }

    public class AccountData
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public UserData User { get; set; }

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

        public string Description { get; set; }

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
