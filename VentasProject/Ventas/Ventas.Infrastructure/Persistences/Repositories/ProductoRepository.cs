using Microsoft.EntityFrameworkCore;
using Ventas.Domain.Entities;
using Ventas.Infrastructure.Commons.Bases.Request;
using Ventas.Infrastructure.Commons.Bases.Response;
using Ventas.Infrastructure.Persistences.Contexts;
using Ventas.Infrastructure.Persistences.Interfaces;

namespace Ventas.Infrastructure.Persistences.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly PosContext _context;

        public ProductoRepository(PosContext context)
        {
            _context = context;
        }

        public async Task<bool> EditProduct(Producto product)
        {
            _context.Update(product);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<BaseEntityResponse<Producto>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Producto>();
            var products = (from p in _context.Productos select p).AsNoTracking().AsQueryable();                            

            if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        products = products.Where(x => x.Nombre!.Contains(filters.TextFilter));
                        break;                   
                }
            }else if(filters.Sort is null)
            {
                filters.Sort = "Id";
            }

            response.TotalRecords = await products.CountAsync();
            response.Items = await Ordering(filters, products, !(bool)filters.Download!).ToListAsync();
            return response;
        }

        public async Task<IEnumerable<Producto>> ListSelectProducts()
        {
            var products = await _context.Productos.AsNoTracking().ToListAsync();

            return products;

        }

        public async Task<Producto> ProductById(int productId)
        {
            var product = await _context.Productos!.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(productId));
            return product!;
        }

        public async Task<bool> RegisterProduct(Producto product)
        {
            await _context.AddAsync(product);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> RemoveProducto(int productId)
        {
            var product = _context.Productos.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(productId));

            _context.Update(product);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;


        }
    }
}
