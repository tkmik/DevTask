using FluentResults;

namespace DevTask.Core.Models.Errors
{
    public class NotFoundError : IError
    {
        private const string errorMessage = "NotFound: the requested object not found by provided params.";

        public List<IError> Reasons => [];

        public string Message => errorMessage;

        public Dictionary<string, object> Metadata => new()
        {
            ["statusCode"] = 404,
            ["errorMessage"] = errorMessage
        };
    }
}
