using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace estabelecimentoMVC.Models
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
        }
        
        public DbSet<Estabelecimento> Estabelecimento { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Municipio> Municipio { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Estabelecimento>()
                .HasOne(e => e.Categoria)
                .WithMany(c => c.Estabelecimentos)
                .HasForeignKey(e => e.IdCategoria);
        }
    }
}

