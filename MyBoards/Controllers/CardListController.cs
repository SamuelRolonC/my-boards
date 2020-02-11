using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBoards.Models;

namespace MyBoards.Controllers
{
    public class CardListController : Controller
    {
        private readonly MyBoardsContext _context;

        public CardListController(MyBoardsContext context)
        {
            _context = context;
        }

        // GET: CardList
        public async Task<IActionResult> Index()
        {
            var myBoardsContext = _context.CardLists.Include(c => c.Board);
            return View(await myBoardsContext.ToListAsync());
        }

        // GET: CardList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardList = await _context.CardLists
                .Include(c => c.Board)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardList == null)
            {
                return NotFound();
            }

            return View(cardList);
        }

        // GET: CardList/Create
        public IActionResult Create()
        {
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Name");
            return View();
        }

        // POST: CardList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BoardId")] CardList cardList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Name", cardList.BoardId);
            return View(cardList);
        }

        // GET: CardList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardList = await _context.CardLists.FindAsync(id);
            if (cardList == null)
            {
                return NotFound();
            }
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Name", cardList.BoardId);
            return View(cardList);
        }

        // POST: CardList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BoardId")] CardList cardList)
        {
            if (id != cardList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardListExists(cardList.Id))
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
            ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Name", cardList.BoardId);
            return View(cardList);
        }

        // GET: CardList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardList = await _context.CardLists
                .Include(c => c.Board)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardList == null)
            {
                return NotFound();
            }

            return View(cardList);
        }

        // POST: CardList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardList = await _context.CardLists.FindAsync(id);
            _context.CardLists.Remove(cardList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardListExists(int id)
        {
            return _context.CardLists.Any(e => e.Id == id);
        }
    }
}
