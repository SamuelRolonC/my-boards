using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Models
{
    public class Card
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int CardListId { get; set; }
        public CardList CardList { get; set; }

        public List<State> States { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Responsible> Responsibles { get; set; }
    }
}
