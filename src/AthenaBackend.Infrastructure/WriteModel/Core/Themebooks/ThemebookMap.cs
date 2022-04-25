using AthenaBackend.Common.DependecyInjection;
using AthenaBackend.Domain.Core.Themebooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Themebooks
{

    public class ThemebookMap : IEntityTypeConfiguration<Themebook>
    {
        public void Configure(EntityTypeBuilder<Themebook> builder)
        {
            builder.ConfigureAsAggregate<Themebook, Guid>();


            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder.HasOne(x => x.Concept)
                   .WithOne(x => x.Themebook)
                   .HasForeignKey<ThemebookConcept>(b => b.ThemebookId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(x => x.Type,
                                type =>
                                {
                                    type.Property(x => x.Id).HasColumnName("ThemebookType");
                                });

            builder.Property(x => x.ExamplesOfApplication)
                    .HasConversion(v => JsonConvert.SerializeObject(v),
                                   v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Property(x => x.IdentityMisteryOptions)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));
            builder.Property(x => x.TitleExamples)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Property(x => x.CrewRelationships)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.HasMany(x => x.TagQuestions)
                   .WithOne(x => x.Themebook)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Improvements)
                   .WithOne(x => x.Themebook)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(x => x.CrudOperationLog,
                                 crud =>
                                 {
                                     crud.OwnsOne(x => x.Creation,
                                                       creation =>
                                                       {
                                                           creation.Property(x => x.UserOperationId).HasColumnName("UserCreationId");
                                                           creation.Property(x => x.UserOperationDateTime).HasColumnName("CreationDatetime");
                                                       });

                                     crud.OwnsOne(x => x.Update,
                                                       update =>
                                                       {
                                                           update.Property(x => x.UserOperationId).HasColumnName("UserUpdateId");
                                                           update.Property(x => x.UserOperationDateTime).HasColumnName("UpdateDatetime");
                                                       });
                                     crud.OwnsOne(x => x.Deletion,
                                                       deletion =>
                                                       {
                                                           deletion.Property(x => x.UserOperationId).HasColumnName("UserDeletionId");
                                                           deletion.Property(x => x.UserOperationDateTime).HasColumnName("DeletionDatetime");
                                                       });
                                 });

        }
    }
}
