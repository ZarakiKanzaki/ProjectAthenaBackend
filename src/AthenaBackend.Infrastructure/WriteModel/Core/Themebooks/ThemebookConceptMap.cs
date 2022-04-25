using AthenaBackend.Domain.Core.Themebooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Themebooks
{
    public class ThemebookConceptMap : IEntityTypeConfiguration<ThemebookConcept>
    {
        public void Configure(EntityTypeBuilder<ThemebookConcept> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Question);

            builder.Property(x => x.Answers)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Ignore(x => x.Themebook);
        }
    }
}
