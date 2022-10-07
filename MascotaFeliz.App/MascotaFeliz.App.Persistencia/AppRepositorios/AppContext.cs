using Microsoft.EntityFrameworkCore;
using MascotaFeliz.App.Dominio;

namespace MascotaFeliz.App.Persistencia
{
    public class AppContext:DbContext
    {
        public DbSet<Persona> Personas {get;set;}
        public DbSet<Dueno> Duenos  {get;set;}
        public DbSet<Veterinario> Veterinarios {get;set;}
        public DbSet<Mascota> Mascotas {get;set;}
        public DbSet<Historia> Historias {get;set;}
        public DbSet<VisitaPyP> VisitasPyP {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer("Data Source =datamilemascota.mssql.somee.com ; Initial Catalog = datamilemascota; user id=MILU7920_SQLLogin_1; pwd=8kvwazhj8o");
                //.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MascotaFelizData");
            }
        }
    }
}