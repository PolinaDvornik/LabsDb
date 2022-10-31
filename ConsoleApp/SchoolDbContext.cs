using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassType> ClassTypes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options) { }
    }
}
