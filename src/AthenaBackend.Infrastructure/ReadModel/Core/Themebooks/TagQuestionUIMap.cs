using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AthenaBackend.Infrastructure.ReadModel.Core.Themebooks
{
    public class TagQuestionMap : IEntityTypeConfiguration<TagQuestionUI>
    {
        public void Configure(EntityTypeBuilder<TagQuestionUI> builder)
        {
            builder.ToView(nameof(TagQuestionUI));

            builder.HasNoKey();
            builder.Property(x => x.Type);
            builder.Property(x => x.Question);

            builder.Property(x => x.Answers)
                   .HasConversion(v => JsonConvert.SerializeObject(v),
                                  v => JsonConvert.DeserializeObject<List<string>>(v));

        }
    }
}
