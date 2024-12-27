using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Models;
using Microsoft.Data.SqlClient;

namespace DotNetCoreSqlDb.Data
{
    public class MyDatabaseContext : DbContext
    {
       
        public MyDatabaseContext (DbContextOptions<MyDatabaseContext> options, IHttpContextAccessor accessor)
            : base(options)
        {
            
        }

        public DbSet<DotNetCoreSqlDb.Models.Todo> Todo { get; set; } = default!;
    }
}
