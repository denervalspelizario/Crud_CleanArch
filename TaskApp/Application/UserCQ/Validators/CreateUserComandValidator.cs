using Application.UserCQ.Commands;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.UserCQ.Validators
{
    // no abstract precisa indicar a classe que será validada no caso será a CreateUserCommand
    public class CreateUserComandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserComandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo email não pode ser vazio")
                .EmailAddress().WithMessage("O campo de email não é valido");

            RuleFor(x => x.Username).NotEmpty().WithMessage("O campo nome não pode ser vazio");

        }
    }
}
