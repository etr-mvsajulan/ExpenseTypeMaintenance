﻿// <auto-generated />
using Expense.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Expense.Migrations
{
    [DbContext(typeof(ExpenseTypeDBContext))]
    partial class ExpenseTypeDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Expense.Models.DBEntities.ExpenseTypes", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Ecode")
                        .IsRequired()
                        .HasColumnType("varchar(50");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("varchar(50");

                    b.HasKey("id");

                    b.ToTable("Expensetype");
                });
#pragma warning restore 612, 618
        }
    }
}
