﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimchasEF.Data;

namespace SimchasEF.Data.Migrations
{
    [DbContext(typeof(SimchaContext))]
    [Migration("20190602165544_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimchasEF.Data.Contribution", b =>
                {
                    b.Property<int>("SimchaId");

                    b.Property<int>("ContributorId");

                    b.Property<decimal>("Amount");

                    b.HasKey("SimchaId", "ContributorId");

                    b.HasIndex("ContributorId");

                    b.ToTable("Contributions");
                });

            modelBuilder.Entity("SimchasEF.Data.Contributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AlwaysInclude");

                    b.Property<decimal>("Balance");

                    b.Property<string>("Cell");

                    b.Property<DateTime>("DateJoined");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Contributors");
                });

            modelBuilder.Entity("SimchasEF.Data.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<int>("ContributorId");

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.HasIndex("ContributorId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("SimchasEF.Data.Simcha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Simchas");
                });

            modelBuilder.Entity("SimchasEF.Data.Contribution", b =>
                {
                    b.HasOne("SimchasEF.Data.Contributor", "Contributor")
                        .WithMany("Contributions")
                        .HasForeignKey("ContributorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SimchasEF.Data.Simcha", "Simcha")
                        .WithMany("Contributions")
                        .HasForeignKey("SimchaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SimchasEF.Data.Deposit", b =>
                {
                    b.HasOne("SimchasEF.Data.Contributor", "Contributor")
                        .WithMany()
                        .HasForeignKey("ContributorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
