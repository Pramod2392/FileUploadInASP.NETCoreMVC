namespace FileUploadInMVC.Models
{
    public class FileModel
    {
        public byte[] fileContent { get; set; }
        public string fileName { get; set; }    
        public IFormFile file { get; set; }

    }
}
