using DevTask.Core.Models.Errors;
using FluentResults;

namespace DevTask.Api.Extensions
{
    public static class FluentResultExtensions
    {
        public static IResult ToMinimalApiResult<TResult>(this Result<TResult> result)
        {
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }

            return FailedResult(result.Errors);
        }

        public static IResult ToMinimalApiResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return TypedResults.Ok();
            }

            return FailedResult(result.Errors);
        }

        private static IResult FailedResult(IReadOnlyList<IError> errors)
        {
            if (errors.Any(x => x is NotFoundError))
            {
                return TypedResults.NotFound(errors.Select(x => x.Message));
            }

            if (errors.Any(x => x is ConflictError))
            {
                return TypedResults.Conflict(errors.Select(x => x.Message));
            }

            return TypedResults.BadRequest(errors.Select(x => x.Message));
        }
    }
}
