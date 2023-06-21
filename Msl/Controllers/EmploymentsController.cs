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
    public class EmploymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmploymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employments
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
                    var AdminResult = _context.Employments.Include(e => e.ApplicationUser);
                    if (AdminResult == null)
                    {
                        return View(await AdminResult.ToListAsync());
                    }
                    return View(AdminResult);
                }
                var result = _context.Employments.Where(c => c.ApplicationUser.UserName == CurrentUserName).Include(e => e.ApplicationUser);
                if (result.Count() < 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Employments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employments
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employment == null)
            {
                return NotFound();
            }

            return View(employment);
        }

        // GET: Employments/Create
        public IActionResult Create()
        {

            //ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id");
            return View();
        }

        // POST: Employments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,CompanyBusiness,Designation,Department,Responsibilities,CompanyLocation,From,To,ApplicationUserId")] Employment employment)
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

                employment.ApplicationUserId = CurrentUserId;
                _context.Add(employment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", employment.ApplicationUserId);
            return View(employment);
        }

        // GET: Employments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employments.FindAsync(id);
            if (employment == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", employment.ApplicationUserId);
            return View(employment);
        }

        // POST: Employments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,CompanyBusiness,Designation,Department,Responsibilities,CompanyLocation,From,To,ApplicationUserId")] Employment employment)
        {
            if (id != employment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentExists(employment.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", employment.ApplicationUserId);
            return View(employment);
        }

        // GET: Employments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employments
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employment == null)
            {
                return NotFound();
            }

            return View(employment);
        }
        [Authorize(Roles = "Admin")]
        // POST: Employments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employment = await _context.Employments.FindAsync(id);
            _context.Employments.Remove(employment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentExists(int id)
        {
            return _context.Employments.Any(e => e.Id == id);
        }
    }
}
