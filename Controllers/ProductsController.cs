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
    public async Task<ActionResult<List<Product>>> GetProductsAsync(StoreDbContext context)
    {
        var products = await context.Products
                                    .AsNoTracking()
                                    .ToListAsync();

        return Ok(products);
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProductsWithCategoryAsync(StoreDbContext context)
    {
        var products = await context.Products
                                    .Include(x => x.Category)
                                    .AsNoTracking()
                                    .ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id, StoreDbContext context)
    {
        var product = await context.Products
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(prop => prop.Id == id);

        return Ok(product);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductWithCategoryAsync(int id, StoreDbContext context)
    {
        var product = await context.Products
                                   .Include(categ => categ.Category)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(prop => prop.Id == id);

        return Ok(product);
    }

    [HttpGet("categories/{id:int}")]
    public async Task<ActionResult<List<Product>>> GetProducstByCategory(int id, StoreDbContext context)
    {
        var products = await context.Products
                                   .Include(x => x.Category)
                                   .AsNoTracking()
                                   .Where(prop => prop.CategoryId == id)
                                   .ToListAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync(StoreDbContext context, Product model)
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
    public async Task<ActionResult> UpdateProductAsync(
        int id,
        StoreDbContext context,
        Product model)
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
    public async Task<ActionResult> DeleteProductAsync(int id, StoreDbContext context)
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
