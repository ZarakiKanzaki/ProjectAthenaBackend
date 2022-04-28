﻿// <auto-generated />
using System;
using AthenaBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AthenaBackend.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20220424185400_UpdateOnConcept")]
    partial class UpdateOnConcept
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.16");

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.TagQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Answers")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Question")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ThemebookId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ThemebookId");

                    b.ToTable("TagQuestion");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.Themebook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CrewRelationships")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamplesOfApplication")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MisteryOptions")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleExamples")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Themebooks");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.ThemebookConcept", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Answers")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Question")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ThemebookId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ThemebookId")
                        .IsUnique();

                    b.ToTable("ThemebookConcept");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.ThemebookImprovement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Decription")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ThemebookId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ThemebookId");

                    b.ToTable("ThemebookImprovement");
                });

            modelBuilder.Entity("AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI.TagQuestionUI", b =>
                {
                    b.Property<string>("Answers")
                        .HasColumnType("TEXT");

                    b.Property<string>("Question")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ThemebookId")
                        .HasColumnType("TEXT");

                    b.Property<short>("Type")
                        .HasColumnType("INTEGER");

                    b.ToView("TagQuestionUI");
                });

            modelBuilder.Entity("AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI.ThemebookConceptUI", b =>
                {
                    b.Property<string>("Answers")
                        .HasColumnType("TEXT");

                    b.Property<string>("Question")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ThemebookId")
                        .HasColumnType("TEXT");

                    b.ToView("ThemebookConceptUI");
                });

            modelBuilder.Entity("AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI.ThemebookImprovementUI", b =>
                {
                    b.Property<string>("Decription")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ThemebookId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.ToView("ThemebookImprovementUI");
                });

            modelBuilder.Entity("AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI.ThemebookUI", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CrewRelationships")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamplesOfApplication")
                        .HasColumnType("TEXT");

                    b.Property<string>("MisteryOptions")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleExamples")
                        .HasColumnType("TEXT");

                    b.Property<short>("TypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToView("ThemebookUI");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.TagQuestion", b =>
                {
                    b.HasOne("AthenaBackend.Domain.Core.Themebooks.Themebook", "Themebook")
                        .WithMany("TagQuestions")
                        .HasForeignKey("ThemebookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Themebook");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.Themebook", b =>
                {
                    b.OwnsOne("AthenaBackend.Common.DomainDrivenDesign.CrudOperationLog", "CrudOperationLog", b1 =>
                        {
                            b1.Property<Guid>("ThemebookId")
                                .HasColumnType("TEXT");

                            b1.HasKey("ThemebookId");

                            b1.ToTable("Themebooks");

                            b1.WithOwner()
                                .HasForeignKey("ThemebookId");

                            b1.OwnsOne("AthenaBackend.Common.DomainDrivenDesign.OperationLog", "Creation", b2 =>
                                {
                                    b2.Property<Guid>("CrudOperationLogThemebookId")
                                        .HasColumnType("TEXT");

                                    b2.Property<DateTime?>("UserOperationDateTime")
                                        .HasColumnType("TEXT")
                                        .HasColumnName("CreationDatetime");

                                    b2.Property<Guid?>("UserOperationId")
                                        .HasColumnType("TEXT")
                                        .HasColumnName("UserCreationId");

                                    b2.HasKey("CrudOperationLogThemebookId");

                                    b2.ToTable("Themebooks");

                                    b2.WithOwner()
                                        .HasForeignKey("CrudOperationLogThemebookId");
                                });

                            b1.OwnsOne("AthenaBackend.Common.DomainDrivenDesign.OperationLog", "Deletion", b2 =>
                                {
                                    b2.Property<Guid>("CrudOperationLogThemebookId")
                                        .HasColumnType("TEXT");

                                    b2.Property<DateTime?>("UserOperationDateTime")
                                        .HasColumnType("TEXT")
                                        .HasColumnName("DeletionDatetime");

                                    b2.Property<Guid?>("UserOperationId")
                                        .HasColumnType("TEXT")
                                        .HasColumnName("UserDeletionId");

                                    b2.HasKey("CrudOperationLogThemebookId");

                                    b2.ToTable("Themebooks");

                                    b2.WithOwner()
                                        .HasForeignKey("CrudOperationLogThemebookId");
                                });

                            b1.OwnsOne("AthenaBackend.Common.DomainDrivenDesign.OperationLog", "Update", b2 =>
                                {
                                    b2.Property<Guid>("CrudOperationLogThemebookId")
                                        .HasColumnType("TEXT");

                                    b2.Property<DateTime?>("UserOperationDateTime")
                                        .HasColumnType("TEXT")
                                        .HasColumnName("UpdateDatetime");

                                    b2.Property<Guid?>("UserOperationId")
                                        .HasColumnType("TEXT")
                                        .HasColumnName("UserUpdateId");

                                    b2.HasKey("CrudOperationLogThemebookId");

                                    b2.ToTable("Themebooks");

                                    b2.WithOwner()
                                        .HasForeignKey("CrudOperationLogThemebookId");
                                });

                            b1.Navigation("Creation");

                            b1.Navigation("Deletion");

                            b1.Navigation("Update");
                        });

                    b.OwnsOne("AthenaBackend.Domain.WellKnownInstances.ThemebookType", "Type", b1 =>
                        {
                            b1.Property<Guid>("ThemebookId")
                                .HasColumnType("TEXT");

                            b1.Property<short>("Id")
                                .HasColumnType("INTEGER")
                                .HasColumnName("ThemebookType");

                            b1.Property<string>("Name")
                                .HasColumnType("TEXT");

                            b1.HasKey("ThemebookId");

                            b1.ToTable("Themebooks");

                            b1.WithOwner()
                                .HasForeignKey("ThemebookId");
                        });

                    b.Navigation("CrudOperationLog");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.ThemebookConcept", b =>
                {
                    b.HasOne("AthenaBackend.Domain.Core.Themebooks.Themebook", "Themebook")
                        .WithOne("Concept")
                        .HasForeignKey("AthenaBackend.Domain.Core.Themebooks.ThemebookConcept", "ThemebookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Themebook");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.ThemebookImprovement", b =>
                {
                    b.HasOne("AthenaBackend.Domain.Core.Themebooks.Themebook", "Themebook")
                        .WithMany("Improvements")
                        .HasForeignKey("ThemebookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Themebook");
                });

            modelBuilder.Entity("AthenaBackend.Domain.Core.Themebooks.Themebook", b =>
                {
                    b.Navigation("Concept");

                    b.Navigation("Improvements");

                    b.Navigation("TagQuestions");
                });
#pragma warning restore 612, 618
        }
    }
}