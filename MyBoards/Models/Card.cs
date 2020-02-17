using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Display(Name = "Lista")]
        public CardList CardList { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public int StateId { get; set; }
        [Display(Name = "Estado")]
        public State State { get; set; }

        public IList<CardTag> CardTags { get; set; }
        public IList<CardResponsible> CardResponsibles { get; set; }

        // Para cargar las listas de las relaciones en las vistas
        [NotMapped]
        [Display(Name = "Etiquetas")]
        public string[] SelectedTags { get; set; }

        [NotMapped]
        [Display(Name = "Responsables")]
        public string[] SelectedResponsibles { get; set; }

        public bool LoadTags()
        {
            if (CardTags != null)
            {
                SelectedTags = new string[CardTags.Count];
                int i = 0;

                foreach (var item in CardTags)
                {
                    SelectedTags[i] = item.TagId.ToString();
                    i++;
                }

                return true;
            }

            return false;
        }

        public bool LoadResponsibles()
        {
            if (CardResponsibles != null)
            {
                SelectedResponsibles = new string[CardResponsibles.Count];
                int i = 0;

                foreach (var item in CardResponsibles)
                {
                    SelectedResponsibles[i] = item.ResponsibleId.ToString();
                    i++;
                }

                return true;
            }

            return false;
        }
    }
}
