using Microsoft.EntityFrameworkCore;

namespace EFCore_GraphQL.Models
{
    public class EFCore_GraphQLDbContext : DbContext
    {
        public EFCore_GraphQLDbContext()
            : base() { }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"User ID=postgres;Password=12345;Server=localhost;Port=5432;Database=EFCore_GraphQLDb;Integrated Security=true;Pooling=true;"
            );

            base.OnConfiguring(optionsBuilder);
        }
    }
}
