using MyBoards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Services
{
    public class CreateCardTag
    {
        public static void Execute(Card card, MyBoardsContext context)
        {
            CardTag cardTag;

            foreach (var item in card.SelectedTags)
            {
                int id;

                if (Int32.TryParse(item, out id))
                {
                    cardTag = new CardTag()
                    {
                        TagId = id,
                        CardId = card.Id
                    };

                    context.Add(cardTag);
                    context.SaveChangesAsync();
                }
            }
        }
    }
}
