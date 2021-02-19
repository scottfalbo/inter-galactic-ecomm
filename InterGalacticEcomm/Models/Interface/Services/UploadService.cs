using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterGalacticEcomm.Models.Interface.Services
{
    public interface IUploadService
    {
        Task<Product> Upload(IFormFile file);
    }

    public class UploadService : IUploadService
    {
        //dep injection
        public IConfiguration Configuration { get; }
        public UploadService(IConfiguration config)
        {
            Configuration = config;
        }
        //end dep injec
        public async Task<Product> Upload(IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(Configuration.GetConnectionString("StorageAccount"), "images");

            await container.CreateIfNotExistsAsync();

            BlobClient blob = container.GetBlobClient(file.FileName);

            using var stream = file.OpenReadStream();

            BlobUploadOptions options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
            };

            if (!blob.Exists())
            {
                await blob.UploadAsync(stream, options);
            }

            Product product = new Product()
            {
                Name = file.FileName,
                URL = blob.Uri.ToString()
            };
            return product;
        }
    }
}
