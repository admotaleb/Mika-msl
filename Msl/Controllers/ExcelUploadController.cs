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

namespace Msl.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ExcelUploadController : Controller
    {
        ApplicationDbContext _db;


        
        public ExcelUploadController(ApplicationDbContext db)
        {

            _db = db;

           


        }

      
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormCollection form, IFormFile formFile)
        {
            
            List<ExcelUp> excelUp = new List<ExcelUp>();
            var mainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFile");
            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
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
                        if (reader.GetValue(0)!=null)
                        {
                            excelUp.Add(new ExcelUp
                            {
                                Name = reader.GetValue(0).ToString(),
                                Email = reader.GetValue(1).ToString(),

                            });
                        }
                        else
                        {
                            
                        }
                       
                    }

                    foreach (var row in excelUp)
                    {
                        _db.excelUps.Add(row);
                        _db.SaveChanges();
                    }
                }
            }
            return View();
        }
      
        public IActionResult GetExcelData( )
        {

           var r= HttpContext.Session.GetString("roleName");
            if (r!=null) {
                var result = _db.excelUps.Where(c => c.Name == r);
                 if(result.Count()<0) {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            return RedirectToAction(nameof(Index));


        }

        public IActionResult DeleteExcelData()
        {

            _db.excelUps.RemoveRange(_db.excelUps);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}












