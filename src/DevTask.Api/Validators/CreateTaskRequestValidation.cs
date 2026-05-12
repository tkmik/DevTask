using DevTask.Api.Contracts.Task;
using DevTask.Core.Models.Entity;
using FluentValidation;

namespace DevTask.Api.Validators
{
    public sealed class CreateTaskRequestValidation : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200)
                .WithMessage("Title must be less than 100 characters");
            RuleFor(x => x.IsCompleted)
                .NotNull();
            RuleFor(x => x.Priority)
                .NotNull()
                .When(x => Enum.IsDefined(typeof(PriorityType), x.Priority));
        }
    }
}
