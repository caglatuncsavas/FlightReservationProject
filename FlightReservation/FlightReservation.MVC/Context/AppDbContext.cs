using FlightReservation.MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlightReservation.MVC.Context;

public sealed class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
