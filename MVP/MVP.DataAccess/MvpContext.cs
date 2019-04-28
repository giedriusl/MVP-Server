using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace MVP.DataAccess
{
    public class MvpContext : DbContext
    {
        public MvpContext() : base()
        {
        }

        public MvpContext(DbContextOptions<MvpContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MvpContextFactory : IDesignTimeDbContextFactory<MvpContext>
    {
        public MvpContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MvpContext>();
            builder.UseSqlServer(args[0],
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(MvpContext).GetTypeInfo().Assembly.GetName().Name));

            return new MvpContext(builder.Options);
        }
    }
}
