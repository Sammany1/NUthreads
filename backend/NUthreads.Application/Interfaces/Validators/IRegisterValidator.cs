using FluentValidation;
using NUthreads.Domain.DTOs;

namespace NUthreads.Application.Interfaces.Validators
{
    public interface IRegisterValidator : IValidator<NewUserDTO> { }

}
