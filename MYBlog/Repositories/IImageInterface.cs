namespace MYBlog.Repositories
{
    public interface IImageInterface
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
