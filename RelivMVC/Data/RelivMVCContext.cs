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
    }
}
