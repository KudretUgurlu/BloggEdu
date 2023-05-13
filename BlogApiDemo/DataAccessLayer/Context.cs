using Microsoft.EntityFrameworkCore;

namespace BlogApiDemo.DataAccessLayer
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=BloggEduApiDB;Integrated Security=True;");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
