using FlightReservation.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightReservation.MVC.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(P=> P.FirstName).IsRequired().HasColumnType("varchar(50)");
        builder.Property(P => P.LastName).IsRequired().HasColumnType("varchar(50)");
        builder.Property(P => P.Email).IsRequired().HasColumnType("varchar(100)");
        builder.Property(P => P.Password).IsRequired().HasColumnType("varchar(15)");


        builder.HasIndex(p => p.Email).IsUnique();
    }
}
