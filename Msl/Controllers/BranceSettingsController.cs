using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Msl.Data;
using Msl.Models;

namespace Msl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BranceSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranceSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BranceSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.branceSettings.ToListAsync());
        }

        // GET: BranceSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BranceSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BranceId,Name,IpAddress")] BranceSetting branceSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branceSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branceSetting);
        }

        // GET: BranceSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branceSetting = await _context.branceSettings.FindAsync(id);
            if (branceSetting == null)
            {
                return NotFound();
            }
            return View(branceSetting);
        }

        // POST: BranceSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BranceId,Name,IpAddress")] BranceSetting branceSetting)
        {
            if (id != branceSetting.BranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branceSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranceSettingExists(branceSetting.BranceId))
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
            return View(branceSetting);
        }

        // GET: BranceSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branceSetting = await _context.branceSettings
                .FirstOrDefaultAsync(m => m.BranceId == id);
            if (branceSetting == null)
            {
                return NotFound();
            }

            return View(branceSetting);
        }

        // POST: BranceSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branceSetting = await _context.branceSettings.FindAsync(id);
            _context.branceSettings.Remove(branceSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranceSettingExists(int id)
        {
            return _context.branceSettings.Any(e => e.BranceId == id);
        }
    }
}
