using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace FMIS.Models
{
    public class Medicaldbcontext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dietician> Dieticians { get; set; }
        public DbSet<DieticianDataEntry> DieticianDataEntries { get; set; }
    }
   
}