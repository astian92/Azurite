using Azurite.Storehouse.Models.ViewModels;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface IAccountWorker
    {
        bool Authenticate(LoginViewModel model);
    }
}