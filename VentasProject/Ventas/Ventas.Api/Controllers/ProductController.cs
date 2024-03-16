using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventas.Application.Dtos.Request;
using Ventas.Application.Interfaces;
using Ventas.Infrastructure.Commons.Bases.Request;

namespace Ventas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
            
        }

        [HttpPost]
        public async Task<IActionResult> ListProducts([FromBody] BaseFiltersRequest filters)
        {
            var response = await _productApplication.ListProduct(filters);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        
        public async Task<IActionResult> ProductById(int id)
        {
            var response = await _productApplication.ProductById(id);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProduct([FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.RegisterProduct(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.EditProduct(id,requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var response = await _productApplication.RemoveProduct(id);
            return Ok(response);
        }
    }
}
