using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCPListener.Persistence.Entities;

namespace TCPListener.Persistence;

public class TeatTableEntityConfiguration : IEntityTypeConfiguration<TestTableEntity>
{
    public void Configure(EntityTypeBuilder<TestTableEntity> builder)
    {
        builder.ToTable("TestTable");
        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.EncData).HasColumnName("EncData");
        builder.Property(x => x.CreatedAt).HasColumnName("Created");
        builder.HasKey(nameof(TestTableEntity.Id));
    }
}