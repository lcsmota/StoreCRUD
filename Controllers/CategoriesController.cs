using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Context;
using Store.Models;

namespace Store.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategoriesAsync([FromServices] StoreDbContext context)
    {
        var categories = await context.Categories.AsNoTracking().ToListAsync();

        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetCategoryAsync(
        int id,
        [FromServices] StoreDbContext context)
    {
        var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (category is null) return NotFound("Categoria não encontrada.");

        return Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> CreateCategoryAsync(
        [FromServices] StoreDbContext context,
        [FromBody] Category model)
    {
        try
        {
            context.Categories.Add(model);
            await context.SaveChangesAsync();

            return Ok(model);
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar cadastrar categoria.");
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult> UpdateCategoryAsync(
        int id,
        [FromBody] Category model,
        [FromServices] StoreDbContext context)
    {
        if (model.Id != id) return BadRequest("Verifique os dados e tente novamente.");

        try
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar atualizar categoria.");
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult> DeleteCategoryAsync(int id, [FromServices] StoreDbContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category is null) return NotFound("Categoria não encontrada.");

        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar remover categoria");
        }
    }
}
