using Azurite.CDN.Data;
using Azurite.CDN.Workers.Contracts;
using Azurite.Infrastructure.Data.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Azurite.CDN.Workers.Implementations
{
    public class DeleteWorker : IRemoveWorker
    {
        private readonly IRepository<ProductImages> rep;
        private readonly string ContentPath = HostingEnvironment.MapPath(@"~/Content");

        public DeleteWorker(IRepository<ProductImages> repository)
        {
            this.rep = repository;
        }

        public void DeleteFile(Guid id)
        {
            var fileRecord = rep.Get(id);
            File.Delete(ContentPath + @"\" + fileRecord.ImagePath);

            rep.Remove(fileRecord.Id);
            rep.Save();
        }

        public void DeleteFiles(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var fileRecord = rep.Get(id);
                File.Delete(ContentPath + @"\" + fileRecord.ImagePath);

                rep.Remove(fileRecord.Id);
            }

            rep.Save();
        }
    }
}

