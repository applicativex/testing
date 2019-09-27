﻿// <auto-generated />
using System;
using Accounting.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Accounting.Host.Migrations
{
    [DbContext(typeof(AccountingContext))]
    [Migration("20190927221148_V4")]
    partial class V4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Accounting.Host.AccountData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Currency");

                    b.Property<Guid>("UserAccountId");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Accounting.Host.AccountEntryData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("EntryDate");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountEntries");
                });

            modelBuilder.Entity("Accounting.Host.AccountTransactionData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("CreditEntryId");

                    b.Property<Guid>("DebitEntryId");

                    b.Property<DateTimeOffset>("TransactionDate");

                    b.HasKey("Id");

                    b.HasIndex("CreditEntryId")
                        .IsUnique();

                    b.HasIndex("DebitEntryId")
                        .IsUnique();

                    b.ToTable("AccountTransactions");
                });

            modelBuilder.Entity("Accounting.Host.AccountEntryData", b =>
                {
                    b.HasOne("Accounting.Host.AccountData", "Account")
                        .WithMany("Entries")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Accounting.Host.AccountTransactionData", b =>
                {
                    b.HasOne("Accounting.Host.AccountEntryData")
                        .WithOne()
                        .HasForeignKey("Accounting.Host.AccountTransactionData", "CreditEntryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Accounting.Host.AccountEntryData")
                        .WithOne()
                        .HasForeignKey("Accounting.Host.AccountTransactionData", "DebitEntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
