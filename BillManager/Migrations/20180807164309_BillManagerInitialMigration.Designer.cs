using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BillManager.Entities;

namespace BillManager.Migrations
{
    [DbContext(typeof(BillManagerDbContext))]
    [Migration("20180807164309_BillManagerInitialMigration")]
    partial class BillManagerInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BillManager.Entities.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("BillManager.Entities.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("BillManager.Entities.SplitBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int>("BillId");

                    b.Property<int>("FriendId");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("FriendId");

                    b.ToTable("SplitBills");
                });

            modelBuilder.Entity("BillManager.Entities.SplitBill", b =>
                {
                    b.HasOne("BillManager.Entities.Bill", "Bill")
                        .WithMany("FriendsSplit")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BillManager.Entities.Friend", "Friend")
                        .WithMany("SplitBills")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
