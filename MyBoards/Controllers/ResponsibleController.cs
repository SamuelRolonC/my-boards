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
    public class ResponsibleController : Controller
    {
        private readonly MyBoardsContext _context;

        public ResponsibleController(MyBoardsContext context)
        {
            _context = context;
        }

        // GET: Responsible
        public async Task<IActionResult> Index()
        {
            return View(await _context.Responsibles.ToListAsync());
        }

        // GET: Responsible/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsible = await _context.Responsibles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsible == null)
            {
                return NotFound();
            }

            return View(responsible);
        }

        // GET: Responsible/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Responsible/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Responsible responsible)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsible);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsible);
        }

        // GET: Responsible/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsible = await _context.Responsibles.FindAsync(id);
            if (responsible == null)
            {
                return NotFound();
            }
            return View(responsible);
        }

        // POST: Responsible/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Responsible responsible)
        {
            if (id != responsible.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsible);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsibleExists(responsible.Id))
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
            return View(responsible);
        }

        // GET: Responsible/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsible = await _context.Responsibles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsible == null)
            {
                return NotFound();
            }

            return View(responsible);
        }

        // POST: Responsible/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsible = await _context.Responsibles.FindAsync(id);
            _context.Responsibles.Remove(responsible);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsibleExists(int id)
        {
            return _context.Responsibles.Any(e => e.Id == id);
        }
    }
}
