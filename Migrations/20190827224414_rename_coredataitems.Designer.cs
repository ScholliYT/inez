﻿// <auto-generated />
using System;
using INEZ.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace INEZ.Migrations
{
    [DbContext(typeof(InezContext))]
    [Migration("20190827224414_rename_coredataitems")]
    partial class rename_coredataitems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview8.19405.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("INEZ.Data.Entities.CoreDataItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brands")
                        .HasColumnType("nvarchar(127)")
                        .HasMaxLength(127);

                    b.Property<string>("DatasourceUrl")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("EAN")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("GenericName")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("ImageSmallUrl")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(127)")
                        .HasMaxLength(127);

                    b.HasKey("Id");

                    b.ToTable("CoreDataItems");
                });

            modelBuilder.Entity("INEZ.Data.Entities.ShoppingListItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Checked")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreationTimeStamp")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(127)")
                        .HasMaxLength(127);

                    b.HasKey("Id");

                    b.ToTable("ShoppingListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
