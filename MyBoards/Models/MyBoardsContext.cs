using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Models
{
    public class MyBoardsContext: DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<CardList> CardLists { get; set; }
        public DbSet<Card> Cards { get; set; }

        public MyBoardsContext (DbContextOptions<MyBoardsContext> options): base(options)
        {
                
        }
    }
}
