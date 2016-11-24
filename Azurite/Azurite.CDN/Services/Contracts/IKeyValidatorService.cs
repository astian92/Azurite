using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.CDN.Services.Contracts
{
    public interface IKeyValidatorService
    {
        void Validate(Guid key); //throws if key invalid
    }
}