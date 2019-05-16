using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;

namespace BillSplitter.Controllers
{
    [Authorize]
    public class LedgersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LedgersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ledgers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ledger.ToListAsync());
        }

        // GET: Ledgers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledger = await _context.Ledger
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ledger == null)
            {
                return NotFound();
            }

            return View(ledger);
        }

        // GET: Ledgers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ledgers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Amount,PayeeUserId,PayerUserId")] Ledger ledger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ledger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ledger);
        }

        // GET: Ledgers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledger = await _context.Ledger.FindAsync(id);
            if (ledger == null)
            {
                return NotFound();
            }
            return View(ledger);
        }

        // POST: Ledgers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Amount,PayeeUserId,PayerUserId")] Ledger ledger)
        {
            if (id != ledger.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ledger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LedgerExists(ledger.ID))
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
            return View(ledger);
        }

        // GET: Ledgers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledger = await _context.Ledger
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ledger == null)
            {
                return NotFound();
            }

            return View(ledger);
        }

        // POST: Ledgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ledger = await _context.Ledger.FindAsync(id);
            _context.Ledger.Remove(ledger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LedgerExists(int id)
        {
            return _context.Ledger.Any(e => e.ID == id);
        }
    }
}
