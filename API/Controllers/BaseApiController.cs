using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[controller]")] // /api/users npr, uzima prvo ime od Controller file-a, UsersController npr
public class BaseApiController : ControllerBase
{

}
