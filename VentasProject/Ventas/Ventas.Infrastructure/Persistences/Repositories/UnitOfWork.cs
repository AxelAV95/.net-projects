using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Infrastructure.Persistences.Contexts;
using Ventas.Infrastructure.Persistences.Interfaces;

namespace Ventas.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PosContext _context;
        public IProductoRepository Producto { get; private set; }

        public UnitOfWork(PosContext context)
        {
            _context = context;
            Producto = new ProductoRepository(_context);
        }
     

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
