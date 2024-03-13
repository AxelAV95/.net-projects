using Ventas.Domain.Entities;
using Ventas.Infrastructure.Commons.Bases.Request;
using Ventas.Infrastructure.Commons.Bases.Response;

namespace Ventas.Infrastructure.Persistences.Interfaces
{
    public interface IProductoRepository
    {
        Task<BaseEntityResponse<Producto>> ListProducts(BaseFiltersRequest filters);
        Task<IEnumerable<Producto>> ListSelectProducts();

        Task<Producto> ProductById(int productId);

        Task<bool> RegisterProduct(Producto product);
        Task<bool> EditProduct(Producto product);
        Task<bool> RemoveProducto(int productId);
    }
}
