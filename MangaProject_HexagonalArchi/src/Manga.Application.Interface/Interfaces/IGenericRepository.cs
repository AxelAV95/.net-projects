using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProject.Application.Interface.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync(string storedProcedure);
        Task<T> GetByIdAsync(string storedProcedure, object parameter);
        Task<bool> ExecAsync(string storedProcedure, object parameter);
    }
}
