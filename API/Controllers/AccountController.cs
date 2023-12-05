using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService) 
    {
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("register")] // POST: api/account/register
    public async Task<ActionResult<UserDto >> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("User already exists");
        using var hmac = new HMACSHA512(); //e37 - for salting and hashing the password

        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(), //lower da nam je u db isto radi lakseg uporedjivanja
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
        // return user; //zbog linije 19 <AppUser> e37 7min
        return new UserDto //se4 45
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };

    }

    [HttpPost("login")] //! ne mozes ovo da zab kad pravis rest api
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null) return Unauthorized("invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        //?comparing the entered password with the one in db that's salted?
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password"); ;
        }

        // return user;
        return new UserDto //se4 45
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };

    }

    // da vidimo jel vec postoji taj usernanme 
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());

    }
}
