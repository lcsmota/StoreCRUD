using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Context;
using Store.Models;

namespace Store.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProductsAsync([FromServices] StoreDbContext context)
    {
        var products = await context.Products
                                    .AsNoTracking()
                                    .ToListAsync();

        return Ok(products);
    }

    [HttpGet("productsWithCateg")]
    public async Task<ActionResult<List<Product>>> GetProductsWithCategoryAsync([FromServices] StoreDbContext context)
    {
        var products = await context.Products
                                    .Include(x => x.Category)
                                    .AsNoTracking()
                                    .ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id, [FromServices] StoreDbContext context)
    {
        var product = await context.Products
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(prop => prop.Id == id);

        if (product is null) return NotFound("Produto não encontrado.");

        return Ok(product);
    }

    [HttpGet("{id:int}/productsWithCateg")]
    public async Task<ActionResult<Product>> GetProductWithCategoryAsync(int id, [FromServices] StoreDbContext context)
    {
        var product = await context.Products
                                   .Include(categ => categ.Category)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(prop => prop.Id == id);

        if (product is null) return NotFound("Produto não encontrado.");

        return Ok(product);
    }

    [HttpGet("categories/{id:int}")]
    public async Task<ActionResult<List<Product>>> GetProducstByCategory(int id, [FromServices] StoreDbContext context)
    {
        var products = await context.Products
                                   .Include(x => x.Category)
                                   .AsNoTracking()
                                   .Where(prop => prop.CategoryId == id)
                                   .ToListAsync();

        return Ok(products);
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<Product>> CreateProductAsync(
        [FromServices] StoreDbContext context,
        [FromBody] Product model)
    {
        try
        {
            context.Products.Add(model);
            await context.SaveChangesAsync();

            return Ok(model);
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar cadastrar produto.");
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "employee")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult> UpdateProductAsync(
        int id,
        [FromServices] StoreDbContext context,
        [FromBody] Product model)
    {
        if (model.Id != id) return BadRequest("Verifique os dados e tente novamente.");

        try
        {
            context.Entry<Product>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar atualizar produto.");
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult> DeleteProductAsync(int id, [FromServices] StoreDbContext context)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product is null) return NotFound("Produto não encontrado.");

        try
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar remover produto.");
        }
    }
}
