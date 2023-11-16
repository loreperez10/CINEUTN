using Microsoft.EntityFrameworkCore;
using Web.Repos.Models;

namespace Web.Repos;

public partial class AdopcionGarritasFelicesContext : DbContext
{
    public AdopcionGarritasFelicesContext()
    {
    }

    public AdopcionGarritasFelicesContext(DbContextOptions<AdopcionGarritasFelicesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genero> Generos { get; set; }
    public virtual DbSet<Edad> Edades { get; set; }
    public virtual DbSet<Enfermedad> Enfermedades { get; set; }
    public virtual DbSet<FutAdoptado> FutAdoptados { get; set; }
    public virtual DbSet<FutAdoptante> FutAdoptantes { get; set; }
    public virtual DbSet<MaloAdoptante> MaloAdoptantes { get; set; }
    public virtual DbSet<Vacuna> Vacunas { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //  => optionsBuilder.UseSqlServer("name=conexion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
