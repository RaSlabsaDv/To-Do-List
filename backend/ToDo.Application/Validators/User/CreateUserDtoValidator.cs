using FluentValidation;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(150);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(250);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
    }
}