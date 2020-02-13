using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBoards.Models;

namespace MyBoards.Controllers
{
    public class CardController : Controller
    {
        private readonly MyBoardsContext _context;
        private readonly ILogger _logger;

        public CardController(MyBoardsContext context, ILogger<CardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Card
        public async Task<IActionResult> Index()
        {
            var myBoardsContext = _context.Cards.Include(c => c.CardList);
            return View(await myBoardsContext.ToListAsync());
        }

        // GET: Card/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.CardList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            ViewData["Tag"] = new SelectList(_context.Tags, "Id", "Name");

            return View(card);
        }

        // GET: Card/Create
        public IActionResult Create()
        {
            ViewData["CardListId"] = new SelectList(_context.CardLists, "Id", "Name");
            ViewData["Tag"] = new SelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Card/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CardListId,SelectedTags")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();

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

                        _context.Add(cardTag);
                        await _context.SaveChangesAsync();
                    }                    
                }                

                return RedirectToAction(nameof(Index));
            }
            ViewData["CardListId"] = new SelectList(_context.CardLists, "Id", "Name", card.CardListId);
            return View(card);
        }

        // GET: Card/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.CardTags)
                .SingleOrDefaultAsync(c => c.Id == id);
                //.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            if (card.CardTags != null)
            {
                card.SelectedTags = new string[card.CardTags.Count];
                int i = 0;

                foreach (var item in card.CardTags)
                {
                    card.SelectedTags[i] = item.TagId.ToString();
                    i++;
                }
            }
            else
            {
                card.SelectedTags = null;
            }

            ViewData["Tag"] = new MultiSelectList(_context.Tags, "Id", "Name", card.SelectedTags);
            ViewData["CardListId"] = new SelectList(_context.CardLists, "Id", "Name");

            return View(card);
        }

        // POST: Card/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CardListId,SelectedTags")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();

                    CardTag cardTag;

                    foreach (var item in card.SelectedTags)
                    {
                        int tgId;

                        if (Int32.TryParse(item, out tgId))
                        {
                            cardTag = new CardTag()
                            {
                                TagId = tgId,
                                CardId = card.Id
                            };

                            _context.Add(cardTag);
                            try
                            {
                                await _context.SaveChangesAsync();
                            }
                            catch (Exception e)
                            {
                                _logger.LogInformation("LOG: " + e.Message);
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardListId"] = new SelectList(_context.CardLists, "Id", "Name", card.CardListId);
            return View(card);
        }

        // GET: Card/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.CardList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
