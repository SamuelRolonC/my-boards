using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBoards.Models;

namespace MyBoards.Models
{
    public class MyBoardsContext: DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<CardList> CardLists { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<State> States { get; set; }       

        public MyBoardsContext (DbContextOptions<MyBoardsContext> options): base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CardResposible Many-To-Many relation
            modelBuilder.Entity<CardResponsible>()
                .HasKey(x => new { x.CardId, x.ResponsibleId });

            modelBuilder.Entity<CardResponsible>()
                .HasOne(x => x.Card)
                .WithMany(y => y.CardResponsibles)
                .HasForeignKey(y => y.ResponsibleId);

            modelBuilder.Entity<CardResponsible>()
                .HasOne(x => x.Responsible)
                .WithMany(y => y.CardResponsibles)
                .HasForeignKey(y => y.CardId);

            // CardState Many-To-Many relations
            modelBuilder.Entity<CardState>()
                .HasKey(x => new { x.CardId, x.StateId });

            modelBuilder.Entity<CardState>()
                .HasOne(x => x.Card)
                .WithMany(y => y.CardStates)
                .HasForeignKey(y => y.StateId);

            modelBuilder.Entity<CardState>()
                .HasOne(x => x.State)
                .WithMany(y => y.CardStates)
                .HasForeignKey(y => y.CardId);

            // CardTag Many-To-Many relations
            modelBuilder.Entity<CardTag>()
                .HasKey(x => new { x.CardId, x.TagId });

            modelBuilder.Entity<CardTag>()
                .HasOne(x => x.Card)
                .WithMany(y => y.CardTags)
                .HasForeignKey(y => y.TagId);

            modelBuilder.Entity<CardTag>()
                .HasOne(x => x.Tag)
                .WithMany(y => y.CardTags)
                .HasForeignKey(y => y.CardId);

            var board = new Board { Id = 1, Name = "Principal" };            
            modelBuilder.Entity<Board>().HasData(board);

            var cardList = new CardList { Id = 1, Name = "Infinity Seguros", BoardId = board.Id };             
            modelBuilder.Entity<CardList>().HasData(cardList);

            var tag = new Tag { Id = 1, Name = "Issue", Color = "Amarillo" };
            modelBuilder.Entity<Tag>().HasData(tag);

            var state = new State { Id = 1, Title = "En desarrollo" };
            modelBuilder.Entity<State>().HasData(state);

            var responsible = new Responsible { Id = 1, Name = "Samuel" };
            modelBuilder.Entity<Responsible>().HasData(responsible);
        }        
    }
}
