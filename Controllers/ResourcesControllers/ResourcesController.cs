using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Pulse.Controllers.ChatControllers;
using Pulse.EntityFramework.Models.Resource;
using Web.ViewModels.ResourceViewModels;

namespace Web.Controllers.ResourcesControllers
{
    public class ResourcesController : Controller
    {
        private readonly StorageCredentials _storageCredentials;
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly CloudBlobContainer _container;

        public ResourcesController()
        {
            _storageCredentials = new StorageCredentials("pulsestorage", "wU7fD/rsasGTCx9Z0t6LxSxn2w2Oc7aiBTUzXOxSV+RYrSeKfVNJqrMdeczXz9WyRW496ipEJYBEQI0oVB1jZg==");
            _cloudStorageAccount = new CloudStorageAccount(_storageCredentials, true);
            _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            _container = _cloudBlobClient.GetContainerReference("resources");
        }
        [Authorize]
        public IActionResult Index()
        {
            var model = new ResourceViewModel
            {
                ResourceList = GetResources()
            };
            return View("Views/Resources/Index.cshtml", model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Upload()
        {
            return View("Views/Resources/Upload.cshtml");
        }

        [HttpPost]
        [Route("Files/UploadFile")]
        public async Task<ActionResult> UploadFile()
        {
            long size = 0;
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = $@"Resources\{filename}";
                size += file.Length;
                var newBlob = _container.GetBlockBlobReference(filename);
                var stream = file.OpenReadStream();
                await newBlob.UploadFromStreamAsync(stream);
            }
            string message = $"{files.Count} file(s) / { size} bytes uploaded successfully!";
            return Json(message);
        }

        [HttpGet]
        [Route("Files/GetFiles")]
        public List<Resource> GetResources()
        {
            var resources = _container.ListBlobs(prefix: "", useFlatBlobListing: true).OfType<CloudBlockBlob>().ToList();
            var list = new List<Resource>();

            foreach (var resource in resources)
            {
                Console.WriteLine(resource);
                list.Add(
                    new Resource
                    {
                        Filename = resource.Name.Substring(10),
                        DateAdded = DateTime.Parse(resource.Properties.LastModified.ToString()),
                        Location = resource.StorageUri.PrimaryUri.ToString()
                    }
                );
            }

            return list;
        }


        [HttpPost]
        [Route("Resources/Delete")]
        public IActionResult Delete(string filename)
        {

            var blob = _container.GetBlockBlobReference("Resources/"+filename);
            blob.Delete();
            return RedirectToAction("Index", "Resources");
        }
    }
}
