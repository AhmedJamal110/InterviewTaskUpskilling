using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTask.API.Persistence.Configrations
{
    public class InventoryTransactionConfigration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.Property(x => x.Type)
                   .HasConversion(S => S.ToString(), S => (TransactionType)Enum.Parse(typeof(TransactionType), S));
        }
    }
}
