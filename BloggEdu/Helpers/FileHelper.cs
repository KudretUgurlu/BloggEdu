using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BloggEdu.Helpers
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> ResmiKaydet(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Resimlerin kaydedileceği klasörü belirledim
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "writer", "assets", "images", "faces");

            // Eğer klasör yoksa oluşturur
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Resmi benzersiz bir dosya adıyla kaydedin
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Resmin yolu iletebilirsiniz
            return "/writer/assets/images/faces/" + uniqueFileName;
        }
    }
}
