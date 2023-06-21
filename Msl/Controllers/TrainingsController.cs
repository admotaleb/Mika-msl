using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Msl.Data;
using Msl.Models;

namespace Msl.Controllers
{
    [Authorize]
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trainings
        public async Task<IActionResult> Index()
        {
            var CurrentUserName = HttpContext.User.Identity.Name;
            var roleName = HttpContext.Session.GetString("roleName");
            if (roleName == null && CurrentUserName == null)
            {
                return Redirect("~/Identity/Account/LogIn");

            }
            if (roleName != null)
            {
                if (roleName == "Admin")
                {
                    var AdminResult = _context.training.Include(e => e.ApplicationUser);
                    if (AdminResult == null)
                    {
                        return View(await AdminResult.ToListAsync());
                    }
                    return View(AdminResult);
                }
                var result = _context.training.Where(c => c.ApplicationUser.UserName == CurrentUserName).Include(e => e.ApplicationUser);
                if (result.Count() < 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));

            }

        // GET: Trainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.training
                .Include(t => t.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Trainings/Create
        public IActionResult Create()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id");
            return View();
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainingName,Institute,From,To,Duration,ApplicationUserId")] Training training)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("userName");
                if (userName == null)
                {
                    return Redirect("~/Identity/Account/LogIn");

                }
                var CurrentUserInfo = _context.applicationUsers.FirstOrDefault(a => a.UserName == userName);
                var CurrentUserId = CurrentUserInfo.Id;
                
                training.ApplicationUserId = CurrentUserId;
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           // ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", training.ApplicationUserId);
            return View(training);
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", training.ApplicationUserId);
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainingName,Institute,From,To,Duration,ApplicationUserId")] Training training)
        {
            if (id != training.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", training.ApplicationUserId);
            return View(training);
        }

        [Authorize(Roles = "Admin")]
        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.training
                .Include(t => t.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        [Authorize(Roles = "Admin")]
        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _context.training.FindAsync(id);
            _context.training.Remove(training);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _context.training.Any(e => e.Id == id);
        }
    }
}
