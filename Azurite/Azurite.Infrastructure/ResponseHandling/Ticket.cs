namespace Azurite.Infrastructure.ResponseHandling
{
    public class Ticket : ITicket
    {
        /// <summary>
        /// Shows whether or not the operation was a success.
        /// </summary>
        private bool isOK;

        public bool IsOK
        {
            get
            {
                return this.isOK;
            }
        }

        /// <summary>
        /// When the error is known it should have its error code with it.
        /// </summary>
        private ErrorCodes code;

        public ErrorCodes Code
        {
            get
            {
                return this.code;
            }
        }

        /// <summary>
        /// An inner ticket to save if there was another reason for this tickets results.
        /// </summary>
        private ITicket inner;

        public ITicket Inner
        {
            get
            {
                return this.inner;
            }
        }

        /// <summary>
        /// A custom message from the ticket issuer.
        /// </summary>
        private string message;

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        /// <summary>
        /// Creates a ticket with a status that says whether or not everything went ok - Ticket.IsOk = true/false.
        /// </summary>
        public Ticket(bool status)
        {
            this.isOK = status;
        }

        /// <summary>
        /// Creates a ticket with a status that says whether or not everything went ok - Ticket.IsOk = true/false and adds a message to the ticket.
        /// </summary>
        public Ticket(bool status, string message)
        {
            this.message = message;
            this.isOK = status;
        }

        /// <summary>
        /// Creates a ticket with a IsOk = false and sets the ErrorCode and the message.
        /// </summary>
        public Ticket(ErrorCodes code, string message)
            : this(false, message)
        {
            this.code = code;
        }

        /// <summary>
        /// Creates a ticket with IsOk = false, sets the error code and the message and inserts an inner ticket (like an inner exception).
        /// </summary>
        public Ticket(ErrorCodes code, string message, ITicket inner)
            : this(code, message)
        {
            this.inner = inner;
        }
    }
}
