using Microsoft.EntityFrameworkCore;
using MyBoards.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBoards.Services
{
    public class EditCardResponsible
    {
        public static void Execute(MyBoardsContext context, int cardId, string[] strNewResponsiblesIds)
        {
            var newResponsiblesId = ArrayStringToInt.Execute(strNewResponsiblesIds);

            var card = context.Cards
                .Include("CardResponsibles")
                .Single(c => c.Id == cardId);

            // Delete relations
            foreach (var cardResponsible in card.CardResponsibles)
            {
                var index = Array.IndexOf(newResponsiblesId, cardResponsible.ResponsibleId);
                if (index == -1)
                {
                    var crdRsponsbl = new CardResponsible { CardId = card.Id, ResponsibleId = cardResponsible.ResponsibleId };
                    card.CardResponsibles.Remove(crdRsponsbl);
                }
            }

            // Add relations
            foreach (var newResponsibleId in newResponsiblesId)
            {
                if (!card.CardResponsibles.Any(ct => ct.ResponsibleId == newResponsibleId))
                {
                    var newCardResponsible = new CardResponsible { ResponsibleId = newResponsibleId, CardId = card.Id };
                    card.CardResponsibles.Add(newCardResponsible);
                }
            }
        }
    }
}
