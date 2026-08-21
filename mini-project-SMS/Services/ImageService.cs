using mini_project_SMS.Services.Interfaces;

namespace mini_project_SMS.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        private readonly string[] _allowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png"
        };

        private const long MaxFileSize = 2 * 1024 * 1024;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            var extension = Path.GetExtension(image.FileName)
                .ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException(
                    "Only JPG, JPEG and PNG images are allowed.");
            }

            if (image.Length > MaxFileSize)
            {
                throw new ArgumentException(
                    "Image size cannot exceed 2 MB.");
            }

            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "students");

            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(
                filePath,
                FileMode.Create);

            await image.CopyToAsync(stream);

            return $"/images/students/{fileName}";
        }

        public void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return;
            }

            var fileName = Path.GetFileName(imagePath);

            var filePath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "students",
                fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
