using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APIPersona.Modelo;

public partial class ExamenPersonasContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ExamenPersonasContext()
    {
    }

    public ExamenPersonasContext(DbContextOptions<ExamenPersonasContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration=configuration;
    }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DBconection");
        optionsBuilder.UseSqlServer(connectionString);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persona__3214EC07D45F9864");

            entity.ToTable("Persona");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
