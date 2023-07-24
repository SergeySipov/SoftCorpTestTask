using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class TestController : ApiControllerBase
{
    [Authorize]
    public IActionResult GetSomeAuthorizedTxt()
    {
        return Ok("Info from private controller");
    }

    [AllowAnonymous]
    public IActionResult GetSomeNonAuthorizedTxt()
    {
        return Ok("Info from PUBLIC controller");
    }
}
