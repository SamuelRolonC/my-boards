using Microsoft.EntityFrameworkCore;
using MyBoards.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBoards.Services
{
    public class EditCardTag
    {
        public static void Execute(MyBoardsContext context, int cardId, string[] strNewTagsIds)
        {
            var newTagsId = ArrayStringToInt.Execute(strNewTagsIds);

            var card = context.Cards
                .Include("CardTags")
                .Single(c => c.Id == cardId);

            // Delete relations
            foreach (var cardTag in card.CardTags)
            {
                var index = Array.IndexOf(newTagsId, cardTag.TagId);
                if (index == -1)
                {
                    var crdTg = new CardTag { CardId = card.Id, TagId = cardTag.TagId };
                    card.CardTags.Remove(crdTg);
                }
            }

            // Add relations
            foreach (var newTagId in newTagsId)
            {

                if (!card.CardTags.Any(ct => ct.TagId == newTagId))
                {
                    var newCardTag = new CardTag { TagId = newTagId, CardId = card.Id };
                    card.CardTags.Add(newCardTag);
                }
            }
        }
    }
}
