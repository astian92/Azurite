namespace Azurite.Infrastructure.ResponseHandling
{
    public interface ITicket
    {
        bool IsOK { get; }

        ErrorCodes Code { get; }

        string Message { get; }

        ITicket Inner { get; }
    }
}
