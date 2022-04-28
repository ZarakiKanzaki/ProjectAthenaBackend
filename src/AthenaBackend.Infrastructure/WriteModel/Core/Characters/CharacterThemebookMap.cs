using AthenaBackend.Domain.Core.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Characters
{

    public class CharacterThemebookMap : IEntityTypeConfiguration<CharacterThemebook>
    {
        public void Configure(EntityTypeBuilder<CharacterThemebook> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IdentityMistery).IsRequired();
            builder.Property(x => x.FadeCrackLevel).IsRequired();
            builder.Property(x => x.AttentionLevel).IsRequired();
            builder.Property(x => x.Concept).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Flipside);
            builder.Property(x => x.CharacterId);


            builder.OwnsOne(x => x.Type,
                    type =>
                    {
                        type.Property(x => x.Id).HasColumnName("ThemebookType");
                    });

            builder.Ignore(x => x.Character);
        }
    }
}
