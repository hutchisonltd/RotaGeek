using Microsoft.EntityFrameworkCore;
using RotaGeek.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaGeek.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }

    }
}
