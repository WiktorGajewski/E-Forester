﻿// <auto-generated />
using System;
using E_Forester.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_Forester.Data.Migrations
{
    [DbContext(typeof(E_ForesterDbContext))]
    partial class E_ForesterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("E_Forester.Model.Database.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<int>("ForestUnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ForestUnitId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("E_Forester.Model.Database.ForestUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ForestUnits");
                });

            modelBuilder.Entity("E_Forester.Model.Database.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("ForestUnitId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ForestUnitId", "Year")
                        .IsUnique();

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("E_Forester.Model.Database.PlanExecution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<double>("ExecutedHectares")
                        .HasColumnType("float");

                    b.Property<double>("HarvestedCubicMeters")
                        .HasColumnType("float");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("PlanItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PlanId");

                    b.HasIndex("PlanItemId");

                    b.ToTable("PlanExecutions");
                });

            modelBuilder.Entity("E_Forester.Model.Database.PlanItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActionGroup")
                        .HasColumnType("int");

                    b.Property<int>("Assortments")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<double>("Factor")
                        .HasColumnType("float");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<double>("PlannedCubicMeters")
                        .HasColumnType("float");

                    b.Property<double>("PlannedHectares")
                        .HasColumnType("float");

                    b.Property<int>("SubareaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PlanId");

                    b.HasIndex("SubareaId");

                    b.ToTable("PlanItems");
                });

            modelBuilder.Entity("E_Forester.Model.Database.Subarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<int>("DivisionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Subareas");
                });

            modelBuilder.Entity("E_Forester.Model.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("ForestUnitUser", b =>
                {
                    b.Property<int>("AssignedForestUnitsId")
                        .HasColumnType("int");

                    b.Property<int>("AssignedUsersId")
                        .HasColumnType("int");

                    b.HasKey("AssignedForestUnitsId", "AssignedUsersId");

                    b.HasIndex("AssignedUsersId");

                    b.ToTable("ForestUnitUser");
                });

            modelBuilder.Entity("E_Forester.Model.Database.Division", b =>
                {
                    b.HasOne("E_Forester.Model.Database.ForestUnit", "ForestUnit")
                        .WithMany("Divisions")
                        .HasForeignKey("ForestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForestUnit");
                });

            modelBuilder.Entity("E_Forester.Model.Database.Plan", b =>
                {
                    b.HasOne("E_Forester.Model.Database.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Forester.Model.Database.ForestUnit", "ForestUnit")
                        .WithMany("Plans")
                        .HasForeignKey("ForestUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("ForestUnit");
                });

            modelBuilder.Entity("E_Forester.Model.Database.PlanExecution", b =>
                {
                    b.HasOne("E_Forester.Model.Database.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("E_Forester.Model.Database.Plan", "Plan")
                        .WithMany("PlanExecutions")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Forester.Model.Database.PlanItem", "PlanItem")
                        .WithMany("PlanExecutions")
                        .HasForeignKey("PlanItemId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Plan");

                    b.Navigation("PlanItem");
                });

            modelBuilder.Entity("E_Forester.Model.Database.PlanItem", b =>
                {
                    b.HasOne("E_Forester.Model.Database.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("E_Forester.Model.Database.Plan", "Plan")
                        .WithMany("PlanItems")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Forester.Model.Database.Subarea", "Subarea")
                        .WithMany("PlanItems")
                        .HasForeignKey("SubareaId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Plan");

                    b.Navigation("Subarea");
                });

            modelBuilder.Entity("E_Forester.Model.Database.Subarea", b =>
                {
                    b.HasOne("E_Forester.Model.Database.Division", "Division")
                        .WithMany("Subareas")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");
                });

            modelBuilder.Entity("E_Forester.Model.Database.User", b =>
                {
                    b.OwnsMany("E_Forester.Model.Database.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Token")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("ForestUnitUser", b =>
                {
                    b.HasOne("E_Forester.Model.Database.ForestUnit", null)
                        .WithMany()
                        .HasForeignKey("AssignedForestUnitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Forester.Model.Database.User", null)
                        .WithMany()
                        .HasForeignKey("AssignedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("E_Forester.Model.Database.Division", b =>
                {
                    b.Navigation("Subareas");
                });

            modelBuilder.Entity("E_Forester.Model.Database.ForestUnit", b =>
                {
                    b.Navigation("Divisions");

                    b.Navigation("Plans");
                });

            modelBuilder.Entity("E_Forester.Model.Database.Plan", b =>
                {
                    b.Navigation("PlanExecutions");

                    b.Navigation("PlanItems");
                });

            modelBuilder.Entity("E_Forester.Model.Database.PlanItem", b =>
                {
                    b.Navigation("PlanExecutions");
                });

            modelBuilder.Entity("E_Forester.Model.Database.Subarea", b =>
                {
                    b.Navigation("PlanItems");
                });
#pragma warning restore 612, 618
        }
    }
}
