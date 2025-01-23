using Microsoft.EntityFrameworkCore;
using ConexionPostgres.Models; // Agrega esta línea

namespace ConexionPostgres.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<YourModel> YourModels { get; set; }
        // Agrega otros DbSet aquí
    }
}
