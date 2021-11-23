using Finanzas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cartera> Carteras { get; set; }
        public DbSet<Costo> Costos { get; set; }
        public DbSet<CostosOperacion> CostosOperaciones { get; set; }
        public DbSet<Letra> Letras { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }
        public DbSet<OperacionCartera> OperacionCarteras { get; set; }
        public DbSet<OperacionLetra> OperacionLetras { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Tasa> Tasas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Cartera Entity 
            builder.Entity<Cartera>().ToTable("Carteras");

            //Contraints
            builder.Entity<Cartera>().HasKey(c => c.Id);
            builder.Entity<Cartera>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Cartera>().HasMany(c => c.operacionCarteras).WithOne(oc => oc.Cartera).HasForeignKey(oc => oc.CarteraId);
            builder.Entity<Cartera>().HasOne(c => c.Perfil).WithMany(p => p.Carteras).HasForeignKey(c => c.PerfilId);
            builder.Entity<Cartera>().HasMany(c => c.Letras).WithOne(l => l.Cartera).HasForeignKey(l => l.CarteraId);

            //Costo Entity
            builder.Entity<Costo>().ToTable("Costos");

            //Contraints
            builder.Entity<Costo>().HasKey(c => c.Id);
            builder.Entity<Costo>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Costo>().HasMany(c => c.CostosOperaciones).WithOne(co => co.Costo).HasForeignKey(co => co.CostoId);

            //CostosOperacion Entity
            builder.Entity<CostosOperacion>().ToTable("CostoOperaciones");

            //Contraints
            builder.Entity<CostosOperacion>().HasKey(co => new { co.OperacionId, co.CostoId });


            //Letra Entity
            builder.Entity<Letra>().ToTable("Letras");

            //Contraints
            builder.Entity<Letra>().HasKey(l => l.Id);
            builder.Entity<Letra>().Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Letra>().HasMany(l => l.OperacionLetras).WithOne(ol => ol.Letra).HasForeignKey(ol => ol.LetraId);

            //Operacion Entity
            builder.Entity<Operacion>().ToTable("Operacion");

            //Contraints
            builder.Entity<Operacion>().HasKey(o => o.Id);
            builder.Entity<Operacion>().Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Operacion>().HasMany(o => o.CostosOperaciones).WithOne(co => co.Operacion).HasForeignKey(co => co.OperacionId);
            builder.Entity<Operacion>().HasMany(o => o.OperacionCarteras).WithOne(oc => oc.Operacion).HasForeignKey(oc => oc.OperacionId);
            builder.Entity<Operacion>().HasMany(o => o.OperacionLetras).WithOne(ol => ol.Operacion).HasForeignKey(ol => ol.OperacionId);
            builder.Entity<Operacion>().HasOne(o => o.Tasa).WithMany(t => t.Operaciones).HasForeignKey(o => o.TasaId);

            //OperacionCartera Entity
            builder.Entity<OperacionCartera>().ToTable("OperacionCarteras");

            //Contraints
            builder.Entity<OperacionCartera>().HasKey(oc => new { oc.OperacionId, oc.CarteraId });


            //OperacionLetra Entity
            builder.Entity<OperacionLetra>().ToTable("OperacionLetras");

            //Contraints
            builder.Entity<OperacionLetra>().HasKey(ol => new { ol.OperacionId, ol.LetraId });


            //Perfil Entity
            builder.Entity<Perfil>().ToTable("Perfiles");

            //Contraints
            builder.Entity<Perfil>().HasKey(p => p.Id);
            builder.Entity<Perfil>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Perfil>().HasOne(p => p.Usuario).WithOne(u => u.Perfil).HasForeignKey<Perfil>(p => p.UsuarioId);

            //Periodo Entity
            builder.Entity<Periodo>().ToTable("Periodos");

            //Contraints
            builder.Entity<Periodo>().HasKey(p => p.Id);
            builder.Entity<Periodo>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            

            //Tasa Entity
            builder.Entity<Tasa>().ToTable("Tasas");

            //Contraints
            builder.Entity<Tasa>().HasKey(t => t.Id);
            builder.Entity<Tasa>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();

            //Relationships
            builder.Entity<Tasa>().HasOne(t => t.Periodo).WithMany(p => p.TasasEfectivas).HasForeignKey(t => t.PeriodoId);
            builder.Entity<Tasa>().HasOne(t => t.PeriodoCapitalizacion).WithMany(p => p.TasasNominales).HasForeignKey(t => t.PeriodoCapitalizacionId);

            //Usuario Entity
            builder.Entity<Usuario>().ToTable("Usuarios");

            //Contraints
            builder.Entity<Usuario>().HasKey(u => u.Id);
            builder.Entity<Usuario>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();

            //TODO: Aply Naming Convention
        }
    }
}
