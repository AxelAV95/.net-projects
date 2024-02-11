using APIUsers7._0.Models;
using Microsoft.EntityFrameworkCore;

namespace APIUsers7._0.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
    }
}
