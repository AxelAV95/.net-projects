using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProject.Application.UseCase.UseCases.Manga.Commands.CreateCommand
{
    public class CreateMangaValidator : AbstractValidator<CreateMangaCommand>
    {
        public CreateMangaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío")
                .MaximumLength(255).WithMessage("El nombre del manga no puede exceder los 255 caracteres");

            RuleFor(x => x.Sinopsis)
                .NotEmpty().WithMessage("El campo sinopsis no puede ser vacío");

            RuleFor(x => x.Autor)
                .NotEmpty().WithMessage("El campo autor no puede ser vacío");

            RuleFor(x => x.Tomo)
                .NotEmpty().WithMessage("El campo tomo no puede ser vacío")
                .Must(BeAPositiveInteger).WithMessage("El campo tomo debe ser un número entero positivo");
        }

        private bool BeAPositiveInteger(int arg)
        {
            return arg > 0;
        }

    }
}
