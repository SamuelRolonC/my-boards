using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Models
{
    public class CardState
    {
        public int StateId { get; set; }
        public int CardId { get; set; }

        public State State { get; set; }
        public Card Card { get; set; }
    }
}
