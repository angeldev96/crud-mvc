using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crud_mvc.Models;

namespace crud_mvc.Controllers
{
    public class CuponesController : Controller
    {
        private readonly PromosContext _context;

        public CuponesController(PromosContext context)
        {
            _context = context;
        }

        // GET: Cupones
        public async Task<IActionResult> Index()
        {
            var promosContext = _context.Cupones.Include(c => c.Cliente);
            return View(await promosContext.ToListAsync());
        }

        // GET: Cupones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cupones == null)
            {
                return NotFound();
            }

            var cupone = await _context.Cupones
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.IdCupon == id);
            if (cupone == null)
            {
                return NotFound();
            }

            return View(cupone);
        }

        // GET: Cupones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            return View();
        }

        // POST: Cupones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCupon,CodigoCupon,Credito,FechaCreacion,FechaVencimiento,ClienteId")] Cupone cupone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cupone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", cupone.ClienteId);
            return View(cupone);
        }

        // GET: Cupones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cupones == null)
            {
                return NotFound();
            }

            var cupone = await _context.Cupones.FindAsync(id);
            if (cupone == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", cupone.ClienteId);
            return View(cupone);
        }

        // POST: Cupones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCupon,CodigoCupon,Credito,FechaCreacion,FechaVencimiento,ClienteId")] Cupone cupone)
        {
            if (id != cupone.IdCupon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cupone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuponeExists(cupone.IdCupon))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", cupone.ClienteId);
            return View(cupone);
        }

        // GET: Cupones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cupones == null)
            {
                return NotFound();
            }

            var cupone = await _context.Cupones
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.IdCupon == id);
            if (cupone == null)
            {
                return NotFound();
            }

            return View(cupone);
        }

        // POST: Cupones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cupones == null)
            {
                return Problem("Entity set 'PromosContext.Cupones'  is null.");
            }
            var cupone = await _context.Cupones.FindAsync(id);
            if (cupone != null)
            {
                _context.Cupones.Remove(cupone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuponeExists(int id)
        {
          return _context.Cupones.Any(e => e.IdCupon == id);
        }
    }
}
