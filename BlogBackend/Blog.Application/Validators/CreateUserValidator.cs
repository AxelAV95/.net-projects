using FluentValidation;

public class CreateUserValidator : AbstractValidator<UserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().Length(3, 50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Role).Must(role => role == "Admin" || role == "User");
    }
}
