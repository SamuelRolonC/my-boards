using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBoards.Models;
using MyBoards.Services;

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
                .Include(c => c.State)
                .Include(c => c.CardTags)
                    .ThenInclude(ct => ct.Tag)
                .Include(c => c.CardResponsibles)
                    .ThenInclude(cr => cr.Responsible)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }            

            return View(card);
        }

        // GET: Card/Create
        public IActionResult Create()
        {
            ViewData["CardListId"] = new SelectList(_context.CardLists, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Title");

            ViewData["Tag"] = new MultiSelectList(_context.Tags, "Id", "Name");            
            ViewData["Responsible"] = new MultiSelectList(_context.Responsibles, "Id", "Name");

            return View();
        }

        // POST: Card/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CardListId,StateId,SelectedTags,SelectedResponsibles")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();

                CreateCardTag.Execute(card, _context);                
                CreateCardResponsible.Execute(card, _context);

                return RedirectToAction(nameof(Index));
            }
                        
            ViewData["CardListId"] = new SelectList(_context.CardLists, "Id", "Name", card.CardListId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Title", card.StateId);

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
                .Include(c => c.CardResponsibles)
                .SingleOrDefaultAsync(c => c.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            card.LoadTags();
            card.LoadResponsibles();

            ViewData["Tag"] = new MultiSelectList(_context.Tags, "Id", "Name");
            ViewData["Responsible"] = new MultiSelectList(_context.Responsibles, "Id", "Name");

            ViewData["CardListId"] = new SelectList(_context.CardLists, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Title");

            return View(card);
        }

        // POST: Card/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CardListId,StateId,SelectedTags,SelectedResponsibles,CardTags")] Card card)
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

                    EditCardTag.Execute(_context, id, card.SelectedTags);
                    EditCardResponsible.Execute(_context, id, card.SelectedResponsibles);

                    _context.SaveChanges();
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
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Title", card.StateId);

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
