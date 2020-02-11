using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Models
{
    public class CardTag
    {
        public int CardId { get; set; }
        public int TagId { get; set; }

        public Card Card { get; set; }
        public Tag Tag { get; set; }
    }
}
