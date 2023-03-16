using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelivMVC.Models;

namespace RelivMVC.Data
{
    public class RelivMVCContext : DbContext
    {
        public RelivMVCContext (DbContextOptions<RelivMVCContext> options)
            : base(options)
        {
        }

        public DbSet<RelivMVC.Models.Product> Product { get; set; } = default!;

        public DbSet<RelivMVC.Models.Category>? Category { get; set; }

        public DbSet<RelivMVC.Models.State>? State { get; set; }
    }
}
