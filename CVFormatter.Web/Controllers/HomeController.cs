using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CVFormatter.Web.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using CVFormatter.Web.Extensions;

namespace CVFormatter.Web.Controllers
{
    public class HomeController : Controller
    {
        public IFileProvider fileProvider;

        public HomeController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "Files\\Imported", 
                        file.GetFileName());

            var outputFileName = Path.GetFileNameWithoutExtension(path) + "_NEW.pdf";

            var outputPath = Path.Combine(
                             Directory.GetCurrentDirectory(), "Files\\Exported",
                             outputFileName);

            // Step1: Upload
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Step2 : 
            var logoPath = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot",
                           "nova-logo.jpg"); 

            var blockPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot",
                            "white-border.jpg");

            //var settings = new FormatterSettings();
            //settings.BorderPath = blockPath;
            //settings.LogoPath = logoPath;

            //ResumeFormatter resumeFormatter = new ResumeFormatter(settings);
            //resumeFormatter.Format(path, outputPath);

            return RedirectToAction("Files");
        }

        public IActionResult Files()
        {
            var model = new FileViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents("Exported"))
            {
                model.Files.Add(new FileDetail { Name = item.Name, Path = item.PhysicalPath});
            }

            return View(model);
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
            {
                return Content("filename not present");
            }

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(), 
                           "Files\\Exported", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/pdf", Path.GetFileName(path));
        }
        public IActionResult Formatter()
        {
            
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
