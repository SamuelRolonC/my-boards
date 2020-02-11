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
        [Display(Name = "Título")]
        public string Title { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Lista")]
        public int CardListId { get; set; }
        public CardList CardList { get; set; }

        public IList<CardState> CardStates { get; set; }
        public IList<CardTag> CardTags { get; set; }
        public IList<CardResponsible> CardResponsibles { get; set; }
    }
}
