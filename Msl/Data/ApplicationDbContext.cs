using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Msl.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msl.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<ExcelUp> excelUps { get; set; }

    }
}
