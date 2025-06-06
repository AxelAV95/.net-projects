using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> products = new();

    [HttpGet]
    public IActionResult Get() => Ok(products);

    [HttpPost]
    public IActionResult Create(Product product)
    {
        product.Id = products.Count + 1;
        products.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "No file uploaded" });
        }

        var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "Images");
        if (!Directory.Exists(uploadsDir))
            Directory.CreateDirectory(uploadsDir);

        var filePath = Path.Combine(uploadsDir, file.FileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var imageUrl = $"http://localhost:5013/images/{file.FileName}";
        return Ok(new { url = imageUrl });
    }

}
