﻿// <auto-generated />
using EFCore_GraphQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EFCore_GraphQL.Models.Migrations
{
    [DbContext(typeof(EFCore_GraphQLDbContext))]
    [Migration("20190801165224_includePhones")]
    partial class includePhones
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EFCore_GraphQL.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("EFCore_GraphQL.Models.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Number");

                    b.Property<int>("OwnerId");

                    b.Property<int>("PhoneNumberClass");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("EFCore_GraphQL.Models.PhoneNumber", b =>
                {
                    b.HasOne("EFCore_GraphQL.Models.Person", "Owner")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}