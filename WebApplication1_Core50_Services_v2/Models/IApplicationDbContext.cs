
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1_Core50.Models;

namespace WebApplication1_Core50.Contexts
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; set; }
        DbSet<tbl_contacts> tbl_contacts { get; set; }
        DbSet<tblBook> tblBook { get; set; }
        DbSet<tbl_employees> tbl_employees { get; set; }
        DbSet<tbl_products> tbl_products { get; set; }

        Task<int> SaveChanges();
    }
}