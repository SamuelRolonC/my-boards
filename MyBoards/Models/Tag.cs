using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Models
{
    public class Tag
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
    }
}
