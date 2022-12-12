using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassLocationsController : Controller
    {
        private readonly EducatedMoneyContext _context;

        public ClassLocationsController(EducatedMoneyContext context)
        {
            _context = context;
        }

        // GET: ClassLocations
        public async Task<IActionResult> Index()
        {
              return View(await _context.ClassLocations.ToListAsync());
        }

        // GET: ClassLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassLocations == null)
            {
                return NotFound();
            }

            var classLocation = await _context.ClassLocations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (classLocation == null)
            {
                return NotFound();
            }

            return View(classLocation);
        }

        // GET: ClassLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,CampusName,City,State")] ClassLocation classLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classLocation);
        }

        // GET: ClassLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassLocations == null)
            {
                return NotFound();
            }

            var classLocation = await _context.ClassLocations.FindAsync(id);
            if (classLocation == null)
            {
                return NotFound();
            }
            return View(classLocation);
        }

        // POST: ClassLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationId,CampusName,City,State")] ClassLocation classLocation)
        {
            if (id != classLocation.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassLocationExists(classLocation.LocationId))
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
            return View(classLocation);
        }

        // GET: ClassLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassLocations == null)
            {
                return NotFound();
            }

            var classLocation = await _context.ClassLocations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (classLocation == null)
            {
                return NotFound();
            }

            return View(classLocation);
        }

        // POST: ClassLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassLocations == null)
            {
                return Problem("Entity set 'EducatedMoneyContext.ClassLocations'  is null.");
            }
            var classLocation = await _context.ClassLocations.FindAsync(id);
            if (classLocation != null)
            {
                _context.ClassLocations.Remove(classLocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassLocationExists(int id)
        {
          return _context.ClassLocations.Any(e => e.LocationId == id);
        }
    }
}
