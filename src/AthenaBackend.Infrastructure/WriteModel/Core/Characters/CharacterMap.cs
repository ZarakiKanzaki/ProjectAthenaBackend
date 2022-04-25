using AthenaBackend.Common.DependecyInjection;
using AthenaBackend.Domain.Core.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Characters
{

    public class CharacterMap : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {

            builder.ConfigureAsAggregate<Character, Guid>();


            builder.Property(x => x.Mythos).IsRequired();
            builder.Property(x => x.Logos).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Note);

            builder.HasMany(a => a.Tags)
                   .WithOne(a => a.Character)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Themebooks)
                   .WithOne(a => a.Character)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Ignore(x => x.PowerTags)
                   .Ignore(x => x.WeaknessTags)
                   .Ignore(x => x.MythosThemebooks)
                   .Ignore(x => x.LogosThemebooks);

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
