namespace mini_project_SMS.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile image);

        void DeleteImage(string? imagePath);
    }
}
