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
        public DbSet<Metafar.Core.Models.Saldo> Saldo { get; set; }


    }
}
