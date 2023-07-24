using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private int? _userId;
    private string? _userEmail;
    
    public int UserId
    {
        get
        {
            _userId ??= User.GetUserId();
            return _userId.Value;
        }
    }

    public string UserEmail
    {
        get
        {
            _userEmail ??= User.GetUserEmail();
            return _userEmail;
        }
    }
}