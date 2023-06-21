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
    public class EducationalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Educationals
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
                    var AdminResult = _context.educationals.Include(e => e.ApplicationUser);
                    if (AdminResult == null)
                    {
                        return View(await AdminResult.ToListAsync());
                    }
                    return View(AdminResult);
                }
                var result = _context.educationals.Where(c => c.ApplicationUser.UserName == CurrentUserName).Include(e => e.ApplicationUser);
                if (result.Count() < 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Educationals/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var educational = await _context.educationals
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educational == null)
            {
                return NotFound();
            }

            return View(educational);
        }

        // GET: Educationals/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id");
            return View();
        }

        // POST: Educationals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SscExam,SscSubject,SscInstitute_Name,SscPassing_Year,SscResult,HscExam,HscSubject,HscInstitute_Name,HscPassing_Year,HscResult,HonorsExam,HonorsSubject,HonorsInstitute_Name,HonorsPassing_Year,HonorsResult,MastersExam,MastersSubject,MastersInstitute_Name,MastersPassing_Year,MastersResult,ApplicationUserId")] Educational educational)
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
                var EmployeeExist = _context.educationals.FirstOrDefault(c => c.ApplicationUserId == CurrentUserId);
                if (EmployeeExist != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                educational.ApplicationUserId = CurrentUserId;

                _context.Add(educational);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(educational);
        }

        // GET: Educationals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educational = await _context.educationals.FindAsync(id);
            if (educational == null)
            {
                return NotFound();
            }
            
            return View(educational);
        }

        // POST: Educationals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SscExam,SscSubject,SscInstitute_Name,SscPassing_Year,SscResult,HscExam,HscSubject,HscInstitute_Name,HscPassing_Year,HscResult,HonorsExam,HonorsSubject,HonorsInstitute_Name,HonorsPassing_Year,HonorsResult,MastersExam,MastersSubject,MastersInstitute_Name,MastersPassing_Year,MastersResult,ApplicationUserId")] Educational educational)
        {
            if (id != educational.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userName = HttpContext.Session.GetString("userName");
                    if (userName == null)
                    {
                        return Redirect("~/Identity/Account/LogIn");

                    }
                    var CurrentUserInfo = _context.applicationUsers.FirstOrDefault(a => a.UserName == userName);
                    var CurrentUserId = CurrentUserInfo.Id;

                    educational.ApplicationUserId = CurrentUserId;
                    _context.Update(educational);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationalExists(educational.Id))
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
            
            return View(educational);
        }
        [Authorize(Roles = "Admin")]
        // GET: Educationals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educational = await _context.educationals
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educational == null)
            {
                return NotFound();
            }

            return View(educational);
        }
        [Authorize(Roles = "Admin")]
        // POST: Educationals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educational = await _context.educationals.FindAsync(id);
            _context.educationals.Remove(educational);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationalExists(int id)
        {
            return _context.educationals.Any(e => e.Id == id);
        }
    }
}
