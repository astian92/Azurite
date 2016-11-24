using Azurite.CDN.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Configuration;

namespace Azurite.CDN.Services.Implementations
{
    public class KeyValidatorService : IKeyValidatorService
    {
        public void Validate(Guid key)
        {
            var securityKeyStr = WebConfigurationManager.AppSettings["SecurityKey"];
            Guid securityKey = Guid.Parse(securityKeyStr);

            if (!securityKey.Equals(key))
            {
                throw new AuthenticationException("The provided key is incorrect!");
            }
        }
    }
}