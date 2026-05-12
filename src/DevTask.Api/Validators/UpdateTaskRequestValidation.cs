using DevTask.Api.Contracts.Task;
using FluentValidation;

namespace DevTask.Api.Validators
{
    public class UpdateTaskRequestValidation : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out var parsed) && parsed != Guid.Empty);
        }
    }
}
