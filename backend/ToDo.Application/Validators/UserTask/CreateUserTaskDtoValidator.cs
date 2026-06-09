using FluentValidation;

public class CreateUserTaskDtoValidator : AbstractValidator<CreateUserTaskDto>
{
    public CreateUserTaskDtoValidator()
    {
        RuleFor(x => x.Label).NotEmpty().MinimumLength(2).MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(2000).When(x => x.Description != null);
        RuleFor(x => x.Deadline).GreaterThan(DateTimeOffset.UtcNow).When(x => x.Deadline != null);
        RuleFor(x => x.Reminder).GreaterThan(DateTimeOffset.UtcNow).When(x => x.Reminder != null);
    }
}