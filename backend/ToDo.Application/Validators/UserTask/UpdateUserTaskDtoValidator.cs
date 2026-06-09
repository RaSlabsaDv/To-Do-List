using FluentValidation;

public class UpdateUserTaskDtoValidator : AbstractValidator<UpdateUserTaskDto>
{
    public UpdateUserTaskDtoValidator()
    {
        RuleFor(x => x.Label).MinimumLength(2).MaximumLength(200).When(x => x.Label != null);
        RuleFor(x => x.Description).MaximumLength(2000).When(x => x.Description != null);
        RuleFor(x => x.Deadline).GreaterThan(DateTimeOffset.UtcNow).When(x => x.Deadline != null);
        RuleFor(x => x.Reminder).GreaterThan(DateTimeOffset.UtcNow).When(x => x.Reminder != null);
    }
}