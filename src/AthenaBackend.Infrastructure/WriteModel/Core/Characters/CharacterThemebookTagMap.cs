using AthenaBackend.Domain.Core.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Characters
{

    public class CharacterThemebookTagMap : IEntityTypeConfiguration<CharacterThemebookTag>
    {
        public void Configure(EntityTypeBuilder<CharacterThemebookTag> builder)
        {


            builder.HasKey(x => x.Id);

            builder.Property(x => x.CharacterThemebookId);
            builder.Property(x => x.TagId);
            builder.Property(x => x.TagName);

            builder.Ignore(x => x.CharacterThemebook);
        }
    }
}
