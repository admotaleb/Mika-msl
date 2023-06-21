using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using ExcelDataReader.Log;
using GeoLocation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Msl.Data;
using Msl.Models;
using Newtonsoft.Json;

namespace Msl.Controllers
{
    [Authorize]
    public class AttendencesController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public AttendencesController(ApplicationDbContext context)
        {
            _context = context;
            

        }

        // GET: Attendences
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.attendences.Include(a => a.ApplicationUser).Include(c=>c.BranceSetting);
            
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendence = await _context.attendences
                .Include(a => a.ApplicationUser).Include(c => c.BranceSetting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendence == null)
            {
                return NotFound();
            }

            return View(attendence);
        }

        // GET: Attendences/Create
        public IActionResult Create()
        {
            var userName = HttpContext.Session.GetString("userName");
            if (userName==null)
            {
                return Redirect("~/Identity/Account/LogIn");

            }
            var CurrentUserInfo = _context.applicationUsers.FirstOrDefault(a => a.UserName == userName);
            var CurrentUserId = CurrentUserInfo.Id;

            string dat = DateTime.Now.ToString("MM/dd/yyyy");
            var info = _context.attendences.FirstOrDefault(c=>c.ApplicationUserId== CurrentUserId && c.Date== dat);



            if (info != null)
            {
                return RedirectToAction("CheckOut", new { id = info.Id });
            }


            ViewData["UserId"] = new SelectList(_context.applicationUsers, "Id", "Id");
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId");

            return View();
        }

        // POST: Attendences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CheckIn,CheckInReason,Date,CheckOut,CheckOutReason,Present,PrivateIp,PublicIp,HostName,City,Latitude,Longitude,ApplicationUserId, BranceSettingBranceId")] Attendence attendence)
        {
            // For Ip address, Host Name, 
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //string ipAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(Address => Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
            //string hostName = System.Net.Dns.GetHostName();

           

            // For Mac Address
            //JsonToView model = new JsonToView();
            //GeoHelper geoHelper = new GeoHelper();
            //var result = await geoHelper.GetGeoInfo();
            //model = JsonConvert.DeserializeObject<JsonToView>(result);
            
           


            var userName = HttpContext.Session.GetString("userName");
            if (userName == null)
            {
                return Redirect("~/Identity/Account/LogIn");

            }
            var CurrentUserInfo = _context.applicationUsers.FirstOrDefault(a => a.UserName == userName);
            var CurrentUserId = CurrentUserInfo.Id;
            string dat = DateTime.Now.ToString("MM/dd/yyyy");
            var info = _context.attendences.FirstOrDefault(c => c.ApplicationUserId == CurrentUserId && c.Date == dat);

            var BranceInfo =_context.branceSettings.FirstOrDefault(c=>c.IpAddress== attendence.PublicIp);
            if(BranceInfo == null)
            {
                return RedirectToAction(nameof(Index));
            }


            if (info != null)
            {
                return RedirectToAction("CheckOut", new { id = info.Id });
            }

            if (ModelState.IsValid)
            {
                //attendence.City = model.City;
                //attendence.Latitude = model.Latitude;
                //attendence.Longitude = model.Longitude;
                //attendence.PublicIp = model.ip;
                //attendence.HostName = hostName;
                //attendence.PrivateIp = ipAddress;
                attendence.BranceSettingBranceId = BranceInfo.BranceId;
                attendence.ApplicationUserId = CurrentUserId;
                attendence.CheckIn = DateTime.Now.ToString("h:mm:ss tt");
                attendence.Date = DateTime.Now.ToString("MM/dd/yyyy");
                _context.Add(attendence);
                await _context.SaveChangesAsync();
                var TimeSetting = _context.timeSetting.FirstOrDefault();
                if (Convert.ToDateTime(attendence.CheckIn)<= Convert.ToDateTime(TimeSetting.InTime))
                {
                    return RedirectToAction("CheckOut", new { id = attendence.Id });
                }
                else
                {
                    return RedirectToAction("Edit", new { id = attendence.Id });
                }
                
            }
            ViewData["UserId"] = new SelectList(_context.applicationUsers, "ID", "ID", attendence.ApplicationUserId);
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId", attendence.BranceSettingBranceId);
            return View(attendence);
        }

        // GET: Attendences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendence = await _context.attendences.FindAsync(id);
            if (attendence == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.applicationUsers, "Id", "Id", attendence.ApplicationUserId);
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId", attendence.BranceSettingBranceId);

            return View(attendence);
        }

        // POST: Attendences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckIn,CheckInReason,Date,CheckOut,CheckOutReason,Present,PrivateIp,PublicIp,HostName,City,Latitude,Longitude,ApplicationUserId, BranceSettingBranceId")] Attendence attendence)
        {
            if (id != attendence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendenceExists(attendence.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

               
                    return RedirectToAction("CheckOut", new { id = attendence.Id });
                
            }
            ViewData["UserId"] = new SelectList(_context.applicationUsers, "Id", "Id", attendence.ApplicationUserId);
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId", attendence.BranceSettingBranceId);
            return View(attendence);
        }

        // GET: Attendences/CheckOut/5
        public async Task<IActionResult> CheckOut(int? id)
        {
            var userName = HttpContext.Session.GetString("userName");
            if (userName == null)
            {
                return Redirect("~/Identity/Account/LogIn");

            }
            var CurrentUserInfo = _context.applicationUsers.FirstOrDefault(a => a.UserName == userName);
            var CurrentUserId = CurrentUserInfo.Id;
            string dat = DateTime.Now.ToString("MM/dd/yyyy");
            var info = _context.attendences.FirstOrDefault(c => c.ApplicationUserId == CurrentUserId && c.Date == dat);
            if (info==null)
            {
                return RedirectToAction(nameof(Create));
            }
            var TimeSetting = _context.timeSetting.FirstOrDefault();
            if (Convert.ToDateTime(info.CheckIn) >= Convert.ToDateTime(TimeSetting.InTime) && info.CheckInReason==null)
            {
                return RedirectToAction("Edit", new { id = info.Id });

            }
            
            if (id == null)
            {
                return NotFound();
            }

            var attendence = await _context.attendences.FindAsync(id);
            if (attendence == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.applicationUsers, "Id", "Id", attendence.ApplicationUserId);
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId", attendence.BranceSettingBranceId);
            return View(attendence);
        }

        // POST: Attendences/CheckOut/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(int id, [Bind("Id,CheckIn,CheckInReason,Date,CheckOut,CheckOutReason,Present,PrivateIp,PublicIp,HostName,City,Latitude,Longitude,ApplicationUserId, BranceSettingBranceId")] Attendence attendence)
        {
            if (id != attendence.Id)
            {
                return NotFound();
            }
            // For Mac Address
            //JsonToView model = new JsonToView();
            //GeoHelper geoHelper = new GeoHelper();
            //var result = await geoHelper.GetGeoInfo();
            //model = JsonConvert.DeserializeObject<JsonToView>(result);

            var BranceInfo = _context.branceSettings.FirstOrDefault(c => c.IpAddress == attendence.PublicIp);
            if (BranceInfo == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    attendence.CheckOut = DateTime.Now.ToString("h:mm:ss tt");
                    _context.Update(attendence);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendenceExists(attendence.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var TimeSetting = _context.timeSetting.FirstOrDefault();
                if (Convert.ToDateTime(attendence.CheckOut) >= Convert.ToDateTime(TimeSetting.OutTime))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("OutReason", new { id = attendence.Id });
                }
                
            }
            ViewData["UserId"] = new SelectList(_context.applicationUsers, "Id", "Id", attendence.ApplicationUserId);
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId", attendence.BranceSettingBranceId);
            return View(attendence);
        }

        // GET: Attendences/OutReason/5
        public async Task<IActionResult> OutReason(int? id)
        {
            var userName = HttpContext.Session.GetString("userName");
            if (userName == null)
            {
                return Redirect("~/Identity/Account/LogIn");

            }
            var CurrentUserInfo = _context.applicationUsers.FirstOrDefault(a => a.UserName == userName);
            var CurrentUserId = CurrentUserInfo.Id;
            if (CurrentUserId==null)
            {
                return RedirectToAction();
            }
            string dat = DateTime.Now.ToString("MM/dd/yyyy");
            var info = _context.attendences.FirstOrDefault(c => c.ApplicationUserId == CurrentUserId && c.Date == dat);
            if (info == null)
            {
                return RedirectToAction(nameof(Create));
            }
            if (info.CheckOutReason !=null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (id == null)
            {
                return NotFound();
            }

            var attendence = await _context.attendences.FindAsync(id);
            if (attendence == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.applicationUsers, "Id", "Id", attendence.ApplicationUserId);
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId", attendence.BranceSettingBranceId);
            return View(attendence);
        }

        // POST: Attendences/OutReason/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OutReason(int id, [Bind("Id,CheckIn,CheckInReason,Date,CheckOut,CheckOutReason,Present,PrivateIp,PublicIp,HostName,City,Latitude,Longitude,ApplicationUserId, BranceSettingBranceId")] Attendence attendence)
        {
            if (id != attendence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendenceExists(attendence.Id))
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
            ViewData["UserId"] = new SelectList(_context.applicationUsers, "Id", "Id", attendence.ApplicationUserId);
            ViewData["BranceId"] = new SelectList(_context.branceSettings, "BranceId", "BranceId", attendence.BranceSettingBranceId);
            return View(attendence);
        }

        // GET: Attendences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendence = await _context.attendences
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendence == null)
            {
                return NotFound();
            }

            return View(attendence);
        }

        // POST: Attendences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendence = await _context.attendences.FindAsync(id);
            _context.attendences.Remove(attendence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendenceExists(int id)
        {
            return _context.attendences.Any(e => e.Id == id);
        }
    }
}
