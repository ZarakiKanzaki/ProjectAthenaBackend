using AthenaBackend.Domain.Core.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Characters
{

    public class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CharacterId);
            builder.Property(x => x.IsSubtractive);
            builder.Property(x => x.Type);
            builder.Property(x => x.Level);
            builder.Property(x => x.Name).IsRequired();

            builder.Ignore(x => x.Character);

        }
    }
}
