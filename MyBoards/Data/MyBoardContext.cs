using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBoards.Models;

    public class MyBoardContext : DbContext
    {
        public MyBoardContext (DbContextOptions<MyBoardContext> options)
            : base(options)
        {
        }

        public DbSet<MyBoards.Models.Responsible> Responsible { get; set; }
    }
