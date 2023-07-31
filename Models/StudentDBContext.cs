using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCDemoProject.Models
{
    public class StudentDBContext : DbContext
    {
        public DbSet<Student> studentList { get; set; }
    }
}