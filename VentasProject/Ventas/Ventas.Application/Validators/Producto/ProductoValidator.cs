using FluentValidation;
using Ventas.Application.Dtos.Request;

namespace Ventas.Application.Validators.Producto
{
    public class ProductoValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Nombre).NotNull().WithMessage("El campo nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo nombre no puede estar vacio");

            RuleFor(x => x.Descripcion).NotNull().WithMessage("El campo descripción no puede ser nulo")
                .NotEmpty().WithMessage("El campo descripción no puede estar vacio");

            RuleFor(x => x.Cantidad).NotNull().WithMessage("El campo cantidad no puede ser nulo")
                .NotEmpty().WithMessage("El campo cantidad no puede estar vacio");

            RuleFor(x => x.Precio).NotNull().WithMessage("El campo precio no puede ser nulo")
                .NotEmpty().WithMessage("El campo precio no puede estar vacio");

        }
    }
}
