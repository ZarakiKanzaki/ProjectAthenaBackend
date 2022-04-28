using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthenaBackend.Infrastructure.ReadModel.Core.Themebooks
{

    public class ThemebookImprovementUIMap : IEntityTypeConfiguration<ThemebookImprovementUI>
    {
        public void Configure(EntityTypeBuilder<ThemebookImprovementUI> builder)
        {
            builder.ToView("ThemebookImprovementUI");
            builder.HasNoKey();

            builder.Property(x => x.ThemebookId);
            builder.Property(x => x.Title);
            builder.Property(x => x.Decription);
        }
    }


}
