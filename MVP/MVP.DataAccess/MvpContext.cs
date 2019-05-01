using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MVP.Entities.Entities;

namespace MVP.DataAccess
{
    public class MvpContext : IdentityDbContext<User>
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
