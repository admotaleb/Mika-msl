using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Msl.Data;
using Msl.Models;

namespace Msl.Controllers
{
    [Authorize]
    public class Employee_DetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public Employee_DetailsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment= hostEnvironment;
        }

        // GET: Employee_Details
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
                    var AdminResult = _context.employee_Details.Include(e => e.ApplicationUser).Include(e => e.Department);
                    if (AdminResult == null)
                    {
                        return View(await AdminResult.ToListAsync());
                    }
                    return View(AdminResult);
                }
                var result = _context.employee_Details.Where(c => c.ApplicationUser.UserName==CurrentUserName).Include(e => e.ApplicationUser).Include(e => e.Department);
                if (result.Count() < 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Employee_Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Details = await _context.employee_Details
                .Include(e => e.ApplicationUser)
                .Include(e => e.Department).Include(e=>e.ApplicationUser.BranceSetting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_Details == null)
            {
                return NotFound();
            }

            return View(employee_Details);
        }

        // GET: Employee_Details/Create
        public IActionResult Create()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Employee_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fathers_Name,Mothers_Name,Spouse_Name,Desgnation,DseAuthorized,Joning_Date,Date_of_birth,Blood_Group,NID,Contact_No_Personal,EmergencyContact,Present_Address,Permanent_Address,ImageFile,DepartmentId,ApplicationUserId,Gender,MaritalStatus,Religion")] Employee_Details employee_Details)
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
                var EmployeeExist = _context.employee_Details.FirstOrDefault(c=>c.ApplicationUserId==CurrentUserId);
                if (EmployeeExist!=null)
                {
                    return RedirectToAction(nameof(Index));
                }
                employee_Details.ApplicationUserId = CurrentUserId;
                //Save image to wwwroot/image
                if (employee_Details.ImageFile != null) {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(employee_Details.ImageFile.FileName);
                string extension = Path.GetExtension(employee_Details.ImageFile.FileName);
                employee_Details.Pictures = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await employee_Details.ImageFile.CopyToAsync(fileStream);
                }
                 }
                //  End image//
                _context.Add(employee_Details);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", employee_Details.ApplicationUserId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee_Details.DepartmentId);
            return View(employee_Details);
        }

        // GET: Employee_Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Details = await _context.employee_Details.FindAsync(id);
            if (employee_Details == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", employee_Details.ApplicationUserId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee_Details.DepartmentId);
            return View(employee_Details);
        }

        // POST: Employee_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fathers_Name,Mothers_Name,Spouse_Name,Desgnation,DseAuthorized,Joning_Date,Date_of_birth,Blood_Group,NID,Contact_No_Personal,EmergencyContact,Present_Address,Permanent_Address,Pictures,ImageFile,DepartmentId,ApplicationUserId,Gender,MaritalStatus,Religion")] Employee_Details employee_Details)
        {
            if (id != employee_Details.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                try
                {
                    if (employee_Details.ImageFile !=null)
                    {
                        //Save image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(employee_Details.ImageFile.FileName);
                        string extension = Path.GetExtension(employee_Details.ImageFile.FileName);
                        employee_Details.Pictures = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await employee_Details.ImageFile.CopyToAsync(fileStream);
                        }
                        //  End image//
                        
                    }
                    _context.Update(employee_Details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Employee_DetailsExists(employee_Details.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.applicationUsers, "Id", "Id", employee_Details.ApplicationUserId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee_Details.DepartmentId);
            return View(employee_Details);
        }

        [Authorize(Roles = "Admin")]
        // GET: Employee_Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Details = await _context.employee_Details
                .Include(e => e.ApplicationUser)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_Details == null)
            {
                return NotFound();
            }

            return View(employee_Details);
        }

        [Authorize(Roles = "Admin")]
        // POST: Employee_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee_Details = await _context.employee_Details.FindAsync(id);
            _context.employee_Details.Remove(employee_Details);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employee_DetailsExists(int id)
        {
            return _context.employee_Details.Any(e => e.Id == id);
        }
    }
}
