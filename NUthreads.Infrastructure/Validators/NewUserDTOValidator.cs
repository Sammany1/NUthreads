using FluentValidation;
using NUthreads.Domain.DTOs;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Validators;

public class NewUserDTOValidator : AbstractValidator<NewUserDTO>,INewUserDTOValidator
{
    public NewUserDTOValidator()
    {
        try
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required")
                .Must(email => email.Contains('@')).WithMessage("Email is not valid")
                .Must(email => (email.Split('@')[1] == "nu.edu.eg")).WithMessage("Email is not NU")
                .Must(email =>
                {
                    var firstPart = email.Split('@')[0];
                    return firstPart.Length > 4 && firstPart.Substring(firstPart.Length - 4).All(char.IsDigit);
                }).WithMessage("This is not a student email");


            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required");


            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        } catch (Exception ex) {
            throw new ValidationException("An Error Occured While Vaildating" + ex.Message);
                }
        //Password ----- 
    }
}
