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

        public DbSet<Student> students { get; set; }
        public DbSet<ExcelUp> excelUps { get; set; }
        public DbSet<ForceSale> forceSale { get; set; }
        public DbSet<Attendence> attendences { get; set; }
        public DbSet<TimeSetting> timeSetting { get; set; }
        public DbSet<BranceSetting> branceSettings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee_Details> employee_Details { get; set; }
        public DbSet<Educational> educationals { get; set; }
        public DbSet<Nominee> nominees  { get; set; }
        public DbSet<Training> training   { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<ActivitiesReport> ActivitiesReports { get; set; }

    }
}
