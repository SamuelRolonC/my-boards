using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Models
{
    public class State
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; }

        public IList<CardState> CardStates { get; set; }
    }
}
