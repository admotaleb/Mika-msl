using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Msl.Models;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using System.Data;
using System.IO;
using Msl.Data;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;
using QuickMailer;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Msl.Controllers
{
    [Authorize]
    public class ForceSaleController : Controller
    {
        private readonly ApplicationDbContext _db;
         



        public ForceSaleController(ApplicationDbContext db)
        {

            _db = db;




        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Index(IFormCollection form, IFormFile formFile)
        {

            List<ForceSale> forceSales = new List<ForceSale>();
            var mainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFile");
            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
            }
            if(formFile==null)
            {
                ViewBag.Massage = "Please Select File";
                return View();
            }
            var filePath = Path.Combine(mainPath, formFile.FileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            var fileName = formFile.FileName;
            string extention = Path.GetExtension(fileName);

            //var fileName = "./wwwroot/UploadFile/abcd.xlsx";
            // For .net core, the next line requires NuGet package, 
            // System.Text.Encoding.CodePages
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each ROW
                    {
                        if (reader.GetValue(0) != null)
                        {
                            forceSales.Add(new ForceSale
                            {
                                AC_Code = reader.GetValue(0).ToString(),
                                AccountName = reader.GetValue(1).ToString(),
                                Market_Value = reader.GetValue(2).ToString(),
                                Net_Worth = reader.GetValue(3).ToString(),
                                Balance = reader.GetValue(4).ToString(),
                                Ratio = reader.GetValue(5).ToString(),
                                 TWS= reader.GetValue(6).ToString(),
                                Trader = reader.GetValue(7).ToString(),

                            });
                        }
                        else
                        {

                        }

                    }

                    foreach (var row in forceSales)
                    {
                        _db.forceSale.Add(row);
                        _db.SaveChanges();
                    }
                }
            }

            //Send Mail Code Start
            try
            {
                List<string> TomailAddress = new List<string>();
                

                Email email = new Email();
                var userData = _db.applicationUsers.Select(c => c.Email).ToList(); 
                TomailAddress = GetValidMail(userData);
                foreach (var TomMail in TomailAddress)
                {
                    
                    var AccCode = (from uAcc in _db.applicationUsers.Where(c=>c.Email==TomMail)
                                   from FrAcc in _db.forceSale.Where(c=>c.TWS==uAcc.Tws)
                                   select new
                                   {
                                       AC_Code = FrAcc.AC_Code,
                                   }).ToList();
                    string mailaddresTo = TomMail.ToString();
                    string Acc = string.Join(",", AccCode);
                    if (String.IsNullOrEmpty(Acc))
                    {
                        
                    }
                    else
                    {
                        email.SendEmail(mailaddresTo, Credential.Email, Credential.Password, MailMassage.Subject, Acc);
                    }
                }
               
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            //send mail code end
        }

        public IActionResult GetExcelData()
        {

            var roleName = HttpContext.Session.GetString("roleName");
            if (roleName == null)
            {
                return Redirect("~/Identity/Account/LogIn");

            }
            if (roleName != null)
            {
                ViewBag.roleName = roleName;
                var CurrentUserName = HttpContext.User.Identity.Name;
                var CurrentUserInfo =_db.applicationUsers.FirstOrDefault(c => c.UserName == CurrentUserName);

                
                if (roleName =="Admin")
                {
                    var AdminResult = _db.forceSale.ToList();
                    if (AdminResult==null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(AdminResult);
                }
                var result = _db.forceSale.Where(c => c.TWS == CurrentUserInfo.Tws);
                if (result.Count() < 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));


        }

        public IActionResult DeleteExcelData()
        {

            _db.forceSale.RemoveRange(_db.forceSale);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public List<string> GetValidMail(List<string> mails)
        {
            List<string> ValidMail = new List<string>();
            Email email = new Email();
            if (mails == null)
            {
                return ValidMail;
            }
            if (mails.Any())
            {

                foreach (var mail in mails)
                {
                    bool isvalid = email.IsValidEmail(mail);
                    if (isvalid)
                    {
                        ValidMail.Add(mail);
                    }

                }
            }
            return ValidMail;
        }

        [HttpGet]
        public async Task<IActionResult> Status(int id)
        {
            var user = _db.forceSale.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Status(ForceSale user)
        {
            if (ModelState.IsValid)
            {
                var AccStatus = _db.forceSale.FirstOrDefault(c => c.Id == user.Id);
                if (AccStatus == null)
                {
                    return NotFound();
                }
                AccStatus.Status = user.Status;


                _db.forceSale.Update(AccStatus);
                var result =  _db.SaveChanges();
                if (result!=null)
                {
                    //ViewBag.SaveSucceeded = "Data Save succeeded";
                    return RedirectToAction(nameof(GetExcelData));
                }
                
            }

            return View();
        }

    }
}












