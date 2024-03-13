using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductoRepository Producto { get;  }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
