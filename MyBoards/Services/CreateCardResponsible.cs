using MyBoards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Services
{
    public class CreateCardResponsible
    {
        public static void Execute(Card card, MyBoardsContext context)
        {
            CardResponsible cardResponsible;

            foreach (var item in card.SelectedResponsibles)
            {

                if (Int32.TryParse(item, out int id))
                {
                    cardResponsible = new CardResponsible()
                    {
                        ResponsibleId = id,
                        CardId = card.Id
                    };

                    context.Add(cardResponsible);
                    context.SaveChangesAsync();
                }
            }
        }
    }
}
