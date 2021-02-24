using Microsoft.EntityFrameworkCore;
using Rurouni_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rurouni_v2.Data
{
    public class JournalDbContext : DbContext
    {
        public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options)
        {

        }

        public DbSet<JournalModel> Journals { get; set; }
    }
}
