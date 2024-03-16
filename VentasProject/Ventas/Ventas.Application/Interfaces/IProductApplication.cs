using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Commons.Bases;
using Ventas.Application.Dtos.Request;
using Ventas.Application.Dtos.Response;
using Ventas.Infrastructure.Commons.Bases.Request;
using Ventas.Infrastructure.Commons.Bases.Response;

namespace Ventas.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProduct(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<ProductSelectResponseDto>>> ListSelectProduct();

        Task<BaseResponse<ProductResponseDto>> ProductById(int id);
        Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto);

        Task<BaseResponse<bool>> EditProduct(int id, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProduct(int id);


    }
}
