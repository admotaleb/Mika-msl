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
    public class NomineesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NomineesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nominees
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
                    var AdminResult = _context.nominees.Include(e => e.ApplicationUser);
                    if (AdminResult == null)
                    {
                        return View(await AdminResult.ToListAsync());
                    }
                    return View(AdminResult);
                }
                var result = _context.nominees.Where(c => c.ApplicationUser.UserName == CurrentUserName).Include(e => e.ApplicationUser);
                if (result.Count() < 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Nominees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominee = await _context.nominees
                .Include(n => n.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nominee == null)
            {
                return NotFound();
            }

            return View(nominee);
        }

        // GET: Nominees/Create
        public IActionResult Create()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id");
            return View();
        }

        // POST: Nominees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomineeName,LegalGuardianName,Address,NID,Contact,Relation,DateOfBirth,Entitlement,ApplicationUserId")] Nominee nominee)
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

                nominee.ApplicationUserId = CurrentUserId;
                _context.Add(nominee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", nominee.ApplicationUserId);
            return View(nominee);
        }

        // GET: Nominees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominee = await _context.nominees.FindAsync(id);
            if (nominee == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", nominee.ApplicationUserId);
            return View(nominee);
        }

        // POST: Nominees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomineeName,LegalGuardianName,Address,NID,Contact,Relation,DateOfBirth,Entitlement,ApplicationUserId")] Nominee nominee)
        {
            if (id != nominee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nominee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NomineeExists(nominee.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", nominee.ApplicationUserId);
            return View(nominee);
        }
        [Authorize(Roles = "Admin")]
        // GET: Nominees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominee = await _context.nominees
                .Include(n => n.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nominee == null)
            {
                return NotFound();
            }

            return View(nominee);
        }

        [Authorize(Roles = "Admin")]
        // POST: Nominees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nominee = await _context.nominees.FindAsync(id);
            _context.nominees.Remove(nominee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NomineeExists(int id)
        {
            return _context.nominees.Any(e => e.Id == id);
        }
    }
}
