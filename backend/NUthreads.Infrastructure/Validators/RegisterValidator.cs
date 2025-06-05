using FluentValidation;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Validators;
using NUthreads.Domain.DTOs;

public class RegisterValidator : AbstractValidator<NewUserDTO>, IRegisterValidator
{
    public RegisterValidator(IUserRepository _users)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .Must(email => email.Contains('@')).WithMessage("Email must contain '@'")
            .Must(email =>
            {
                var parts = email.Split('@');
                return parts.Length == 2 && parts[1] == "nu.edu.eg";
            }).WithMessage("Email must be a valid NU student email (ending with @nu.edu.eg)")
            .Must(email =>
            {
                var firstPart = email.Split('@')[0];
                return firstPart.Length > 4 && firstPart.Substring(firstPart.Length - 4).All(char.IsDigit);
            }).WithMessage("Email prefix must end in 4 digits (student ID)")
            .Must(email => !_users.EmailExists(email))
            .WithMessage("Email already exists");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .Must(Username => !_users.UsernameExists(Username)).WithMessage("Username is taken.");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Matches("^[a-zA-Z]+$").WithMessage("First name must contain only letters");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Matches("^[a-zA-Z]+$").WithMessage("Last name must contain only letters");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}
