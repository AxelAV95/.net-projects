using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Commons.Bases;
using Ventas.Application.Dtos.Request;
using Ventas.Application.Dtos.Response;
using Ventas.Application.Interfaces;
using Ventas.Application.Validators.Producto;
using Ventas.Domain.Entities;
using Ventas.Infrastructure.Commons.Bases.Request;
using Ventas.Infrastructure.Commons.Bases.Response;
using Ventas.Infrastructure.Persistences.Interfaces;
using Ventas.Utilities.Static;

namespace Ventas.Application.Services
{
   
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductoValidator _validationRules;

        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper, ProductoValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProduct(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductResponseDto>>();
            var products = await _unitOfWork.Producto.ListProducts(filters);

            if(products is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<ProductResponseDto>>(products);
                response.Message = ReplayMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

      
        public async Task<BaseResponse<bool>> EditProduct(int id, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var productEdit = await ProductById(id);

            if(productEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_QUERY_EMPTY;
            }

            var product = _mapper.Map<Producto>(requestDto);
            product.Id = id;
            response.Data = await _unitOfWork.Producto.EditProduct(product);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplayMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_FAILED;

            }

            return response;
        }

        

        public Task<BaseResponse<IEnumerable<ProductSelectResponseDto>>> ListSelectProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<ProductResponseDto>> ProductById(int id)
        {
            var response = new BaseResponse<ProductResponseDto>();
            var product = await _unitOfWork.Producto.ProductById(id);

            if (product is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProductResponseDto>(product);
                response.Message = ReplayMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }

            var product = _mapper.Map<Producto>(requestDto);
            response.Data = await _unitOfWork.Producto.RegisterProduct(product);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplayMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_FAILED;

            }

            return response;
            

        }

        public async Task<BaseResponse<bool>> RemoveProduct(int id)
        {
            var response = new BaseResponse<bool>();
            var product = await ProductById(id);

            if (product is null)
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_QUERY_EMPTY;
            }

           
            response.Data = await _unitOfWork.Producto.RemoveProducto(id);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplayMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplayMessage.MESSAGE_FAILED;

            }

            return response;
        }
    }
}
