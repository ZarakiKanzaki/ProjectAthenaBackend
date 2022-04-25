using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AthenaBackend.Infrastructure.ReadModel.Core.Themebooks
{
    public class ThemebookUIMap : IEntityTypeConfiguration<ThemebookUI>
    {
        public void Configure(EntityTypeBuilder<ThemebookUI> builder)
        {
            builder.ToView(nameof(ThemebookUI));

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Type,
                    type =>
                    {
                        type.Property(x => x.Id).HasColumnName("ThemebookType");
                    });

            builder.Property(x => x.ExamplesOfApplication)
                    .HasConversion(v => JsonConvert.SerializeObject(v),
                                   v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Property(x => x.MisteryOptions)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));
            builder.Property(x => x.TitleExamples)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Property(x => x.CrewRelationships)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Ignore(x => x.ThemebookConcept)
                   .Ignore(x => x.TagQuestions)
                   .Ignore(x => x.Improvements);

        }
    }
}
