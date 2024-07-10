using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCore.Api.UseCases.VerifyToken;
[ApiController]
[Route("/[controller]")]
public class VerifyTokenController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public IActionResult VerifyToken()
    {
        return new OkObjectResult("Authentic Token");
    }
}
