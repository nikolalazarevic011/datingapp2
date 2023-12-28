using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Authorize] //e46 da ne mozes bez tokena da pristupis rutaama
public class UsersController : BaseApiController
{

    // private readonly DataContext _context; // da bi mogao u ostalim metodama 
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper) //ctor short, ctrl + . za line 11
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpGet]
    //IEnumerable for a list
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();

        return Ok(users);

    }


    [HttpGet("{username}")] // /api/users/username
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        //find dobar kad searchujes po primary key, kao sto je id
        return await _userRepository.GetMemberAsync(username);

    }
}
