using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Infrastructure.ResponseHandling
{
    public enum ErrorCodes
    {
        LoginFailed = 1,
        RegisterFailed,
        UsernameAlreadyExists,
        EmailIsAlreadyInUse,
        IncorrectUsernameOrPassword,
        NotApprovedAccount,
        LockedOutAccount,
    }
}
