using AthenaBackend.Infrastructure.ReadModel.Core.User.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthenaBackend.Infrastructure.ReadModel.Core.User
{
    public class UserUIMap : IEntityTypeConfiguration<UserUI>
    {
        public void Configure(EntityTypeBuilder<UserUI> builder)
        {
            builder.ToView(nameof(UserUI));

            builder.HasKey(x => x.Id);
        }
    }
}
