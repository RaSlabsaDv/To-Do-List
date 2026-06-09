using FluentValidation;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Name).MinimumLength(2).MaximumLength(150).When(x => x.Name != null);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(250).When(x => x.Email != null);
        RuleFor(x => x.Password).MinimumLength(6).MaximumLength(50).When(x => x.Password != null);
    }
}