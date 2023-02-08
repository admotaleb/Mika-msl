using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Msl.Data;
using Msl.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Msl.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        
        public IActionResult Index()
        {
            return View(_db.applicationUsers.ToList());
        }

        [HttpGet]
        
        public  async Task<IActionResult>Create()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if(ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    var IsRoleSave = await _userManager.AddToRoleAsync(user, "User");
                    //ViewBag.SaveSucceeded = "Data Save succeeded";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user =  _db.applicationUsers.FirstOrDefault(c=>c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _db.applicationUsers.FirstOrDefault(c => c.Id == user.Id);
                if (userInfo == null)
                {
                    return NotFound();
                }
                userInfo.UserName=user.UserName;
                userInfo.Email=user.Email;

                var result = await _userManager.UpdateAsync(userInfo);
                if (result.Succeeded)
                {
                    //ViewBag.SaveSucceeded = "Data Save succeeded";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = _db.applicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpGet]

        public async Task<IActionResult> LockOut(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = _db.applicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> LockOut(ApplicationUser user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var userInfo = _db.applicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }

            userInfo.LockoutEnd = DateTime.Now.AddYears(100);
            _db. SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public async Task<IActionResult> Active(string id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var InactiveUser = _db.applicationUsers.FirstOrDefault(c => c.Id == id);
            if (InactiveUser==null)
            {
                return NotFound();
            }

            return View(InactiveUser);
        }

        [HttpPost]

        public async Task<IActionResult> Active(ApplicationUser user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var userInfo = _db.applicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }

            userInfo.LockoutEnd = DateTime.Now.AddDays(-1);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InactiveUser = _db.applicationUsers.FirstOrDefault(c => c.Id == id);
            if (InactiveUser == null)
            {
                return NotFound();
            }

            return View(InactiveUser);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var userInfo = _db.applicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }

            _db.applicationUsers.Remove(userInfo);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
