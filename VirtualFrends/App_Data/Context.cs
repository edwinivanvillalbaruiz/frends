using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using VirtualFrends.Models;

namespace VirtualFrends
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Contexto : DbContext
    {
        public Contexto() : base("MyDbContextConnectionString")
        {
            Database.SetInitializer<Contexto>(new MyDbInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public class MyDbInitializer : CreateDatabaseIfNotExists<Contexto>
        {
            protected override void Seed(Contexto context)
            {
                //context.Departamentos.Add(new Departamentos { CodDpto = "9999", Departamento = "Prueba" });
                //base.Seed(context);
            }
        }
        public DbSet<Diario> Diario { get; set; }
        public DbSet<Terceros> Terceros { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Bancos> Bancos { get; set; }
        public DbSet<Ventas> Ventas { get; set; }

        public DbSet<TipoCuenta> TipoCuenta { get; set; }
        public IDbSet<Archivo> Archivos { get; set; }
    }
}