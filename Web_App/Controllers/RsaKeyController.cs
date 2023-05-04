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
    public class RsaKeyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RsaKeyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RsaKey
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RsaKeys.Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RsaKey/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RsaKeys == null)
            {
                return NotFound();
            }

            var rsaKey = await _context.RsaKeys
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsaKey == null)
            {
                return NotFound();
            }

            return View(rsaKey);
        }

        // GET: RsaKey/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: RsaKey/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Cypher,KeyLength,PPrime,QPrime,Exponent,RsaCypher,KeySecret,UserId")] RsaKey rsaKey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rsaKey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rsaKey.UserId);
            return View(rsaKey);
        }

        // GET: RsaKey/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RsaKeys == null)
            {
                return NotFound();
            }

            var rsaKey = await _context.RsaKeys.FindAsync(id);
            if (rsaKey == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rsaKey.UserId);
            return View(rsaKey);
        }

        // POST: RsaKey/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,Cypher,KeyLength,PPrime,QPrime,Exponent,RsaCypher,KeySecret,UserId")] RsaKey rsaKey)
        {
            if (id != rsaKey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rsaKey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RsaKeyExists(rsaKey.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rsaKey.UserId);
            return View(rsaKey);
        }

        // GET: RsaKey/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RsaKeys == null)
            {
                return NotFound();
            }

            var rsaKey = await _context.RsaKeys
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsaKey == null)
            {
                return NotFound();
            }

            return View(rsaKey);
        }

        // POST: RsaKey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RsaKeys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RsaKeys'  is null.");
            }
            var rsaKey = await _context.RsaKeys.FindAsync(id);
            if (rsaKey != null)
            {
                _context.RsaKeys.Remove(rsaKey);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RsaKeyExists(int id)
        {
          return (_context.RsaKeys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
