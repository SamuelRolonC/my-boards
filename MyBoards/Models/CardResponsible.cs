using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Models
{
    public class CardResponsible
    {
        public int CardId { get; set; }
        public int ResponsibleId { get; set; }

        public Card Card { get; set; }        
        public Responsible Responsible { get; set; }
    }
}
