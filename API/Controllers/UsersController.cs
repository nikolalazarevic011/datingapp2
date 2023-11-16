using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Authorize] //e46 da ne mozes bez tokena da pristupis rutaama
public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    // private readonly DataContext _context; // da bi mogao u ostalim metodama 

    public UsersController(DataContext context) //ctor short, ctrl + . za line 11
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }


    [HttpGet("{id}")] // /api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user;
    }
}
