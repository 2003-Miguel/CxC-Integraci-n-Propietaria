using Microsoft.EntityFrameworkCore;
using CxcProject.Models;
using System.Collections.Generic;

namespace CxcProject.Data
{
    public class CxcDbContext : DbContext
    {
        public CxcDbContext(DbContextOptions<CxcDbContext> options) : base(options) { }

        public DbSet<TipoDocumento> TiposDocumentos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<AsientoContable> AsientosContables { get; set; }
    }
}
