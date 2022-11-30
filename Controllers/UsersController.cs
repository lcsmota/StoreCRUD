using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Context;
using Store.Models;
using Store.Services;

namespace Store.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<List<User>>> GetUsersAsync([FromServices] StoreDbContext context)
    {
        var users = await context.Users
                                 .AsNoTracking()
                                 .ToListAsync();

        return users;
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<User>> GetUsersAsync(int id, [FromServices] StoreDbContext context)
    {
        var user = await context.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(prop => prop.Id == id);

        return user;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<User>> CreateUserAsync([FromServices] StoreDbContext context, [FromBody] User model)
    {
        try
        {
            model.Role = "employee";

            context.Users.Add(model);
            await context.SaveChangesAsync();

            model.Password = string.Empty;
            return model;
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar cadastrar usuário.");
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> AuthenticateAsync([FromServices] StoreDbContext context, [FromBody] User model)
    {
        var user = await context.Users
                                .AsNoTracking()
                                .Where(prop => prop.UserName == model.UserName && prop.Password == model.Password)
                                .FirstOrDefaultAsync();

        if (user is null) return NotFound("Usuário ou senha inválidos!");

        var token = TokenService.GenerateToken(user);

        user.Password = string.Empty;

        return new
        {
            user = user,
            token = token
        };
    }

    [HttpPut("id:int")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult> UpdateUserAsync(int id, [FromServices] StoreDbContext context, [FromBody] User model)
    {
        if (model.Id != id) return BadRequest("Usuário não encontrado.");

        try
        {
            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception)
        {
            return BadRequest("Erro ao tentar atualizar usuário.");
        }
    }

}
