using Metafar.Core.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Infrastructure;
namespace Metafar.Infraestructura.Data
{
    public class ApplicationDbContext : DbContext
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //Aca paso las entidades (modelos)
        public DbSet<Saldo> Saldo { get; set; }
        public DbSet<Operaciones> Operaciones { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


    }
}
