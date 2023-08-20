using FileUploadInMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileUploadInMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public HomeController(IWebHostEnvironment environment)
        {            
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult SingleFileUpload(IFormFile SingleFile)
        {
            using (var fileStream = new FileStream(
                                        Path.Combine(_environment.ContentRootPath,"file.png"), 
                                        FileMode.Create, FileAccess.Write))
            {
                SingleFile.CopyTo(fileStream);                
            }
            return RedirectToAction("Index");
        }

        public IActionResult MultipleFileUpload(IEnumerable<IFormFile> MultipleFiles)
        {
            int fileCount = 1;
            foreach (var file in MultipleFiles)
            {
                using (var fileStream = new FileStream(
                                        Path.Combine($"{_environment.ContentRootPath}//Images", $"file{fileCount}.png"),
                                        FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                } 
                fileCount++;
                              
            }
            return RedirectToAction("Index");
        }


        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}