using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApiDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public ApiDbContext( DbContextOptions<ApiDbContext> options ) : base( options ){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<Entity>();

            base.OnModelCreating(builder);
        }
    }
}
