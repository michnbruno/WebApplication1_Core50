
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1_Core50.Models;

namespace WebApplication1_Core50.Models
{
    public class ApplicationDbContext : DbContext //, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<tbl_products> tbl_products { get; set; }
        public DbSet<tbl_employees> tbl_employees { get; set; }
        public DbSet<tbl_contacts> tbl_contacts { get; set; }
        public DbSet<tblBook> tblBook { get; set; }
        public DbSet<tblPlaces> tblPlaces { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
