using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using WebAppCrypto.Data;

namespace WebAppCrypto.Controllers
{
    public class CaesarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaesarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Caesars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Caesars.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Caesars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Caesars == null)
            {
                return NotFound();
            }

            var caesar = await _context.Caesars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caesar == null)
            {
                return NotFound();
            }

            return View(caesar);
        }

        // GET: Caesars/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Caesars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,PlainText,CypherText,UserId")] Caesar caesar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caesar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", caesar.UserId);
            return View(caesar);
        }

        // GET: Caesars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Caesars == null)
            {
                return NotFound();
            }

            var caesar = await _context.Caesars.FindAsync(id);
            if (caesar == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", caesar.UserId);
            return View(caesar);
        }

        // POST: Caesars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,PlainText,CypherText,UserId")] Caesar caesar)
        {
            if (id != caesar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caesar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaesarExists(caesar.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", caesar.UserId);
            return View(caesar);
        }

        // GET: Caesars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Caesars == null)
            {
                return NotFound();
            }

            var caesar = await _context.Caesars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caesar == null)
            {
                return NotFound();
            }

            return View(caesar);
        }

        // POST: Caesars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Caesars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Caesars'  is null.");
            }
            var caesar = await _context.Caesars.FindAsync(id);
            if (caesar != null)
            {
                _context.Caesars.Remove(caesar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaesarExists(int id)
        {
          return (_context.Caesars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
