using CVFormatter.Web.Extensions;
using CVFormatter.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

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
            {
                return Content("File not selected");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files\\Imported", file.GetFileName());

            var outputFileName = Path.GetFileNameWithoutExtension(path) + "_NEW.pdf";

            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\Exported", outputFileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "nova-logo.jpg");
            var blockPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "white-border.jpg");

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
                model.Files.Add(new FileDetail { Name = item.Name, Path = item.PhysicalPath });
            }

            return View(model);
        }

        public async Task<IActionResult> Download(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                return Content("filename not present");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files\\Exported", fileName);

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}