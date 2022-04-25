using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AthenaBackend.Infrastructure.ReadModel.Core.Themebooks
{
    public class ThemebookConceptUIMap : IEntityTypeConfiguration<ThemebookConceptUI>
    {
        public void Configure(EntityTypeBuilder<ThemebookConceptUI> builder)
        {
            builder.ToView(nameof(ThemebookConceptUI));
            
            builder.HasNoKey();
            builder.Property(x => x.Question);

            builder.Property(x => x.Answers)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));
        }
    }
}
