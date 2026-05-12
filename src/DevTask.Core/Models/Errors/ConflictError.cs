using FluentResults;

namespace DevTask.Core.Models.Errors
{
    public class ConflictError : IError
    {
        public List<IError> Reasons => [];

        private const string errorMessage = "Conflict: an error occurred while processing the operation!";

        public string Message => errorMessage;

        public Dictionary<string, object> Metadata => new()
        {
            ["statusCode"] = 409,
            ["errorMessage"] = errorMessage
        };
    }
}
