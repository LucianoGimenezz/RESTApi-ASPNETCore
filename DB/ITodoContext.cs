using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface ITodoContext
    {

         DbSet<Todo> Todos { get; set; }
         DbSet<User> Users { get; set; }

        public int SaveChanges();
    }
}
