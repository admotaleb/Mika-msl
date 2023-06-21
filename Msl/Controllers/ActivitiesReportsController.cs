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
    public class ActivitiesReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActivitiesReports
        public async Task<IActionResult> Index()
        {
            var CurrentUserName = HttpContext.User.Identity.Name;
            var roleName = HttpContext.Session.GetString("roleName");
            var CurrentUserInfo = _context.applicationUsers.FirstOrDefault(c=>c.UserName== CurrentUserName);
            if (roleName == null && CurrentUserName == null)
            {
                return Redirect("~/Identity/Account/LogIn");

            }
            if (roleName != null)
            {
                if (roleName == "Admin")
                {
                    var applicationDbContext = _context.ActivitiesReports.Include(a => a.BranceSetting);
                    return View(await applicationDbContext.ToListAsync());
                }
                var result = _context.ActivitiesReports.Where(c => c.BranceSettingBranceId == CurrentUserInfo.BranceSettingBranceId).Include(a => a.BranceSetting);
                if (result.Count() < 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));
            
        }
        [Authorize(Roles = "Admin")]
        // Post: ActivitiesReports
        [HttpPost]
        public async Task<IActionResult> Index(DateTime FirstDate, DateTime LastDate)
        {
            var activitiesReport = await _context.ActivitiesReports.Include(a=>a.BranceSetting).Where(m => m.Date >= FirstDate && m.Date <= LastDate).ToListAsync();
            return View(activitiesReport);
        }
        // GET: ActivitiesReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activitiesReport = await _context.ActivitiesReports
                .Include(a => a.BranceSetting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activitiesReport == null)
            {
                return NotFound();
            }

            return View(activitiesReport);
        }

        // GET: ActivitiesReports/Create
        public IActionResult Create()
        {
            ViewData["BranceSettingBranceId"] = new SelectList(_context.branceSettings, "BranceId", "Name");
            return View();
        }

        // POST: ActivitiesReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Trader,Date,Turnover,AverageClients,BoOpen,Investment,Withdraw,Visit,ClientsNo,ExpectedBoOpen,ZoomMeeting,PhysicalVisit,Leave,BranceSettingBranceId")] ActivitiesReport activitiesReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activitiesReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranceSettingBranceId"] = new SelectList(_context.branceSettings, "BranceId", "Name", activitiesReport.BranceSettingBranceId);
            return View(activitiesReport);
        }

        // GET: ActivitiesReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activitiesReport = await _context.ActivitiesReports.FindAsync(id);
            if (activitiesReport == null)
            {
                return NotFound();
            }
            ViewData["BranceSettingBranceId"] = new SelectList(_context.branceSettings, "BranceId", "Name", activitiesReport.BranceSettingBranceId);
            return View(activitiesReport);
        }

        // POST: ActivitiesReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Trader,Date,Turnover,AverageClients,BoOpen,Investment,Withdraw,Visit,ClientsNo,ExpectedBoOpen,ZoomMeeting,PhysicalVisit,Leave,BranceSettingBranceId")] ActivitiesReport activitiesReport)
        {
            if (id != activitiesReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activitiesReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivitiesReportExists(activitiesReport.Id))
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
            ViewData["BranceSettingBranceId"] = new SelectList(_context.branceSettings, "BranceId", "Name", activitiesReport.BranceSettingBranceId);
            return View(activitiesReport);
        }
        [Authorize(Roles = "Admin")]
        // GET: ActivitiesReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activitiesReport = await _context.ActivitiesReports
                .Include(a => a.BranceSetting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activitiesReport == null)
            {
                return NotFound();
            }

            return View(activitiesReport);
        }
        [Authorize(Roles = "Admin")]
        // POST: ActivitiesReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activitiesReport = await _context.ActivitiesReports.FindAsync(id);
            _context.ActivitiesReports.Remove(activitiesReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivitiesReportExists(int id)
        {
            return _context.ActivitiesReports.Any(e => e.Id == id);
        }
    }
}
