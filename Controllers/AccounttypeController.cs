using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXSM3935_C_Sharp_Final_Project.Data;
using EXSM3935_C_Sharp_Final_Project.Models;

namespace EXSM3935_C_Sharp_Final_Project.Controllers
{
    public class AccounttypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccounttypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accounttype
        public async Task<IActionResult> Index()
        {
              return View(await _context.Accounttypes.ToListAsync());
        }

        // GET: Accounttype/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounttypes == null)
            {
                return NotFound();
            }

            var accounttype = await _context.Accounttypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounttype == null)
            {
                return NotFound();
            }

            return View(accounttype);
        }

        // GET: Accounttype/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounttype/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InterestRate")] Accounttype accounttype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounttype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accounttype);
        }

        // GET: Accounttype/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounttypes == null)
            {
                return NotFound();
            }

            var accounttype = await _context.Accounttypes.FindAsync(id);
            if (accounttype == null)
            {
                return NotFound();
            }
            return View(accounttype);
        }

        // POST: Accounttype/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InterestRate")] Accounttype accounttype)
        {
            if (id != accounttype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounttype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccounttypeExists(accounttype.Id))
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
            return View(accounttype);
        }

        // GET: Accounttype/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounttypes == null)
            {
                return NotFound();
            }

            var accounttype = await _context.Accounttypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounttype == null)
            {
                return NotFound();
            }

            return View(accounttype);
        }

        // POST: Accounttype/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounttypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Accounttypes'  is null.");
            }
            var accounttype = await _context.Accounttypes.FindAsync(id);
            if (accounttype != null)
            {
                _context.Accounttypes.Remove(accounttype);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccounttypeExists(int id)
        {
          return _context.Accounttypes.Any(e => e.Id == id);
        }
    }
}
