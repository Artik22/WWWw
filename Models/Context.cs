using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;


namespace web.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public Context(DbContextOptions<Context> options): base(options)
        {
            Database.EnsureCreated();
        }

    
    }
}
