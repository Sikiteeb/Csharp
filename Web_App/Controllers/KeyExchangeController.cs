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
    public class KeyExchangeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeyExchangeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KeyExchange
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.KeyExchange.Include(k => k.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: KeyExchange/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KeyExchange == null)
            {
                return NotFound();
            }

            var keyExchange = await _context.KeyExchange
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyExchange == null)
            {
                return NotFound();
            }

            return View(keyExchange);
        }

        // GET: KeyExchange/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: KeyExchange/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,P,G,ASecret,BSecret,CommonSecret,UserId")] KeyExchange keyExchange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keyExchange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", keyExchange.UserId);
            return View(keyExchange);
        }

        // GET: KeyExchange/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KeyExchange == null)
            {
                return NotFound();
            }

            var keyExchange = await _context.KeyExchange.FindAsync(id);
            if (keyExchange == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", keyExchange.UserId);
            return View(keyExchange);
        }

        // POST: KeyExchange/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,P,G,ASecret,BSecret,CommonSecret,UserId")] KeyExchange keyExchange)
        {
            if (id != keyExchange.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keyExchange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyExchangeExists(keyExchange.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", keyExchange.UserId);
            return View(keyExchange);
        }

        // GET: KeyExchange/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KeyExchange == null)
            {
                return NotFound();
            }

            var keyExchange = await _context.KeyExchange
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyExchange == null)
            {
                return NotFound();
            }

            return View(keyExchange);
        }

        // POST: KeyExchange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KeyExchange == null)
            {
                return Problem("Entity set 'ApplicationDbContext.KeyExchange'  is null.");
            }
            var keyExchange = await _context.KeyExchange.FindAsync(id);
            if (keyExchange != null)
            {
                _context.KeyExchange.Remove(keyExchange);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeyExchangeExists(int id)
        {
          return (_context.KeyExchange?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
