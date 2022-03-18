#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektDotNet.Data;
using ProjektDotNet.Models;

namespace ProjektDotNet.Controllers
{
    [Authorize]
    public class GameContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameContents
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameContents.ToListAsync());
        }

        // GET: GameContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameContent = await _context.GameContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameContent == null)
            {
                return NotFound();
            }

            return View(gameContent);
        }

        // GET: GameContents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content")] GameContent gameContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameContent);
        }

        // GET: GameContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameContent = await _context.GameContents.FindAsync(id);
            if (gameContent == null)
            {
                return NotFound();
            }
            return View(gameContent);
        }

        // POST: GameContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content")] GameContent gameContent)
        {
            if (id != gameContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameContentExists(gameContent.Id))
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
            return View(gameContent);
        }

        // GET: GameContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameContent = await _context.GameContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameContent == null)
            {
                return NotFound();
            }

            return View(gameContent);
        }

        // POST: GameContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameContent = await _context.GameContents.FindAsync(id);
            _context.GameContents.Remove(gameContent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameContentExists(int id)
        {
            return _context.GameContents.Any(e => e.Id == id);
        }
    }
}
