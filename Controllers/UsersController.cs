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
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<User>> Post([FromServices] StoreDbContext context, [FromBody] User model)
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
    public async Task<ActionResult<dynamic>> Authenticate([FromServices] StoreDbContext context, [FromBody] User model)
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
}
