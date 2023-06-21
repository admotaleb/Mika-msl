using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Msl.Data;
using Msl.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Msl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;

        public RolesController(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var Role = _roleManager.Roles.ToList();
            ViewBag.Roles = Role;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (name==null)
            {
                return NotFound();
            }
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var IsExist =await _roleManager.RoleExistsAsync(role.Name);
            if (IsExist)
            {
                ViewBag.IsExist = "This Role Already Exist";
                ViewBag.role=role;
                return View();
            }
           var result =await _roleManager.CreateAsync(role);
            
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Edit(string id)
        {
            if (id==null)
            {
                NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }
            ViewBag.id=Role.Id;
            ViewBag.role=Role.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {
            
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }
            Role.Name = name;
            var IsExist = await _roleManager.RoleExistsAsync(Role.Name);
            if (IsExist)
            {
                ViewBag.IsExist = "This Role Already Exist";
                ViewBag.role = Role.Name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(Role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }
            ViewBag.id = Role.Id;
            ViewBag.role = Role.Name;
            return View();
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);
            if (role == null) { 
            return NotFound();
            }
           var result=await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Assign()
        {
            ViewData["UserId"] = new SelectList(_db.applicationUsers.Where(c=>c.LockoutEnd<DateTime.Now|| c.LockoutEnd==null).ToList(), "Id", "UserName") ;
            ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Assign(UserAssignVM userRole)
        {
            var user = _db.applicationUsers.FirstOrDefault(c=>c.Id == userRole.UserId);
            var userexist = await _userManager.IsInRoleAsync(user, userRole.RoleId);
            if (userexist)
            {
                var roleRemove = await _userManager.RemoveFromRoleAsync(user, userRole.RoleId);
                ViewBag.Msg = "This role already assign";
                ViewData["UserId"] = new SelectList(_db.applicationUsers.Where(c => c.LockoutEnd < DateTime.Now || c.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
                return View();
            }
            

            var role = await _userManager.AddToRoleAsync(user, userRole.RoleId);
            return RedirectToAction(nameof(Index));
             
        }


        public IActionResult AssignUserRole()
        {
            var result = from ur in _db.UserRoles
                         join r in _db.Roles on ur.RoleId equals r.Id
                         join a in _db.applicationUsers on ur.UserId equals a.Id
                         select new
                         UserRoleMaping()
                         {
                             UserId = ur.UserId,
                             RoleId = ur.RoleId,
                             UserName = a.UserName,
                             RoleName = r.Name
                         };

            ViewBag.UserRole=result;

            return View();
        }
    }
}
