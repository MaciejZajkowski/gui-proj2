using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using library.Models;

namespace library.Data{

public class MvcWebAppDbContext : DbContext
    {
        public MvcWebAppDbContext(DbContextOptions < MvcWebAppDbContext > options)
            :base(options)
            {
            }
        public DbSet<Book> Books{get;set;}
    }
}