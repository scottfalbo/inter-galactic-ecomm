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
        Task<Product> Upload(IFormFile file, int Id);
    }

    public class UploadService : IUploadService
    {
        //dep injection
        public IConfiguration Configuration { get; }
        public IAdmin _admin;
        public UploadService(IConfiguration config, IAdmin admin)
        {
            Configuration = config;
            _admin = admin;
        }
        //end dep injec
        public async Task<Product> Upload(IFormFile file, int Id)
        {
            BlobContainerClient container = new BlobContainerClient(Configuration.GetConnectionString("ImageStuff"), "images");

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

            Product product = await _admin.GetProduct(Id);
            product.URL = blob.Uri.ToString();
            await _admin.UpdateProduct(Id, product);


            return product;
        }
    }
}
