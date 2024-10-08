﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MYBlog.Repositories
{
    public class ImageImplementation : IImageInterface
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;
        public ImageImplementation(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._account = new Account(configuration.GetSection("Cloudinary")["CloudName"]
                , configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
       public async Task<string> UploadAsync(IFormFile file)
        {
            var client= new Cloudinary(_account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
        var uploadResult = await client.UploadAsync(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
    }
    }
}
