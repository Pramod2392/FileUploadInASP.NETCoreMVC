using FileUploadInMVC.Models;
using Microsoft.AspNetCore.Mvc;
namespace FileUploadInMVC.Controllers {
    public class FileUploadController : Controller {
        private readonly MSSQLDataAccess _mSSQLDataAccess;
        private readonly FileModel _fileModel;
        public FileUploadController(MSSQLDataAccess mSSQLDataAccess, FileModel fileModel) {
            this._mSSQLDataAccess = mSSQLDataAccess;
            this._fileModel = fileModel;
            this._fileModel.fileContent = new byte[100000];
        }
        public IActionResult Index() { return View();}
        public async Task<IActionResult> SingleFileUploadToDatabase(IFormFile SingleFile) {
            using var memoryStream = new MemoryStream();
            await SingleFile.CopyToAsync(memoryStream);
            byte[] fileContent = memoryStream.ToArray();
            await _mSSQLDataAccess.SaveFileToDatabaseAsync(fileContent, SingleFile.FileName);
            _fileModel.fileContent = fileContent;
            _fileModel.fileName = SingleFile.FileName;            
            return View("Index", _fileModel);
        }
        public async Task<IActionResult> ListFiles() {
            var queryResult = await _mSSQLDataAccess.GetAllFilesFromDatabaseAsync();
            return View("Files",queryResult);
        }
    }
}



