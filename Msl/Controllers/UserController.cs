using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Msl.Data;
using Msl.Models;
using QuickMailer;
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
        [AllowAnonymous]
        public  async Task<IActionResult>Create()
        {
            ViewData["BranceId"] = new SelectList(_db.branceSettings, "BranceId", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    var IsRoleSave = await _userManager.AddToRoleAsync(user, "User");

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    // Mail send

                    Email email = new Email();
                    string Tomail = user.Email;

                    email.SendEmail(Tomail, Credential.Email, Credential.Password, "Email Confirmation Link", confirmationLink);

                    //mail end

                    TempData["EmailConfirme"] = "Please Check Your Email";
                    return Redirect("~/Identity/Account/LogIn");
                    //return RedirectToAction("Index", "Employee_Details");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewData["BranceId"] = new SelectList(_db.branceSettings, "BranceId", "Name");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["BranceId"] = new SelectList(_db.branceSettings, "BranceId", "Name");
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
                userInfo.FastName= user.FastName;
                userInfo.LastName = user.LastName;
                userInfo.UserName=user.UserName;
                userInfo.Email=user.Email;
                userInfo.PhoneNumber=user.PhoneNumber;
                userInfo.BranceSettingBranceId = user.BranceSettingBranceId;
                userInfo.Tws=user.Tws;

                userInfo.BranceSettingBranceId=user.BranceSettingBranceId;


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
            ViewData["BranceId"] = new SelectList(_db.branceSettings, "BranceId", "Name");
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
