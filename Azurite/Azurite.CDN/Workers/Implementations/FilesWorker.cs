using Azurite.CDN.Data;
using Azurite.CDN.Workers.Contracts;
using Azurite.Infrastructure.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Web.Hosting;
using Azurite.CDN.Models;

namespace Azurite.CDN.Workers.Implementations
{
    public class FilesWorker : IFilesWorker
    {
        private readonly IRepository<ProductImages> rep;
        private readonly string ContentPath = HostingEnvironment.MapPath(@"~/Content");

        public FilesWorker(IRepository<ProductImages> rep)
        {
            this.rep = rep;
        }

        public async Task<RequestFile> GetFile(string filepath)
        {
            return await ReadFile(filepath);
        }

        public async Task<RequestFile> GetFileFromId(Guid fileId)
        {
            var productFile = rep.Get(fileId);
            return await ReadFile(productFile.ImagePath);
        }

        private async Task<RequestFile> ReadFile(string filename)
        {
            var file = new RequestFile();

            var fInfo = new FileInfo(this.ContentPath + @"\" + filename);
            file.ContentType = GetContentFromExtension(fInfo.Extension);

            using (var stream = new FileStream(fInfo.FullName, FileMode.Open, FileAccess.Read)) //file access is important to NOT LOCK the file !
            {
                file.Bites = new byte[stream.Length];
                await stream.ReadAsync(file.Bites, 0, (int)stream.Length);

                return file;
            }
        }

        private string GetContentFromExtension(string extension)
        {
            string result = null;

            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    result = "image/jpg";
                    break;
                case ".png":
                    result = "image/png";
                    break;
                default:
                    result = "application/octet-stream";
                    break;
            }

            return result;
        }
    }
}